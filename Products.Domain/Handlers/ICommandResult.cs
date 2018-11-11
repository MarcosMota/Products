namespace Products.Domain.Handlers
{
    public interface ICommandResult
    {
        bool Success { get; }
        string Message { get; }
        object Data { get; }
    }
}