-- Clear existing data and reseed identity columns
DELETE FROM EventRatings;
DBCC CHECKIDENT ('EventRatings', RESEED, 0);

DELETE FROM Ratings;
DBCC CHECKIDENT ('Ratings', RESEED, 0);

DELETE FROM Events;
DBCC CHECKIDENT ('Events', RESEED, 0);

DELETE FROM Members;
DBCC CHECKIDENT ('Members', RESEED, 0);

DELETE FROM Memberships;
DBCC CHECKIDENT ('Memberships', RESEED, 0);

-------------------
--   Variables   --
-------------------
DECLARE @Random FLOAT;
DECLARE @i INT = 1;

-- Insert data into Ratings table
INSERT INTO Ratings (RatingValue, Description) 
VALUES (1, 'Glad'), (2, 'Neutral'), (3, 'Sur');

-- Insert data into Memberships table
INSERT INTO Memberships (Name, Description) 
VALUES ('Aktiv', 'Aktivt medlemskab.'), ('Passiv', 'Passivt medlemskab.');

-------------------
-- Members Table --
-------------------

-- Ensure Memberships exist before inserting into Members table
-- Valid MembershipId values are 1 and 2 from the Memberships table insert above

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

SET @i = 0; -- Reset @i to 0

WHILE @i < 25 -- Create 25 events
BEGIN
    SET @Random = RAND();

    IF @Random < 0.5
    BEGIN
        INSERT INTO Events (Date, Name, Description, Attendees)
        VALUES (DATEADD(YEAR, @i, '2020-01-01'), 'Event ' + CAST(@i AS NVARCHAR), 'Beskrivelse af event! :D Nummer: ' + CAST(@i AS NVARCHAR), @i * 10.5);
    END
    ELSE
    BEGIN
        INSERT INTO Events (Date, Name, Description)
        VALUES (DATEADD(YEAR, @i, '2020-01-01'), 'Event ' + CAST(@i AS NVARCHAR), 'Beskrivelse af event! :D Nummer: ' + CAST(@i AS NVARCHAR));
    END

    SET @i = @i + 1;
END

------------------------
-- EventRatings Table -- 
------------------------

SET @i = 0;

-- Generate 4000 event ratings
WHILE @i < 4000
BEGIN 
    SET @Random = RAND();

    IF @Random < 0.33
        INSERT INTO EventRatings (EventId, RatingId) VALUES ((@i % 24) + 1, 1);
    ELSE IF @Random < 0.66
        INSERT INTO EventRatings (EventId, RatingId) VALUES ((@i % 24) + 1, 2);
    ELSE
        INSERT INTO EventRatings (EventId, RatingId) VALUES ((@i % 24) + 1, 3);

    SET @i = @i + 1;
END
