create table test.pivot_discrete_attrib
(
	product_id	int			not null,
	attrib_code	varchar(16)	not null,
	value_		money,
	src_info	nvarchar(64),

    constraint pk_pivot_discrete_attrib primary key (product_id, attrib_code)
);
