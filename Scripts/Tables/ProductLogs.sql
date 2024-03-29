IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('ProductLogs'))
BEGIN

CREATE TABLE [dbo].[ProductLogs]
(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	ProductId BIGINT NOT NULL,
	OrderId BIGINT NOT NULL,
	Quantity INT NOT NULL,
	[Action] VARCHAR(12) NOT NULL,
	CreatedOn DATETIME NULL,

	PRIMARY KEY(Id),
	CONSTRAINT [FK_ProductLogs_Products] FOREIGN KEY(ProductId) REFERENCES Products(Id)
)
END
GO