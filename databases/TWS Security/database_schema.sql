create database [TWS Security];
use [TWS Security];

-- Stores each solution subscribed to the business solution bundle --
create table Solutions(
	id int primary key not null identity(1, 1), 
	name varchar(25) not null unique, 
	sign varchar(5) not null unique, -- Five chars identifier for the solution i.e (TWSS)
	description varchar(max),
);

-- Stores all the possible permits related with their respective solution --
create table Permits(
	id int primary key not null identity (1, 1),
	name varchar(25) not null unique,
	description varchar(max),
	solution int not null,
	
	foreign key (solution) references Solutions(id),
);

-- Stores profiles that relates collection of permits to attach directly a bunch of them to an specific user --
create table Profiles(
	id int primary key not null identity(1, 1),
	name varchar(25) not null unique,
	description varchar(max),
);

-- (M-M Table) relates permits with profiles --
create table Profiles_Permits(
	permit int not null,
	profile int not null,

	foreign key (permit) references Permits(id),
	foreign key (profile) references Profiles(id),
);

-- Stores the accounts information to be able sign in into business solutions --
create table Accounts(
	id int primary key not null identity(1, 1),
	[user] varchar(50) not null unique,
	password varbinary(max) not null,
);

-- (M-M Table) relates special permits with accounts --
create table Accounts_Permits(
	account int not null,
	permit int not null,

	foreign key (account) references Accounts(id),
	foreign key (permit) references Permits(id),
);