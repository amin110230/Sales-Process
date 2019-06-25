IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procInsertOrder'))
DROP PROCEDURE procInsertOrder
GO

--EXEC procInsertOrder

CREATE PROCEDURE procInsertOrder
@pTotalAmount NUMERIC(18,2)
AS

DECLARE @vOrderId VARCHAR(128)
SET @vOrderId = 'ORD-' + (SELECT CONVERT(VARCHAR(10), getdate(), 110)) + '-' + CONVERT(VARCHAR(20), (SELECT COUNT(*) FROM Orders) + 1)

INSERT INTO Orders VALUES(@vOrderId, GETDATE(), @pTotalAmount, GETDATE(), 0)

SELECT * FROM Orders 
WHERE OrderId = @vOrderId

GO