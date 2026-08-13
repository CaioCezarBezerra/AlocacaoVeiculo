# Sistema de Alocação de Veículos e Assistência

Projeto desenvolvido como desafio técnico Full Stack para gerenciamento de veículos, grupos de veículos, empresas de assistência, planos de assistência e associação entre veículos e planos.

O sistema é composto por:

- Backend desenvolvido em ASP.NET Core / .NET 8
- Frontend desenvolvido em Angular
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- Docker / Docker Compose
- Git / GitHub

---

## Objetivo

O objetivo do projeto é disponibilizar uma aplicação para gerenciamento das seguintes entidades:

- Grupos de Veículos
- Veículos
- Empresas de Assistência
- Planos de Assistência
- Associação entre Veículos e Planos de Assistência

A aplicação possui uma API REST responsável pelas regras de negócio e persistência dos dados, além de um frontend Angular para interação com o usuário.

---

# Tecnologias utilizadas

## Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- Data Annotations
- Dependency Injection
- Repository Pattern
- Service Layer
- DTOs

## Frontend

- Angular
- TypeScript
- SCSS
- Angular Router

## Infraestrutura

- Docker
- Docker Compose
- Git
- GitHub

---

# Arquitetura do Backend

O backend foi organizado seguindo separação de responsabilidades.

Fluxo principal:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server
```

## Controllers

Responsáveis por:

- Receber requisições HTTP
- Validar entrada da API
- Chamar a camada de Service
- Retornar códigos HTTP adequados

## Services

Responsáveis por:

- Regras de negócio
- Validações
- Conversão entre DTOs e entidades
- Comunicação com os repositories

## Repositories

Responsáveis por:

- Acesso ao banco
- Consultas com Entity Framework Core
- Persistência das entidades
- Atualização e exclusão dos registros

## DTOs

Os endpoints não expõem diretamente as entidades de domínio.

São utilizados DTOs específicos para:

- Criação
- Atualização
- Resposta da API

---

# Estrutura do projeto

```text
Sistema de Alocação de Veículos e Assistência/
│
├── AlocacaoVeiculosAssistencia/
│   │
│   ├── AlocacaoVeiculosAssistencia.slnx
│   │
│   └── AlocacaoVeiculosAssistencia/
│       │
│       ├── Application/
│       │   ├── DTOs/
│       │   ├── Interfaces/
│       │   │   ├── Repository/
│       │   │   └── Services/
│       │   └── Services/
│       │
│       ├── Controllers/
│       │
│       ├── Data/
│       │   ├── Configurations/
│       │   ├── Repository/
│       │   ├── AppDbContext.cs
│       │   └── AppDbContextFactory.cs
│       │
│       ├── Domain/
│       │   └── Entities/
│       │
│       ├── Migrations/
│       │
│       ├── Program.cs
│       ├── appsettings.json
│       └── docker-compose.yml
│
├── front-AlocacaoVeiculoAssistencia/
│   └── AlocacaoVeiculo/
│       ├── src/
│       ├── public/
│       ├── angular.json
│       ├── package.json
│       └── package-lock.json
│
├── .gitignore
└── README.md
```

---

# Modelo de dados

O sistema utiliza as seguintes entidades.

## GruposVeiculos

Representa os grupos aos quais os veículos pertencem.

Campos principais:

```text
Id
Nome
Descricao
```

Exemplos:

```text
SUV
Sedan
Hatch
```

---

## Veiculos

Representa os veículos cadastrados.

Campos principais:

```text
Id
Modelo
Placa
GrupoId
```

Relacionamento:

```text
Veiculos.GrupoId
        ↓
GruposVeiculos.Id
```

---

## EmpresasAssistencia

Representa as empresas responsáveis pelos planos de assistência.

Campos principais:

```text
Id
Nome
Endereco
```

---

## PlanosAssistencia

Representa os planos disponibilizados pelas empresas de assistência.

Campos utilizados no projeto:

```text
Id
Plano
Descricao
ValorCobertura
EmpresaId
```

Relacionamento:

```text
PlanosAssistencia.EmpresaId
            ↓
