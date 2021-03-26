namespace TransactionAuthorizer.Domain.Interfaces.UseCases
{
    public interface IOutputPort 
    { 
        bool HasErrors { get; }
    }
}