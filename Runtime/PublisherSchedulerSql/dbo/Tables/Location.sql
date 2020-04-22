CREATE TABLE [dbo].[Location] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Location] PRIMARY KEY CLUSTERED ([Id] ASC)
);

