# Arquitetura e Desenvolvimento de APIs - API Empresas

## Aluno: Rodrigo Pinheiro Alcantara - RU 4844626

Este projeto implementa uma API RESTful completa (CRUD) para gestão de **Empresas** e **Funcionários**, utilizando C# (.NET 8) e Entity Framework Core (com MySQL).

---

### Requisitos Obrigatórios

* .NET SDK 8.0 ou superior (ou versão compatível como o .NET 9.0)
* Docker Desktop (para o banco de dados MySQL)
* Cliente HTTP para testes (Postman ou Thunder Client)

### 🛠️ Configuração e Execução

1.  **Ajuste da Conexão:**
    * Verifique se o arquivo `appsettings.json` usa a porta **3307** e a senha **MinhaSenha123** para o MySQL (conforme configuramos no desenvolvimento).

2.  **Restauração de Pacotes e Build:**
    * Abra o PowerShell na pasta do projeto (`ApiEmpresas`) e execute:
    ```bash
    dotnet restore
    dotnet build
    ```

3.  **Inicie o MySQL com Docker:**
    * Execute no terminal (se o container ainda não estiver rodando):
    ```bash
    docker run --name mysql-api -e MYSQL_ROOT_PASSWORD=MinhaSenha123 -p 3307:3306 -d mysql
    ```

4.  **Aplique as Migrações do Banco de Dados:**
    * Execute o comando para criar/atualizar as tabelas `Empresas` e `Funcionarios` no MySQL:
    ```bash
    dotnet ef database update
    ```

5.  **Execute a API:**
    * A API será iniciada e estará acessível nas portas HTTPS e HTTP:
    ```bash
    dotnet run
    ```
    O endereço base para os testes será **`http://localhost:5254`**.

---

### 🚀 Endpoints Implementados (CRUD Completo)

| Entidade | Método | URL Exemplo | Corpo JSON (Exemplo) | Objetivo |
| :--- | :--- | :--- | :--- | :--- |
| **Empresa** | `POST` | `/api/Empresas` | `{ "nome": "Nova Empresa", "cnpj": "99.999.999/0001-99" }` | **CREATE** (Insere nova empresa) |
| **Empresa** | `GET` | `/api/Empresas` | N/A | **READ** (Lista todas as empresas) |
| **Empresa** | `PUT` | `/api/Empresas/{id}` | `{ "id": 1, "nome": "Empresa Editada", ... }` | **UPDATE** (Atualiza por ID) |
| **Empresa** | `DELETE` | `/api/Empresas/{id}` | N/A | **DELETE** (Exclui por ID) |
| **Funcionário**| `POST` | `/api/Funcionarios` | `{ "Nome": "João Dev", "EmpresaId": 1, "Salario": 5000.00 }` | **CREATE** (Insere novo funcionário) |
| **Funcionário**| `GET` | `/api/Funcionarios` | N/A | **READ** (Lista todos os funcionários) |
| **Funcionário**| `PUT` | `/api/Funcionarios/{id}` | `{ "id": 1, "EmpresaId": 1, "Salario": 7000.00 }` | **UPDATE** (Atualiza por ID) |
| **Funcionário**| `DELETE` | `/api/Funcionarios/{id}` | N/A | **DELETE** (Exclui por ID) |