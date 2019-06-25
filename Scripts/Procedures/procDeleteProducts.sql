IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procDeleteProducts'))
DROP PROCEDURE procDeleteProducts
GO

--EXEC procDeleteProducts 1

CREATE PROCEDURE procDeleteProducts
@pId BIGINT

AS
	UPDATE P
	SET P.IsAvailable = 0
	FROM Products P
	WHERE P.Id = @pId
GO