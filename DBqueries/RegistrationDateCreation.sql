USE [CLINICS]
GO

/****** Object:  Table [dbo].[REGISTRATION_DATE]    Script Date: 14.05.2022 19:26:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[REGISTRATION_DATE](
	[DateID] [int] NOT NULL identity(1,1),
	[Date] [date] NOT NULL,
	[TimeID] [int] NOT NULL,
	[DoctorID] [int] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[DateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[REGISTRATION_DATE]  WITH CHECK ADD  CONSTRAINT [FK_REGISTRATION_DATE_REGISTRATION_TIME] FOREIGN KEY([TimeID])
REFERENCES [dbo].[REGISTRATION_TIME] ([TimeID])
GO

ALTER TABLE [dbo].[REGISTRATION_DATE] CHECK CONSTRAINT [FK_REGISTRATION_DATE_REGISTRATION_TIME]
GO

