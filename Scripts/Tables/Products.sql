IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('Products'))
BEGIN

CREATE TABLE [dbo].[Products]
(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	ProductName NVARCHAR(128) NOT NULL,
	ProductCode NVARCHAR(16) NOT NULL,
	ProductUnit NVARCHAR(32) NOT NULL,
	ProductPrice FLOAT NOT NULL,
	StockQuantity INT NOT NULL,

	PRIMARY KEY(Id)
)
END
GO


IF NOT EXISTS(SELECT * FROM sys.columns WHERE object_id=OBJECT_ID('Products') AND name='CreatedOn')
BEGIN
	ALTER TABLE Products ADD CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE object_id=OBJECT_ID('Products') AND name='IsAvailable')
BEGIN
	ALTER TABLE Products ADD IsAvailable BIT DEFAULT 1 NOT NULL
END