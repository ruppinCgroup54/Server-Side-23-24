CREATE TABLE [dbo].[UsersTable] (
    [firstName]  NVARCHAR (20) NOT NULL,
    [familyName] NVARCHAR (20) NOT NULL,
    [email]      NVARCHAR (50) NOT NULL,
    [password]   NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([email] ASC)
);

