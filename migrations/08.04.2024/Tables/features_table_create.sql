use [TWS Security];

create table Features(
	id int primary key not null identity(1, 1),
	Name nvarchar(25) unique not null,
	Description nvarchar(max)
); 