CREATE TABLE [dbo].[PersonCapacity] (
    [Person_Id]   INT NOT NULL,
    [Capacity_Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.PersonCapacity] PRIMARY KEY CLUSTERED ([Person_Id] ASC, [Capacity_Id] ASC),
    CONSTRAINT [FK_dbo.PersonCapacity_dbo.Capacity_Capacity_Id] FOREIGN KEY ([Capacity_Id]) REFERENCES [dbo].[Capacity] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.PersonCapacity_dbo.Person_Person_Id] FOREIGN KEY ([Person_Id]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Person_Id]
    ON [dbo].[PersonCapacity]([Person_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Capacity_Id]
    ON [dbo].[PersonCapacity]([Capacity_Id] ASC);

