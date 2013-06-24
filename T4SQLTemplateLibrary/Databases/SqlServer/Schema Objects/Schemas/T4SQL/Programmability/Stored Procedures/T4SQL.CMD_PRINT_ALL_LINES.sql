CREATE PROCEDURE T4SQL.CMD_PRINT_ALL_LINES
(
	@inMessage	NVARCHAR(MAX)
)
AS
	SET NOCOUNT ON

	IF @inMessage IS NULL OR @inMessage = N''
		RETURN;

	DECLARE @tNewLine NVARCHAR(3), @tNL INT;
	
	SET @tNewLine = N'
';
	SET @tNL = LEN(@tNewLine);

	DECLARE @tSegLen INT, @tTextLen INT, @tStart INT, @tEnd INT;

	SET @tSegLen = 3000;
	SET @tTextLen = LEN(@inMessage);
	SET @tStart = 1;

	WHILE 1 = 1
	BEGIN
		SET @tEnd = @tStart + @tSegLen

		IF @tEnd < @tTextLen
		BEGIN
			SET @tEnd = CHARINDEX(@tNewLine, @inMessage, @tEnd);
			IF @tEnd > 0
			BEGIN
				PRINT SUBSTRING(@inMessage, @tStart, @tEnd - @tStart);
				SET @tStart = @tEnd + @tNL;
				CONTINUE;
			END
		END;

		PRINT SUBSTRING(@inMessage, @tStart, @tTextLen - @tStart + 1);
		BREAK;
	END;
	
----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		June ‎23, ‎2013, ‏‎11:24:38 PM
--	Primary Host:		http://t4sql.codeplex.com
--	Change Log:
--	Author				Date			Comment
--
--
--
--
--	(Keep code clean)
--
----------------------------------------------------------------------------------------------------
