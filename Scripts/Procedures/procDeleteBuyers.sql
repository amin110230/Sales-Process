IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procDeleteBuyers'))
DROP PROCEDURE procDeleteBuyers
GO

--EXEC procListOfBuyers 1

CREATE PROCEDURE procDeleteBuyers
@pId BIGINT
AS
	UPDATE B
	SET B.IsActive = 0
	FROM Buyers B
	WHERE B.Id = @pId
GO