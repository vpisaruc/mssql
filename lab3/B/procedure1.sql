-- Хранимую процедуру без параметров или с параметрами
USE supermarket
GO

IF OBJECT_ID ( N'dbo.selectCashbox', 'P' ) IS NOT NULL
      DROP PROCEDURE dbo.selectCashbox
GO
CREATE PROCEDURE dbo.selectCashbox @transactionAmount INT OUTPUT AS
BEGIN
      SELECT t.id, t."date", t.paymentAmount
      FROM tbTransaction  t
      WHERE t.cashboxNumber = 1

      SET @transactionAmount = @@ROWCOUNT
      RETURN (SELECT AVG(t.paymentAmount) from tbTransaction t)
END
GO

DECLARE @OutParm INT, @RetVal INT
EXEC @RetVal = dbo.selectCashbox @OutParm OUTPUT
SELECT @OutParm "Колличество транзакций на 1 кассе", @RetVal "Средний чек" 
GO