create database projeto;

GO

USE [projeto]

GO

CREATE TABLE [dbo].[USER](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nome] [nvarchar](200) NOT NULL UNIQUE,
	[Senha] [Nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL
)

GO

CREATE TABLE [DBO].[RESET_PASSWORD_TOKEN](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Token] [Nvarchar](200) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Cadastro] [Datetime] NOT NULL,
	[VALIDADE] [int],
	Foreign key ([IdUser]) References [dbo].[User](Id)
)

GO

INSERT INTO [dbo].[User]
SELECT
    'MESTRE',
    'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', -- 123456
	'email@teste'