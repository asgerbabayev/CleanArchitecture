namespace CleanArchitecture.Application.Common.Exceptions; 
public class NotFoundException : Exception
{
    public NotFoundException() : base() { }
    public NotFoundException(string message) : base(message: message) { }
    public NotFoundException(string message, Exception innerException) : base(message: message, innerException: innerException) { }
    public NotFoundException(string name, object key) : base(message: $"Entity \"{name}\" ({key}) was not found") { }
}
