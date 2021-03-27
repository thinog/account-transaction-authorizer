using TransactionAuthorizer.Domain.Attributes;
using TransactionAuthorizer.Domain.Interfaces.UseCases;
using TransactionAuthorizer.Domain.Interfaces.Repositories;
using TransactionAuthorizer.Application.Models;

namespace TransactionAuthorizer.Application.UseCases.CreateAccount
{
    [HandledObject(typeof(CreateAccountInput))]
    public class CreateAccountUseCase : IUseCase
    {
        private ICreateAccountOutput _outputPort;
        private IUnitOfWork _unitOfWork;
        private IAccountRepository _accountRepository;        

        public CreateAccountUseCase(
            IUnitOfWork unitOfWork,
            IAccountRepository accountRepository)
        {
            _outputPort = new CreateAccountDefaultOutput();
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public void SetOutputPort(IOutputPort output)
        {
            _outputPort = (ICreateAccountOutput)output;
        }

        public IOutputPort Execute(IInputPort input)
        {
            var inputPort = (CreateAccountInput)input;
            var account = _accountRepository.GetAccount();

            bool valid = CreateAccountValidator.Validate(_outputPort, account);

            if(!valid)
            {
                _outputPort.Fill(new AccountDetailsModel(account));
            }
            else
            {
                _accountRepository.Insert(inputPort.ToAccountEntity());
                _unitOfWork.Save();
                _outputPort.Fill(inputPort.Account);
            }
            
            return _outputPort;
        }
    }
}