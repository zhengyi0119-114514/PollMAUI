using System;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
namespace PollMgr;

public static class Account
{
    public static Uri BaseUri
    {
        get
        {
            return new Uri("https://api.jmxhyz.site/");
        }
    }
    public static async Task<String> RegisterAsync(string name, string password)
    {
        RestClient client = new(BaseUri);
        RestRequest request = new("api/register", Method.Post)
        {
            RequestFormat = DataFormat.Json,
        };
        request.AddJsonBody(new { username = name, password = password });
        var result = await client.ExecuteAsync(request);
        JObject obj = JObject.Parse(result.Content ?? String.Empty);
        Int32 code = Int32.Parse(obj["code"]?.ToString() ?? "0");
        if (code != 201)
        {
            String message = obj["message"]?.ToString() ?? String.Empty;
            throw code switch
            {
                0 => new AccountException(message, AccountExceptionType.Network),
                409 => new AccountException(message, AccountExceptionType.Exist),
                _ => new AccountException(message)
            };
        }
        else
        {

            return obj["data"]!["token"]!.ToString();
        }

    }
    public static async Task<String> LoginAsync(string username, string password)
    {
        RestClient client = new(BaseUri);
        RestRequest request = new("api/login", Method.Post)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(new { username = username, password = password });
        var result = await client.ExecuteAsync(request);
        JObject obj = JObject.Parse(result.Content ?? String.Empty);
        Int32 code = Int32.Parse(obj["code"]?.ToString() ?? "0");
        if (code != 200)
        {
            String message = obj["message"]?.ToString() ?? String.Empty;
            throw code switch
            {
                0 => new AccountException(message, AccountExceptionType.Network),
                401 => new AccountException(message, AccountExceptionType.NotFound),
                _ => new AccountException(message, AccountExceptionType.Unknown),
            };
        }
        else
        {
            return obj["data"]!["token"]!.ToString();
        }
    }
    public static async Task<AccountInfo> GetAccountInfoAsync(String Token)
    {
        AccountInfo accountInfo = new("NULL");
        RestClient client = new(BaseUri);
        RestRequest request = new("api/user/profile")
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(Token, "Bearer")
        };
        var result = await client.ExecuteAsync(request);
        JObject obj = JObject.Parse(result.Content ?? String.Empty);
        Int32 code = Int32.Parse(obj["code"]?.ToString() ?? "0");
        if (code != 200)
        {
            String message = obj["message"]?.ToString() ?? String.Empty;
            throw code switch
            {
                0 => new AccountException(message, AccountExceptionType.Network),
                401 => new AccountException(message, AccountExceptionType.Token),
                _ => new AccountException(message, AccountExceptionType.Unknown),
            };
        }
        else
        {
            var data = obj["data"]!;
            var userInfo = data["user_info"];
            accountInfo.Id = (int)userInfo!["id"]!;
            accountInfo.IsAdmin = (int)userInfo!["is_admin"]! != 0;
            accountInfo.Score = (Int32)userInfo!["score"]!;
            var stats = data["stats"];

            accountInfo.TotalAnwers = (Int32)stats!["total_answers"]!;
            accountInfo.CorrectAnwers = Int32.Parse((string)stats!["correct_answers"]!);
            accountInfo.WrongAnwers = Int32.Parse((string)stats!["wrong_answers"]!);
            accountInfo.LastActivity = DateTime.Parse((String)stats!["last_activity"]!);
            return accountInfo;
        }
    }

}

