IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procListOfBuyersOrderedByOrder'))
DROP PROCEDURE procListOfBuyersOrderedByOrder
GO

--EXEC procListOfBuyersOrderedByOrder 1

CREATE PROCEDURE procListOfBuyersOrderedByOrder
@pOrderId BIGINT
AS

	SELECT B.BuyersName, BOD.PayablePCT, BOD.AmountPayable, (BOD.AmountPayable - BOD.PaidAmount) AS DueAmount
	FROM BuyerOrderDetails BOD
	INNER JOIN Orders O ON BOD.OrderId = O.Id
	INNER JOIN Buyers B ON BOD.BuyersId = B.Id
	WHERE BOD.OrderId = @pOrderId

GO