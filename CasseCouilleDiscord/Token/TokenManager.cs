using Newtonsoft.Json;
using System;
using System.IO;

namespace CasseCouilleDiscord.Token
{
    internal class TokenManager
    {
        public static string tokenDiscord;

        public static void LoadToken()
        {
            if (File.Exists("token.json"))
            {
                string json = File.ReadAllText("token.json");
                var tokenData = JsonConvert.DeserializeObject<TokenData>(json);
                tokenDiscord = tokenData.Token;
                Console.Clear();
                Program.Menu();
            }
            else
            {
                TokenRegister();
            }
        }

        static void SaveToken()
        {
            var tokenData = new TokenData { Token = tokenDiscord };
            string json = JsonConvert.SerializeObject(tokenData);
            File.WriteAllText("token.json", json);
        }

        public static void TokenRegister()
        {
            Messages.Logo.Token();

            object tokenDiscordRes = Messages.Print.Question("Quel est votre token", "str");

            tokenDiscord = Convert.ToString(tokenDiscordRes);
            Console.ResetColor();

            bool isTokenValid = Validator.IsTokenValid(tokenDiscord).Result;

            if (isTokenValid)
            {
                Console.WriteLine("Token verifier et valide !");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Le token enregistré est invalide. Veuillez vérifier le token (si vous pensez qu'il est valide, sauvegardez-le)");
                Console.ResetColor();
            }

            object choix = Messages.Print.Question("Voulez-vous enregister le token (oui/non)", "str");
            Console.ResetColor();

            if (Convert.ToString(choix).ToLower() == "oui")
            {
                SaveToken();
                Console.Clear();
            }
            else if (Convert.ToString(choix).ToLower() == "non")
            {
                Console.Clear();
                Console.WriteLine("[!] Token non enregister");
            }

            Program.Menu();
        }

        public class TokenData
        {
            public string Token { get; set; }
        }
    }
}
