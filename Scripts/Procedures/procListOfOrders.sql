IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procListOfOrders'))
DROP PROCEDURE procListOfOrders
GO

--EXEC procListOfOrders null

CREATE PROCEDURE procListOfOrders
@pIsPaid BIT NULL
AS

SELECT * FROM Orders
WHERE IsPaid = @pIsPaid OR @pIsPaid IS NULL
ORDER BY OrderDate

GO