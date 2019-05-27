using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SkypeMicToggler.Config;

namespace SkypeMicToggler.Controllers
{
    public class MicController : Controller
    {
        private readonly UserStore _userStore;
        private readonly SkypeMicTogglerConfig _config;

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

        public MicController(UserStore userStore, IOptions<SkypeMicTogglerConfig> config)
        {
            _userStore = userStore;
            _config = config.Value;
        }

        // GET
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

                _userStore.Add(this.User);
            }

            return View(_userStore);
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

        public static void BringConferenceWindowToFront(IntPtr conferenceWindow)
        {
            // Verify that Skype is a running process.
            if (conferenceWindow == IntPtr.Zero)
            {
                return;
            }

            // Make Skype the foreground application
            SetForegroundWindow(conferenceWindow);
        }
    }
}