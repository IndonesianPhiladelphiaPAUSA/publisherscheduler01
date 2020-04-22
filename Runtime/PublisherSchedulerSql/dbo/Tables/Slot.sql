CREATE TABLE [dbo].[Slot] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Begin]      DATETIME       NOT NULL,
    [End]        DATETIME       NOT NULL,
    [LocationId] INT            NOT NULL,
    [IsActive]   BIT            NOT NULL,
    [Name]       NVARCHAR (MAX) DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_dbo.Slot] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Slot_dbo.Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_LocationId]
    ON [dbo].[Slot]([LocationId] ASC);

