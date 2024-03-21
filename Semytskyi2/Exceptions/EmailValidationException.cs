namespace Semytskyi2.Exceptions
{
    public class EmailValidationException : ValidationException
    {
        public EmailValidationException(string? message) : base(message)
        {
        }
    }
}