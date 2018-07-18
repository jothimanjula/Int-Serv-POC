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
           (1,'ManjulaJ','Manjula','Jothilingam', GETDATE(),NULL,NULL,NULL,'N','manjula@123')
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

Create Table Insurance(
InsurerId int primary Key not null Identity,
InsurerName nvarchar(50) not null,
DateOfBirth DateTime not null,
ReceivedClaimAmount Decimal not null,
ApprovedTotalAmount Decimal not null,
ApprovedOverrideAmount Decimal not null
)
GO

Create table InsuranceLineItems(
InsurerLineItemId int primary key not null identity,
InsurerId int not null,
BillDate DateTime not null,
ClaimItemDescription nvarchar(500),
AmountClaimed Decimal Not null,
BenefitEmergency Bit not null,
BenefitId int,
BenefitAmount Decimal not null,
ApprovedAmount Decimal not null,
FOREIGN KEY (BenefitId) REFERENCES Benefit(BenefitId),
FOREIGN KEY (InsurerId) REFERENCES Insurance(InsurerId)
)
GO