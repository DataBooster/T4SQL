--	Run in test schema

insert into test.date_time_points (date_, catalog_id, position_id, value_, other_col)
select
	d.date_,
	c.ordinal_number					as catalog_id,
	p.ordinal_number					as position_id,
	dbms_random.value(0, 10000)			as value_,
	to_char(c.ordinal_number) || '#' || to_char(p.ordinal_number) || '#' || to_char(o.ordinal_number)
										as other_col
from
	t4sql.utl_ordinal_number	p,
	t4sql.utl_ordinal_number	c,
	t4sql.vw_ordinal_date		d,
	t4sql.utl_ordinal_number	o
where
	mod(p.ordinal_number, 3) + 1 = c.ordinal_number
	and p.ordinal_number	<= 9
	and mod(o.ordinal_number, 3) + 1 <> c.ordinal_number
	and c.ordinal_number	<= 3
	and d.day_				= -o.ordinal_number * 9
	and o.ordinal_number	<= 16
	and not exists (select null from test.date_time_points where rownum = 1)
order by
	1, 2, 3
;
/

declare
	tExists	PLS_INTEGER;
begin
	select count(*) into tExists from test.sim_type;

	if tExists = 0 then
		insert into test.sim_type (sim_type_id, sim_type_desc)
		values (0, 'Base Simulation');
		insert into test.sim_type (sim_type_id, sim_type_desc)
		values (1, 'What-If Simulation');
	end if;
end;
/

insert into test.simulation (id_, sim_desc, sim_type, creator)
select
	s.ORDINAL_NUMBER		as id_,
	'Simulation#' || to_char(s.ORDINAL_NUMBER) || ' Title' as sim_desc,
	mod(s.ORDINAL_NUMBER, 2)	as sim_type,
	'tester'					as creator
from
	t4sql.utl_ordinal_number	s
where
		s.ORDINAL_NUMBER	<= 4
	and not exists (select null from test.simulation where rownum = 1);


insert into test.prod_type (prod_code, prod_name)
select
	'PRD' || to_char(t.ordinal_number - 1)		as prod_code,
	'Product ' ||  CHR(ASCII('A') + t.ordinal_number - 1)	as prod_name
from
	t4sql.utl_ordinal_number	t
where
		t.ORDINAL_NUMBER	<= 17
	and not exists (select null from test.prod_type where rownum = 1);


insert into test.grp3_class (grp3_class, description_)
select
	'CL3G' || TO_CHAR(t.ordinal_number - 1)					as grp3_class,
	'Grp3 Class ' || CHR(ASCII('A') + t.ordinal_number * 3)	as description_
from
	t4sql.utl_ordinal_number	t
where
		t.ORDINAL_NUMBER	<= 5
	and not exists (select null from test.grp3_class WHERE ROWNUM = 1);


insert into test.grouping_flat_values (simulation_id, position_id, is_cash, prod_code, grp1_type, grp2_code, grp3_class, value1, value2, value3, value4)
select
	s.ordinal_number								as simulation_id,
	p.ordinal_number								as position_id,
	DECODE(mod(p.ordinal_number, 3), 0, 1, 0)		as is_cash,
	'PRD' || TO_CHAR(MOD((p.ordinal_number + s.ordinal_number), 17))
													as prod_code,
	'TP1G' || TO_CHAR(mod(p.ordinal_number, 7))		AS grp1_type,
	'CD2G' || TO_CHAR(MOD((p.ordinal_number + s.ordinal_number), 11))
													AS grp2_code,
	'CL3G' || TO_CHAR(mod(p.ordinal_number, 5))		AS grp3_class,
	dbms_random.value(0, 10000.01)					AS value1,
	dbms_random.value(0, 100000)					AS value2,
	dbms_random.value(0, 30000)						AS value3,
	dbms_random.value(0, 10000000.01)				AS value4
from
	t4sql.utl_ordinal_number		p,
	t4sql.utl_ordinal_number		s
where
		p.ordinal_number	<= 256
	and s.ordinal_number	<= 4
	and not exists (select null from test.grouping_flat_values WHERE ROWNUM = 1)
order by
	1, 2, 3, 4, 5, 6, 7
;


insert into test.pivot_discrete_attrib (product_id, attrib_code, value_, src_info)
select
	p.ordinal_number														AS product_id,
	'FIELD_' || CHR(ASCII('A') + a.ordinal_number - 1)						AS attrib_code,
	dbms_random.value(0, 10000.01)											AS value_,
	'REM:' || TO_CHAR(p.ordinal_number) || '#' || TO_CHAR(a.ordinal_number)	AS src_info
from
	t4sql.utl_ordinal_number	a,
	t4sql.utl_ordinal_number	p
where
		MOD((a.ordinal_number + p.ordinal_number), 3)	<> 0
	and mod(p.ordinal_number, 3)	<> 0
	and a.ordinal_number	<= 8
	and p.ordinal_number	<= 16
	and not exists (select null from test.pivot_discrete_attrib WHERE ROWNUM = 1)
order by
	1, 2
;
