CREATE TABLE [dbo].[User] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (100) NOT NULL,
    [Password]   VARCHAR (100) NOT NULL,
    [LocationId] INT           NOT NULL,
    [Admin] BIT NOT NULL DEFAULT 0, 
    [Status]     BIT           CONSTRAINT [DF_EmployeeTable_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_EmployeeTable] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_User_ToLocation] FOREIGN KEY (LocationId) REFERENCES [Location](Id)
);

