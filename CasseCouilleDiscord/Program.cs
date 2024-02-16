using System;

namespace CasseCouilleDiscord
{
    internal class Program
    {
        static void Main()
        {
            Token.TokenManager.LoadToken();
            Console.ReadKey();
        }

        public static void Menu()
        {
            Console.Title = "CC-Discord | GabzDEV - Souciss";
            Messages.Logo.AppLogo();

            Console.ForegroundColor = ConsoleColor.Gray;
            string textMenu = @"
╔══════════════════════╗
║                      ║
║   [1] Spam Message   ║
║   [2] Spam React     ║
║   [3] Token          ║
║   [4] Quitter        ║
║                      ║
╚══════════════════════╝";

            Console.WriteLine(textMenu);

            object choix = Messages.Print.Question("Choix", "int");
            Console.ResetColor();

            switch (choix)
            {
                case 1:
                    Console.Clear();
                    Modules.SpamMessage.Message();
                    break;
                case 2:
                    Console.Clear();
                    Modules.SpamReact.React();
                    break;
                case 3:
                    Console.Clear();
                    Token.ChangeToken.Change();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Menu();
                    break;
            }
        }
    }
}
