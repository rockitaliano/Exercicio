--cria o database
CREATE DATABASE BD_PROCESSES  
ON   
( NAME = BD_PROCESSES_Dados,  
    FILENAME = 'D:\Data\MSSQL15.MSSQLSERVER\MSSQL\DATA\BD_Processes.mdf')  
LOG 
ON  
( NAME = BD_PROCESSES_Log,  
    FILENAME = 'D:\Data\MSSQL15.MSSQLSERVER\MSSQL\DATA\BD_Processes.ldf')  
GO

USE BD_PROCESSES
GO

-- cria as tabelas e da a carga inicial
CREATE TABLE contacorrente (
	idcontacorrente VARCHAR(37) PRIMARY KEY, -- id da conta corrente
	numero INT NOT NULL UNIQUE, -- numero da conta corrente
	nome VARCHAR(100) NOT NULL, -- nome do titular da conta corrente
	ativo BIT NOT NULL default 0, -- indicativo se a conta esta ativa. (0 = inativa, 1 = ativa).
	CHECK (ativo in (0,1))
);

CREATE TABLE movimento (
	idmovimento VARCHAR(37) PRIMARY KEY, -- identificacao unica do movimento
	idcontacorrente VARCHAR(37) NOT NULL, -- identificacao unica da conta corrente
	datamovimento VARCHAR(25) NOT NULL, -- data do movimento no formato DD/MM/YYYY
	tipomovimento VARCHAR(1) NOT NULL, -- tipo do movimento. (C = Credito, D = Debito).
	valor REAL NOT NULL, -- valor do movimento. Usar duas casas decimais.
	CHECK (tipomovimento in ('C','D')),
	FOREIGN KEY(idcontacorrente) REFERENCES contacorrente(idcontacorrente)
);

CREATE TABLE idempotencia (
	chave_idempotencia VARCHAR(37) PRIMARY KEY, -- identificacao chave de idempotencia
	requisicao VARCHAR(1000), -- dados de requisicao
	resultado VARCHAR(1000) -- dados de retorno
);

INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('B6BAFC09-6967-ED11-A567-055DFA4A16C9', 123, 'Katherine Sanchez', 1);
INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('FA99D033-7067-ED11-96C6-7C5DFA4A16C9', 456, 'Eva Woodward', 1);
INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('382D323D-7067-ED11-8866-7D5DFA4A16C9', 789, 'Tevin Mcconnell', 1);
INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('F475F943-7067-ED11-A06B-7E5DFA4A16C9', 741, 'Ameena Lynn', 0);
INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('BCDACA4A-7067-ED11-AF81-825DFA4A16C9', 852, 'Jarrad Mckee', 0);
INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('D2E02051-7067-ED11-94C0-835DFA4A16C9', 963, 'Elisha Simons', 0);