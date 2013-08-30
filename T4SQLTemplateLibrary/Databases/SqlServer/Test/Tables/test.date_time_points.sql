create table test.date_time_points
(
	date_		date			not null,
	catalog_id	int				not null,
	position_id	int				not null,
	value_		decimal(16, 2)	not null,
	other_col	nvarchar(50),

	constraint pk_date_time_points primary key	(date_, catalog_id, position_id)
);
