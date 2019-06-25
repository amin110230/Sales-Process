IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procListOfOrdersByBuyer'))
DROP PROCEDURE procListOfOrdersByBuyer
GO

-- EXEC procListOfOrdersByBuyer 2

CREATE PROCEDURE procListOfOrdersByBuyer
@pBuyerId BIGINT
AS

SELECT *
FROM Orders O
INNER JOIN BuyerOrderDetails BOD ON O.Id = BOD.OrderId
INNER JOIN Buyers B ON BOD.BuyersId = B.Id
WHERE B.Id = @pBuyerId
GO