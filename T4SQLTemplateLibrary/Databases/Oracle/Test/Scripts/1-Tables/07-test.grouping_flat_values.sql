create table test.grouping_flat_values
(
	simulation_id	NUMBER(7) not null,
	position_id		NUMBER(9) not null,
	is_cash			CHAR(1) not null,

	prod_code		NVARCHAR2(16) not null,
	grp1_type		NVARCHAR2(12),
	grp2_code		NVARCHAR2(12),
	grp3_class		NVARCHAR2(12),
	value1			NUMBER(16, 2),
	value2			NUMBER(14) not null,
	value3			NUMBER(5),
	value4			NUMBER not null,

    constraint pk_grouping_flat_values primary key (simulation_id, position_id), 
    constraint fk_grouping_flat_values_sim_id foreign key (simulation_id) references test.simulation(id_),
    constraint fk_grouping_flat_values_prd_cd foreign key (prod_code) references test.prod_type(prod_code),
    constraint fk_grouping_flat_values_g3_cls foreign key (grp3_class) references test.grp3_class(grp3_class)
);
create index ix_grouping_flat_values1 on test.grouping_flat_values (simulation_id, is_cash, prod_code);
create index ix_grouping_flat_values2 on test.grouping_flat_values (simulation_id, grp1_type);
create index ix_grouping_flat_values3 on test.grouping_flat_values (simulation_id, grp2_code);
create index ix_grouping_flat_values4 on test.grouping_flat_values (simulation_id, grp3_class);
