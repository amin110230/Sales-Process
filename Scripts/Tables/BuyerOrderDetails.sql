IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('BuyerOrderDetails'))
BEGIN

CREATE TABLE [dbo].[BuyerOrderDetails]
(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	OrderId BIGINT NOT NULL,
	BuyersId BIGINT NOT NULL,
	PayablePCT FLOAT NOT NULL,
	AmountPayable NUMERIC(18,2) NOT NULL,
	PaidAmount NUMERIC(18,2) NOT NULL,
	CreatedOn DATETIME NOT NULL,

	PRIMARY KEY(Id),
	CONSTRAINT [FK_BuyerOrderDetails_Orders] FOREIGN KEY(OrderId) REFERENCES Orders(Id),
	CONSTRAINT [FK_BuyerOrderDetails_Buyers] FOREIGN KEY(BuyersId) REFERENCES Buyers(Id)
)
END
GO