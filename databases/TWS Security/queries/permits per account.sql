select p. id, p.name, a.id, a.[user] from Accounts_Permits ap
	inner join Permits p on p.id = ap.permit
	inner join Accounts a on a.id = ap.account