IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procMakePayment'))
DROP PROCEDURE procMakePayment
GO

--EXEC procMakePayment

CREATE PROCEDURE procMakePayment
@pBuyerOrderId BIGINT,
@pBuyersPaidAmount NUMERIC(18,2)
AS

	UPDATE BOD
	SET BOD.PaidAmount = BOD.PaidAmount + @pBuyersPaidAmount
	FROM BuyerOrderDetails BOD
	WHERE BOD.Id = @pBuyerOrderId

	IF(@pBuyersPaidAmount > 0)
	BEGIN
		INSERT INTO PaymentLogs
		VALUES(@pBuyerOrderId, @pBuyersPaidAmount, GETDATE())
	END

GO