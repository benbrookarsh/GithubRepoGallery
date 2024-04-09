namespace Server.Shared.Exceptions;

public  class UserAuthenticationException : Exception
{
    protected UserAuthenticationException(string message) : base(message){}
    /// <summary>
    /// a safe message we can show to the world
    /// </summary>
    public string SafeMessage { get; } 
}
public class UserIncorrectPasswordException : Exception
{
    public UserIncorrectPasswordException(string userName) : base($"the given password is innorecet for user {userName}"){}
}
public class UserNotFoundException : UserAuthenticationException
{
    public UserNotFoundException(string userName) : base($"user with name {userName} is not found"){}
}

public class BookMarkNotFoundException : Exception
{
    public BookMarkNotFoundException(string userName) : base($"user with name {userName} is not found"){}
}