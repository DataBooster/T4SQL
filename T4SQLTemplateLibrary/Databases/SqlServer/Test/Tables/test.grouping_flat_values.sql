create table test.grouping_flat_values
(
	simulation_id	int not null,
	position_id		int not null,
	is_cash			bit not null,

	prod_code		nvarchar(16) not null,
	grp1_type		nvarchar(12),
	grp2_code		nvarchar(12),
	grp3_class		nvarchar(12),
	value1			decimal(16, 2),
	value2			int not null,
	value3			smallint,
	value4			money not null,

    constraint pk_grouping_flat_values primary key (simulation_id, position_id), 
    constraint fk_grouping_flat_values_sim_id foreign key (simulation_id) references test.simulation(id),
    constraint fk_grouping_flat_values_prd_cd foreign key (prod_code) references test.prod_type(prod_code),
    constraint fk_grouping_flat_values_g3_cls foreign key (grp3_class) references test.grp3_class(grp3_class)
);
go
create index ix_grouping_flat_values1 on test.grouping_flat_values (simulation_id, is_cash, prod_code);
go
create index ix_grouping_flat_values2 on test.grouping_flat_values (simulation_id, grp1_type);
go
create index ix_grouping_flat_values3 on test.grouping_flat_values (simulation_id, grp2_code);
go
create index ix_grouping_flat_values4 on test.grouping_flat_values (simulation_id, grp3_class);
