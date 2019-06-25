IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('ProductOrderDetaits'))
BEGIN

CREATE TABLE [dbo].[ProductOrderDetaits]
(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	OrderId BIGINT NOT NULL,
	ProductId BIGINT NOT NULL,
	Quantity INT NOT NULL,
	Price NUMERIC(18,2) NOT NULL,

	PRIMARY KEY(Id),	
	CONSTRAINT [FK_ProductOrderDetaits_Orders] FOREIGN KEY(OrderId) REFERENCES Orders(Id),
	CONSTRAINT [FK_ProductOrderDetaits_Products] FOREIGN KEY(ProductId) REFERENCES Products(Id)
)
END
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE object_id=OBJECT_ID('ProductOrderDetaits') AND name='CreatedOn')
BEGIN
	ALTER TABLE ProductOrderDetaits ADD CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
END