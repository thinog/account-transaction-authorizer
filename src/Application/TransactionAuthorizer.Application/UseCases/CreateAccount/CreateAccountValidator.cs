using TransactionAuthorizer.Domain.Entities;

namespace TransactionAuthorizer.Application.UseCases.CreateAccount
{
    public static class CreateAccountValidator
    {
        public static bool Validate(ICreateAccountOutput output, Account account)
        {            
            if(account is not null)
                output.AccountAlreadyInitialized();

            return !output.HasErrors;
        }
    }
}