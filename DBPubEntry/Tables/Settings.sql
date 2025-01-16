CREATE TABLE [dbo].[Settings]
(
    [Id] INT NOT NULL IDENTITY,
    [Key] VARCHAR(50) NOT NULL, 
    [Value] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Setting] PRIMARY KEY ([Id]) 
)
