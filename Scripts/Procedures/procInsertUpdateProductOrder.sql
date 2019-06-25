IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procInsertAndUpdateProductOrder'))
DROP PROCEDURE procInsertAndUpdateProductOrder
GO

--EXEC procInsertUpdateProductOrder

CREATE PROCEDURE procInsertAndUpdateProductOrder
	@pProductId BIGINT,
	@pOrderId BIGINT,
	@pOrderedQuantity INT
	AS

	DECLARE @vProductPrice FLOAT
	SET @vProductPrice = (SELECT ProductPrice FROM Products WHERE Id = @pProductId)

	INSERT INTO ProductOrderDetaits 
	VALUES(@pOrderId, @pProductId, @pOrderedQuantity, (@pOrderedQuantity * @vProductPrice), GETDATE())

	UPDATE P
	SET p.StockQuantity = p.StockQuantity - @pOrderedQuantity
	FROM Products P
	WHERE p.Id = @pProductId

	INSERT INTO ProductLogs
	VALUES(@pProductId, @pOrderId, @pOrderedQuantity, 'ORDERED', GETDATE())

GO