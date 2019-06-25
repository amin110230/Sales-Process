IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procInsertUpdateBuyer'))
DROP PROCEDURE procInsertUpdateBuyer
GO

CREATE PROCEDURE procInsertUpdateBuyer
@pId BIGINT,
@pBuyersName NVARCHAR(128),
@pBuyersCode NVARCHAR(16),
@pBuyersRegion NVARCHAR(32),
@pBuyersMobile NVARCHAR(14),
@pBuyersEmail NVARCHAR(32),
@pIsUpdate BIT
AS

IF(@pIsUpdate = 0)
BEGIN
	INSERT INTO Buyers VALUES(@pBuyersName, @pBuyersCode, @pBuyersRegion, @pBuyersMobile, @pBuyersEmail, GETDATE(), 1)
END

ELSE
BEGIN
	UPDATE b
	SET b.BuyersName = @pBuyersName,
		b.BuyersCode = @pBuyersCode,
		b.BuyersRegion = @pBuyersRegion,
		b.BuyersMobile = @pBuyersMobile,
		b.BuyersEmail = @pBuyersEmail,
		b.IsActive = 1
	FROM Buyers b WHERE b.Id = @pId
END
GO