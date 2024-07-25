IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'aec_developer_challenge')
BEGIN
    CREATE DATABASE aec_developer_challenge COLLATE Latin1_General_CI_AS;
END
GO

USE aec_developer_challenge;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Requisicao]') AND type in (N'U'))
BEGIN
	CREATE TABLE Requisicao
	(
		Codigo BIGINT IDENTITY(1, 1) NOT NULL,
		ControllerOrigem VARCHAR(100) NOT NULL,
		MetodoOrigem VARCHAR(100) NOT NULL,
		MetodoHttp VARCHAR(20) NOT NULL,
		Data DATETIME NOT NULL,
		Parametros VARCHAR(200) NULL,
		Retorno NVARCHAR(MAX) NULL,
		CONSTRAINT PK_Requisicao PRIMARY KEY (Codigo)
	);
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogRequisicao]') AND type in (N'U'))
BEGIN
	CREATE TABLE LogRequisicao
	(
		Codigo BIGINT IDENTITY(1, 1) NOT NULL,
    		CodigoRequisicao BIGINT,
		Tipo VARCHAR(20) NOT NULL,
		Data DATETIME NOT NULL,
		Mensagem VARCHAR(5000) NOT NULL,
		CONSTRAINT PK_LogRequisicao PRIMARY KEY (Codigo),
		CONSTRAINT FK_Requisicao_LogRequisicao FOREIGN KEY (CodigoRequisicao) REFERENCES Requisicao(Codigo)
	);
END
GO