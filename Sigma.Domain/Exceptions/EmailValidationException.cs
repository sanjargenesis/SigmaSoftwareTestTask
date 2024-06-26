namespace Sigma.Domain.Exceptions;

public class EmailValidationException : Exception
{
    public EmailValidationException(string message) : base(message)
    {
    }
}
