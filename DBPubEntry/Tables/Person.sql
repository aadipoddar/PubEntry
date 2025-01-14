CREATE TABLE [dbo].Person (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (250) NOT NULL,
    [Number]  VARCHAR (20)  NOT NULL,
    [Loyalty] BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

