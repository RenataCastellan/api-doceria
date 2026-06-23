DROP DATABASE IF EXISTS encanto_doceria;
CREATE DATABASE encanto_doceria;
USE encanto_doceria;

CREATE TABLE Administradores (
    IdAdmin INT AUTO_INCREMENT PRIMARY KEY,
    Login VARCHAR(100) NOT NULL,
    Senha VARCHAR(255) NOT NULL
);

CREATE TABLE Clientes (
    IdCliente INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Senha VARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT NOW()
);

CREATE TABLE Enderecos (
    IdEndereco INT AUTO_INCREMENT PRIMARY KEY,
    Rua VARCHAR(150) NOT NULL,
    Numero VARCHAR(20) NOT NULL,
    Complemento VARCHAR(100),
    Bairro VARCHAR(100) NOT NULL,
    Cidade VARCHAR(100) NOT NULL,
    Estado VARCHAR(2) NOT NULL,
    Cep VARCHAR(10),
    IdCliente INT NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
);

CREATE TABLE Produtos (
    IdProduto INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Descricao VARCHAR(500) NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    Imagem VARCHAR(500),
    Categoria VARCHAR(100) NOT NULL,
    Estoque INT NOT NULL DEFAULT 0,
    Ativo TINYINT(1) NOT NULL DEFAULT 1,
    IdAdmin INT NOT NULL,
    FOREIGN KEY (IdAdmin) REFERENCES Administradores(IdAdmin)
);

CREATE TABLE Pedidos (
    IdPedido INT AUTO_INCREMENT PRIMARY KEY,
    Data DATETIME DEFAULT NOW(),
    Status VARCHAR(50) NOT NULL DEFAULT 'Pendente',
    Total DECIMAL(10,2) NOT NULL DEFAULT 0,
    IdCliente INT NOT NULL,
    IdEndereco INT NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (IdEndereco) REFERENCES Enderecos(IdEndereco)
);

CREATE TABLE ItensPedido (
    IdItem INT AUTO_INCREMENT PRIMARY KEY,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    IdPedido INT NOT NULL,
    IdProduto INT NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(IdPedido),
    FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto)
);

CREATE TABLE Pagamentos (
    IdPagamento INT AUTO_INCREMENT PRIMARY KEY,
    Tipo VARCHAR(50) NOT NULL,
    Status VARCHAR(50) NOT NULL DEFAULT 'Pendente',
    Valor DECIMAL(10,2) NOT NULL,
    DataPagamento DATETIME DEFAULT NOW(),
    IdPedido INT NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(IdPedido)
);

CREATE TABLE Carrinhos (
    IdCarrinho INT AUTO_INCREMENT PRIMARY KEY,
    CreatedAt DATETIME DEFAULT NOW(),
    IdCliente INT NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
);

CREATE TABLE ItensCarrinho (
    IdItem INT AUTO_INCREMENT PRIMARY KEY,
    Quantidade INT NOT NULL,
    IdCarrinho INT NOT NULL,
    IdProduto INT NOT NULL,
    FOREIGN KEY (IdCarrinho) REFERENCES Carrinhos(IdCarrinho),
    FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto)
);