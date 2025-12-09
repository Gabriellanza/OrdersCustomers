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

git clone https://github.com/Gabriellanza/OrdersCustomers.git
cd SEU-REPO
docker compose up --build
Front-end disponível em: http://localhost:3000



🔑 Autenticação
Implementada via OAuth2 / Auth0
Todos os endpoints da API requerem token válido.
Front-end já configurado para login automático via Auth0.

💾 Banco de Dados
Migrations automáticas criam tabelas, índices e procedure.
Scripts DDL e seeds disponíveis em database/.
Procedure é executada pelo worker ao processar eventos de ordem.

🛠 CI/CD
Pipeline de GitHub Actions:
Build da API
Execução de testes
Lint do front-end e back-end


⚙ Observações
Front-end na porta 3000, API na porta 5000.
Worker consome eventos automaticamente do RabbitMQ.
Banco é criado e atualizado automaticamente via migrations.
Fluxo de ordem é event-driven.
Autenticação segura via OAuth2.