EmpresasAssistencia.Id
```

---

## VeiculosAssistencia

Representa a associação entre um veículo e um plano.

Campos principais:

```text
Id
VeiculoId
PlanoId
```

Relacionamentos:

```text
VeiculoId
    ↓
Veiculos.Id
```

e:

```text
PlanoId
   ↓
PlanosAssistencia.Id
```

---

# Relacionamentos

Visão simplificada:

```text
GruposVeiculos
      │
      │
      └──── Veiculos
               │
               │
               └──── VeiculosAssistencia
                           │
                           │
                           └──── PlanosAssistencia
                                      │
                                      │
                                      └──── EmpresasAssistencia
```

---

# Banco de dados

O projeto utiliza:

```text
SQL Server
```

Nome utilizado atualmente:

```text
AlocacaoVeiculos
```

Porta utilizada pelo SQL Server:

```text
1433
```

As migrations do Entity Framework Core são versionadas no repositório.

---

# Configuração da conexão com o banco

Por segurança, a connection string não deve ficar fixa no código-fonte ou com credenciais reais no repositório.

A aplicação utiliza a variável de ambiente:

```text
ConnectionStrings__DefaultConnection
```

## Exemplo para execução local

```text
Server=localhost,1433;Database=AlocacaoVeiculos;User Id=sa;Password=<SENHA>;TrustServerCertificate=True;
```

## Exemplo para execução dentro do Docker

```text
Server=sqlserver,1433;Database=AlocacaoVeiculos;User Id=sa;Password=<SENHA>;TrustServerCertificate=True;
```

> Nunca substituir `<SENHA>` por uma senha real neste README ou em arquivos versionados no Git.

O desafio exige que a connection string seja fornecida por variável de ambiente e não permaneça fixa no `appsettings.json`.

---

# Executando o Backend

## Pré-requisitos para desenvolvimento local

É necessário possuir:

- .NET 8 SDK
- SQL Server ou SQL Server via Docker
- Git

Entre na pasta do backend:

```bash
cd AlocacaoVeiculosAssistencia/AlocacaoVeiculosAssistencia
```

Restaure os pacotes:

```bash
dotnet restore
```

Execute:

```bash
dotnet run
```

---

# Swagger

Com a API executando no ambiente atual de desenvolvimento, o Swagger pode ser acessado em:

```text
https://localhost:7005/swagger
```

A porta poderá variar conforme a configuração do ambiente.

---

# Endpoints da API

## Grupos de Veículos

```text
GET    /api/GruposVeiculos
GET    /api/GruposVeiculos/{id}
POST   /api/GruposVeiculos
PUT    /api/GruposVeiculos/{id}
DELETE /api/GruposVeiculos/{id}
```

---

## Veículos

```text
GET    /api/Veiculos
GET    /api/Veiculos/{id}
POST   /api/Veiculos
PUT    /api/Veiculos/{id}
DELETE /api/Veiculos/{id}
```

---

## Empresas de Assistência

```text
GET    /api/EmpresasAssistencia
GET    /api/EmpresasAssistencia/{id}
POST   /api/EmpresasAssistencia
PUT    /api/EmpresasAssistencia/{id}
DELETE /api/EmpresasAssistencia/{id}
```

---

## Planos de Assistência

```text
GET    /api/PlanosAssistencias
GET    /api/PlanosAssistencias/{id}
POST   /api/PlanosAssistencias
PUT    /api/PlanosAssistencias/{id}
DELETE /api/PlanosAssistencias/{id}
```

---

## Associação Veículo / Plano

```text
GET    /api/VeiculosAssistencias
GET    /api/VeiculosAssistencias/{id}
POST   /api/VeiculosAssistencias
PUT    /api/VeiculosAssistencias/{id}
DELETE /api/VeiculosAssistencias/{id}
```

---

# Exemplos de utilização da API

## Criar Grupo

Exemplo:

```json
{
  "nome": "SUV",
  "descricao": "Veículos utilitários esportivos"
}
```

---

## Criar Veículo

```json
{
  "modelo": "Fiat Pulse",
  "placa": "ABC1D23",
  "grupoId": 1
}
```

Exemplo de resposta:

```json
{
  "id": 1,
  "modelo": "Fiat Pulse",
  "placa": "ABC1D23",
  "grupoId": 1,
  "grupoNome": "SUV"
}
```

---

## Criar Empresa de Assistência

```json
{
  "nome": "Empresa Exemplo",
  "endereco": "São Paulo - SP"
}
```

---

## Criar Plano

```json
{
  "plano": "Gold",
  "descricao": "Plano Gold",
  "valorCobertura": 100,
  "empresaId": 2
}
```

Exemplo de resposta:

```json
{
  "id": 3,
  "plano": "Gold",
  "descricao": "Plano Gold",
  "valorCobertura": 100,
  "empresaId": 2,
  "empresaNome": "Empresa Exemplo"
}
```

---

## Vincular Veículo a Plano

```json
{
  "veiculoId": 1,
  "planoId": 3
}
```

Exemplo de resposta:

```json
{
  "id": 1,
  "veiculoId": 1,
  "veiculo": "Fiat Pulse",
  "planoId": 3,
  "plano": "Gold"
}
```

---

# Códigos HTTP utilizados

A API foi planejada para utilizar os seguintes códigos:

```text
200 OK
```

Consulta ou atualização realizada com sucesso.

```text
201 Created
```

Recurso criado com sucesso.

```text
204 No Content
```

Exclusão realizada com sucesso.

```text
400 Bad Request
```

Dados enviados são inválidos.

```text
404 Not Found
```

Recurso solicitado não existe.

```text
409 Conflict
```

Conflito de unicidade ou regra de negócio.

---

# Regras de negócio

## Placa do veículo

A placa deve ser única no sistema.

Formatos previstos pelo desafio:

### Formato antigo

```text
AAA1234
```

### Mercosul

```text
AAA1A23
```

A validação completa dessa regra ainda está em desenvolvimento.

---

## Associação entre Veículo e Plano

O mesmo par:

```text
VeiculoId + PlanoId
```

não deve ser cadastrado mais de uma vez.

Exemplo inválido:

```text
VeiculoId = 4
PlanoId = 3
```

caso essa mesma associação já exista.

O comportamento final deverá retornar:

```text
409 Conflict
```

e possuir também constraint de unicidade no banco.

---

# Exclusões e dependências

Foi escolhida a estratégia de **bloquear a exclusão quando existirem registros dependentes**, em vez de realizar exclusão em cascata indiscriminadamente.

Exemplo:

```text
Grupo de Veículos
       │
       └── possui Veículos
