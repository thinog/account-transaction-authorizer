using TransactionAuthorizer.Domain.Attributes;
using TransactionAuthorizer.Domain.Interfaces.UseCases;
using Newtonsoft.Json;

namespace TransactionAuthorizer.Application.UseCases.CreateAccount
{
    [HandledObject(typeof(CreateAccountInput))]
    public class CreateAccountUseCase : IUseCase
    {
        public ICreateAccountOutput _outputPort { get; set; }

        public CreateAccountUseCase()
        {
            _outputPort = new CreateAccountDefaultOutput();
        }

        public void SetOutputPort(IOutputPort output)
        {
            _outputPort = (ICreateAccountOutput)output;
        }

        public void Execute(IInputPort input)
        {
            var inputPort = (CreateAccountInput)input;

            _outputPort.Ok(new Models.AccountDetailsModel{ ActiveCard = true, AvailableLimit = 123 });
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(_outputPort.Account);
        }
    }
}