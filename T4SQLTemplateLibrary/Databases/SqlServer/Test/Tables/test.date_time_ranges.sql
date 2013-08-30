create table test.date_time_ranges
(
	catalog_id	int				not null,
	position_id	int				not null,
	value_		decimal(16, 2)	not null,
	other_col	nvarchar(50),
	start_date	date			not null,
	end_date	date,

	constraint pk_date_time_ranges primary key (catalog_id, position_id, start_date)
);
