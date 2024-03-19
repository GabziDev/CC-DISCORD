using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CasseCouilleDiscord.Token
{
    internal class Validator
    {
        public static async Task<bool> IsTokenValid(string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"{token}");

                var response = await httpClient.GetAsync("https://discord.com/api/v10/users/@me");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var userData = JObject.Parse(jsonResponse);

                    if (userData["id"] != null)
                    {
                        Console.WriteLine($"Token valide");
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine($"Erreur lors de la vérification du token : {response.StatusCode}");
                }
            }

            return false;
        }
    }
}
