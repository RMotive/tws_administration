use [TWS Security];

insert into Contact(Name, Lastname, Email, Phone) VALUES 
('Juan','Perez Mendez', 'JuanPM@hotmail.com', '+526641571220'),
('Luis Enrique', 'Garcia', 'LuisG@gmail.com','+526641571330' ), 
('Enrique','Segoviano', 'ESegovianoM@hotmail.com', '+526641571229');

insert into Accounts([User],Password,Wildcard,Contact) VALUES (N'twsm_dev', 0x7477736D6465763230323324, 1, 1), ('quality_account', 0x7175616C697479, 1, 2);
insert into Accounts_Permits(Account,Permit) VALUES (1,2), (2,3);