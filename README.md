# Projeto Orders & Customers

Documentação do projeto desenvolvido com front-end, back-end, worker e serviços integrados.

## 🛠 Tecnologias Utilizadas

- **Back-end:** .NET 8 (ASP.NET Core)
- **Front-end:** React 18
- **Banco de dados:** PostgreSQL
- **Mensageria:** RabbitMQ
- **Autenticação:** OAuth2 (Auth0)
- **CI/CD:** GitHub Actions
- **Testes:** xUnit / Integration Tests
- **Containerização:** Docker / Docker Compose

## 🚀 Como Executar
### Pré-requisitos

- Docker & Docker Compose
- Node.js (para rodar o front-end)

### Passos

git clone: https://github.com/Gabriellanza/OrdersCustomers.git

cd SEU-REPO

docker compose up --build

Front-end disponível em: http://localhost:3000


🚀 Informações Adicionais
📚 Coleção Postman

Coleção disponível na pasta do repositório, contendo todos os endpoints da API com exemplos completos de requisições e respostas.

🔑 Autenticação
Autenticação implementada via OAuth2 com Auth0.
Todos os endpoints da API exigem token válido.
O front-end já está configurado para realizar login automático utilizando o Auth0.

💾 Banco de Dados
O banco de dados é provisionado e mantido automaticamente via migrations, responsáveis pela criação de:
Tabelas
Índices
Procedures armazenadas
A procedure principal de processamento de ordens é executada de forma assíncrona pelo Worker, através do consumo de eventos do RabbitMQ.

⚙ Worker e Processamento Assíncrono
O Worker atua como um consumer do RabbitMQ, sendo responsável por:
Consumir eventos de criação/atualização de ordens
Executar a stored procedure no banco de dados
Esse fluxo torna a aplicação event-driven, com comunicação totalmente orientada a eventos.

🛠 CI/CD
Pipeline configurado via GitHub Actions, com as seguintes etapas:
Build da API
Execução de testes automatizados
Lint do front-end
Lint do back-end

⚙ Observações Técnicas
Front-end disponível na porta 3000
API disponível na porta 5000
Worker consome eventos automaticamente via RabbitMQ
Banco de dados criado e atualizado automaticamente por migrations
Fluxo de ordens totalmente event-driven
Autenticação segura utilizando OAuth2

