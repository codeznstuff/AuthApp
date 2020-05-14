using System;
using Colorify;
using static Colorify.Colors;
using Colorify.UI;
using ToolBox.Bridge;
using ToolBox.Notification;
using ToolBox.Platform;

namespace Infrastructure
{
    static class Program
    {
        private static Format _colorify { get; set; }
        public static INotificationSystem _notificationSystem { get; set; }
        public static IBridgeSystem _bridgeSystem { get; set; }
        public static ShellConfigurator _shell { get; set; }

        static void Main(string[] args)
        {
            try
            {
                _notificationSystem = NotificationSystem.Default;
                switch (OS.GetCurrent())
                {
                    case "win":
                        _bridgeSystem = BridgeSystem.Bat;
                        _colorify = new Format(Theme.Dark);
                        break;
                    case "gnu":
                        _bridgeSystem = BridgeSystem.Bash;
                        _colorify = new Format(Theme.Dark);
                        break;
                    case "mac":
                        _bridgeSystem = BridgeSystem.Bash;
                        _colorify = new Format(Theme.Light);
                        break;
                }
                _shell = new ShellConfigurator(_bridgeSystem, _notificationSystem);
                Menu();
                _colorify.ResetColor();
                _colorify.Clear();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageException("Ahh my eyes! Why this console is too small?");
            }
            catch (Exception ex)
            {
                MessageException(ex.ToString());
            }
        }

        static void Menu()
        {
            _colorify.Clear();
            _colorify.DivisionLine('=', Colorify.Colors.bgInfo);
            _colorify.AlignCenter("SHELL", Colorify.Colors.bgInfo);
            _colorify.DivisionLine('=', Colorify.Colors.bgInfo);
            _colorify.BlankLines();
            _colorify.Write($"{" 1]",-4}"); _colorify.WriteLine("Setup NGINX Container");
            _colorify.Write($"{" 2]",-4}"); _colorify.WriteLine("Setup MSSQL Container");
            _colorify.BlankLines();
            _colorify.DivisionLine('=', Colorify.Colors.bgInfo);
            _colorify.BlankLines();
            _colorify.Write($"{" Make your choice:",-25}");
            string opt = Console.ReadLine();
            opt = opt.ToLower();

            _colorify.Clear();
            switch (opt)
            {
                case "1": SetupContainer("NGINX"); break;
                case "2": SetupContainer("MSSQL"); break;
                default: Menu(); break;
            }
        }

        static void Back()
        {
            _colorify.BlankLines();
            _colorify.Write("Press [ANY] key to continue ");
            Console.ReadKey();
            Menu();
        }

        static void MessageException(string message)
        {
            _colorify.ResetColor();
            _colorify.Clear();
            _colorify.WriteLine(message, bgDanger);
        }

        static void SetupContainer(string container)
        {
            try
            {
                if (container.Equals("MSSQL"))
                {
                    _shell.Term("docker-compose -f docker-compose.mssql.yml up -d", Output.Internal);
                    _shell.Term("dotnet build ../../../../../../Database/DbUp/DbUp.sln", Output.Internal);
                    _shell.Term("dotnet ../../../../../../Database/DbUp/bin/Debug/netcoreapp2.2/DbUp.dll", Output.Internal);
                }

                if (container.Equals("NGINX"))
                {
                    _shell.Term("docker-compose -f docker-compose.nginx.yml up -d", Output.Internal);
                }

                Back();
            }
            catch (Exception ex)
            {
                MessageException(ex.ToString());
            }
        }
    }
}