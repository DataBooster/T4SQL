

insert into test.date_time_points (date_, catalog_id, position_id, value_, other_col)
select
	d.date_,
	c.ordinal_number					as catalog_id,
	p.ordinal_number					as position_id,
	abs(checksum(newid())) % 10000.01	as value_,
	convert(nvarchar, c.ordinal_number) + '#' + convert(nvarchar, p.ordinal_number) + '#' + convert(nvarchar, o.ordinal_number)
										as other_col
from
	t4sql.utl_ordinal_number	p,
	t4sql.utl_ordinal_number	c,
	t4sql.vw_ordinal_date		d,
	t4sql.utl_ordinal_number	o
where
	(p.ordinal_number % 3) + 1 = c.ordinal_number
	and p.ordinal_number	<= 9
	and (o.ordinal_number % 3) + 1 <> c.ordinal_number
	and c.ordinal_number	<= 3
	and d.day_				= -o.ordinal_number * 9
	and o.ordinal_number	<= 16
	and not exists (select top 1 null from test.date_time_points)
order by
	1, 2, 3
;

if not exists (select top 1 null from test.sim_type)
insert into test.sim_type (sim_type_id, sim_type_desc)
values
(0, N'Base Simulation'),
(1, N'What-If Simulation');


insert into test.simulation (id, sim_desc, sim_type, creator)
select
	s.ORDINAL_NUMBER		as id,
	N'Simulation#' + convert(nvarchar, s.ORDINAL_NUMBER) + N' Title' as sim_desc,
	s.ORDINAL_NUMBER % 2	as sim_type,
	'tester'				as creator
from
	t4sql.utl_ordinal_number	s
where
		s.ORDINAL_NUMBER	<= 4
	and not exists (select top 1 null from test.simulation);


insert into test.prod_type (prod_code, prod_name)
select
	N'PRD' + convert(nvarchar, t.ordinal_number - 1)		as prod_code,
	N'Product ' +  CHAR(ASCII('A') + t.ordinal_number - 1)	as prod_name
from
	t4sql.utl_ordinal_number	t
where
		t.ORDINAL_NUMBER	<= 17
	and not exists (select top 1 null from test.prod_type);


insert into test.grp3_class (grp3_class, description_)
select
	N'CL3G' + convert(nvarchar, t.ordinal_number - 1)			as grp3_class,
	N'Grp3 Class ' + CHAR(ASCII('A') + t.ordinal_number * 3)	as description_
from
	t4sql.utl_ordinal_number	t
where
		t.ORDINAL_NUMBER	<= 5
	and not exists (select top 1 null from test.grp3_class);


insert into test.grouping_flat_values (simulation_id, position_id, is_cash, prod_code, grp1_type, grp2_code, grp3_class, value1, value2, value3, value4)
select
	s.ordinal_number	as simulation_id,
	p.ordinal_number	as position_id,
	case p.ordinal_number % 3
		when 0 then 1
		else 0
	end					as is_cash,
	'PRD' + convert(nvarchar, (p.ordinal_number + s.ordinal_number) % 17)
						as prod_code,
	'TP1G' + convert(nvarchar, p.ordinal_number % 7)
						AS grp1_type,
	'CD2G' + convert(nvarchar, (p.ordinal_number + s.ordinal_number) % 11)
						AS grp2_code,
	'CL3G' + convert(nvarchar, p.ordinal_number % 5)
						AS grp3_class,
	abs(checksum(newid())) % 10000.01		AS value1,
	abs(checksum(newid())) % 100000			AS value2,
	abs(checksum(newid())) % 30000			AS value3,
	abs(checksum(newid())) % 10000000.01	AS value4
from
	t4sql.utl_ordinal_number		p,
	t4sql.utl_ordinal_number		s
where
		p.ordinal_number	<= 256
	and s.ordinal_number	<= 4
	and not exists (select top 1 null from test.grouping_flat_values)
order by
	1, 2, 3, 4, 5, 6, 7
;


insert into test.pivot_discrete_attrib (product_id, attrib_code, value_, src_info)
select
	p.ordinal_number														AS product_id,
	'FIELD_' + CHAR(ASCII('A') + a.ordinal_number - 1)						AS attrib_code,
	abs(checksum(newid())) % 10000.01										AS value_,
	'REM:' + convert(nvarchar, p.ordinal_number) + '#' + convert(nvarchar, a.ordinal_number)	AS src_info
from
	t4sql.utl_ordinal_number	a,
	t4sql.utl_ordinal_number	p
where
		(a.ordinal_number + p.ordinal_number) % 3	<> 0
	and p.ordinal_number % 3	<> 0
	and a.ordinal_number	<= 8
	and p.ordinal_number	<= 16
	and not exists (select top 1 null from test.pivot_discrete_attrib)
order by
	1, 2
;
