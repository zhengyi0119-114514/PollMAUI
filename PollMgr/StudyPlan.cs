using System;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
namespace PollMgr;
public class StudyPlan
{
    public String? Text { get; set; }
    public static async Task<StudyPlan> GenerateStudyPlan(String token)
    {
        RestClient client = new(Account.BaseUri);
        RestRequest request = new("api/generate-study-plan")
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer"),
            RequestFormat = DataFormat.Json
        };
        var obj = JObject.Parse((await client.PostAsync(request)).Content!);
        var output = obj["output"];
        if (output == null)
        {
            throw new AccountException("我不到啊？！");
        }
        else
        {
            return new() { Text = output["text"]!.ToString() };
        }
    }
}