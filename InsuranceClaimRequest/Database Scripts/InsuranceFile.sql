Create Table Benefit
(
   BenefitID int primary key not null IDENTITY,
   BenefitCode nchar(10) Not Null,
   BenefitName nvarchar(50) Not Null,
   NormalBenefitPercentage int,
   EmergencyBenefitPercentage int
)

Insert into Benefit Values ('HR','Hospital Room',50,80)
Insert into Benefit Values ('PD','Prescription Drugs',40,70)
Insert into Benefit Values ('SR','Surgery',90,100)

Create Table Insurance(
InsurerId int primary Key not null Identity,
InsurerName nvarchar(50) not null,
DateOfBirth DateTime not null,
ReceivedClaimAmount Decimal not null,
ApprovedTotalAmount Decimal not null,
ApprovedOverrideAmount Decimal not null
)

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
