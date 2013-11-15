create table test.date_time_ranges
(
	catalog_id	NUMBER(5)		not null,
	position_id	NUMBER(9)		not null,
	value_		NUMBER(16, 2)	not null,
	other_col	NVARCHAR2(50),
	start_date	date			not null,
	end_date	date,

	constraint pk_date_time_ranges primary key (catalog_id, position_id, start_date)
);
