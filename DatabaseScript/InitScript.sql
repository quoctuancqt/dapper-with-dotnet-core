USE [DataManagement]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/8/2018 3:09:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserMobile] [varchar](50) NULL,
	[UserEmail] [varchar](50) NULL,
	[FaceBookUrl] [varchar](50) NULL,
	[LinkedInUrl] [varchar](50) NULL,
	[TwitterUrl] [varchar](50) NULL,
	[PersonalWebUrl] [varchar](50) NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF_User_IsDeleted]  DEFAULT ((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
