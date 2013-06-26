﻿IF NOT EXISTS (SELECT 1 FROM T4SQL.WORKSPACE_ENTRY WHERE OBJECT_ID(WORKITEM_TABLE_NAME, N'U') = OBJECT_ID(N'test.sample_workspace') AND OBJECT_ID(PROPERTY_TABLE_NAME) = OBJECT_ID(N'test.sample_properties'))
INSERT INTO T4SQL.WORKSPACE_ENTRY
(
	WORKITEM_TABLE_NAME,
	PROPERTY_TABLE_NAME,
	AUTONOMOUS_OWNER,
	WORKSPACE_DESCRIPTION
)
VALUES
(
	N'test.sample_workspace',
	N'test.sample_properties',
	N'test',
	N'Sample Workspace'
);
