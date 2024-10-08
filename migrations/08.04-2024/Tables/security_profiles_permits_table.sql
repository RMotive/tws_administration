USE [TWS Security]
GO
/****** Object:  Table [dbo].[Profiles_Permits]    Script Date: 08/04/2024 09:39:28 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles_Permits](
	[permit] [int] NOT NULL,
	[profile] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Profiles_Permits]  WITH CHECK ADD FOREIGN KEY([permit])
REFERENCES [dbo].[Permits] ([id])
GO
ALTER TABLE [dbo].[Profiles_Permits]  WITH CHECK ADD FOREIGN KEY([permit])
REFERENCES [dbo].[Permits] ([id])
GO
ALTER TABLE [dbo].[Profiles_Permits]  WITH CHECK ADD FOREIGN KEY([profile])
REFERENCES [dbo].[Profiles] ([id])
GO
ALTER TABLE [dbo].[Profiles_Permits]  WITH CHECK ADD FOREIGN KEY([profile])
REFERENCES [dbo].[Profiles] ([id])
GO
