use [TWS Security];


insert into Permits([Name], Description, Solution, Reference)
	values('Quality Assurance', 'Provides the internal permit to perform quality activities', 1173, 'AAA000001'); -- NOTE: SET THE SOLUTION AS THE PREVIOUS SOLUTION OBJECT CREATED. --
insert into Accounts([user], password, Wildcard, Contact)
	values('tws_quality', convert(varbinary(max), 'twsquality2024$', 0), 1, 4); -- Set a valid Contact id
insert into Accounts_Permits(Account, Permit)
	values(6, 6); -- First is the id for tws_quality account and the second one is for the permit id -- 

	select * from Accounts