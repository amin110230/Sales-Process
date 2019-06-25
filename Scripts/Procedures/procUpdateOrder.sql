IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procUpdateOrder'))
DROP PROCEDURE procUpdateOrder
GO

--EXEC procUpdateOrder

CREATE PROCEDURE procUpdateOrder
@pId BIGINT
AS
	DECLARE @vTotalPaidAmount NUMERIC(18,2)
	SET @vTotalPaidAmount = (SELECT SUM(PaidAmount) FROM BuyerOrderDetails WHERE OrderId = @pId)

	UPDATE O
	SET O.IsPaid = 1
	FROM Orders O
	WHERE O.Id = @pId AND O.OrderTotalAmount = @vTotalPaidAmount

GO