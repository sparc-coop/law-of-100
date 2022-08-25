using LawOf100.Features.Habits.Entities;
using LawOf100.Features.Users.Entities;
using Sparc.Core;
using Sparc.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LawOf100.Features.Slack.Entities;

namespace LawOf100.Features.Slack
{
    public class GetMembers : PublicFeature<SendMessageRequest, Rootobject>
    {

        private readonly IConfiguration _config;
        public GetMembers(IConfiguration config)
        {
            _config = config;
        }

        public override async Task<Rootobject> ExecuteAsync(SendMessageRequest request)
        {
            Rootobject data = new Rootobject();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://slack.com/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["SlackToken"]);

                //get user list api.slack.com/methods/users.list
                HttpResponseMessage response = await client.GetAsync("users.list?limit=10");

                if (response.IsSuccessStatusCode)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Rootobject>(responseText);
                    return data;
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                    return data;
                }
            }
        }
    }
}

