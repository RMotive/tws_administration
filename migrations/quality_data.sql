use [TWS Security];


insert into Accounts([user], password, Wildcard)
	values('tws_quality', convert(varbinary(max), 'twsquality2024$', 0), 1);