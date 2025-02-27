using System;

namespace PollMgr;

public enum AccountExceptionType
{
    Unknown = 0,
    NotFound = 1,
    Exist = 2,
    Network = 3,
    Token =4
}
public class AccountException : Exception
{
    public AccountException(String? message, AccountExceptionType type = AccountExceptionType.Unknown) : base(message)
    {
    }
}
