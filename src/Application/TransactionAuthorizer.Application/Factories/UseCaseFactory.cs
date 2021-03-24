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
        #pragma warning disable 0618 // disables warning of "obsolete method"
        public static (IUseCase UseCase, IInputPort InputPort) CreateUseCase(string json)
        {
            var useCases = GetAllUseCases();

            foreach(var useCase in useCases)
            {
                var handledObject = (HandledObjectAttribute)useCase.GetCustomAttributes(typeof(HandledObjectAttribute), false).FirstOrDefault();

                if(handledObject is not null)
                {
                    var jSchema = new JsonSchemaGenerator().Generate(handledObject.GetType());
                    var jObject = JObject.Parse(json);

                    bool valid = jObject.IsValid(jSchema);

                    if(valid)
                    {
                        var inputPort = JsonConvert.DeserializeObject(json, handledObject.GetType());
                        return ((IUseCase)Activator.CreateInstance(useCase), (IInputPort)inputPort);
                    }
                }
            }

            return (null, null);
        }
        #pragma warning restore 0618 // enables warning of "obsolete method"

        private static IEnumerable<Type> GetAllUseCases()
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