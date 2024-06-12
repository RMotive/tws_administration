use [TWS Security];

drop table Accounts_Permits;
drop table Accounts;

create table Contact(
id int identity(1,1) PRIMARY KEY not null,
Name varchar(50) not null,
Lastname varchar(50) not null,
Email varchar(30) not null,
Phone Varchar(13) not null
);

create table Accounts(
id int identity(1,1) PRIMARY KEY not null,
[User] varchar(50) unique not null,
Password varbinary(max) not null,
Wildcard bit default 0,
Contact int unique not null,
constraint FK@Accounts_Contact foreign key(Contact) references Contact(id),
);

create table Accounts_Permits(
Account int not null,
Permit int not null,
constraint FK@Accounts_Permits_Accounts foreign key(Account) references Accounts(id),
constraint FK@Accounts_Permits_Permits foreign key(Permit) references Permits(id),
);