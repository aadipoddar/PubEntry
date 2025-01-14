CREATE TABLE [dbo].AdvanceDetail (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [AdvanceId]   INT NOT NULL,
    [PaymentModeId] INT NOT NULL,
    [Amount]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_AdvanceDetail_ToAdvance] FOREIGN KEY (AdvanceId) REFERENCES [Advance](Id), 
    CONSTRAINT [FK_AdvanceDetail_ToPaymentMode] FOREIGN KEY ([PaymentModeId]) REFERENCES [PaymentMode](Id)
);

