use [TWS Security];


insert into Solutions([Name], Sign, Description)
	values('TWS Administration', 'TWSMA', 'Solution that handles and provides the core data from the main business activities and internal management.');
insert into Permits([Name], Description, Solution, Reference)
	values('Quality Assurance', 'Provides the internal permit to perform quality activities', 26, 'AAA000001'); -- NOTE: SET THE SOLUTION AS THE PREVIOUS SOLUTION OBJECT CREATED. --