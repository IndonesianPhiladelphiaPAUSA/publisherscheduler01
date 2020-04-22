CREATE TABLE [dbo].[Assignment] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [SlotId]     INT NOT NULL,
    [TaskTypeId] INT NOT NULL,
    [PersonId]   INT NOT NULL,
    CONSTRAINT [PK_dbo.Assignment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Assignment_dbo.Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Assignment_dbo.Slot_SlotId] FOREIGN KEY ([SlotId]) REFERENCES [dbo].[Slot] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Assignment_dbo.TaskType_TaskTypeId] FOREIGN KEY ([TaskTypeId]) REFERENCES [dbo].[TaskType] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SlotId]
    ON [dbo].[Assignment]([SlotId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TaskTypeId]
    ON [dbo].[Assignment]([TaskTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PersonId]
    ON [dbo].[Assignment]([PersonId] ASC);

