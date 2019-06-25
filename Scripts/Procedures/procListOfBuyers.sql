IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procListOfBuyers'))
DROP PROCEDURE procListOfBuyers
GO

--EXEC procListOfBuyers NULL

CREATE PROCEDURE procListOfBuyers
@pIsActive BIT NULL
AS

SELECT * FROM Buyers
WHERE IsActive = @pIsActive OR @pIsActive IS NULL
ORDER BY Id DESC

GO