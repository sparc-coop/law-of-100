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

namespace LawOf100.Features.Slack;

public record SendMessageRequest(string userAt100, int Days);
public class SendMessage : PublicFeature<SendMessageRequest, Rootobject>
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
    private readonly Encoding _encoding = new UTF8Encoding();
    public override async Task<Rootobject> ExecuteAsync(SendMessageRequest request)
    {
        // giphy api
        HttpClient giphyClient = new HttpClient();
            //var giphykey = "4N6leZuzErvFU6scwP4mnTotgocowAar";
        giphyClient.BaseAddress = new Uri("https://api.giphy.com/v1/gifs/random");
        giphyClient.DefaultRequestHeaders.Accept.Clear();
        giphyClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage gifResponse = await giphyClient.GetAsync("?api_key=4N6leZuzErvFU6scwP4mnTotgocowAar&tag=you+did+it&rating=g");
        string responseText = await gifResponse.Content.ReadAsStringAsync();
        var gif = JsonConvert.DeserializeObject<Gifobject>(responseText);
        var gifUrl = gif.Data.Images.Downsized.Url;
        

        var fifty = request.userAt100 + " just hit 50 days on Law of 100! Halfway there!";
        var ninety = request.userAt100 + " just hit 90 days on Law of 100! 10 more to go!";
        var hundred = request.userAt100 + " just hit 100 days on Law of 100!" + "\n" + gifUrl;

        string msgText;
        switch (request.Days)
        {
            case 50:
                msgText = fifty;
                break;
            case 90:
                msgText = ninety;
                break;
            case 100:
                msgText = hundred;
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

        Rootobject data = new Rootobject();



        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://slack.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["SlackToken"]);

            //get user list api.slack.com/methods/users.list
            //HttpResponseMessage response = await client.GetAsync("users.list?limit=10");

            //send channel message api.slack.com/methods/chat.postMessage
            HttpResponseMessage response = await client.PostAsync("chat.postMessage", httpContent);
            if (response.IsSuccessStatusCode)
            {
                //string responseText = await response.Content.ReadAsStringAsync();
                //data = JsonConvert.DeserializeObject<Rootobject>(responseText);
                return data;
            }
            else
            {
                Console.WriteLine("Internal server Error");
                return data;
            }
        }

       // return data;

    }


    public class Payload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

    }



}

public class Gifobject
{
    public Data Data { get; set; }
}

public class Data
{
    public string id { get; set; }
    public string url { get; set; }
    public string slug { get; set; }
    public string bitly_gif_url { get; set; }
    public string bitly_url { get; set; }
    public string embed_url { get; set; }

    public Images Images { get; set; }
}

public class Images
{
    public Downsized Downsized { get; set; }
}

public class Downsized
{
    public string Url { get; set; }
}

public class Rootobject
{
    public Member[] Members { get; set; }
}

public class Member
{
    public string id { get; set; }
    public string team_id { get; set; }
    public string name { get; set; }
    public bool deleted { get; set; }
    public string color { get; set; }
    public string real_name { get; set; }
    public string tz { get; set; }
    public string tz_label { get; set; }
    public int tz_offset { get; set; }
    public Profile profile { get; set; }
    public bool is_admin { get; set; }
    public bool is_owner { get; set; }
    public bool is_primary_owner { get; set; }
    public bool is_restricted { get; set; }
    public bool is_ultra_restricted { get; set; }
    public bool is_bot { get; set; }
    public bool is_app_user { get; set; }
    public int updated { get; set; }
    public bool is_email_confirmed { get; set; }
    public string who_can_share_contact_card { get; set; }
}

public class Profile
{
    public string title { get; set; }
    public string phone { get; set; }
    public string skype { get; set; }
    public string real_name { get; set; }
    public string real_name_normalized { get; set; }
    public string display_name { get; set; }
    public string display_name_normalized { get; set; }
    public string status_text { get; set; }
    public string status_emoji { get; set; }
    public object[] status_emoji_display_info { get; set; }
    public int status_expiration { get; set; }
    public string avatar_hash { get; set; }
    public bool always_active { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string image_24 { get; set; }
    public string image_32 { get; set; }
    public string image_48 { get; set; }
    public string image_72 { get; set; }
    public string image_192 { get; set; }
    public string image_512 { get; set; }
    public string status_text_canonical { get; set; }
    public string team { get; set; }
    public string image_original { get; set; }
    public bool is_custom_image { get; set; }
    public string image_1024 { get; set; }
    public string api_app_id { get; set; }
    public string bot_id { get; set; }
}


