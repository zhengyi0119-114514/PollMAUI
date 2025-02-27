using System;
using Newtonsoft.Json;
namespace PollMgr;

public class AccountInfo(String name)
{
    public String Name { get; set; } = name;
    public String? Token { get; set; }
    public override string ToString()
    {
        return String.Format("Name: {0} Token:{1}", Name, Token);
    }
    public Int32 Id { get; set; }
    public bool IsAdmin { get; set; }
    public Int32 TotalAnwers { get; set; }
    public Int32 CorrectAnwers { get; set; }
    public Int32 WrongAnwers { get; set; }
    public DateTime LastActivity { get; set; }
    public Int32 Score {get;set;}
}
