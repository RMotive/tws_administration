use [TWS Security];

-- Injecting solutions --
insert into Solutions
	values ('TWS Admin', 'TWSA', 'Solution that handles all administrative business operations');

-- Injecting Permits --
insert into Permits
	values ('Login', 'Allows the account to login into TWS Admin', 1);
insert into Permits
	values ('Wildcard', 'Allows the account full access into TWS Admin', 1);

-- Injecting testing account --
insert into Accounts
	values ('twsm_dev', Cast('twsmdev2023$' As varbinary(max)));

-- Injecting the permit into the dev account --
insert into Accounts_Permits
	values (1, 2);

