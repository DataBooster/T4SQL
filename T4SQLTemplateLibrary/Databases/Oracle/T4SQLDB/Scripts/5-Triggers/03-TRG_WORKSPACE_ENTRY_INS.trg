CREATE OR REPLACE TRIGGER TRG_WORKSPACE_ENTRY_INS
BEFORE INSERT OR UPDATE ON T4SQL.WORKSPACE_ENTRY
FOR EACH ROW
DECLARE
	tWorkitem_Schema		VARCHAR2(30)	:= T4SQL.META.PARSENAME(:new.WORKITEM_TABLE_NAME, 2);
	tWorkitem_Table			VARCHAR2(30)	:= T4SQL.META.PARSENAME(:new.WORKITEM_TABLE_NAME, 1);
	tProperty_Schema		VARCHAR2(30)	:= T4SQL.META.PARSENAME(:new.PROPERTY_TABLE_NAME, 2);
	tProperty_Table			VARCHAR2(30)	:= T4SQL.META.PARSENAME(:new.PROPERTY_TABLE_NAME, 1);
BEGIN
	IF tWorkitem_Schema IS NULL OR tProperty_Schema IS NULL THEN
		RAISE_APPLICATION_ERROR(-21741, 'Schema must be specified in qualified table name');
	END IF;

	IF LENGTH(tWorkitem_Table) > 22 THEN
		RAISE_APPLICATION_ERROR(-20972, 'Table name in WORKITEM_TABLE_NAME is too long (maximum length 22 bytes)');
	END IF;

	IF LENGTH(tProperty_Table) > 22 THEN
		RAISE_APPLICATION_ERROR(-20972, 'Table name in PROPERTY_TABLE_NAME is too long (maximum length 22 bytes)');
	END IF;

	IF NOT T4SQL.META.EXISTS_TABLE(tWorkitem_Schema, tWorkitem_Table) THEN
		RAISE_APPLICATION_ERROR(-20942, 'WORKITEM_TABLE_NAME table does not exist in the database');
	END IF;

	IF NOT T4SQL.META.EXISTS_TABLE(tProperty_Schema, tProperty_Table) THEN
		RAISE_APPLICATION_ERROR(-20942, 'PROPERTY_TABLE_NAME table does not exist in the database');
	END IF;

	IF UPDATING AND :old.WORKITEM_TABLE_NAME = 'T4SQL.SEED_WORKITEM' AND :old.PROPERTY_TABLE_NAME = 'T4SQL.SEED_PROPERTY'
		AND (:new.WORKITEM_TABLE_NAME <> 'T4SQL.SEED_WORKITEM' OR :new.PROPERTY_TABLE_NAME <> 'T4SQL.SEED_PROPERTY') THEN
		RAISE_APPLICATION_ERROR(-21001, 'Built-in workspace T4SQL.SEED_... cannot be removed!');
	END IF;

	:new.WORKITEM_TABLE_NAME	:= tWorkitem_Schema || '.' || tWorkitem_Table;
	:new.PROPERTY_TABLE_NAME	:= tProperty_Schema || '.' || tProperty_Table;

	-- Compare new workspace with SEED workspace to check compatibility of table definitions.
	IF NOT T4SQL.META.MATCH_TABLE_DEFINITION('T4SQL', 'SEED_WORKITEM', tWorkitem_Schema, tWorkitem_Table) OR
		NOT T4SQL.META.MATCH_TABLE_DEFINITION('T4SQL', 'SEED_PROPERTY', tProperty_Schema, tProperty_Table) THEN
		RAISE_APPLICATION_ERROR(-21002, 'Tables definition of add-in workspace is not compatible with built-in workspace T4SQL.SEED_...!');
	END IF;

	-- Create trigger for each new workspace to auto-copy properties' default values while inserting a new workitem.
	T4SQL.META.CREATE_WORKITEM_TRIGGER(:new.WORKITEM_TABLE_NAME);
END;
/
