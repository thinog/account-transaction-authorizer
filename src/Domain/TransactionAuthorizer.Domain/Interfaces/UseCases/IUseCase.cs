namespace TransactionAuthorizer.Domain.Interfaces.UseCases
{
    public interface IUseCase
    {
        void SetOutputPort(IOutputPort output);
        void Execute(IInputPort input);
    }
}