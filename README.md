# account-transaction-authorizer
Account transaction authorizer :)

# Decisões arquiteturais
## Camadas projeto
- Presentation: 
- Infrastructure: 
- Infrastructure.IoC: 
- Application: 
- Domain: 

## Patterns utilizados
- Dependency Injection: 
- Repository: 
- Unit of Work: 
- Factory Method: 
- Command Pattern (Use cases): 

## EF Core In-Memory

## Testes unitários
- xunit: 
- moq: 
- coverlet: 
- reportgenerator: 

## Testes de integração

# Comandos
dotnet test -p:CollectCoverage=true -p:CoverletOutputFormat=opencover -p:CoverletOutput='./results/'

dotnet new tool-manifest

dotnet tool install dotnet-reportgenerator-globaltool

dotnet reportgenerator "-reports:OpenCover.xml" "-targetdir:coveragereport" -reporttypes:Html


# Executando o projeto
dotnet build
./src/Presentation/TransactionAuthorizer.Presentation.CLI/bin/Debug/net5.0/TransactionAuthorizer.Presentation.CLI.exe < operations.txt

TODO:
- [x] Process account creation
- [x] Process transactions
- [x] Dependency Injection
- [x] EF Core In-Memory
- [x] Transaction / UnitOfWork
- [x] Unit tests with high code coverage (>= 80%)
- [ ] Integration tests
- [ ] Validate all scenario combinations
- [ ] Dockerize the application
- [ ] Publish/run scripts
- [ ] Dev env script