IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('PaymentLogs'))
BEGIN

CREATE TABLE [dbo].[PaymentLogs]
(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	BuyerOrderDetailsId BIGINT NOT NULL,
	PaidAmount NUMERIC(18,2),
	CreatedOn DATETIME NOT NULL,

	PRIMARY KEY(Id),
	CONSTRAINT [FK_PaymentLogs_BuyerOrderDetails] FOREIGN KEY(BuyerOrderDetailsId) REFERENCES BuyerOrderDetails(Id)
)
END
GO