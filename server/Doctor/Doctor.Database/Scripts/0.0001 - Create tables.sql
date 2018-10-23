IF NOT EXISTS (SELECT TOP 1 * FROM sys.tables WHERE [name] = 'users')
CREATE TABLE [dbo].[users] (
	[userId] INT IDENTITY(1,1) NOT NULL,
	[login] NVARCHAR(100) NOT NULL,
	[email] NVARCHAR(255) NOT NULL,
	[passwordHash] BINARY(64) NOT NULL,
	[passwordSalt] BINARY(128) NOT NULL,
	CONSTRAINT [PK_users_userId] PRIMARY KEY (UserId),
	CONSTRAINT [UK_users_login] UNIQUE ([login]),
	CONSTRAINT [UK_users_email] UNIQUE (email),
) ON [PRIMARY]
GO