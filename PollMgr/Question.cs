using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollMgr
{
    public class Question
    {
        public Int32 Id { get; set; }
        public Int32 Difficulty { get; set; }
        public List<String>? Options { get; set; }
        public String? Type { get; set; }
        public String? Answer { get; set; }
        public DateTime CreateTime { get; set; }
        public String? Content { get; set; }
        public String? Category { get; set; }
        public async Task SubmitAnswerAsync(String token,String answer)
        {
            RestClient client = new(Account.BaseUri);
            RestRequest request = new("api/submit-answer",Method.Post) 
            { 
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer"),
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new { question_id = Id, user_answer =answer});
            var obj = JObject.Parse((await client.PostAsync(request)).Content!);
            Int32 code = Int32.Parse(obj["code"]!.ToString());
            String message = obj["message"]!.ToString();
            if (code !=200)
            {
                throw code switch
                {
                    404 => throw new AccountException(message, AccountExceptionType.NotFound),
                    _ => throw new AccountException(message)
                };
            }
        }
        public override string ToString()
        {
            return $"{Id},难度：{Difficulty},标题:{Content}";
        }
        public static List<String> parseOptions(String o)
        {
            var array = JArray.Parse(o);
            List<String> list = [];
            foreach (var option in array)
            {
                list.Add(option.ToString());
            }
            return list;
        }
        public static async Task<Question[]> GetQuestionsAsync()
        {
            RestSharp.RestClient client = new(Account.BaseUri);
            RestSharp.RestRequest request = new("api/questions");
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
                var questions = obj["data"]!;
                List<Question> result = [];
                foreach (var question in questions)
                {
                    var options = parseOptions(question["options"]!.ToString());
                    var id = Int32.Parse(question["id"]!.ToString());
                    var content = question["content"]!.ToString();
                    var type = question["type"]!.ToString();
                    var answer = question["answer"]!.ToString();
                    var category = question["category"]!.ToString();
                    var difficulty = Int32.Parse(question["difficulty"]!.ToString());
                    var createTime = DateTime.Parse(question["created_at"]!.ToString());
                    result.Add(new()
                    {
                        Id = id,
                        Content = content,
                        Type = type,
                        Answer = answer,
                        Difficulty = difficulty,
                        Category = category,
                        CreateTime = createTime,
                        Options = options
                    });
                }
                return [.. result];
            }
        }
    }
}
