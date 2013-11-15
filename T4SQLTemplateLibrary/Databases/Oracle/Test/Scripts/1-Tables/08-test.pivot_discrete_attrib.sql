create table test.pivot_discrete_attrib
(
	product_id	NUMBER(9)	not null,
	attrib_code	VARCHAR2(16)	not null,
	value_		NUMBER,
	src_info	NVARCHAR2(64),

    constraint pk_pivot_discrete_attrib primary key (product_id, attrib_code)
);
