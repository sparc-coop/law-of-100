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
        HttpClient SlackApiClient = new HttpClient();
        private readonly IConfiguration _config;
        public SlackService(IConfiguration config)
        {
            _config = config;
        }
        //SlackApiClient.BaseAddress = new Uri("https://slack.com/api/");
        //SlackApiClient.DefaultRequestHeaders.Accept.Clear();
        //    SlackApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    SlackApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["SlackToken"]);

    }
    
}
