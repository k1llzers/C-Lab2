namespace Semytskyi2.Exceptions
{
    public class SoPastDateOfBirthException : ValidationException
    {
        public SoPastDateOfBirthException(string? message) : base(message)
        {
        }
    }
}