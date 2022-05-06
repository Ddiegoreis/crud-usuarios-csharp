
# User Crud

### Banco de dados
    - Script para o banco de dados utilizado na aplicação "create_database.sql".

## User

### Criação de usuário - `Post /api/user`
- Exemplo:
    
    ```
    {
        "nome": "User name",
        "senha": "PASSWORD",
        "email": "example@mail.com"
    }
    ```
- Resposta:

    ```
    {
        id - integer,
        nome - string,
        senha - string,
        email- string
    }
    ```
### Listar usuários - `Get /api/user`

- Resposta:

    ```
    [{
        id - integer,
        nome - string,
        senha - string,
        email- string
    }]
    ```

### Retornar usuário por ID - `Get /api/user/{id}`
    #### Obs.: O id deve ser informado na rota via e o Bearer token enviado via auth.

- Resposta:

    ```
    {
        id - integer,
        nome - string,
        senha - string,
        email- string
    }
    ```

### Deletear usuário por ID - `Delete /api/user/{id}`
    #### Obs.: O id deve ser informado na rota via e o Bearer token enviado via auth.

- Resposta:

    ```
    {
        id - integer,
        nome - string,
        senha - string,
        email- string
    }
    ```
### Atualizar usuário por ID - `Put /api/user/{id}`
    #### Obs.: O id deve ser informado na rota via e o Bearer token enviado via auth.

- Resposta:

    ``` Usuario atualizado ```

## Token

### Retornar token - `Post /api/token`
- Exemplo:
    
    ```
    {
        "email": "example@mail.com",
        "senha": "PASSWORD",
    }
    ```

- Resposta
    ``` "Token" ```

## Reset Password

### Retornar chave para alteração de senha - `Get /api/ChangePassword/{id}`
    #### Obs.: O id deve ser informado na rota via e o Bearer token enviado via auth.

- Resposta
    ``` Sua chave para alteração é "XXXXXX" ```

### Reset Password - `Put api/ChangePassword`
    #### Obs.: O  Bearer token deve ser enviado via auth.

- Exemplo
    ```
    {
        "id": 1,
        "nome": "User name",
        "senha": "PASSWORD",
        "email": "example@mail.com",
        "senhaNova":"NEWPASSWORD", 
        "token":"TOKEN PARA RESET DE SENHA AQUI" 
    }
    ```

- Resposta
    ``` "Senha alterada com sucesso" ```