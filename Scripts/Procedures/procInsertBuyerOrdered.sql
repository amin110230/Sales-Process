IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procInsertBuyerOrdered'))
DROP PROCEDURE procInsertBuyerOrdered
GO

--EXEC procInsertBuyerOrdered

CREATE PROCEDURE procInsertBuyerOrdered
	@pBuyerId BIGINT,
	@pOrderId BIGINT,
	@pBuyerPCT FLOAT,
	@pTotalPrice NUMERIC(18,2)
	AS

	DECLARE @vPayableAmount NUMERIC(18,2)
	SET @vPayableAmount = @pTotalPrice * (@pBuyerPCT / 100)

	INSERT INTO BuyerOrderDetails
	VALUES(@pOrderId, @pBuyerId, @pBuyerPCT, @vPayableAmount, 0, GETDATE())

GO