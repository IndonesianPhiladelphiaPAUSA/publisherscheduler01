CREATE TABLE [dbo].[Capacity] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Capacity] PRIMARY KEY CLUSTERED ([Id] ASC)
);

