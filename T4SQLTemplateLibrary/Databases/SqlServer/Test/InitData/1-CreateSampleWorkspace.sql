IF OBJECT_ID(N'test.sample_workspace') IS NULL AND OBJECT_ID(N'test.sample_properties') IS NULL
	EXECUTE T4SQL.META_CREATE_WORKSPACE @inWorkitem_Table = N'test.sample_workspace', @inProperty_Table = N'test.sample_properties', @inWorkspace_Description = N'Sample Workspace', @inAutonomous_Owner = N'test';
