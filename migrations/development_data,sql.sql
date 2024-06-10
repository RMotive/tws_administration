use [TWS Security];

insert into Accounts(Password, [User], Wildcard)
	values(convert(varbinary(max), 'twsmdev2024$'), 'twsm_dev', 1);