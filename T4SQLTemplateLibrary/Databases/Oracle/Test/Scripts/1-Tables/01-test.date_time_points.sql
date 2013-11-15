create table test.date_time_points
(
	date_		date			not null,
	catalog_id	number(5)		not null,
	position_id	number(9)		not null,
	value_		number(16, 2)	not null,
	other_col	nvarchar2(50),

	constraint pk_date_time_points primary key	(date_, catalog_id, position_id)
);
