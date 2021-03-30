# Transaction Authorizer
Projeto de code challenge do Nubank.

## 1. Objetivo
Aplicação console desenvolvida em .NET que gerencia criação de contas e autoriza transações na mesma.

Tem como objetivo cumprir as regras e usos no arquivo [assets/Coding_Challenge_-_Authorizer.pdf]

---

## 2. Pré requisitos
- [Visual Studio Code][vscode]
  - [Extensão Remote Containers][remote-containers]
- [Docker][docker]

O ambiente de desenvolvimento é executado dentro de um remote-container, gerenciado pelo VS Code. Por isso a necessidade dele e da extensão. Caso o VS Code não mostre um pop-up orientando a abrir a aplicação em um remote-container, basta pressionar Ctrl+Shift+p e escrever "remote containers open attached", que será apresentada a opção de abrir o ambiente de desenvolvimento containerizado. **Todos os comandos devem ser executados de dentro desse container.**

---

## 3. Decisões arquiteturais
### 3.1. Camadas projeto
- **Presentation.CLI:** Camada responsável unicamente pelo CLI da aplicação. Recebe o arquivo de entrada, invoca a injeção de dependências e repassa a entrada tratada para a camada de Application;
- **Infrastructure:** Onde ficam centralizados os repositórios de dados. Repositórios implementados em cima do Entity Framework Core, porém utilizando banco In-Memory do mesmo. Essa camada não é referenciada pela de Application, então Application lida apenas com as interfaces do Domain e o DI cuida da injeção dessa camada;
- **Infrastructure.IoC:** Responsável unicamente pela injeção de dependências do projeto, usando o injetor da própria Microsoft. Apartada da camada de Infrastructure principal para remover a necessidade da camada de apresentação ter de conhecer os repositórios;
- **Application:** Camada onde fica toda a lógica de negócio (casos de uso, validadores, models, boundaries). Explicarei mais a baixo sobre a estrutura seguida nessa camada;
- **Domain:** Núcleo da aplicação. Armazena as principais interfaces, enums e entidades do sistema. Não possui nenhuma lógica associada a negócio. Criei as entidades com propriedades diferentes dos modelos da camada de Application apenas para provar o ponto de que o core da aplicação não está sujeito a negócio ou apresentação. Outras camadas devem se adaptar ao core e suas evoluções, não o contrário.

### 3.2. Patterns utilizados
- **Dependency Injection:** Utilizei com o objetivo de diminuir o acoplamento entre camadas, melhorar a testabilidade do código e, principalmente, seguir os conceitos de SOLID. Também ajuda a deixar o código mais limpo, visto que remove a necessidade de instanciar manualmente todas as dependências. Como a aplicação não deve manter estado entre execuções, optei por injetar as dependências dos casos de uso como Transient, para serem reinstanciadas a cada chamada, evitando assim que o output seja "cacheado", e as outras classes como Scoped, sendo instanciadas a cada requisição à aplicação;
- **Repository:** Visei isolar o acesso a dados da camada de Application. Consigo criar repositórios especializados e, na estrutura que montei, posso ter um base genérico, para evitar duplicação desnecessária de código;
- **Unit of Work:** Com o UoW, posso trabalhar tranquilamente com transações compostas de escrita de dados sem precisar me preocupar que sejam feitas pela metade. Faço toda escrita/leitura necessária e concretizo a transação completa com a ajuda desse pattern;
- **Factory Method:** Como a chamada da aplicação vem por um único ponto de entrada e esse ponto de entrada pode receber diversos objetos diferentes (cada qual tendo um set diferente de regras de negócio), optei por gerir a chamada do caso de uso por meio desse pattern. Todos os casos de uso recebem um atributo chamado "HandledObjectAttribute", que possui o tipo de objeto/input que aquele caso de uso processa. Então consigo segregar os objetos por caso de uso, utilizando de código genérico e extensível. Também consigo ignorar objetos que não são tratados pelo sistema;
- **Command Pattern (Use cases):** Ao tentar seguir alguns conceitos de arquitetura hexagonal, optei por desenvolver a camada de negócio (Application) no modelo de casos de uso. Cada caso de uso da camada de negócios é associado com os casos da aplicação mesmo. Então se a aplicação possui dois casos de uso (criar conta e autorizar transação), teremos dois sets de casos de uso. Nesse caso, o caso de uso é o handler do comando, e o objeto de input é o comando própriamente dito.

### 3.3. EF Core In-Memory
Como um dos requisitos era não utilizar fontes de dados externas, optei por utilizar essa estrutura In-Memory. Como ela é uma extensão do EF Core e a implementação dos repositórios do EF Core é abstraída, apenas injeto essa estrutura In-Memory no repositório e os dados são persistidos apenas durante o lifetime da aplicação. Ou seja, uma estrutura volátil que é limpa a cada ciclo de execução.

Outra vantagem de utilizar é que, se em algum momento decidir utilizar alguma fonte de dados externa, posso apenas alterar a injeção de dependências, sem precisar tocar nos meus repositórios (partindo do pressuposto que a fonte de dados externa é suportada pelo EF Core).

### 3.4. Newtonsoft.Json ao invés de System.Text.Json
Tomei essa decisão unicamente pelo System.Text.Json não possuir um validador de schema de JSON em cima de classes. Precisei fazer isso no factory de casos de uso e teria de escrever muito mais código para fazer com ele. Já o Newtonsoft.Json possui um suporte bem ampla a validação de schema. Essa foi a base da decisão.

