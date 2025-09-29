# Projeto Cadastro de Equipamentos - Bradesco

## 🔹 Descrição
Sistema para cadastro e gerenciamento de equipamentos e usuários, com funcionalidades de:
- Cadastro de usuários e equipamentos
- Vínculo entre usuários e equipamentos
- Exportação de dados em CSV
- Dashboard com contagem dos registros

## 🔹 Tecnologias
- .NET 6 (ASP.NET MVC)
- C#
- SQL Server
- HTML5, CSS3, JS
- Bootstrap 5 (estilização e responsividade)

## 🔹 Pré-requisitos
- Visual Studio 2022 ou VS Code
- .NET 6 SDK
- SQL Server (ou SQL Server Express)
- Navegador moderno (Chrome, Edge ou Firefox)

## 🔹 Estrutura do banco
- **Tabelas**: Usuários, Equipamentos, Vinculos, Logs de E-mails
- **Procedures**: CRUD e consultas específicas

> Todos os scripts estão na pasta `/Scripts`:
- `ScriptCriacao.sql` → Criação do banco e tabelas
- `Procedures.sql` → Procedures de inserção, atualização e consultas

## 🔹 Instruções de execução

1. **Banco de dados**
   - Execute `ScriptCriacao.sql` no seu SQL Server para criar as tabelas.
   - Execute `Procedures.sql` para criar as procedures necessárias.

2. **Configuração do projeto**
   - Abra a solução `CadastroEquipamento.sln` no Visual Studio.
   - No arquivo `appsettings.json`, configure a `ConnectionString` do seu SQL Server:
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Server=SEU_SERVIDOR;Database=CadastroEquipamento;User Id=USUARIO;Password=SENHA;"
     }
     ```

3. **Rodando a aplicação**
   - Defina `CadastroEquipamento.Web` como projeto inicial.
   - Execute a aplicação (F5 ou Ctrl+F5).
   - O sistema abrirá a tela de **Home**, que redireciona para o **Dashboard**.

4. **Exportação de dados**
   - Acesse a seção **Relatórios** para exportar:
     - Usuários
     - Equipamentos
     - Vínculos
     - API Usuários
     - Logs de E-mails

## 🔹 Documentação e instruções de uso
- **Cadastro de Usuários/Equipamentos**: Preencher formulário e salvar.
- **Vínculo**: Selecionar usuário e equipamento e vincular.
- **Desvincular**: Botão de desvinculação envia e-mail de notificação.
- **Dashboard**: Contagem animada de registros.
- **Relatórios**: Dropdown para exportação de CSV.

## 🔹 Repositório Git
- O código está versionado em GitHub.  
- Para clonar:
```bash
git clone https://github.com/seu-usuario/CadastroEquipamento.git
