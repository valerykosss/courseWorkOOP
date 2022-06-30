USE [CLINICS]

CREATE TABLE [dbo].[CLIENT](
	[ClientID] [int] NOT NULL identity(1,1),
	[ClientName] [nvarchar](20) NOT NULL,
	[ClientSurname] [nvarchar](30) NOT NULL,
	[TelephoneNumber] [nvarchar](15) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[UserType] [int] NOT NULL,
 CONSTRAINT [PK_CLIENT] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



