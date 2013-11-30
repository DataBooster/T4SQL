if not exists (select top 1 null from test.sample_workspace)
begin
	insert into test.sample_workspace
	(
		WORKITEM_NAME,
		TEMPLATE_NAME,
		WORKITEM_DESCRIPTION,
		WORKITEM_USER,
		BUILD_ORDER
	)
	values
		(N'testVTimePointsToRanges', 'T4SQL.Date.VTimePointsToRanges', N'Test example of T4SQL.Date.VTimePointsToRanges', N'test', 10),
		(N'testVTimeRangesToSeries', 'T4SQL.Date.VTimeRangesToSeries', N'Test example of T4SQL.Date.VTimeRangesToSeries', N'test', 20),
		(N'testVTimePointsToSeries', 'T4SQL.Date.VTimePointsToSeries', N'Test example of T4SQL.Date.VTimePointsToSeries', N'test', 30),
		(N'testVTimeSeriesToRanges', 'T4SQL.Date.VTimeSeriesToRanges', N'Test example of T4SQL.Date.VTimeSeriesToRanges', N'test', 40),
		(N'testVTimeRangesCheckSum', 'T4SQL.Date.VTimeRangesCheckSum', N'Test example of T4SQL.Date.VTimeRangesCheckSum', N'test', 50),
		(N'testVPivot', 'T4SQL.Pivot.VPivot', N'Test example of T4SQL.Pivot.VPivot', N'test', 60),
		(N'testVUnpivot', 'T4SQL.Pivot.VUnpivot', N'Test example of T4SQL.Pivot.VUnpivot', N'test', 70),
		(N'testVGroupingSets', 'T4SQL.Grouping.VGroupingSets', N'Test example of T4SQL.Grouping.VGroupingSets', N'test', 80),
		(N'testVNaviForeignKey', 'T4SQL.Assoc.VNaviForeignKey', N'Test example of T4SQL.Assoc.VNaviForeignKey', N'test', 90),
		(N'testVFullPivot', 'T4SQL.Pivot.VFullPivot', N'Test example of T4SQL.Pivot.VFullPivot', N'test', 100)
end
