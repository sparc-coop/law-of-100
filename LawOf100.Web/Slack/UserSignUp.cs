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
using System.Reflection.Metadata.Ecma335;

namespace LawOf100.Features.Slack;

public record UserSignUpRequest(string email);
public class UserSignUp : PublicFeature<UserSignUpRequest, bool>
{

    public UserSignUp(IRepository<Habit> habits, IRepository<Account> accounts, IConfiguration config)
    {
        Habits = habits;
        Accounts = accounts;
        _config = config;
    }

    public IRepository<Habit> Habits { get; }
    public IRepository<Account> Accounts { get; }

    private readonly IConfiguration _config;
    public override async Task<bool> ExecuteAsync(UserSignUpRequest request)
    {

        Payload payload = new Payload()
        {
            Channel = "C040XKHTEMA",
            Text = "A new join request!" + "\n" + "Email: " + request.email,
        };

        //var stringPayload = JsonConvert.SerializeObject(payload);
        //var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri("https://slack.com/api/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["SlackToken"]);

        //    //send channel message api.slack.com/methods/chat.postMessage
        //    HttpResponseMessage response = await client.PostAsync("chat.postMessage", httpContent);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Error");
        //        return false;
        //    }
        //}

        return await new SlackService(_config).SlackApiPost(payload);

    }

}





