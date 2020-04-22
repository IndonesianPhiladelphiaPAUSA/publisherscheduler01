CREATE TABLE [dbo].[PersonAvail] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [Begin]       DATETIME NOT NULL,
    [End]         DATETIME NOT NULL,
    [IsAvailable] BIT      NOT NULL,
    [PersonId]    INT      NOT NULL,
    CONSTRAINT [PK_dbo.PersonAvail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.PersonAvail_dbo.Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PersonId]
    ON [dbo].[PersonAvail]([PersonId] ASC);

