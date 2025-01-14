CREATE TABLE [dbo].[Transaction] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [PersonId]        INT          NOT NULL,
    [Male]            INT          NOT NULL,
    [Female]          INT          NOT NULL,
    [Cash]            INT          NOT NULL,
    [Card]            INT          NOT NULL,
    [UPI]             INT          NOT NULL,
    [Amex]            INT          NOT NULL,
    [ReservationTypeId] INT          NOT NULL,
    [DateTime]        DATETIME     DEFAULT (((getdate() AT TIME ZONE 'UTC') AT TIME ZONE 'India Standard Time')) NOT NULL,
    [ApprovedBy]      VARCHAR (50) NULL,
    [LocationId]      INT          NOT NULL,
    [UserId]          INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Transaction_ToPerson] FOREIGN KEY (PersonId) REFERENCES [Person](Id),
    CONSTRAINT [FK_Transaction_ToRservationType] FOREIGN KEY ([ReservationTypeId]) REFERENCES [ReservationType](Id), 
    CONSTRAINT [FK_Transaction_ToReservationType] FOREIGN KEY (ReservationTypeId) REFERENCES ReservationType(Id),
    CONSTRAINT [FK_Transaction_ToLocation] FOREIGN KEY (LocationId) REFERENCES [Location](Id), 
    CONSTRAINT [FK_Transaction_ToUser] FOREIGN KEY (UserId) REFERENCES [User](Id)
);

