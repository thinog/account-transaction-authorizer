using System.Collections.Generic;

namespace TransactionAuthorizer.Domain.Utils
{
    public static class Extensions
    {
        public static void AddIfNotExists(this List<string> list, string value)
        {
            if(list is not null && !list.Contains(value))
                list.Add(value);
        }
    }
}