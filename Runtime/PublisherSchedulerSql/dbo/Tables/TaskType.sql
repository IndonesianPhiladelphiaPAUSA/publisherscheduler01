CREATE TABLE [dbo].[TaskType] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_dbo.TaskType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

