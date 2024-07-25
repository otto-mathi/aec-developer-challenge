# Aec - Developer Challenge

Repositório referente ao desafio Aec Developer Challenge.

## Índice
[Objetivo](#objetivo)  
[Introdução](#introdução)  
[Possíveis Melhorias](#possíveis-melhorias)  
[Pré-requisitos](#pré-requisitos)  
[Instruções](#instruções)

## Objetivo
Implementar um sistema que atenda os requisitos contidos no arquivo de escopo ([Link do PDF](teste-aec-developer-challenge.pdf)).

## Introdução
O sistema foi implementado utilizando C# no framework .NET 8, banco de dados SQL Server, e Docker.

Instruções de como rodar: [Instruções](#instruções).

## Possíveis Melhorias
Dispor mais rotas de climas.
Dispor rotas de requisições e logs de requisições.
Melhorar o tratamento de erros.
Mais refatorações de código.

## Pré-requisitos
Necessárias as instalações do [Docker](https://docs.docker.com/get-docker/) e [Docker Compose](https://docs.docker.com/compose/install/).

## Instruções

Passo 1: baixe, instale, e rode o [Docker](https://docs.docker.com/get-docker/) e o [Docker Compose](https://docs.docker.com/compose/install/).

Passo 2: clone esse projeto numa pasta em seu computador.

Passo 3: abra o prompt de comando.

Passo 4: digite o seguinte comando docker-compose -f <CAMINHO_ONDE_SEU_PROJETO_FOI_CLONADO>/AeC.DeveloperChallenge/docker-compose.yml up -d.

Passo 5: aguarde o processamento ser concluído.

Passo 6: o Swagger da aplicação estará disponível em:
[http://localhost:5030](http://localhost:5030)

Dúvidas e sugestões: ottomn@gmail.com