create table test.simulation
(
	id_				number(8) not null primary key,
	sim_desc		nvarchar2(64) not null,
	sim_type		number(4) not null,
	created_time	date default sysdate not null,
	creator			nvarchar2(64),

    CONSTRAINT FK_simulation_sim_type FOREIGN KEY (sim_type) REFERENCES test.sim_type(sim_type_id)
);
