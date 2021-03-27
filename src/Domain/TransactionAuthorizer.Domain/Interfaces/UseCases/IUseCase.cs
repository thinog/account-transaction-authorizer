namespace TransactionAuthorizer.Domain.Interfaces.UseCases
{
    public interface IUseCase
    {
        void SetOutputPort(IOutputPort output);
        IOutputPort Execute(IInputPort input);
    }
}