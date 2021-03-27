using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Domain.Interfaces.UseCases;

namespace TransactionAuthorizer.Application.UseCases.CreateAccount
{
    public interface ICreateAccountOutput : IOutputPort
    {        
        AccountModel Account { get; set; }

        void Fill(AccountDetailsModel accountDetails);
        void AccountAlreadyInitialized();
    }
}