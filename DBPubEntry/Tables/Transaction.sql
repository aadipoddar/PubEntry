CREATE TABLE [dbo].[Transaction] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [PersonId]        INT          NOT NULL,
    [Male]            INT          NOT NULL,
    [Female]          INT          NOT NULL,
    [Cash]            INT          NOT NULL DEFAULT 0,
    [Card]            INT          NOT NULL DEFAULT 0,
    [UPI]             INT          NOT NULL DEFAULT 0,
    [Amex]            INT          NOT NULL DEFAULT 0,
    [OnlineQR]        INT          NOT NULL DEFAULT 0, 
    [ReservationTypeId] INT          NOT NULL,
    [DateTime]        DATETIME     DEFAULT (((getdate() AT TIME ZONE 'UTC') AT TIME ZONE 'India Standard Time')) NOT NULL,
    [ApprovedBy]      VARCHAR (250) NULL,
    [LocationId]      INT          NOT NULL,
    [UserId]          INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Transaction_ToPerson] FOREIGN KEY (PersonId) REFERENCES [Person](Id),
    CONSTRAINT [FK_Transaction_ToReservationType] FOREIGN KEY (ReservationTypeId) REFERENCES ReservationType(Id),
    CONSTRAINT [FK_Transaction_ToLocation] FOREIGN KEY (LocationId) REFERENCES [Location](Id), 
    CONSTRAINT [FK_Transaction_ToUser] FOREIGN KEY (UserId) REFERENCES [User](Id)
);

