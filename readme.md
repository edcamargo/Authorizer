# Exemplo de uso do Autorizador

### Para esse projeto usei principios de Solid e Clean Code, o foco em ter um código mais limpo e reutilizável.

### Como o programa deve funcionar?
Seu programa receberá como entrada linhas em formato json na entrada padrão ( stdin ) e
deve fornecer uma saída em formato json para cada uma das entradas - imagine isso como um
fluxo de eventos chegando ao autorizador.

### Como o programa deve ser executado?
Dado um arquivo chamado operations que contém diversas linhas descrevendo operações no
formato json.

## Procedimentos

  * Realizar a descompactação dos fontes.
  * Entrar no diretório do projeto.

### `dotnet test` 

  - Execução dos tests
  - Entrar em Authorizer\test\Authorizer.Unit.Test rodar o comando `dotnet test`

### `dotnet run` 

  - Execução do programa
  - Entrar em Authorizer\src\Authorizer.Application.Worker rodar `dotnet run`