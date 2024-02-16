using System;
using System.IO;

namespace CasseCouilleDiscord.Token
{
    internal class ChangeToken
    {
        public static void Change()
        {
            object choix = Messages.Print.Question("Voulez-vous modifier le token (oui/non)", "str");
            Console.ResetColor();

            if (Convert.ToString(choix).ToLower() == "oui")
            {
                Console.Clear();

                if (File.Exists("token.json"))
                {
                    File.Delete("token.json");
                }

                TokenManager.TokenRegister();
            }
            else
            {
                Console.Clear();
                Program.Menu();
            }
        }
    }
}
