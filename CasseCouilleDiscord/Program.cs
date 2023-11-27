using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace CasseCouilleDiscord {
    internal class Program {

        private static string tokenDiscord;
        private static int messageSend = 0;

        static void Main()
        {
            LoadToken();
            Console.ReadKey();
        }

        static void LoadToken()
        {
            if (File.Exists("token.json"))
            {
                string json = File.ReadAllText("token.json");
                var tokenData = JsonConvert.DeserializeObject<TokenData>(json);
                tokenDiscord = tokenData.Token;
                Console.Clear();
                Menu();
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

        static void TokenRegister()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string textToken = @"
 _____      _              
/__   \___ | | _____ _ __  
  / /\/ _ \| |/ / _ \ '_ \ 
 / / | (_) |   <  __/ | | |
 \/   \___/|_|\_\___|_| |_|
";
            Console.WriteLine(textToken);
            Console.ResetColor();

            Console.Write("Quel est votre token ? ");
            Console.ForegroundColor = ConsoleColor.Green;
            tokenDiscord = Console.ReadLine();
            Console.ResetColor();
            SaveToken();
            Console.Clear();
            Menu();
        }

        static void Process()
        {

        }

        static void Menu()
        {
            Console.Title = $"Message envoyer : {messageSend}";
            Console.ForegroundColor = ConsoleColor.Red;
            string textTitle = @"
   _____ _____      _____ _____  _____  _____ ____  _____  _____  
  / ____/ ____|    |  __ \_   _|/ ____|/ ____/ __ \|  __ \|  __ \ 
 | |   | |   ______| |  | || | | (___ | |   | |  | | |__) | |  | |
 | |   | |  |______| |  | || |  \___ \| |   | |  | |  _  /| |  | |
 | |___| |____     | |__| || |_ ____) | |___| |__| | | \ \| |__| |
  \_____\_____|    |_____/_____|_____/ \_____\____/|_|  \_\_____/ ";
            Console.WriteLine(textTitle);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            string textMenu = @"
╔══════════════════════╗
║                      ║
║   [1] Spam Message   ║
║   [2] Spam React     ║
║   [3] Quitter        ║
║                      ║
╚══════════════════════╝";

            Console.WriteLine(textMenu);



            Console.Write("Choix -> ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            int choix = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();

            switch (choix)
            {
                case 1:
                    Console.Clear();
                    SpamMessage();
                    break;
                case 2:
                    Console.Clear();
                    SpamReact();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Process();
                    Menu();
                    break;
            }
        }

        static void SpamMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            string textSpamMessage = @"
 __                                                               
/ _\_ __   __ _ _ __ ___     /\/\   ___  ___ ___  __ _  __ _  ___ 
\ \| '_ \ / _` | '_ ` _ \   /    \ / _ \/ __/ __|/ _` |/ _` |/ _ \
_\ \ |_) | (_| | | | | | | / /\/\ \  __/\__ \__ \ (_| | (_| |  __/
\__/ .__/ \__,_|_| |_| |_| \/    \/\___||___/___/\__,_|\__, |\___|
   |_|                                                 |___/      
";
            Console.WriteLine(textSpamMessage);
            Console.ResetColor();

            Console.Write("\nCombien de message ? ");

            Console.ForegroundColor = ConsoleColor.Green;
            int nbMessage = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();

            Console.Write("ID du channel ? ");

            Console.ForegroundColor = ConsoleColor.Green;
            string idChannel = Console.ReadLine();
            Console.ResetColor();

            string urlAPI = $"https://discord.com/api/v10/channels/{idChannel}/messages";

            Console.Write("Delay (MS) ? ");

            Console.ForegroundColor = ConsoleColor.Green;
            int delay = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();

            Console.Write("Message à spam -> ");

            Console.ForegroundColor = ConsoleColor.Green;
            string messageSpam = Console.ReadLine();
            Console.ResetColor();

            var data = new
            {
                content = messageSpam,
                tts = false,
                flags = 0
            };

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", tokenDiscord);

                for (int i = 0;i < nbMessage;i++)
                {
                    if (Console.KeyAvailable)
                    {
                        Menu();
                        break;
                    }
                    try
                    {
                        var request = new HttpRequestMessage(HttpMethod.Post, urlAPI);
                        var jsonData = JsonConvert.SerializeObject(data);
                        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        request.Content = content;

                        var response = httpClient.SendAsync(request).Result;
                        response.EnsureSuccessStatusCode();

                        string responseData = response.Content.ReadAsStringAsync().Result;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> Message envoyé avec succès ({messageSend}) !");
                        Console.ResetColor();

                        Console.Title = $"Message envoyer : {messageSend}";
                        messageSend++;
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{ex.Message}");
                        Console.ResetColor();
                    }
                    System.Threading.Thread.Sleep(delay / 1000);
                }
            }
            Console.Clear();
            Menu();
        }

        static void SpamReact()
        {

        }

        public class TokenData {
            public string Token { get; set; }
        }
    }
}
