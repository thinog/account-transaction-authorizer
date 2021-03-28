using System;
using System.Collections.Generic;
using System.Linq;
using TransactionAuthorizer.Domain.Entities;

namespace TransactionAuthorizer.Application.UseCases.AuthorizeTransaction
{
    public static class AuthorizeTransactionValidator
    {
        public static bool Validate(IAuthorizeTransactionOutput output, AuthorizeTransactionInput input, Account account)
        {            
            if(account is null)
            {
                output.AccountNotInitialized();
                return false;
            }

            if(!account.Active)
                output.CardNotActive();

            if((account.Limit - input.Transaction.Amount) < 0)
                output.InsufficientLimit();

            if(account.Transactions is not null)
            {
                if(account.Transactions.Count() >= 3)
                    output.HighFrequencySmallInterval();

                if(account.Transactions.Any(t => t.Merchant == input.Transaction.Merchant && t.Value == input.Transaction.Amount))
                    output.DoubledTransaction();
            }

            return !output.HasErrors;
        }
    }
}