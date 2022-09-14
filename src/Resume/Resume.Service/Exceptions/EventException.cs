namespace Resume.Service.Exceptions;

public class EventException : Exception
{
    public int Code { get; private set; }

    public EventException(int code, string message)
        : base(message)
    {
        Code = code;
    }
}
