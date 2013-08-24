CREATE TABLE test.date_time_points
(
	date_		DATE			NOT NULL,
	catalog_id	INT				NOT NULL,
	position_id	INT				NOT NULL,
	value_		DECIMAL(16, 2)	NOT NULL,
	other_col	NVARCHAR(50),
	PRIMARY KEY	(date_, catalog_id, position_id)
);
