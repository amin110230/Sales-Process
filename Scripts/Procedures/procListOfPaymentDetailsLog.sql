IF EXISTS(SELECT * FROM sys.objects WHERE object_id=OBJECT_ID('procListOfPaymentDetailsLog'))
DROP PROCEDURE procListOfPaymentDetailsLog
GO

-- EXEC procListOfPaymentDetailsLog 2, 0

CREATE PROCEDURE procListOfPaymentDetailsLog
@pBuyersId BIGINT,
@pOrderId BIGINT
AS
	SELECT O.OrderId, BOD.AmountPayable, PL.PaidAmount, PL.CreatedOn
	FROM PaymentLogs PL
	INNER JOIN BuyerOrderDetails BOD ON PL.BuyerOrderDetailsId = BOD.Id
	INNER JOIN Buyers B ON BOD.BuyersId = B.Id
	INNER JOIN Orders O ON BOD.OrderId = O.Id
	WHERE (O.Id = @pOrderId OR @pOrderId = 0) AND B.Id = @pBuyersId
GO