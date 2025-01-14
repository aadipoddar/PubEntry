CREATE TABLE [dbo].Advance (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [LocationId]    INT          NOT NULL,
    [PersonId]      INT          NOT NULL,
    [DateTime]      DATETIME     DEFAULT (((getdate() AT TIME ZONE 'UTC') AT TIME ZONE 'India Standard Time')) NOT NULL,
    [AdvanceDate]   DATE         NOT NULL,
    [Booking]       INT          NOT NULL,
    [ApprovedBy]    VARCHAR (50) NULL,
    [TransactionId] INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Advance_ToLocation] FOREIGN KEY (LocationId) REFERENCES [Location](Id), 
    CONSTRAINT [FK_Advance_ToPerson] FOREIGN KEY (PersonId) REFERENCES [Person](Id)
);

