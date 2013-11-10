-- =============================================
-- Script Template
-- =============================================
SET NOCOUNT ON;

IF (SELECT COUNT(*) FROM T4SQL.UTL_ORDINAL_NUMBER) = 0
BEGIN
	DECLARE	@tID INT = 1;

	WHILE @tID <= 65536
	BEGIN
		INSERT INTO T4SQL.UTL_ORDINAL_NUMBER (ORDINAL_NUMBER)
		VALUES (@tID);

		SET @tID = @tID + 1;
	END;
END;
