using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;
using ConfiMicToggler.Config;
using ConfiMicToggler.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfiMicToggler.Controllers
{
    public class MicController : Controller
    {
        private readonly StatisticData _statisticData;
        private readonly ConfiMicTogglerConfig _config;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        /// <summary>
        /// The FindWindowEx API
        /// </summary>
        /// <param name="parentHandle">a handle to the parent window </param>
        /// <param name="childAfter">a handle to the child window to start search after</param>
        /// <param name="className">the class name for the window to search for</param>
        /// <param name="windowTitle">the name of the window to search for</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Initializes a new instance of the <see cref="MicController"/> class.
        /// </summary>
        /// <param name="statisticData">The user store.</param>
        /// <param name="config">The configuration.</param>
        public MicController(StatisticData statisticData, IOptions<ConfiMicTogglerConfig> config)
        {
            _statisticData = statisticData;
            _config = config.Value;
        }


        /// <summary>
        /// Toggles the microfon of the configured conference tool and returns the index page
        /// </summary>
        /// <returns>The index page with statistics who used the mic toggler</returns>
        public IActionResult Index()
        {
            IntPtr targetWindowHandle = IntPtr.Zero;
            Action<IKeyboardSimulator> keysToPress = null;

            if (_config.TargetConferenceTool == "Teams")
            {
                targetWindowHandle = GetWindowHandleForTeams();
                keysToPress = keyBoard => keyBoard.ModifiedKeyStroke(new List<VirtualKeyCode> { VirtualKeyCode.LCONTROL, VirtualKeyCode.LSHIFT }, VirtualKeyCode.VK_M);
            }

            if (_config.TargetConferenceTool == "Skype")
            {
                targetWindowHandle = GetWindowHandleForSkype();
                keysToPress = keyBoard => keyBoard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.F4);
            }

            if (targetWindowHandle != IntPtr.Zero && keysToPress != null)
            {
                BringConferenceWindowToFront(targetWindowHandle);

                InputSimulator inputSimulator = new InputSimulator();
                keysToPress(inputSimulator.Keyboard);

                _statisticData.AddLazyUserCounter(this.User);
            }

            return View(_statisticData);
        }

        private static void BringConferenceWindowToFront(IntPtr conferenceWindow)
        {
            // Verify that Skype is a running process.
            if (conferenceWindow == IntPtr.Zero)
            {
                return;
            }

            // Make Skype the foreground application
            SetForegroundWindow(conferenceWindow);
        }

        private IntPtr GetWindowHandleForSkype()
        {
            return FindWindow("LyncConversationWindowClass", null);
        }

        private IntPtr GetWindowHandleForTeams()
        {
            IntPtr windowHandle = default(IntPtr);
            Process process = Process.GetProcessesByName("Teams")[0];
            if (process != null)
            {
                process.WaitForInputIdle();
                windowHandle = process.MainWindowHandle;
            }

            return windowHandle;
        }
    }
}