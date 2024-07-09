


--> 2024.05-06
use [TWS Security];

delete from Accounts_Permits;
delete from Permits;

alter table Permits 
	add Reference varchar(20) not null unique;