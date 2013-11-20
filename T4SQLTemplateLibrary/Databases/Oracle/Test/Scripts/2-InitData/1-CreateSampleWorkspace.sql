--	Run as a user with execute permission on T4SQL.META package

BEGIN
	IF NOT T4SQL.META.EXISTS_TABLE('test.sample_workspace') AND NOT T4SQL.META.EXISTS_TABLE('test.sample_properties') THEN
		T4SQL.META.CREATE_WORKSPACE('test.sample_workspace', 'test.sample_properties', 'Sample Workspace', 'test');
	END IF;
END;
