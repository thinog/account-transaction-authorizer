namespace TransactionAuthorizer.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        int Save();
    }
}