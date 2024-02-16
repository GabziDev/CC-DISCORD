using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace CasseCouilleDiscord.Modules
{
    internal class SpamReact
    {
        private static HashSet<string> processedMessageIds = new HashSet<string>();

        public static void React()
        {
            Console.Clear();

            Messages.Logo.SpamReact();

            object channelId = Messages.Print.Question("ID du salon où réagir", "str");
            Console.ResetColor();

            string textMenu = @"
╔══════════════════════╗
║                      ║
║   [1] Singe          ║
║   [2] Smile          ║
║                      ║
╚══════════════════════╝";

            Console.WriteLine(textMenu);

            object emojiChoix = Messages.Print.Question("Choix de l'emoji", "int");
            Console.ResetColor();

            string emoji;

            switch (Convert.ToInt32(emojiChoix))
            {
                case 1:
                    emoji = "🐵";
                    break;
                case 2:
                    emoji = "😊";
                    break;
                default:
                    Console.WriteLine("Choix invalide. Utilisation de l'emoji par défaut : 😊");
                    emoji = "😊";
                    break;
            }

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", Token.TokenManager.tokenDiscord);

            string url = $"https://discord.com/api/v10/channels/{channelId}/messages";
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                dynamic messages = JsonConvert.DeserializeObject(responseData);

                foreach (var message in messages)
                {
                    string messageId = message.id;
                    processedMessageIds.Add(messageId);
                }
            }
            else
            {
                Console.WriteLine($"Erreur lors de la récupération des messages : {response.StatusCode}");
            }

            while (true)
            {
                Console.WriteLine("En attente de nouveaux messages...");
                Thread.Sleep(1000); // 1 sec

                HttpResponseMessage newMessagesResponse = httpClient.GetAsync(url).Result;

                if (newMessagesResponse.IsSuccessStatusCode)
                {
                    string responseData = newMessagesResponse.Content.ReadAsStringAsync().Result;
                    dynamic newMessages = JsonConvert.DeserializeObject(responseData);

                    foreach (var message in newMessages)
                    {
                        string messageId = message.id;
                        string messageContent = message.content;

                        if (!processedMessageIds.Contains(messageId))
                        {
                            HttpResponseMessage reactionResponse = httpClient.PutAsync($"https://discord.com/api/v10/channels/{channelId}/messages/{messageId}/reactions/{Uri.EscapeDataString(emoji)}/@me", null).Result;
                            reactionResponse.EnsureSuccessStatusCode();
                            Console.WriteLine($"Réaction ajoutée au message : \"{messageContent}\"");
                            processedMessageIds.Add(messageId);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Erreur lors de la récupération des nouveaux messages : {newMessagesResponse.StatusCode}");
                }
            }
        }
    }
}
