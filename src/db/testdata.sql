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

INSERT INTO Members (Name, Email, PhoneNumber, MembershipId) VALUES
('Kim Kardashian', 'kim-k@gmail.com', '', 1),
('Kanye West', 'kanye-da-goat@gmail.com', '04512345679', 2),
('Kylie Jenner', 'kylie-jenner@yahoo.mail', '04512345680', 1),
('Elvis Parsley', 'rocknrolldiet@vegemail.com', '04512345681', 2),
('Shania Train', 'feellikeawoman@countryhits.org', '04512345682', 1),
('Mick Jogger', 'moveslikejagger@rockmail.com', '04512345683', 2),
('Perry Scope', 'watchingyou@spygadgets.com', '04512345684', 1),
('Al Pacacino', 'sayhello@woolymail.com', '', 2),
('Tom Scruze', 'missionspossible@stuntdouble.com', '04512345686', 1),
('Bridget Bones', 'skeletonkey@archaeologist.com', '04512345687', 2),
('Walter Melon', 'fruitpunch@juicymail.com', '04512345688', 1),
('Olive Yew', 'olivebranch@peaceful.org', '', 2),
('Rusty Nails', 'constructionchaos@buildit.com', '04512345690', 1),
('Sally Mander', 'catchingfire@reptilemail.com', '04512345691', 2),
('Chris P. Bacon', 'smokingrills@porkmail.com', '04512345692', 1),
('Gail Forcewind', 'blownaway@weatheralerts.org', '', 2),
('Luke Atme', 'iseeyou@stargazer.net', '04512345694', 1),
('Teresa Green', 'gardenlife@naturemail.com', '04512345695', 2),
('Anne Chovie', '', '04512345696', 1),
('Earl E. Bird', 'wormcatcher@morningmail.com', '04512345697', 2),
('Stan Dupp', 'standfirm@uprightmail.com', '04512345698', 1),
('Justin Thyme', 'cookingtimer@chefmail.com', '04512345699', 2),
('Robin Banks', '', '04512345700', 1),
('Carmen Getit', 'ontheroad@travelbug.org', '04512345701', 2),
('Ira Fuse', 'shortcircuit@electricmail.com', '', 1),
('Bill Board', 'adman@marketingpro.org', '04512345703', 2),
('Paige Turner', 'flippinggreat@readerscorner.net', '04512345704', 1),
('Mona Lott', 'speakup@publicspeaker.org', '', 2),
('Barb Dwyer', 'barb@dwyer.co.uk', '04512345706', 1),
('Terry Bull', 'terry@bullshop.dk', '', 2),
('Anna Conda', 'anna@snakedealers.nk', '04512345708', 1),
('Bill Ding', 'building-shop@gmail.com', '04512345709', 2),
('Barry Cade', 'barry@test.com', '04512345710', 1),
('Al Beback', 'al-beback@gmail.com', '', 2),
('Sam Sung', 'phones4u@techmail.com', '04512345712', 1),
('Neil Down', '', '04512345713', 2),
('Holly Wood', 'starlight@cinemail.com', '04512345714', 1),
('Claire Voyant', 'futuretold@psychicmail.com', '04512345715', 2),
('Sue Yu', 'legalaction@lawmail.com', '04512345716', 1),
('Dinah Mite', 'bigboom@explosives.com', '', 2),
('Ray Dio', 'onair@broadcast.com', '04512345718', 1),
('Seymour Vision', 'clearview@optics.com', '04512345719', 2),
('Artie Choke', '', '04512345720', 1),
('Ella Vator', 'upanddown@liftmail.com', '04512345721', 2),
('Ivana Tinkle', 'bathroomfix@plumbingworld.com', '04512345722', 1),
('Bea Problem', '', '', 2),
('Pete Sake', 'forpetessake@brewery.com', '04512345724', 1),
('Ray Zorbills', 'cutcosts@financemail.com', '04512345725', 2),
('Lois Price', 'bargainhunt@discountmail.com', '', 1),
('Colin Allcars', 'vehicletracking@automail.com', '04512345727', 2);


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
