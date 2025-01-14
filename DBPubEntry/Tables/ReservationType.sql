CREATE TABLE [dbo].ReservationType (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (10) NOT NULL,
    [Status] BIT NOT NULL DEFAULT 1, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