```

Enquanto existirem veículos relacionados ao grupo, sua exclusão deverá ser bloqueada.

A decisão busca:

- Preservar integridade referencial
- Evitar perda acidental de informações
- Tornar a regra de negócio explícita para o usuário

O tratamento completo das mensagens de conflito ainda está sendo finalizado.

---

# Frontend

O frontend está sendo desenvolvido utilizando Angular.

Pasta:

```text
front-AlocacaoVeiculoAssistencia/AlocacaoVeiculo
```

---

## Executando o frontend

Entre na pasta:

```bash
cd front-AlocacaoVeiculoAssistencia/AlocacaoVeiculo
```

Instale as dependências:

```bash
npm install
```

Execute:

```bash
npm start
```

ou:

```bash
ng serve
```

O frontend fica disponível normalmente em:

```text
http://localhost:4200
```

---

# Estrutura planejada do frontend

```text
src/app/
│
├── core/
│   ├── services/
│   └── interceptors/
│
├── layout/
│   └── menu-lateral/
│
├── pages/
│   ├── dashboard/
│   ├── veiculos/
│   ├── grupos/
│   ├── empresas/
│   ├── planos/
│   └── veiculos-assistencia/
│
├── app.routes.ts
├── app.ts
├── app.html
└── app.scss
```

---

# Telas previstas

O frontend deverá possuir telas para:

- Dashboard
- Grupos de Veículos
- Veículos
- Empresas de Assistência
- Planos de Assistência
- Associação entre Veículos e Planos

Também estão previstos:

- Cadastro
- Edição
- Exclusão
- Listagem
- Filtros
- Mensagens de erro
- Confirmação antes de exclusões
- Loading
- Tratamento de listas vazias

---

# Docker

O objetivo final da configuração Docker é permitir subir:

```text
SQL Server
+
API .NET
+
Frontend Angular
```

através de um único comando:

```bash
docker compose up --build
```

O desafio exige que o ambiente completo possa ser iniciado através de um único `docker compose up`, sem passos manuais adicionais.

No estágio atual, a configuração completa de Docker para os três serviços ainda está em desenvolvimento.

---

# Portas

Portas utilizadas durante o desenvolvimento:

| Serviço | Porta |
|---|---:|
| Frontend Angular | 4200 |
| API .NET | 7005 |
| SQL Server | 1433 |

A configuração final deverá refletir exatamente as portas definidas no `docker-compose.yml`.

---

# Migrations

O projeto utiliza migrations do Entity Framework Core.

As migrations encontram-se na pasta:

```text
AlocacaoVeiculosAssistencia/AlocacaoVeiculosAssistencia/Migrations
```

Para aplicar manualmente:

```bash
dotnet ef database update
```

No ambiente Docker final, as migrations deverão ser executadas automaticamente durante a inicialização.

O script deverá:

1. Aguardar o SQL Server ficar disponível.
2. Aplicar as migrations.
3. Interromper a inicialização caso alguma migration falhe.
4. Poder ser executado mais de uma vez sem causar erro.

---

# Persistência do Banco

O SQL Server deverá utilizar volume nomeado no Docker para que os dados permaneçam disponíveis após:

```bash
docker compose down
```

e um novo:

```bash
docker compose up
```

---

# Logs

O desafio exige persistência de logs utilizando Serilog ou NLog.

Os logs deverão registrar pelo menos:

- Criação de registros
- Atualização de registros
- Exclusão de registros
- Exceções não tratadas

## Localização dos logs

```text
Ainda não configurada.
```

Este item encontra-se em desenvolvimento.

Na implementação final, os logs deverão ficar em um diretório persistido através de volume Docker.

---

# Tratamento global de exceções

A versão final deverá possuir middleware global de exceções.

O objetivo é impedir que erros internos, como stack traces, sejam enviados diretamente ao cliente.

Exemplo de retorno esperado:

```json
{
  "message": "Recurso não encontrado."
}
```

ou:

```json
{
  "message": "Já existe um registro com os dados informados."
}
```

Status atual:

```text
Em desenvolvimento.
```

---

# Filtros previstos

## Veículos

A listagem deverá permitir:

```text
Filtro por Grupo
Filtro por Plano de Assistência
```

## Planos

A listagem deverá permitir:

```text
Filtro por Empresa
```

---

# Confirmação de exclusão

No frontend, exclusões deverão possuir confirmação antes da execução.

Exemplo:

```text
Tem certeza que deseja excluir este registro?

