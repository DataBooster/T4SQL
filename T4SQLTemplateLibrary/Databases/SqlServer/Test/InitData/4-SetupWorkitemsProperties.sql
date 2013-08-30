
--  testVTimePointsToRanges
update	test.sample_properties
set		STRING_VALUE	= N'test.date_time_points'
where	PROPERTY_NAME	= N'SourceView'
	and	WORKITEM_NAME	= N'testVTimePointsToRanges';

update	test.sample_properties
set		STRING_VALUE	= N'test.vw_timepoints_ranges'
where	PROPERTY_NAME	= N'ObjectView'
	and	WORKITEM_NAME	= N'testVTimePointsToRanges';

update	test.sample_properties
set		STRING_VALUE	= N'catalog_id, position_id'
where
		PROPERTY_NAME	= N'KeyColumns'
	and	WORKITEM_NAME	= N'testVTimePointsToRanges';


--  testVTimeRangesToSeries
update	test.sample_properties
set		STRING_VALUE	= N'test.vw_timepoints_ranges'
where	PROPERTY_NAME	= N'SourceView'
	and	WORKITEM_NAME	= N'testVTimeRangesToSeries';

update	test.sample_properties
set		STRING_VALUE	= N'test.vw_timeranges_series'
where	PROPERTY_NAME	= N'ObjectView'
	and	WORKITEM_NAME	= N'testVTimeRangesToSeries';


--	testVTimePointsToSeries
update	test.sample_properties
set		STRING_VALUE	= N'test.date_time_points'
where	PROPERTY_NAME	= N'SourceView'
	and	WORKITEM_NAME	= N'testVTimePointsToSeries';

update	test.sample_properties
set		STRING_VALUE	= N'test.vw_timepoints_series'
where	PROPERTY_NAME	= N'ObjectView'
	and	WORKITEM_NAME	= N'testVTimePointsToSeries';

update	test.sample_properties
set		STRING_VALUE	= N'catalog_id, position_id'
where
		PROPERTY_NAME	= N'KeyColumns'
	and	WORKITEM_NAME	= N'testVTimePointsToSeries';


--  testVTimeSeriesToRanges
update	test.sample_properties
set		STRING_VALUE	= N'test.vw_timepoints_series'
where	PROPERTY_NAME	= N'SourceView'
	and	WORKITEM_NAME	= N'testVTimeSeriesToRanges';

update	test.sample_properties
set		STRING_VALUE	= N'test.vw_timeseries_ranges'
where	PROPERTY_NAME	= N'ObjectView'
	and	WORKITEM_NAME	= N'testVTimeSeriesToRanges';


--  testVTimeRangesCheckSum
update	test.sample_properties
set		STRING_VALUE	= N'test.vw_timepoints_ranges'
where	PROPERTY_NAME	= N'SourceView'
	and	WORKITEM_NAME	= N'testVTimeRangesCheckSum';

update	test.sample_properties
set		STRING_VALUE	= N'test.vw_timeranges_chksum'
where	PROPERTY_NAME	= N'ObjectView'
	and	WORKITEM_NAME	= N'testVTimeRangesCheckSum';

update	test.sample_properties
set		STRING_VALUE	= N'catalog_id, position_id'
where
		PROPERTY_NAME	= N'KeyColumns'
	and	WORKITEM_NAME	= N'testVTimeRangesCheckSum';


--  testVPivot
update	test.sample_properties
set		STRING_VALUE	= N'test.pivot_discrete_attrib'
where	PROPERTY_NAME	= N'SourceView'
	and	WORKITEM_NAME	= N'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= N'test.vw_pivot'
where	PROPERTY_NAME	= N'ObjectView'
	and	WORKITEM_NAME	= N'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= N'product_id'
where
		PROPERTY_NAME	= N'NonPivotedColumns'
	and	WORKITEM_NAME	= N'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= N'attrib_code'
where
		PROPERTY_NAME	= N'PivotColumn'
	and	WORKITEM_NAME	= N'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= N'value_'
where
		PROPERTY_NAME	= N'ValueColumn'
	and	WORKITEM_NAME	= N'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= N'[FIELD_A] AS V_A, [FIELD_B] AS V_B, FIELD_C AS V_C, FIELD_D as V_D, FIELD_E, FIELD_F, FIELD_G, FIELD_H'
where
		PROPERTY_NAME	= N'ValueList'
	and	WORKITEM_NAME	= N'testVPivot';


--  testVUnpivot
update	test.sample_properties
set		STRING_VALUE	= N'test.vw_pivot'
where	PROPERTY_NAME	= N'SourceView'
	and	WORKITEM_NAME	= N'testVUnpivot';

update	test.sample_properties
set		STRING_VALUE	= N'test.vw_unpivot'
where	PROPERTY_NAME	= N'ObjectView'
	and	WORKITEM_NAME	= N'testVUnpivot';

update	test.sample_properties
set		STRING_VALUE	= N'product_id'
where
		PROPERTY_NAME	= N'NonPivotedColumns'
	and	WORKITEM_NAME	= N'testVUnpivot';

update	test.sample_properties
set		STRING_VALUE	= N'attrib_code'
where	PROPERTY_NAME	= N'PivotColumn'
	and	WORKITEM_NAME	= N'testVUnpivot';

update	test.sample_properties
set		STRING_VALUE	= N'value_'
where	PROPERTY_NAME	= N'ValueColumn'
	and	WORKITEM_NAME	= N'testVUnpivot';


--  testVGroupingSets
update	test.sample_properties
set		STRING_VALUE	= N'test.grouping_flat_values'
where	PROPERTY_NAME	= N'SourceView'
	and	WORKITEM_NAME	= N'testVGroupingSets';

update	test.sample_properties
set		STRING_VALUE	= N'test.vw_grouping_sets'
where	PROPERTY_NAME	= N'ObjectView'
	and	WORKITEM_NAME	= N'testVGroupingSets';

update	test.sample_properties
set		STRING_VALUE	= N'SUM(value1) AS SUM_V1, SUM(value2) AS SUM_V2, SUM(value3) AS SUM_V3, SUM(value4) AS SUM_V4'
where	PROPERTY_NAME	= N'AggregateExprs'
	and	WORKITEM_NAME	= N'testVGroupingSets';

update	test.sample_properties
set		STRING_VALUE	= N'simulation_id'
where	PROPERTY_NAME	= N'SimpleGroupByColumns'
	and	WORKITEM_NAME	= N'testVGroupingSets';

update	test.sample_properties
set		STRING_VALUE	= N'() AS ''TOTAL_FUND'', (is_cash) AS ''AGG_CASH'', (is_cash, prod_code) AS ''AGG_PROD'', (grp1_type, grp2_code) as ''agg_grp12'', (grp2_code, grp3_class) as ''agg_grp23'''
where	PROPERTY_NAME	= N'GroupingSetsColumns'
	and	WORKITEM_NAME	= N'testVGroupingSets';


--	testVNaviForeignKey
update	test.sample_properties
set		STRING_VALUE	= N'test.vw_navi_grp_flat'
where	PROPERTY_NAME	= N'ObjectView'
	and	WORKITEM_NAME	= N'testVNaviForeignKey';

update	test.sample_properties
set		STRING_VALUE	= N'test.grouping_flat_values'
where	PROPERTY_NAME	= N'ForeignKeyBaseTable'
	and	WORKITEM_NAME	= N'testVNaviForeignKey';
