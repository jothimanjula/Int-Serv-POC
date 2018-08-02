Create Database Insurance;
GO

/****** Object:  Table [dbo].[Users]    Script Date: 7/18/2018 5:10:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [nvarchar](50) NOT NULL,
	[First Name] [nvarchar](50) NOT NULL,
	[Last Name] [nvarchar](50) NULL,
	[Orgl Stamp] [datetime] NULL,
	[Orgl User] [nvarchar](50) NULL,
	[Updt Stamp] [datetime] NULL,
	[Updt User] [nvarchar](50) NULL,
	[Expired] [char](10) NULL,
	[password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO [dbo].[Users]
           ([LoginId]
           ,[First Name]
           ,[Last Name]
           ,[Orgl Stamp]
           ,[Orgl User]
           ,[Updt Stamp]
           ,[Updt User]
           ,[Expired]
           ,[password])
     VALUES
           ('ManjulaJ','Manjula','Jothilingam', GETDATE(),NULL,NULL,NULL,'N','manjula@123')
GO
Create Table Benefit
(
   BenefitID int primary key not null IDENTITY,
   BenefitCode nchar(10) Not Null,
   BenefitName nvarchar(50) Not Null,
   NormalBenefitPercentage int,
   EmergencyBenefitPercentage int
)
GO
Insert into Benefit Values ('HR','Hospital Room',50,80)
GO
Insert into Benefit Values ('PD','Prescription Drugs',40,70)
GO
Insert into Benefit Values ('SR','Surgery',90,100)
GO

CREATE TABLE [dbo].[Insurance](
	[InsurerId] [nvarchar](10) NOT NULL,
	[InsurerName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[ReceivedClaimAmount] [decimal](18, 0) NOT NULL,
	[ApprovedTotalAmount] [decimal](18, 0) NOT NULL,
	[ApprovedOverrideAmount] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InsurerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



CREATE TABLE [dbo].[InsuranceLineItems](
	[InsurerLineItemId] [int] IDENTITY(1,1) NOT NULL,
	[InsurerId] [nvarchar](10) NOT NULL,
	[BillDate] [datetime] NOT NULL,
	[ClaimItemDescription] [nvarchar](500) NULL,
	[AmountClaimed] [decimal](18, 0) NOT NULL,
	[BenefitEmergency] [bit] NOT NULL,
	[BenefitId] [int] NULL,
	[BenefitAmount] [decimal](18, 0) NOT NULL,
	[ApprovedAmount] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InsurerLineItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[InsuranceLineItems]  WITH CHECK ADD FOREIGN KEY([BenefitId])
REFERENCES [dbo].[Benefit] ([BenefitID])
GO

ALTER TABLE [dbo].[InsuranceLineItems]  WITH CHECK ADD FOREIGN KEY([InsurerId])
REFERENCES [dbo].[Insurance] ([InsurerId])
GO


