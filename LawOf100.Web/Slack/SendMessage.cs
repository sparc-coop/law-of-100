using LawOf100.Features.Habits.Entities;
using LawOf100.Features.Users.Entities;
using Newtonsoft.Json;
using Sparc.Core;
using Sparc.Features;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using static IdentityServer4.IdentityServerConstants;
using System.Text.Json.Nodes;

namespace LawOf100.Features.Slack;

public record SendMessageRequest(string userAt100);
public class SendMessage : Feature<SendMessageRequest, bool>
{

    //public class SlackClient
    //{
    //    private readonly Uri _uri;
    //    private readonly Encoding _encoding = new UTF8Encoding();

    //    public SlackClient(string urlWithAccessToken)
    //    {
    //        _uri = new Uri(urlWithAccessToken);
    //    }

    //    //Post a message using simple strings  
    //    public void PostMessage(string text, string username, string channel)
    //    {
    //        SlackRequest req = new SlackRequest()
    //        {
    //            Channel = channel,
    //            Username = username,
    //            Text = text
    //        };

    //        PostMessage(req);
    //    }

    //    //Post a message using a Payload object  
    //    public void PostMessage(SlackRequest req)
    //    {
    //        string payloadJson = JsonConvert.SerializeObject(req);

    //        using (WebClient client = new WebClient())
    //        {
    //            NameValueCollection data = new NameValueCollection();
    //            data["payload"] = payloadJson;

    //            var response = client.UploadValues(_uri, "POST", data);

    //            //The response text is usually "ok"  
    //            string responseText = _encoding.GetString(response);
    //        }
    //    }
    //}

    public SendMessage(IRepository<Habit> habits, IRepository<Account> accounts)
    {
        Habits = habits;
        Accounts = accounts;
    }

    public IRepository<Habit> Habits { get; }
    public IRepository<Account> Accounts { get; }

    public override async Task<bool> ExecuteAsync(SendMessageRequest request)
    {
        //HttpClient client = new HttpClient();

       Payload payload = new Payload()
        {
            Channel = "C03ULPZ5VDG",
            Username = "SparcBot",
            Text = request.userAt100 + " just hit 100 days on Law of 100!",
            AsUser = false,
        };

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://slack.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", "=" + "xoxb-3985260459745-3998449427552-Y9LUxTudODgb2x75mkXzBvfe");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "xoxb-3985260459745-3998449427552-Y9LUxTudODgb2x75mkXzBvfe");

            //var stringContent = new StringContent(req.ToString());
            //var str = JsonConvert.SerializeObject(req);
            var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("chat.postMessage", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Internal server Error");
                return false;
            }
        }

        //try
        //{
        //    string urlWithAccessToken = "https://slack.com/api/chat.postMessage?token={your_access_token}";

        //    SlackClient client = new SlackClient(urlWithAccessToken);

        //    client.PostMessage(username: "kamleshbhor",
        //               text: "THIS IS A TEST MESSAGE!!",
        //               channel: "#general");


        //    var url = new Uri("");
        //    var response = await client.GetAsync(url);
        //    var responseBody = await response.Content.ReadAsStringAsync();

        //    //colorContrast = JsonConvert.DeserializeObject<ContrastColorResponse>(responseBody);
        //    return true;
        //}
        //catch (Exception ex)
        //{
        //    var error = ex;
        //    return false;
        //}


    }


    public class Payload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("as_user")]
        public bool AsUser { get; set; }
    }
}
