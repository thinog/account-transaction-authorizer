using TransactionAuthorizer.Domain.Attributes;
using TransactionAuthorizer.Domain.Interfaces.UseCases;
using Newtonsoft.Json;

namespace TransactionAuthorizer.Application.UseCases.AuthorizeTransaction
{
    [HandledObject(typeof(AuthorizeTransactionInput))]
    public class CreateAccountUseCase : IUseCase
    {
        IAuthorizeTransactionOutput _outputPort;

        public CreateAccountUseCase()
        {
            _outputPort = new AuthorizeTransactionDefaultOutput();
        }

        public void SetOutputPort(IOutputPort output)
        {
            _outputPort = (IAuthorizeTransactionOutput)output;
        }

        public void Execute(IInputPort input)
        {
            var inputPort = (AuthorizeTransactionInput)input;

            _outputPort.Ok(new Models.AccountDetailsModel{ ActiveCard = true, AvailableLimit = 123 });
            _outputPort.DoubledTransaction();
            _outputPort.HighFrequencySmallInterval();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(_outputPort.Account);
        }
    }
}