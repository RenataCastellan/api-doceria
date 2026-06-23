# 🍬 Encanto Doceria — API

API RESTful desenvolvida em ASP.NET Core para gerenciamento de pedidos de uma doceria artesanal.

**Projeto:** Gestão de Pedidos – Encanto Doceria  
**Curso:** Tecnologia em Análise e Desenvolvimento de Sistemas – IFRO Campus Ji-Paraná  
**Alunas:** Rafaela Pereira da Silva | Renata Lima Lopes Castellan

---

## 🛠️ Tecnologias utilizadas

- ASP.NET Core 8.0
- Entity Framework Core 8
- MySQL com Pomelo Driver
- Swagger
- BCrypt.Net
- JWT Bearer

---

## 📁 Estrutura do projeto

```
api-doceria/
├── Controllers/     → Endpoints da API
├── DataBase/        → Script de criação do banco de dados
├── DataContexts/    → Configuração do banco (EF Core)
├── Dtos/            → Objetos de transferência de dados
├── Exceptions/      → Exceções personalizadas
├── Models/          → Entidades do banco de dados
├── Services/        → Regras de negócio
├── Program.cs       → Configuração da aplicação
└── appsettings.json → Connection string e JWT
```

---

## ⚙️ Como executar

### Pré-requisitos
- .NET 8 SDK
- MySQL Server 8.0+
- Visual Studio 2022

### 1. Clonar o repositório
```bash
git clone https://github.com/RenataCastellan/api-doceria.git
cd api-doceria
```

### 2. Criar o banco de dados
Abra a pasta DataBase encontre o arquivo sql e execute a base no MySQL Workbench

### 3. Configurar a conexão
Edite o `appsettings.json` com suas credenciais do MySQL:
```json
"ConnectionStrings": {
  "mysql": "Server=localhost;Database=encanto_doceria;User=root;Password=SUA_SENHA;"
}
```

### 4. Executar
Abra no Visual Studio e pressione **F5**, ou via terminal:
```bash
dotnet run
```

### 5. Acessar o Swagger
```
https://localhost:{porta}/swagger
```

---

## 🔐 Autenticação JWT

Rotas de cadastro, edição e exclusão de produtos são protegidas e exigem login de administrador.

1. Faça login em **POST /api/Auth/login/admin**
2. Copie o `token` da resposta
3. No Swagger clique em **Authorize** e informe: `Bearer SEU_TOKEN`

---

## 📌 Endpoints

### Públicos
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | / | Status da API |
| GET | /api/Produtos | Lista produtos ativos |
| GET | /api/Produtos?categoria={categoria} | Filtra por categoria |
| GET | /api/Produtos/{id} | Busca produto por ID |
| POST | /api/Clientes | Cadastra cliente |
| POST | /api/Auth/login/cliente | Login do cliente |
| POST | /api/Auth/login/admin | Login do administrador |

### Requerem autenticação de Admin
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | /api/Produtos | Cadastra produto |
| PUT | /api/Produtos/{id} | Edita produto |
| DELETE | /api/Produtos/{id} | Remove produto |

### Cliente e Endereço
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /api/Clientes | Lista clientes |
| GET | /api/Clientes/{id} | Busca cliente por ID |
| POST | /api/Enderecos | Cadastra endereço |
| GET | /api/Enderecos/cliente/{id} | Lista endereços do cliente |
| DELETE | /api/Enderecos/{id} | Remove endereço |

### Carrinho
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | /api/Carrinhos/cliente/{id} | Cria carrinho |
| GET | /api/Carrinhos/{id} | Visualiza carrinho |
| POST | /api/Carrinhos/itens | Adiciona item |
| DELETE | /api/Carrinhos/itens/{id} | Remove item |

### Pedido
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | /api/Pedidos | Cria pedido |
| GET | /api/Pedidos | Lista pedidos |
| GET | /api/Pedidos/{id} | Busca pedido por ID |
| PATCH | /api/Pedidos/{id}/status | Atualiza status |

### Pagamento
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | /api/Pagamentos | Registra pagamento |
| GET | /api/Pagamentos/pedido/{id} | Lista pagamentos do pedido |

---

## 🗄️ Modelo de dados

**Relacionamentos 1:N:**
- Cliente → Endereços
- Cliente → Pedidos
- Cliente → Carrinhos
- Pedido → Pagamentos
- Endereço → Pedidos
- Administrador → Produtos

**Relacionamentos N:N:**
- Pedido ↔ Produto via `ItensPedido`
- Carrinho ↔ Produto via `ItensCarrinho`
