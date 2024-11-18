CREATE TABLE Countries (
	Id int IDENTITY(1,1) NOT NULL,
	Name varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	TwoLetterCode char(2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ThreeLetterCode char(3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CreatedAt datetime2 DEFAULT getutcdate() NOT NULL,
	CONSTRAINT PK_Countries PRIMARY KEY (Id)
); 

CREATE TABLE IPAddresses (
	Id int IDENTITY(1,1) NOT NULL,
	CountryId int NOT NULL,
	IP varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CreatedAt datetime2 DEFAULT getutcdate() NOT NULL,
	UpdatedAt datetime2 DEFAULT getutcdate() NOT NULL,
	CONSTRAINT IX_IPAddresses UNIQUE (IP),
	CONSTRAINT PK_IPAddresses PRIMARY KEY (Id)
);
 
ALTER TABLE IPAddresses ADD CONSTRAINT FK_IPAddresses_Countries FOREIGN KEY (CountryId) REFERENCES Countries(Id);