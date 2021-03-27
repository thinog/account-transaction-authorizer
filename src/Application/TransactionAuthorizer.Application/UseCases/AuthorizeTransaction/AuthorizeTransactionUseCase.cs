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
        private IUnitOfWork _unitOfWork;
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;

        public AuthorizeTransactionUseCase(
            IUnitOfWork unitOfWork,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _outputPort = new AuthorizeTransactionDefaultOutput();
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public void SetOutputPort(IOutputPort output)
        {
            _outputPort = (IAuthorizeTransactionOutput)output;
        }

        public IOutputPort Execute(IInputPort input)
        {
            var inputPort = (AuthorizeTransactionInput)input;
            var account = _accountRepository.GetAccount();
            
            if(account is not null)
                account.Transactions = _transactionRepository.GetLastTransactionsByTime(2);

            bool valid = AuthorizeTransactionValidator.Validate(_outputPort, inputPort, account);

            if(valid)
            {
                account.Limit -= inputPort.Transaction.Amount;
                
                _transactionRepository.Insert(inputPort.ToTransactionEntity());
                _accountRepository.Update(account);
                _unitOfWork.Save();
            }                        

            _outputPort.Ok(new AccountDetailsModel(account));

            return _outputPort;
        }
    }
}