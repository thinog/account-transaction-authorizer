using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using TransactionAuthorizer.Domain.Attributes;
using TransactionAuthorizer.Domain.Interfaces.UseCases;

namespace TransactionAuthorizer.Application.Factories
{
    public static class UseCaseFactory
    {
        #pragma warning disable 0618 // disable "obsolete method" warning caused by JsonSchemaGenerator
        public static IUseCase CreateUseCase(string json, IServiceProvider serviceProvider)
        {
            try
            {
                var useCaseTypes = GetAllUseCaseTypes();

                foreach(var useCaseType in useCaseTypes)
                {
                    var handledObject = GetHandledObjectAttribute(useCaseType);

                    if(handledObject is not null)
                    {
                        var jSchema = new JsonSchemaGenerator().Generate(handledObject.ObjectType);
                        var jObject = JObject.Parse(json);

                        bool valid = jObject.IsValid(jSchema);

                        if(valid) 
                            return (IUseCase)serviceProvider.GetService(useCaseType);
                    }
                }

                return null;
            }
            catch (JsonReaderException)
            {
                throw new JsonReaderException("Invalid JSON format!");
            }
        }
        #pragma warning restore 0618 // restore "obsolete method" warning caused by JsonSchemaGenerator

        public static IInputPort CreateInputPort(string inputJson, IUseCase useCase)
        {
            try
            {
                var handledObject = GetHandledObjectAttribute(useCase.GetType());
                return (IInputPort)JsonConvert.DeserializeObject(inputJson, handledObject.ObjectType);
            }
            catch (JsonReaderException)
            {
                throw new JsonReaderException("Invalid JSON format!");
            }
        }

        private static HandledObjectAttribute GetHandledObjectAttribute(Type type)
        {
            return (HandledObjectAttribute)type.GetCustomAttributes(typeof(HandledObjectAttribute), false).FirstOrDefault();
        }

        private static IEnumerable<Type> GetAllUseCaseTypes()
        {
            var useCaseInterface = typeof(IUseCase);

            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsAssignableTo(useCaseInterface) && t.IsClass);
        }
    }
}