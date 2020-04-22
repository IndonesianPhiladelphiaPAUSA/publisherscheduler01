CREATE TABLE [dbo].[TaskTypeCapacity] (
    [TaskType_Id] INT NOT NULL,
    [Capacity_Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.TaskTypeCapacity] PRIMARY KEY CLUSTERED ([TaskType_Id] ASC, [Capacity_Id] ASC),
    CONSTRAINT [FK_dbo.TaskTypeCapacity_dbo.Capacity_Capacity_Id] FOREIGN KEY ([Capacity_Id]) REFERENCES [dbo].[Capacity] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TaskTypeCapacity_dbo.TaskType_TaskType_Id] FOREIGN KEY ([TaskType_Id]) REFERENCES [dbo].[TaskType] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TaskType_Id]
    ON [dbo].[TaskTypeCapacity]([TaskType_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Capacity_Id]
    ON [dbo].[TaskTypeCapacity]([Capacity_Id] ASC);

