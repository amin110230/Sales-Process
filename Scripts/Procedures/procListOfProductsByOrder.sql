IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procListOfProductsOrderedByOrder'))
DROP PROCEDURE procListOfProductsOrderedByOrder
GO

--EXEC procListOfProductsOrderedByOrder 1

CREATE PROCEDURE procListOfProductsOrderedByOrder
@pOrderId BIGINT
AS

	SELECT P.ProductName, POD.Quantity, POD.Price
	FROM ProductOrderDetaits POD
	INNER JOIN Orders O ON POD.OrderId = O.Id
	INNER JOIN Products P ON POD.ProductId = P.Id
	WHERE POD.OrderId = @pOrderId

GO