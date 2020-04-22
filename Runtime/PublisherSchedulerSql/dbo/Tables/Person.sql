CREATE TABLE [dbo].[Person] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX) NOT NULL,
    [IsActive]      BIT            NOT NULL,
    [SecurityLevel] INT            NULL,
    [aspnetuserid]  NVARCHAR (128) NULL,
    CONSTRAINT [PK_dbo.Person] PRIMARY KEY CLUSTERED ([Id] ASC)
);

