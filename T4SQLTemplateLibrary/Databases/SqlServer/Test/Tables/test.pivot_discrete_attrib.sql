CREATE TABLE test.pivot_discrete_attrib
(
	product_id	INT			NOT NULL,
	attrib_code	VARCHAR(16)	NOT NULL,
	value_		MONEY,
	src_info	NVARCHAR(64),
    PRIMARY KEY (product_id, attrib_code)
);
