IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procInsertUpdateProduct'))
DROP PROCEDURE procInsertUpdateProduct
GO

CREATE PROCEDURE procInsertUpdateProduct
@pId BIGINT,
@pProductName NVARCHAR(128),
@pProductCode NVARCHAR(16),
@pProductUnit NVARCHAR(32),
@pProductPrice FLOAT,
@pStockQuantity INT,
@pIsUpdate BIT
AS

IF(@pIsUpdate = 0)
BEGIN
	INSERT INTO Products VALUES(@pProductName, @pProductCode, @pProductUnit, @pProductPrice, @pStockQuantity, GETDATE(), 1)
END

ELSE
BEGIN
	UPDATE p
	SET p.ProductName = @pProductName,
		p.ProductCode = @pProductCode,
		p.ProductUnit = @pProductUnit,
		p.ProductPrice = @pProductPrice,
		p.StockQuantity = @pStockQuantity,
		p.IsAvailable = 1
	FROM Products p WHERE p.Id = @pId
END
GO