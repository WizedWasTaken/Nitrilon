-- Sl� alle constraints fra, slet alle data og reseed alle identity kolonner for at sikre at databasen altid er ens ved k�rsel af denne fil.

--------------------------------------------------------------------------------
-- Filen bruges til at generere test data til databasen.                      --
-- Dette er en m�de at sikre at databasen altid har ens data                  --
-- !!!! M� IKKE bruges i produktion, da det sletter alt data i databasen !!!! --
--------------------------------------------------------------------------------

-- Sl� alle constraints fra
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all";

-- Slet alle data i alle tabeller
EXEC sp_MSforeachtable "DELETE FROM ?";

-- Reseed alle tabeller, s� de starter fra 1 igen
EXEC sp_MSforeachtable "IF OBJECTPROPERTY(object_id('?', 'U'), 'TableHasIdentity') = 1 BEGIN DBCC CHECKIDENT ('?', RESEED, 0); END";

-- Sl� alle constraints til igen
EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"

-- Automatisk inds�t data i tabellerne

------------------
-- Events Table --
------------------

DECLARE @i INT = 1; -- Lav en variabel @i og s�t den til 1

WHILE @i < 11 -- Lav 10 events
BEGIN
    -- Generer et tilf�ldigt tal mellem 0 og 1
    DECLARE @Random FLOAT;
    SET @Random = RAND();

    -- Tjek om tallet er mindre end 0.5
    IF @Random < 0.5
    BEGIN
        -- Inds�t med Attendees sat til en v�rdi (@i  % 10 * 10.5 i dette eksempel)
        INSERT INTO Events (Date, Name, Description, Attendees)
        VALUES (DATEADD(DAY, @i, '2020-01-01'), 'Event ' + CAST(@i AS NVARCHAR), 'Beskrivelse af event! :D Nummer: ' + CAST(@i AS NVARCHAR), @i * 10.5);
    END
    ELSE
    BEGIN
        -- Inds�t med Attendees som NULL. 
        -- Eksempel p� denne case er at eventet ikke er afholdt endnu, og derfor ikke har nogen dukket op.
        INSERT INTO Events (Date, Name, Description)
        VALUES (DATEADD(DAY, @i, '2020-01-01'), 'Event ' + CAST(@i AS NVARCHAR), 'Beskrivelse af event! :D Nummer: ' + CAST(@i AS NVARCHAR));
    END

    SET @i = @i + 1;
END

-------------------
-- Ratings Table --
-------------------

-- Simpelt bare inds�t de 3 ratings, da de ikke skal v�re tilf�ldige
INSERT INTO Ratings(RatingValue, Description) VALUES (1, 'Sur'), (2, 'Neutral'), (3, 'Glad');

------------------------
-- EventRatings Table -- 
------------------------

SET @i = 1;
while @i < 151 -- Imens @i er mindre end 151
BEGIN 
    -- EventId er sat til at k�re igennem 1 til 10
    -- RatingId k�rer igennem 1 til 3
    INSERT INTO EventRatings (EventId, RatingId) VALUES ((@i % 10) + 1, (@i % 3) + 1);

    SET @i = @i + 1;
END
