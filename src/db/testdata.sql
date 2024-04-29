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
('Jane Johnson', 'john@johnson.com', 2);

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
