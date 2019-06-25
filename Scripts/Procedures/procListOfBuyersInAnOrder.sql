IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procListOfBuyersInAnOrder'))
DROP PROCEDURE procListOfBuyersInAnOrder
GO

-- EXEC procListOfBuyersInAnOrder 1

CREATE PROCEDURE procListOfBuyersInAnOrder
@pId BIGINT
AS

SELECT BOD.Id, B.BuyersName, BOD.AmountPayable AS TotalAmount, BOD.PaidAmount,
	(BOD.AmountPayable - BOD.PaidAmount) AS DueAmount
FROM Orders O
INNER JOIN BuyerOrderDetails BOD ON O.Id = BOD.OrderId
INNER JOIN Buyers B ON BOD.BuyersId = B.Id
WHERE O.Id = @pId
GO