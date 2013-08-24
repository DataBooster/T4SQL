CREATE TABLE test.date_time_ranges
(
	catalog_id	INT				NOT NULL,
	position_id	INT				NOT NULL,
	value_		DECIMAL(16, 2)	NOT NULL,
	other_col	NVARCHAR(50),
	start_date	DATE			NOT NULL,
	end_date	DATE,
	PRIMARY KEY	(catalog_id, position_id, start_date)
);
