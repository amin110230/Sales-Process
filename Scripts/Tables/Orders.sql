IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('Orders'))
BEGIN

CREATE TABLE [dbo].[Orders]
(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	OrderId NVARCHAR(128) NOT NULL,
	OrderDate DATETIME NOT NULL,
	OrderTotalAmount NUMERIC(18,2) NOT NULL,

	PRIMARY KEY(Id)
)
END
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE object_id=OBJECT_ID('Orders') AND name='CreatedOn')
BEGIN
	ALTER TABLE Orders ADD CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE object_id=OBJECT_ID('Orders') AND name='IsPaid')
BEGIN
	ALTER TABLE Orders ADD IsPaid BIT DEFAULT 0 NOT NULL
END