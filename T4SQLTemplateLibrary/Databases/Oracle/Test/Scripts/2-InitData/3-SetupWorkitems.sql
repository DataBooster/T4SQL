--	Run after your T4SQL Template Engine (either Windows Service or Console Test) has started.

DECLARE
	tExists	PLS_INTEGER;
BEGIN
	SELECT COUNT(*) INTO tExists FROM test.sample_workspace;

	IF tExists = 0 THEN
		insert into test.sample_workspace (WORKITEM_NAME, TEMPLATE_NAME, WORKITEM_DESCRIPTION, WORKITEM_USER, BUILD_ORDER)
		VALUES ('testVTimePointsToRanges', 'T4SQL.Date.VTimePointsToRanges', 'Test example of T4SQL.Date.VTimePointsToRanges', 'test', 10);

		insert into test.sample_workspace (WORKITEM_NAME, TEMPLATE_NAME, WORKITEM_DESCRIPTION, WORKITEM_USER, BUILD_ORDER)
		VALUES ('testVTimeRangesToSeries', 'T4SQL.Date.VTimeRangesToSeries', 'Test example of T4SQL.Date.VTimeRangesToSeries', 'test', 20);

		insert into test.sample_workspace (WORKITEM_NAME, TEMPLATE_NAME, WORKITEM_DESCRIPTION, WORKITEM_USER, BUILD_ORDER)
		VALUES ('testVTimePointsToSeries', 'T4SQL.Date.VTimePointsToSeries', 'Test example of T4SQL.Date.VTimePointsToSeries', 'test', 30);

		insert into test.sample_workspace (WORKITEM_NAME, TEMPLATE_NAME, WORKITEM_DESCRIPTION, WORKITEM_USER, BUILD_ORDER)
		VALUES ('testVTimeSeriesToRanges', 'T4SQL.Date.VTimeSeriesToRanges', 'Test example of T4SQL.Date.VTimeSeriesToRanges', 'test', 40);

		insert into test.sample_workspace (WORKITEM_NAME, TEMPLATE_NAME, WORKITEM_DESCRIPTION, WORKITEM_USER, BUILD_ORDER)
		VALUES ('testVTimeRangesCheckSum', 'T4SQL.Date.VTimeRangesCheckSum', 'Test example of T4SQL.Date.VTimeRangesCheckSum', 'test', 50);

		insert into test.sample_workspace (WORKITEM_NAME, TEMPLATE_NAME, WORKITEM_DESCRIPTION, WORKITEM_USER, BUILD_ORDER)
		VALUES ('testVPivot', 'T4SQL.Pivot.VPivot', 'Test example of T4SQL.Pivot.VPivot', 'test', 60);

		insert into test.sample_workspace (WORKITEM_NAME, TEMPLATE_NAME, WORKITEM_DESCRIPTION, WORKITEM_USER, BUILD_ORDER)
		VALUES ('testVUnpivot', 'T4SQL.Pivot.VUnpivot', 'Test example of T4SQL.Pivot.VUnpivot', 'test', 70);

		insert into test.sample_workspace (WORKITEM_NAME, TEMPLATE_NAME, WORKITEM_DESCRIPTION, WORKITEM_USER, BUILD_ORDER)
		VALUES ('testVGroupingSets', 'T4SQL.Grouping.VGroupingSets', 'Test example of T4SQL.Grouping.VGroupingSets', 'test', 80);

		insert into test.sample_workspace (WORKITEM_NAME, TEMPLATE_NAME, WORKITEM_DESCRIPTION, WORKITEM_USER, BUILD_ORDER)
		VALUES ('testVNaviForeignKey', 'T4SQL.Assoc.VNaviForeignKey', 'Test example of T4SQL.Assoc.VNaviForeignKey', 'test', 90);

	END IF;
END;
