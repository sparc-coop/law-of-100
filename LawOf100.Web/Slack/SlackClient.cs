using LawOf100.Features.Slack.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LawOf100.Features.Slack
{
    public class SlackService
    {
        private readonly IConfiguration _config;
        public SlackService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SlackApiPost(Payload payload)
        {

            var stringPayload = JsonConvert.SerializeObject(payload);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://slack.com/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["SlackToken"]);

                //send channel message api.slack.com/methods/chat.postMessage
                HttpResponseMessage response = await client.PostAsync("chat.postMessage", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else
                {
                    Console.WriteLine("Error");
                    return false;
                }
            }
        }

    }
    
}