### 3.5. Clean architecture / Separation of Concerns / Hexagonal Architecture
Tentei seguir os conceitos de arquitetura limpa propostos pelo Uncle Bob, isolando as partes mais centrais da aplicação de implementações mais periféricas. Ou seja, o core e o negócio do meu sistema não dependem de fontes externas, não precisam se preocupar se estão lendo dados de arquivos, APIs ou banco de dados. Quem se preocupa com isso são as camadas mais a cima, que injetam as dependências para essas e se comunicam com elas por meio de boundaries. Isso me ajuda a aplicar a separação de conceitos, modularizando as responsabilidades da minha aplicação. Com base nisso que defini as camadas do projeto, explicadas mais acima. 

Sobre a questão da comunicação por boundaries, aí que entra a arquitetura hexagonal. Um dos conceitos dela é a comunicação por meio de abstrações de entradas e saídas. Com base nisso, consigo tornar minha aplicação muito mais extensível e deixar boa parte da responsabilidade da saída com quem precisará dela. Possuo múltiplas camadas de apresentação e cada uma precisa tratar a saída de forma diferente, porém consumindo os mesmos sets de serviços/casos de uso? Basta apenas que cada camada implemente suas próprias regras de output em cima da abstração que minha aplicação fornece, e pronto. Aplicação desacoplada da apresentação.

### 3.6. Testes unitários
Tentei deixar a cobertura de testes unitários o mais alta possível, tendo como pré-requisito próprio ter uma cobertura mínima de 80%. Para isso, utilizei algumas bibliotecas e ferramentas, visando não só a implementação dos testes, mas uma visualização bem ampla da cobertura, o que me auxiliou a ir suprindo os cenários de testes que precisava cobrir.

Utilizei as seguintes bibliotecas/ferramentas:
- [xUnit][xunit]: É uma biblioteca que fornece toda a base necessária para a implementação dos testes, fornecendo configurações, atributos, asserts, etc., que visam facilitar a implementação de testes;
- [Moq][moq]: Utilizei para criar mock de classes e isolar meus testes de qualquer fonte externa ou de implementações que não acrescentam nada aos testes. Em conjunto com o xUnit, se torna uma ferramenta muito poderosa;
- [Coverlet][coverlet]: Utilizei para gerar os relatórios de cobertura de testes, que não são apresentados nativamente pelo xUnit. Com ele não só imprimo o resumo da cobertura no console, como também gero um relatório detalhado no formato [Open Cover][open-cover], que é consumido pelo Report Generator;
- [Report Generator][report-generator]: Ferramenta que utilizo para gerar uma visualização em HTML dos relatórios Open Cover gerados pelo Coverlet. Com essa visualização consigo ver a cobertura da minha aplicação de linha por linha.

### 3.7. Testes de integração
Como a aplicação possui apenas dois casos de uso e possui uma cobertura alta de testes de unidade, além de ter um único ponto de entrada, optei por criar um script que gera o executável da aplicação e realiza o teste de todos cenários que mapeei, porém a validação é manual. Aumentando os cenários da aplicação e com mais tempo, testes integrados com validação automática são uma decisão mais sábia.

---

## 4. Execução e comandos
Compilar:
```bash
dotnet build
```

Executar testes unitários e visualizar resumo de cobertura:
```bash
dotnet test -p:CollectCoverage=true
```

Executar testes unitários, visualizar resumo de cobertura e gerar relatórios completos (após os comandos, basta abrir o arquivo 'tests/TransactionAuthorizer.UnitTests/results/report/index.html' no navegador)
```bash
dotnet test -p:CollectCoverage=true -p:CoverletOutputFormat=opencover -p:CoverletOutput='./results/'
dotnet reportgenerator "-reports:tests/TransactionAuthorizer.UnitTests/results/coverage.opencover.xml" "-targetdir:tests/TransactionAuthorizer.UnitTests/results/report" -reporttypes:Html
```

Publicar aplicação (após a execução desse comando, o programa "authorize" pode ser chamado de qualquer ponto do SO):
```bash
./publish.sh
```

Executar a aplicação:
```bash
authorize < arquivo_entrada
```

Deixei exemplos de arquivo de entrada já montados na pasta 'examples/'

---

Por fim, resolvi preservar o checklist que criei no início do desafio:
- [x] Process account creation
- [x] Process transactions
- [x] Dependency Injection
- [x] EF Core In-Memory
- [x] Transaction / UnitOfWork
- [x] Unit tests with high code coverage (>= 80%)
- [x] Integration tests
- [x] Validate combined error scenarios
- [x] Dockerize the application
- [x] Publish/run scripts
- [x] Describe architectural decisions in README file


[vscode]: https://code.visualstudio.com/download
[remote-containers]: https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers
[dotnet-sdk]: https://dotnet.microsoft.com/download/dotnet/5.0
[docker]: https://docs.docker.com/get-docker/
[xunit]: https://xunit.net
[moq]: https://github.com/moq/moq4
[coverlet]: https://github.com/coverlet-coverage/coverlet
[open-cover]: https://github.com/OpenCover/opencover
[report-generator]: https://github.com/danielpalme/ReportGenerator
[ef-core-in-memory]: https://docs.microsoft.com/en-us/ef/core/providers/in-memory/
[newtonsoft]: https://www.newtonsoft.com/json
[net-json]: https://docs.microsoft.com/en-us/dotnet/api/system.text.json?view=net-5.0