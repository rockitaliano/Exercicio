# Teste-Nivelamento-Desenvolvedor-CSharp-API-v3

Documentação do Projeto

Introdução

Este projeto consiste na implementação de duas novas funcionalidades para a API REST de um banco: Movimentação de uma conta corrente e Consulta do saldo da conta corrente.

Funcionalidades

Serviço: 

Movimentação de uma conta corrente
O serviço de Movimentação de uma conta corrente permite que um aplicativo se integre com a API para realizar operações de crédito ou débito em contas correntes.

Requisições:

Identificação da requisição: 

Identificador único da requisição.
Identificação da conta corrente: Número da conta corrente envolvida na movimentação.

Valor: 

Valor a ser movimentado.
Tipo de movimento: C (Crédito) ou D (Débito).

Validações de Negócio:

Apenas contas correntes cadastradas e ativas podem receber movimentação.
Apenas valores positivos podem ser recebidos.
Apenas os tipos “débito” ou “crédito” podem ser aceitos.
Respostas:
HTTP 200: 
Movimentação bem-sucedida. Retorna o ID do movimento gerado.
HTTP 400: 
Movimentação inválida. Retorna uma mensagem descritiva e o tipo de falha.
Serviço: Saldo da conta corrente

O serviço de Saldo da conta corrente permite que um aplicativo consulte o saldo atual de uma conta corrente.

Requisições:

Identificação da conta corrente: 
Número da conta corrente a ser consultada.

Validações de Negócio:
Apenas contas correntes cadastradas e ativas podem consultar o saldo.
Respostas:
HTTP 200: 
Consulta bem-sucedida. Retorna os seguintes dados:
Número da conta corrente.
Nome do titular da conta corrente.
Data e hora da resposta da consulta.
Valor do saldo atual.
HTTP 400: 
Consulta inválida. Retorna uma mensagem descritiva e o tipo de falha.
Tecnologias Utilizadas
Dapper: Componente para conexão com o banco de dados.
CQRS (Command Query Responsibility Segregation): Padrão de projeto para separar as operações de comando e de consulta.
Mediator: Padrão de projeto comportamental que reduz as dependências entre objetos.
Swagger:
