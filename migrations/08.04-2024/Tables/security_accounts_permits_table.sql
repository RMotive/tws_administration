USE [TWS Security]
GO
/****** Object:  Table [dbo].[Accounts_Permits]    Script Date: 08/04/2024 09:39:28 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts_Permits](
	[account] [int] NOT NULL,
	[permit] [int] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Accounts_Permits] ([account], [permit]) VALUES (1, 2)
GO
ALTER TABLE [dbo].[Accounts_Permits]  WITH CHECK ADD FOREIGN KEY([account])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[Accounts_Permits]  WITH CHECK ADD FOREIGN KEY([account])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[Accounts_Permits]  WITH CHECK ADD FOREIGN KEY([permit])
REFERENCES [dbo].[Permits] ([id])
GO
ALTER TABLE [dbo].[Accounts_Permits]  WITH CHECK ADD FOREIGN KEY([permit])
REFERENCES [dbo].[Permits] ([id])
GO
