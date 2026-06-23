namespace api_doceria.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}