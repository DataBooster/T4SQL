CREATE TABLE test.grouping_flat_values
(
	simulation_id	INT NOT NULL,
	position_id		INT NOT NULL,
	is_cash			bit not null,

	prod_code		nvarchar(16) not null,
	grp1_type		nvarchar(12),
	grp2_code		nvarchar(12),
	grp3_class		nvarchar(12),

    CONSTRAINT PK_grouping_flat_values PRIMARY KEY (simulation_id, position_id)
);
