-- =============================================
-- Script Template
-- =============================================
SET NOCOUNT ON;

IF (SELECT COUNT(*) FROM dbo.UTL_ORDINAL_NUMBER) = 0
BEGIN
	DECLARE	@tID INT = 1;

	WHILE @tID <= 32768
	BEGIN
		INSERT INTO dbo.UTL_ORDINAL_NUMBER (ORDINAL_NUMBER)
		VALUES (@tID);

		SET @tID = @tID + 1;
	END;
END;
