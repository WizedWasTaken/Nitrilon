-- Slå alle constraints fra, slet alle data og reseed alle identity kolonner for at sikre at databasen altid er ens ved kørsel af denne fil.

--------------------------------------------------------------------------------
-- Filen bruges til at generere test data til produktions databasen.          --
-- Dette er en måde at sikre at databasen altid har ens data                  --
-- Må gerne bruges i produktion, da den resetter den til en default state!    --
--------------------------------------------------------------------------------

-- Delete all data from EventRatings table
DELETE FROM EventRatings;
DBCC CHECKIDENT ('EventRatings', RESEED, 0);

-- Delete all data from Ratings table
DELETE FROM Ratings;
DBCC CHECKIDENT ('Ratings', RESEED, 0);

-- Delete all data from Events table
DELETE FROM Events;
DBCC CHECKIDENT ('Events', RESEED, 0);

-- Delete all data from Members table
DELETE FROM Members;
DBCC CHECKIDENT ('Members', RESEED, 0);

-- Delete all data from Memberships table
DELETE FROM Memberships;
DBCC CHECKIDENT ('Memberships', RESEED, 0);

-------------------
--   Variables   --
-------------------
DECLARE @Random FLOAT; -- Lav en variabel @Random af typen FLOAT
DECLARE @i INT = 1; -- Lav en variabel @i og sæt den til 1


-- Automatisk indsæt data i tabellerne

-------------------
-- Ratings Table --
-------------------

-- Simpelt bare indsæt de 3 ratings, da de ikke skal være tilfældige
INSERT INTO Ratings(RatingValue, Description) VALUES (1, 'Glad'), (2, 'Neutral'), (3, 'Sur');

-----------------------
-- Memberships Table --
-----------------------

INSERT INTO Memberships (Name, Description) VALUES 
('Aktiv', 'Aktivt medlemskab.'), 
('Passiv', 'Passivt medlemskab.');

-------------------
-- Members Table --
-------------------

INSERT INTO Members (Name, Email, MembershipId) VALUES 
('John Doe', 'john@doe.com', 1),
('Jane Doe', 'jane@doe.com', 2),
('John Smith', 'john@smith.com', 1),
('Jane Smith', 'jane@smith.com', 2),
('John Johnson', 'john@johnson.com', 1),
('Jane Johnson', 'jane@johnson.com', 2),
('John Jensen', 'john@jensen.com', 1),
('Jane Jensen', 'jane@jensen.com', 2),
('John Williams', 'john@williams.com', 1),
('Jane Williams', 'jane@williams.com', 2),
('John Brown', 'john@brown.com', 1),
('Jane Brown', 'jane@brown.com', 2),
('John Davis', 'john@davis.com', 1),
('Jane Davis', 'jane@davis.com', 2),
('John Miller', 'john@miller.com', 1),
('Jane Miller', 'jane@miller.com', 2),
('John Wilson', 'john@wilson.com', 1),
('Jane Wilson', 'jane@wilson.com', 2),
('John Moore', 'john@moore.com', 1),
('Jane Moore', 'jane@moore.com', 2),
('John Taylor', 'john@taylor.com', 1),
('Jane Taylor', 'jane@taylor.com', 2),
('John Anderson', 'john@anderson.com', 1),
('Jane Anderson', 'jane@anderson.com', 2),
('John Thomas', 'john@thomas.com', 1),
('Jane Thomas', 'jane@thomas.com', 2),
('John Jackson', 'john@jackson.com', 1),
('Jane Jackson', 'jane@jackson.com', 2),
('John White', 'john@white.com', 1),
('Jane White', 'jane@white.com', 2),
('John Harris', 'john@harris.com', 1),
('Jane Harris', 'jane@harris.com', 2);



------------------
-- Events Table --
------------------

SET @i = 0; -- Sæt @i til 0

WHILE @i < 25 -- Lav 25 events
BEGIN
    -- Generer et tilfældigt tal mellem 0 og 1
    SET @Random = RAND();

    -- Tjek om tallet er mindre end 0.5
    IF @Random < 0.5
    BEGIN
        -- Indsæt med Attendees sat til en værdi (@i  % 10 * 10.5 i dette eksempel)
        INSERT INTO Events (Date, Name, Description, Attendees)
        VALUES (DATEADD(YEAR, @i, '2020-01-01'), 'Event ' + CAST(@i AS NVARCHAR), 'Beskrivelse af event! :D Nummer: ' + CAST(@i AS NVARCHAR), @i * 10.5);
    END
    ELSE
    BEGIN
        -- Indsæt med Attendees som NULL. 
        -- Eksempel på denne case er at eventet ikke er afholdt endnu, og derfor ikke har nogen dukket op.
        INSERT INTO Events (Date, Name, Description)
        VALUES (DATEADD(YEAR, @i, '2020-01-01'), 'Event ' + CAST(@i AS NVARCHAR), 'Beskrivelse af event! :D Nummer: ' + CAST(@i AS NVARCHAR));
    END

    SET @i = @i + 1;
END

------------------------
-- EventRatings Table -- 
------------------------

SET @i = 0;
while @i < 4000 -- Generer 4000 event ratings
BEGIN 
    -- EventId er sat til at køre igennem 1 til 10
    -- RatingId kører igennem 1 til 3
    SET @Random = RAND();

    -- Modulus 24 for at skabe en god spredning af vores ratings.
    IF @Random < 0.33
        INSERT INTO EventRatings (EventId, RatingId) VALUES ((@i % 24) + 1, 1);
    IF @Random >= 0.33 AND @Random < 0.66
		INSERT INTO EventRatings (EventId, RatingId) VALUES ((@i % 24) + 1, 2);
    IF @Random >= 0.66
        INSERT INTO EventRatings (EventId, RatingId) VALUES ((@i % 24) + 1, 3);

    SET @i = @i + 1;
END
