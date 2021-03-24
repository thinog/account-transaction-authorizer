using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Domain.Interfaces.UseCases;

namespace TransactionAuthorizer.Application.UseCases.AuthorizeTransaction
{
    public interface IAuthorizeTransactionOutput : IOutputPort
    {
        AccountModel Account { get; set; }

        void Ok(AccountDetailsModel accountDetails);
        void AccountNotInitialized();
        void CardNotActive();
        void InsufficientLimit();
        void HighFrequencySmallInterval();
        void DoubledTransaction();
    }
}