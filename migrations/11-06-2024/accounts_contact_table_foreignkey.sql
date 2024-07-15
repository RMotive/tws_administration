use [TWS Security];

delete from Accounts_Permits;
delete from Accounts;

alter table Accounts 
	add Contact int not null unique;

create table Contact(
	id int identity(1,1) PRIMARY KEY not null,
	Name varchar(50) not null,
	Lastname varchar(50) not null,
	Email varchar(30) unique not null,
	Phone Varchar(14) unique not null
);

alter table Accounts
	add constraint FK_Accounts_Contact foreign key(Contact)
	references Contact(id);