[ Cancelar ] [ Excluir ]
```

---

# Status atual do projeto

## Backend

- [x] Projeto ASP.NET Core criado
- [x] API REST criada
- [x] Entity Framework Core configurado
- [x] SQL Server configurado
- [x] DbContext criado
- [x] Migrations iniciais
- [x] Entidades de domínio
- [x] DTOs
- [x] Interfaces de Repository
- [x] Interfaces de Service
- [x] Repository Pattern
- [x] Service Layer
- [x] Controllers
- [x] CRUD de Grupos
- [x] CRUD de Veículos
- [x] CRUD de Empresas de Assistência
- [x] CRUD de Planos
- [x] CRUD da associação Veículo / Plano
- [x] Relacionamentos entre entidades
- [x] Swagger
- [x] Connection string utilizando variável de ambiente

### Backend pendente

- [ ] Validação completa da placa
- [ ] Constraint única para placa
- [ ] Constraint única VeiculoId + PlanoId
- [ ] Tratamento completo de 409 Conflict
- [ ] Middleware global de exceções
- [ ] Logs com Serilog ou NLog
- [ ] Persistência de logs em volume Docker
- [ ] Filtros
- [ ] Validações finais
- [ ] Health Check

---

## Frontend

- [x] Projeto Angular criado
- [x] Angular Router configurado
- [x] Estrutura inicial das páginas
- [x] Estrutura inicial do menu lateral

### Frontend pendente

- [ ] Angular Material em toda a interface
- [ ] Reactive Forms
- [ ] Integração completa com API
- [ ] Services para todos os endpoints
- [ ] Interceptor HTTP
- [ ] Snackbar / Toast
- [ ] Tratamento de loading
- [ ] Tratamento de lista vazia
- [ ] Tela completa de Grupos
- [ ] Tela completa de Veículos
- [ ] Tela completa de Empresas
- [ ] Tela completa de Planos
- [ ] Tela completa de Veículos / Assistência
- [ ] Filtros
- [ ] Dialog de confirmação de exclusão
- [ ] Dashboard final

---

## Docker

- [x] SQL Server executado via Docker durante o desenvolvimento

### Docker pendente

- [ ] Dockerfile da API final
- [ ] Dockerfile do frontend
- [ ] Docker Compose com os três serviços
- [ ] Migrations automáticas
- [ ] Healthcheck do SQL Server integrado à API
- [ ] Volume persistente para logs
- [ ] Validação do comando único em repositório limpo

---

# Limitações conhecidas

No estágio atual:

- O frontend ainda está em desenvolvimento.
- A integração completa entre Angular e API ainda não foi concluída.
- Angular Material ainda precisa ser aplicado às telas finais.
- Reactive Forms ainda precisam ser finalizados.
- A validação completa de placas ainda está pendente.
- A constraint de unicidade do vínculo entre veículo e plano ainda precisa ser finalizada.
- O tratamento global de exceções ainda será implementado.
- O tratamento completo de `409 Conflict` ainda está pendente.
- Os logs persistentes ainda não foram configurados.
- O Docker Compose completo ainda está em desenvolvimento.
- As migrations automáticas na inicialização do ambiente ainda estão pendentes.
- Testes automatizados ainda não foram implementados.
- CI ainda não foi configurado.

---

# Itens bônus

O desafio também apresenta funcionalidades opcionais para diferenciação da entrega.

## Paginação e ordenação

```text
Status: não implementado.
```

---

## Testes unitários

```text
Status: não implementado.
```

---

## Seed de dados

```text
Status: não implementado.
```

---

## Health Check

Endpoint previsto:

```text
/health
```

Status:

```text
Não implementado.
```

---

## CI - GitHub Actions

Está prevista a implementação de uma pipeline de CI.

Fluxo planejado:

```text
Push / Pull Request
        ↓
