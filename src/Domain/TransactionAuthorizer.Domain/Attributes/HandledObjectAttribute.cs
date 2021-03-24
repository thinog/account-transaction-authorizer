using System;

namespace TransactionAuthorizer.Domain.Attributes
{
    public class HandledObjectAttribute : Attribute
    {
        public readonly Type ObjectType;

        public HandledObjectAttribute(Type type)
        {
            ObjectType = type;
        }
    }
}