namespace Authorizer.Domain.Interfaces.Services
{
    public interface ITransactionService
    {
        object Execute(string command);
    }
}
