USE [TWS Security]
GO
/****** Object:  User [tws_database_admin]    Script Date: 08/04/2024 09:39:27 p. m. ******/
CREATE USER [tws_database_admin] FOR LOGIN [tws_database_admin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [tws_database_admin]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [tws_database_admin]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [tws_database_admin]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [tws_database_admin]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [tws_database_admin]
GO
ALTER ROLE [db_datareader] ADD MEMBER [tws_database_admin]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [tws_database_admin]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [tws_database_admin]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [tws_database_admin]
GO
