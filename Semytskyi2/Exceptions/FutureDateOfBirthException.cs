namespace Semytskyi2.Exceptions
{
    public class FutureDateOfBirthException : ValidationException
    {
        public FutureDateOfBirthException(string? message) : base(message)
        {
        }
    }
}