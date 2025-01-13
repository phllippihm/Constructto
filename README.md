# 1. Descrição do Projeto

Este projeto foi desenvolvido para solucionar os desafios enfrentados por uma empresa do setor de construção na
gestão de escalas de funcionários alocados para diferentes obras. Antes da implementação, havia dificuldades na
organização, com casos recorrentes de sobreposição de horários de funcionários em múltiplas obras.

Objetivo principal:
Fornecer uma solução eficiente e intuitiva para evitar conflitos de escala, melhorar a alocação de recursos humanos
e otimizar o planejamento de tarefas.


# 2. Estrutura do Código

Backend:
Gerencia toda a lógica de negócios, incluindo o cadastro, edição, exclusão e consulta de dados relacionados a obras,
funcionários, estoque e escalas. Ele também controla as autenticações, conexões com o banco de dados e rotas.

Tecnologias Utilizadas no Backend:
- Linguagem: C#
- Framework: ASP.NET Core 6.0
- ORM: Entity Framework Core
- Autenticação: ASP.NET Identity para gerenciamento de usuários e controle de acesso.


Frontend:
Interface amigável desenvolvida com ASP.NET Razor Pages. Possui integração com bibliotecas externas (como Bootstrap
e jQuery) para melhor responsividade e interatividade.

Banco de Dados:
Estrutura relacional para armazenar informações sobre funcionários, obras, estoque e escalas, garantindo consistência


# 3. Tecnologias Utilizadas

- Linguagem de Programação: C#
- Frameworks: .NET 6 e ASP.NET Core
- Frontend: Razor Pages com Bootstrap e jQuery
- Banco de Dados: SQL Server
- Ferramentas Adicionais:
  - Toastr para notificações dinâmicas.
  - AJAX para atualizações em tempo real.


# 4. Configuração do Ambiente

# Requisitos:

- .NET 6 SDK instalado (disponível em https://dotnet.microsoft.com/).
- SQL Server configurado.
- Ambiente de desenvolvimento com suporte a C# (como Visual Studio ou Visual Studio Code).


# Passos para Configuração:

1. Clone o repositório:
    git clone https://github.com/phllippihm/Constructto.git    

2. Atualize as configurações do banco de dados:
   Abra o arquivo `appsettings.json` e insira a string de conexão correta para o seu banco SQL.

3. Restaure as dependências:
    dotnet restore

4. Prepare o banco de dados:
    dotnet ef database update

5. Execute o projeto:
    dotnet run


# 5. Funcionalidades Principais

Gerenciamento de Funcionários:
- Cadastro, edição, exclusão e listagem de funcionários.
- Validações robustas no backend e frontend.
- Pesquisa dinâmica por nome, CPF e outros critérios.

Controle de Estoque:
- Gerenciamento de materiais e equipamentos com preços e quantidades.
- Funcionalidade para duplicar registros existentes e editá-los.

Cadastro de Obras:
- Registro e consulta de obras.
- Associação de funcionários a uma obra específica.

Gestão de Escalas:
- Alocação intuitiva de funcionários para obras sem conflitos de horários.
- Atualizações dinâmicas em tempo real com o uso de AJAX.


# 6. Contribuição

Contribuições são bem-vindas! Por favor, envie um pull request com a descrição da sua alteração ou melhoria.

Guia de Estilo:
- Utilize padrões de código consistentes em C# e JavaScript.
- Padronize commits com mensagens claras e objetivas.




# Contato

Se você tiver alguma dúvida ou sugestão, entre em contato com philippihargermartins@gmail.com.

