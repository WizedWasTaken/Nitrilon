-- Check if the table Memberships exists before creating
IF OBJECT_ID('Memberships', 'U') IS NULL
BEGIN
    CREATE TABLE "Memberships" (
        "MembershipId" INT IDENTITY(1,1) NOT NULL,
        "Name" NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
        "Description" NVARCHAR(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL DEFAULT NULL,
        PRIMARY KEY ("MembershipId")
    );
END;
GO

-- Check if the table Ratings exists before creating
IF OBJECT_ID('Ratings', 'U') IS NULL
BEGIN
    CREATE TABLE "Ratings" (
        "RatingId" INT IDENTITY(1,1) NOT NULL,
        "RatingValue" INT NOT NULL,
        "Description" NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
        PRIMARY KEY ("RatingId")
    );
END;
GO

-- Check if the table Events exists before creating
IF OBJECT_ID('Events', 'U') IS NULL
BEGIN
    CREATE TABLE "Events" (
        "EventId" INT IDENTITY(1,1) NOT NULL,
        "Date" DATE NOT NULL,
        "Name" NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
        "Description" NVARCHAR(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL DEFAULT NULL,
        "Attendees" INT NULL DEFAULT (0),
        PRIMARY KEY ("EventId")
    );
END;
GO

-- Check if the table Members exists before creating
IF OBJECT_ID('Members', 'U') IS NULL
BEGIN
    CREATE TABLE "Members" (
        "MemberId" INT IDENTITY(1,1) NOT NULL,
        "Name" NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
        "Email" NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
        "MembershipId" INT NOT NULL,
        "PhoneNumber" NVARCHAR(50) NULL DEFAULT NULL,
        PRIMARY KEY ("MemberId"),
        CONSTRAINT "FK__Members__Members__412EB0B6" FOREIGN KEY ("MembershipId") REFERENCES "Memberships" ("MembershipId") ON UPDATE NO ACTION ON DELETE NO ACTION
    );
END;
GO

-- Check if the table EventRatings exists before creating
IF OBJECT_ID('EventRatings', 'U') IS NULL
BEGIN
    CREATE TABLE "EventRatings" (
        "EventRatingId" INT IDENTITY(1,1) NOT NULL,
        "EventId" INT NOT NULL,
        "RatingId" INT NOT NULL,
        PRIMARY KEY ("EventRatingId"),
        CONSTRAINT "FK__EventRati__Event__3B75D760" FOREIGN KEY ("EventId") REFERENCES "Events" ("EventId") ON UPDATE NO ACTION ON DELETE NO ACTION,
        CONSTRAINT "FK__EventRati__Ratin__3C69FB99" FOREIGN KEY ("RatingId") REFERENCES "Ratings" ("RatingId") ON UPDATE NO ACTION ON DELETE NO ACTION
    );
END;
GO