Checkout
        ↓
Restore .NET
        ↓
Build Backend
        ↓
Testes Backend
        ↓
npm ci
        ↓
Build Angular
```

Status:

```text
Não implementado.
```

---

# Segurança

Credenciais reais não devem ser versionadas.

O projeto utiliza `.gitignore` para ignorar arquivos e diretórios locais, incluindo:

```text
.vs/
bin/
obj/
node_modules/
.angular/
dist/
.env
```

Arquivos contendo segredos devem utilizar variáveis de ambiente.

Exemplo:

```text
ConnectionStrings__DefaultConnection
```

Nunca deve ser adicionada uma senha real diretamente ao README ou ao código versionado.

---

# Git

O projeto utiliza Git para versionamento.

Padrão de commits utilizado:

```text
feat: nova funcionalidade
fix: correção de bug
docs: documentação
refactor: refatoração
test: testes
chore: configurações
```

Primeiro commit do projeto:

```text
feat: implementa backend e estrutura inicial do frontend
```

---

# Histórico de desenvolvimento

O repositório mantém histórico de commits para demonstrar a evolução do projeto.

Novas funcionalidades devem ser adicionadas através de commits separados sempre que possível.

Exemplo:

```bash
git add .
git commit -m "feat: implementa cadastro de veiculos"
git push
```

---

# Repositório

GitHub:

```text
COLOQUE_AQUI_A_URL_DO_REPOSITORIO
```

Exemplo:

```text
https://github.com/SEU-USUARIO/AlocacaoVeiculosAssistencia
```

---

# Autor

Projeto desenvolvido como parte de um desafio técnico Full Stack.

Tecnologias principais:

```text
.NET 8
Angular
SQL Server
Entity Framework Core
Docker
```
