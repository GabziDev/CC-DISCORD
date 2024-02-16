using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CasseCouilleDiscord.Program;

namespace CasseCouilleDiscord.Modules
{
    internal class SpamMessage
    {
        private static int messageSend = 0;

        public static async void Message()
        {
            Console.Clear();
            Messages.Logo.SpamMessage();

            object ghostPing = Messages.Print.Question("Ghost Ping (oui/non)", "str");
            Console.ResetColor();

            object nbMessage = Messages.Print.Question("Combien de message", "int");
            Console.ResetColor();

            object idChannel = Messages.Print.Question("ID du channel", "str");
            Console.ResetColor();

            object delay = Messages.Print.Question("Delay (MS)", "int");
            Console.ResetColor();

            object messageSpam = Messages.Print.Question("Contenu du message", "str");
            Console.ResetColor();

            string urlAPI = $"https://discord.com/api/v10/channels/{Convert.ToString(idChannel)}/messages";

            var data = new
            {
                content = Convert.ToString(messageSpam),
                tts = false,
                flags = 0
            };

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", Token.TokenManager.tokenDiscord);

                for (int i = 0; i < Convert.ToInt32(nbMessage); i++)
                {
                    try
                    {
                        try
                        {
                            var request = new HttpRequestMessage(HttpMethod.Post, urlAPI);
                            var jsonData = JsonConvert.SerializeObject(data);
                            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                            request.Content = content;

                            var response = httpClient.SendAsync(request).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                var responseData = response.Content.ReadAsStringAsync().Result;
                                var messageData = JsonConvert.DeserializeObject<MessageResponse>(responseData);

                                if (messageData != null && messageData.id != null)
                                {
                                    string messageId = messageData.id.ToString();

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"> Message envoyé avec succès ({messageSend}) !");
                                    Console.ResetColor();

                                    Console.Title = $"CC-Discord | GabzDEV - Souciss | Msg : {messageSend}";

                                    messageSend++;

                                    if (Convert.ToString(ghostPing) == "oui")
                                    {
                                        await DeleteMessage(Convert.ToString(idChannel), messageId);
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Erreur: Impossible de récupérer l'identifiant du message.");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Erreur lors de l'envoi du message");
                                Console.ResetColor();
                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{ex.Message}");
                            Console.ResetColor();
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{ex.Message}");
                        Console.ResetColor();
                    }
                    Thread.Sleep(Convert.ToInt32(delay) / 1000);
                }
            }

            Console.Clear();
            Menu();
        }

        static async Task DeleteMessage(string channelId, string messageId)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", Token.TokenManager.tokenDiscord);

            string url = $"https://discord.com/api/v10/channels/{channelId}/messages/{messageId}";

            HttpResponseMessage response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Message supprimé avec succès : {messageId}");
            }
            else
            {
                Console.WriteLine($"Erreur lors de la suppression du message.");
            }
        }

        public class MessageResponse
        {
            public string id { get; set; }
        }
    }
}
