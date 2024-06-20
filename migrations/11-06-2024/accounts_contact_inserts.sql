use [TWS Security];

insert into Contact(Name, Lastname, Email, Phone) VALUES 
('Juan','Perez Mendez', 'JuanPM@hotmail.com', '+526641571220'),
('Luis Enrique', 'Garcia', 'LuisG@gmail.com','+526641571330' ), 
('Enrique','Segoviano', 'ESegovianoM@hotmail.com', '+526641571229');

insert into Accounts([User],Password,Wildcard,Contact) VALUES (N'twsm_dev', 0x7477736D6465763230323324, 1, 1), ('quality_account', 0x7175616C697479, 1, 2);
insert into Accounts_Permits(Account,Permit) VALUES (1,2), (2,3);

insert into Contact(Name, Lastname, Email, Phone) VALUES 
	('Developer', '', 'developer@tws.com', '+526640000000');
insert into Contact(Name, Lastname, Email, Phone) VALUES 
	('Quality', '', 'quality@tws.com', '+526640000001');

select * from Contact;
insert into Accounts([User],Password,Wildcard,Contact) VALUES 
	('twsm_dev', CONVERT(varbinary, 'twsmdev2024$'), 1, 1), 
	('twsm_quality', CONVERT(varbinary, 'twsmquality2024$'), 1, 2);

select * from Permits;
insert into Accounts_Permits(Account,Permit) VALUES (3, 1);


