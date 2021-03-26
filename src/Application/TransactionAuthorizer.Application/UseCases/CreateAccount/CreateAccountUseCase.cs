using TransactionAuthorizer.Domain.Attributes;
using TransactionAuthorizer.Domain.Interfaces.UseCases;
using Newtonsoft.Json;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using TransactionAuthorizer.Application.Models;

namespace TransactionAuthorizer.Application.UseCases.CreateAccount
{
    [HandledObject(typeof(CreateAccountInput))]
    public class CreateAccountUseCase : IUseCase
    {
        private ICreateAccountOutput _outputPort;
        private IAccountRepository _accountRepository;

        public CreateAccountUseCase(IAccountRepository accountRepository)
        {
            _outputPort = new CreateAccountDefaultOutput();
            _accountRepository = accountRepository;
        }

        public void SetOutputPort(IOutputPort output)
        {
            _outputPort = (ICreateAccountOutput)output;
        }

        public void Execute(IInputPort input)
        {
            var inputPort = (CreateAccountInput)input;

            var account = _accountRepository.GetAccount();

            if(account is not null)
            {
                _outputPort.Ok(new AccountDetailsModel(account));
                _outputPort.AccountAlreadyInitialized();
                return;
            }

            _accountRepository.Insert(inputPort.ToAccountEntity());

            _outputPort.Ok(inputPort.Account);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(_outputPort.Account);
        }
    }
}