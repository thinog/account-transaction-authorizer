# account-transaction-authorizer
Account transaction authorizer :)

dotnet test -p:CollectCoverage=true -p:CoverletOutputFormat=opencover -p:CoverletOutput='./results/'

dotnet new tool-manifest
dotnet tool install dotnet-reportgenerator-globaltool

dotnet reportgenerator "-reports:OpenCover.xml" "-targetdir:coveragereport" -reporttypes:Html

dotnet build
./src/Presentation/TransactionAuthorizer.Presentation.CLI/bin/Debug/net5.0/TransactionAuthorizer.Presentation.CLI.exe < operations.txt

TODO:
- [ ] Process account creation
- [ ] Process transactions
- [ ] Dependency Injection
- [ ] EF Core In-Memory
- [ ] Transaction / UnitOfWork
- [ ] Unit tests with high code coverage (>= 80%)
- [ ] Integration tests
- [ ] Validate all scenario combinations
- [ ] Dockerize the application
- [ ] Publish/run scripts
- [ ] Dev env script