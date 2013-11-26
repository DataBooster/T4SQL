
--  testVTimePointsToRanges
update	test.sample_properties
set		STRING_VALUE	= 'test.date_time_points'
where	PROPERTY_NAME	= 'SourceView'
	and	WORKITEM_NAME	= 'testVTimePointsToRanges';

update	test.sample_properties
set		STRING_VALUE	= 'test.vw_timepoints_ranges'
where	PROPERTY_NAME	= 'ObjectView'
	and	WORKITEM_NAME	= 'testVTimePointsToRanges';

update	test.sample_properties
set		STRING_VALUE	= 'catalog_id, position_id'
where
		PROPERTY_NAME	= 'KeyColumns'
	and	WORKITEM_NAME	= 'testVTimePointsToRanges';


--  testVTimeRangesToSeries
update	test.sample_properties
set		STRING_VALUE	= 'test.vw_timepoints_ranges'
where	PROPERTY_NAME	= 'SourceView'
	and	WORKITEM_NAME	= 'testVTimeRangesToSeries';

update	test.sample_properties
set		STRING_VALUE	= 'test.vw_timeranges_series'
where	PROPERTY_NAME	= 'ObjectView'
	and	WORKITEM_NAME	= 'testVTimeRangesToSeries';


--	testVTimePointsToSeries
update	test.sample_properties
set		STRING_VALUE	= 'test.date_time_points'
where	PROPERTY_NAME	= 'SourceView'
	and	WORKITEM_NAME	= 'testVTimePointsToSeries';

update	test.sample_properties
set		STRING_VALUE	= 'test.vw_timepoints_series'
where	PROPERTY_NAME	= 'ObjectView'
	and	WORKITEM_NAME	= 'testVTimePointsToSeries';

update	test.sample_properties
set		STRING_VALUE	= 'catalog_id, position_id'
where
		PROPERTY_NAME	= 'KeyColumns'
	and	WORKITEM_NAME	= 'testVTimePointsToSeries';


--  testVTimeSeriesToRanges
update	test.sample_properties
set		STRING_VALUE	= 'test.vw_timepoints_series'
where	PROPERTY_NAME	= 'SourceView'
	and	WORKITEM_NAME	= 'testVTimeSeriesToRanges';

update	test.sample_properties
set		STRING_VALUE	= 'test.vw_timeseries_ranges'
where	PROPERTY_NAME	= 'ObjectView'
	and	WORKITEM_NAME	= 'testVTimeSeriesToRanges';


--  testVTimeRangesCheckSum
update	test.sample_properties
set		STRING_VALUE	= 'test.vw_timepoints_ranges'
where	PROPERTY_NAME	= 'SourceView'
	and	WORKITEM_NAME	= 'testVTimeRangesCheckSum';

update	test.sample_properties
set		STRING_VALUE	= 'test.vw_timeranges_chksum'
where	PROPERTY_NAME	= 'ObjectView'
	and	WORKITEM_NAME	= 'testVTimeRangesCheckSum';

update	test.sample_properties
set		STRING_VALUE	= 'catalog_id, position_id'
where
		PROPERTY_NAME	= 'KeyColumns'
	and	WORKITEM_NAME	= 'testVTimeRangesCheckSum';


--  testVPivot
update	test.sample_properties
set		STRING_VALUE	= 'test.pivot_discrete_attrib'
where	PROPERTY_NAME	= 'SourceView'
	and	WORKITEM_NAME	= 'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= 'test.vw_pivot'
where	PROPERTY_NAME	= 'ObjectView'
	and	WORKITEM_NAME	= 'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= 'product_id'
where
		PROPERTY_NAME	= 'NonPivotedColumns'
	and	WORKITEM_NAME	= 'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= 'attrib_code'
where
		PROPERTY_NAME	= 'PivotColumn'
	and	WORKITEM_NAME	= 'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= 'value_'
where
		PROPERTY_NAME	= 'ValueColumn'
	and	WORKITEM_NAME	= 'testVPivot';

update	test.sample_properties
set		STRING_VALUE	= '''FIELD_A'' AS V_A, ''FIELD_B'' AS V_B, ''FIELD_C'' AS V_C, ''FIELD_D'' as V_D, ''FIELD_E'' as V_E, ''FIELD_F'' as V_F, ''FIELD_G'' as V_G, ''FIELD_H'' as V_H'
where
		PROPERTY_NAME	= 'ValueList'
	and	WORKITEM_NAME	= 'testVPivot';


--  testVUnpivot
update	test.sample_properties
set		STRING_VALUE	= 'test.vw_pivot'
where	PROPERTY_NAME	= 'SourceView'
	and	WORKITEM_NAME	= 'testVUnpivot';

update	test.sample_properties
set		STRING_VALUE	= 'test.vw_unpivot'
where	PROPERTY_NAME	= 'ObjectView'
	and	WORKITEM_NAME	= 'testVUnpivot';

update	test.sample_properties
set		STRING_VALUE	= 'product_id'
where
		PROPERTY_NAME	= 'NonPivotedColumns'
	and	WORKITEM_NAME	= 'testVUnpivot';

update	test.sample_properties
set		STRING_VALUE	= 'attrib_code'
where	PROPERTY_NAME	= 'PivotColumn'
	and	WORKITEM_NAME	= 'testVUnpivot';

update	test.sample_properties
set		STRING_VALUE	= 'value_'
where	PROPERTY_NAME	= 'ValueColumn'
	and	WORKITEM_NAME	= 'testVUnpivot';


--  testVGroupingSets
update	test.sample_properties
set		STRING_VALUE	= 'test.grouping_flat_values'
where	PROPERTY_NAME	= 'SourceView'
	and	WORKITEM_NAME	= 'testVGroupingSets';

update	test.sample_properties
set		STRING_VALUE	= 'test.vw_grouping_sets'
where	PROPERTY_NAME	= 'ObjectView'
	and	WORKITEM_NAME	= 'testVGroupingSets';

update	test.sample_properties
set		STRING_VALUE	= 'SUM(value1) AS SUM_V1, SUM(value2) AS SUM_V2, SUM(value3) AS SUM_V3, SUM(value4) AS SUM_V4'
where	PROPERTY_NAME	= 'AggregateExprs'
	and	WORKITEM_NAME	= 'testVGroupingSets';

update	test.sample_properties
set		STRING_VALUE	= 'simulation_id'
where	PROPERTY_NAME	= 'SimpleGroupByColumns'
	and	WORKITEM_NAME	= 'testVGroupingSets';

update	test.sample_properties
set		STRING_VALUE	= '() AS ''TOTAL_FUND'', (is_cash) AS ''AGG_CASH'', (is_cash, prod_code) AS ''AGG_PROD'', (grp1_type, grp2_code) as ''agg_grp12'', (grp2_code, grp3_class) as ''agg_grp23'''
where	PROPERTY_NAME	= 'GroupingSetsColumns'
	and	WORKITEM_NAME	= 'testVGroupingSets';


--	testVNaviForeignKey
update	test.sample_properties
set		STRING_VALUE	= 'test.vw_navi_grp_flat'
where	PROPERTY_NAME	= 'ObjectView'
	and	WORKITEM_NAME	= 'testVNaviForeignKey';

update	test.sample_properties
set		STRING_VALUE	= 'test.grouping_flat_values'
where	PROPERTY_NAME	= 'ForeignKeyBaseTable'
	and	WORKITEM_NAME	= 'testVNaviForeignKey';
