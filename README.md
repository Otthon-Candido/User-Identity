# User-Identity

Projeto desenvolvido em .NET 6 que tem como objetivo criar uma API que controla todo fluxo de cadastro, alteração e login de um usuário. 
* Login com usuário e senha locais
* Validação de token
* Gestão de usuários (cadastro, edição, remoção)
* Gestão de perfis
* Configurações dos protocolos de autenticação
* "Sign up" - criação de novo usuário (automaticamente bloqueado, e-mails são enviados para o e-mail do novo usuário)

## Stack utilizada
**Back-end:** .net6, C#
**Banco de dados:** [PostgreSQL]

## Documentação da API (principais)

#### Efetua login

```http
  POST /api/login
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `userName` | `string` | **Obrigatório**. login do usuário |
| `password` | `string` | **Obrigatório**. senha do usuário |

#### Registra aplicação

```http
  POST /api/register
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `nickName`      | `string` | **Obrigatório**. Nome da aplicação a ser registrada |
| `email`      | `string` | **Obrigatório**. E-mail da aplicação a ser registrada |
| `password`      | `string` | **Obrigatório**. Senha da aplicação a ser registrada |
| `rePassword`      | `string` | **Obrigatório**. Confirmação da senha da aplicação a ser registrada |

#### Ativar usuário na aplicação
```http
  POST /api/active
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `userId`      | `string` | **Obrigatório**. Id do usário na aplicação |
| `activateCode`      | `string` | **Obrigatório**. Token de ativação da conta |

#### Solicitação de resete de senha

```http
  POST /api/requestPasswordReset
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `email`      | `string` | **Obrigatório**. E-mail do usário na aplicação |

#### Alteração da senha

```http
  POST /api/passwordReset
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `userId`      | `string` | **Obrigatório**. Id do usário na aplicação |
| `password`      | `string` | **Obrigatório**. Nova senha do usário na aplicação |
| `token`      | `string` | **Obrigatório**. Token de redefinição de senha |

## Variáveis de Ambiente

Para rodar esse projeto, você vai precisar adicionar as seguintes variáveis no appSettings

```bash
  "ConnectionStrings": {
    "IdentityConnection": "Host=localhost;Port=5432;Database=Identity;Username=postgres;Password=1234;"
  }
```
Observação: Os parametros devem ser alterados de acordo com a sua configuração do bancos de dados.

É necessário configurar os secrets do .NET. Vá até o terminal do USER.API e execute os seguintes comandos.
```bash
 dotnet user-secrets init
```
```bash
 dotnet user-secrets set "SymmetricSecurityKey" "COLOQUE-OQUE-QUISER-AQUI"
```
```bash
 dotnet user-secrets set "EmailSettings:Port" "465"
```
```bash
 dotnet user-secrets set "EmailSettings:Port" "465"
```
```bash
 dotnet user-secrets set "EmailSettings:SmtpServer" "smtp.gmail.com"
```
```bash
 dotnet user-secrets set "EmailSettings:From" "COLOQUE-SEU-EMAIL"
```
```bash
 dotnet user-secrets set "EmailSettings:Password" "COLOQUE-A-SENHA-DO-SEU-EMAIL"
```

Obeservação a senha não é referente a senha do seu gmail. É necessario gerar uma senha "Senhas de app" no gmail. Vou deixar um link que explica como gerar essa senha no gmail "https://www.youtube.com/watch?v=6o_f_-YMhaU" 

## Configurando banco

Execute os seguintes comandos para configurar o banco de dados projeto.

```bash
dotnet ef migrations add "Nome da Migration"  --project ..\User.Infra 
```
```bash
dotnet ef database update -p ../User.Infra       
```

Observação: Foi criado inicialmente ao rodar as migrations o usuário "teste@gmail.com". Aconselho colocar um usuário com e-mail valido, então basta alterar para sua preferência na pasta "UserDbContext.cs.

## Demonstração

```bash
dotnet run     
```
Observação: Esteja na pasta User.API