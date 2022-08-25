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
using System.Linq.Expressions;
using LawOf100.Features.Slack.Entities;

namespace LawOf100.Features.Slack;

public record SendMessageRequest(string userAt100, int Days);
public class SendMessage : PublicFeature<SendMessageRequest, bool>
{

    public SendMessage(IRepository<Habit> habits, IRepository<Account> accounts, IConfiguration config)
    {
        Habits = habits;
        Accounts = accounts;
        _config = config;
    }

    public IRepository<Habit> Habits { get; }
    public IRepository<Account> Accounts { get; }

    private readonly IConfiguration _config;
    public override async Task<bool> ExecuteAsync(SendMessageRequest request)
    {
        // giphy api
        HttpClient giphyClient = new HttpClient();
        giphyClient.BaseAddress = new Uri("https://api.giphy.com/v1/gifs/random");
        giphyClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage gifResponse = await giphyClient.GetAsync("?api_key=4N6leZuzErvFU6scwP4mnTotgocowAar&tag=you+did+it&rating=g");
        string responseText = await gifResponse.Content.ReadAsStringAsync();
        var gif = JsonConvert.DeserializeObject<Gifobject>(responseText);
        var gifUrl = gif.Data.Images.Downsized.Url;

        string msgText;
        switch (request.Days)
        {
            case 50:
                msgText = request.userAt100 + " just hit 50 days on Law of 100! Halfway there!";
                break;
            case 90:
                msgText = request.userAt100 + " just hit 90 days on Law of 100! 10 more to go!";
                break;
            case 100:
                msgText = request.userAt100 + " just hit 100 days on Law of 100!" + "\n" + gifUrl;
                break;
            default:
                msgText = "";
                break;
        }

        Payload payload = new Payload()
        {
            Channel = "C03ULPZ5VDG",
            Text = msgText,
        };

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
                return true;
            }
            else
            {
                Console.WriteLine("Error");
                return false;
            }
        }

    }


    public class Payload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

    }



}





