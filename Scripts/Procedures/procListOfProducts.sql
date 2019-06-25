IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procListOfProducts'))
DROP PROCEDURE procListOfProducts
GO

--EXEC procListOfProducts true

CREATE PROCEDURE procListOfProducts
@pIsAvailable BIT NULL
AS

SELECT * FROM Products
WHERE IsAvailable = @pIsAvailable OR @pIsAvailable IS NULL
ORDER BY ProductName

GO