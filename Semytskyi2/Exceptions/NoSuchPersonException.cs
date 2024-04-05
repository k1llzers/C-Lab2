namespace Semytskyi2.Exceptions
{
    public class NoSuchPersonException : ValidationException
    {
        public NoSuchPersonException(string? message) : base(message)
        {
        }
    }   
}