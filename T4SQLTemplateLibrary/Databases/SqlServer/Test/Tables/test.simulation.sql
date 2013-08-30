create table test.simulation
(
	id				int not null primary key,
	sim_desc		nvarchar(64) not null,
	sim_type		int not null,
	created_time	datetime not null default getdate(),
	creator			nvarchar(64),

    CONSTRAINT FK_simulation_sim_type FOREIGN KEY (sim_type) REFERENCES test.sim_type(sim_type_id)
);
