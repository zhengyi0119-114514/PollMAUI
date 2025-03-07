using System;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
namespace PollMgr;
public class WrongAnswer
{
    public Int32 RecordId { get; set; }
    public String? Content { get; set; }
    public List<String>? Options { get; set; }
    public String? UserAnswer { get; set; }
    public String? CorrectAnswer { get; set; }
    public DateTime CreateTime { get; set; }
    public String? Answer { get; set; }
    public static async Task<List<WrongAnswer>> GetWrongAnswer(String token)
    {
        RestClient client = new(Account.BaseUri);
        RestRequest request = new("api/user/wrong-answers")
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer"),
            RequestFormat = DataFormat.Json
        };
        var obj = JObject.Parse((await client.GetAsync(request)).Content ?? String.Empty);
        Int32 code = Int32.Parse(obj["code"]!.ToString());
        String message = obj["message"]!.ToString();
        if (code != 200)
        {
            throw code switch
            {
                404 => throw new AccountException(message, AccountExceptionType.NotFound),
                _ => throw new AccountException(message)
            };
        }
        else
        {
            var wrongAnswers = obj["data"]!;
            List<WrongAnswer> result = [];
            foreach (var wrongAnswer in wrongAnswers)
            {
                var options = Question.parseOptions(wrongAnswer["options"]!.ToString());
                var recordId = Int32.Parse(wrongAnswer["record_id"]!.ToString());
                var content = wrongAnswer["content"]!.ToString();
                var userAnswer = wrongAnswer["user_answer"]!.ToString();
                var createTime = DateTime.Parse(wrongAnswer["created_at"]!.ToString());
                var answer = wrongAnswer["answer"]!.ToString();
                result.Add(new()
                {
                    RecordId = recordId,
                    Options = options,
                    Content = content,
                    UserAnswer = userAnswer,
                    CreateTime = createTime,
                    Answer = answer
                });
            }
            return [.. result];
        }
    }
}