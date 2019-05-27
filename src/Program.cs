using System;
using System.IO;
using ConfiMicToggler.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;

namespace ConfiMicToggler
{
    public class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            var skypeConfig = Configuration.GetSection("ConfiMicTogglerConfig").Get<ConfiMicTogglerConfig>();
            var hostName = skypeConfig.Host;
            var port = skypeConfig.Port;

            Console.ForegroundColor = ConsoleColor.Green;
            PrintLogo();
            Console.WriteLine();
            Console.WriteLine("For setup it could be necessary to use this command, executed as administrator:");
            Console.WriteLine($"netsh http add urlacl url=http://{hostName}:{port}/ user={System.Environment.UserDomainName}\\{System.Environment.UserName}");
            Console.WriteLine();

            Console.WriteLine("Usage:");
            Console.WriteLine("======");
            Console.WriteLine($"Start your favorite browser and navigate to http://{hostName}:{port} to toggle the microfon of {skypeConfig.TargetConferenceTool}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                BuildWebHost(args, hostName, port).Run();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Whoops, some error occured, while starting the conference mic toggler\n: {e}");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Press any key to continue...");

                Console.ReadKey();
            }
        }

        public static IWebHost BuildWebHost(string[] args, string host, string port) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseHttpSys(options =>
                {
                    options.Authentication.AllowAnonymous = false;
                    options.Authentication.Schemes = AuthenticationSchemes.NTLM | AuthenticationSchemes.Negotiate;
                })
                .UseUrls($"http://{host}:{port}")
                .Build();

        private static void PrintLogo()
        {
            Console.WriteLine(@"    _____             __ _ __  __ _   _______                _           ");
            Console.WriteLine(@"   / ____|           / _(_)  \/  (_) |__   __|              | |          ");
            Console.WriteLine(@"  | |     ___  _ __ | |_ _| \  / |_  ___| | ___   __ _  __ _| | ___ _ __ ");
            Console.WriteLine(@"  | |    / _ \| '_ \|  _| | |\/| | |/ __| |/ _ \ / _` |/ _` | |/ _ \ '__|");
            Console.WriteLine(@"  | |___| (_) | | | | | | | |  | | | (__| | (_) | (_| | (_| | |  __/ |   ");
            Console.WriteLine(@"   \_____\___/|_| |_|_| |_|_|  |_|_|\___|_|\___/ \__, |\__, |_|\___|_|   ");
            Console.WriteLine(@"                                                  __/ | __/ |            ");
            Console.WriteLine(@"                                                 |___/ |___/             ");
            Console.WriteLine(@",.-~*´¨¯¨`*·~-.¸-(by Roman Lautner roman.lautner@saxsys.de)-,.-~*´¨¯¨`*·~-.¸");
        }
    }
}
