USE [CLINICS]
GO

/****** Object:  Table [dbo].[DOCTOR]    Script Date: 14.05.2022 19:20:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DOCTOR](
	[DoctorID] [int] NOT NULL identity(1,1),
	[DoctorName] [nvarchar](20) NOT NULL,
	[DoctorSurname] [nvarchar](30) NOT NULL,
	[DoctorPatronymic] [nvarchar](30) NOT NULL,
	[DoctorImage] [nvarchar](max) NULL,
 CONSTRAINT [PK_DOCTOR] PRIMARY KEY CLUSTERED 
(
	[DoctorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


