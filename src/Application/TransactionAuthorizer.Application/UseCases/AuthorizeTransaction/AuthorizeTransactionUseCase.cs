using TransactionAuthorizer.Domain.Attributes;
using TransactionAuthorizer.Domain.Interfaces.UseCases;
using Newtonsoft.Json;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using TransactionAuthorizer.Application.Models;
using System.Linq;

namespace TransactionAuthorizer.Application.UseCases.AuthorizeTransaction
{
    [HandledObject(typeof(AuthorizeTransactionInput))]
    public class AuthorizeTransactionUseCase : IUseCase
    {
        private IAuthorizeTransactionOutput _outputPort;
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;

        public AuthorizeTransactionUseCase(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _outputPort = new AuthorizeTransactionDefaultOutput();
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public void SetOutputPort(IOutputPort output)
        {
            _outputPort = (IAuthorizeTransactionOutput)output;
        }

        public void Execute(IInputPort input)
        {
            var inputPort = (AuthorizeTransactionInput)input;

            var account = _accountRepository.GetAccount();

            if(account is null)
                _outputPort.AccountNotInitialized();

            if(!account.Active)
                _outputPort.CardNotActive();

            if((account.Limit - inputPort.Transaction.Amount) < 0)
                _outputPort.InsufficientLimit();

            var transactions = _transactionRepository.GetTransactionsByTime(2);
            
            if(transactions is not null)
            {
                if(transactions.Count() >= 3)
                    _outputPort.HighFrequencySmallInterval();

                if(transactions.GroupBy(t => new { t.Merchant, t.Value }).Any(tg => tg.Count() >= 2))
                    _outputPort.DoubledTransaction();
            }

            if(!_outputPort.HasErrors)
            {
                account.Limit -= inputPort.Transaction.Amount;
                
                _transactionRepository.Insert(inputPort.ToTransactionEntity());
                _accountRepository.Update(account);
            }                        

            _outputPort.Ok(new AccountDetailsModel(account));
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(_outputPort.Account);
        }
    }
}