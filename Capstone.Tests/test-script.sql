-- DELETE DATABASE DATA
DELETE FROM park
DELETE FROM campground
DELETE FROM site
DELETE FROM reservation

-- INSERT sample park
INSERT INTO park VALUES ('Tia Noah Land','The Moon','1701-01-01',10000,2,'The moon oh moon what can I say oh moon? Tis dark and spacy and glorious to behold. Twas a sky twas a night to whit to woo oh what a moon!')
DECLARE @parkID int = (SELECT @@IDENTITY);

-- INSERT sample campground
INSERT INTO campground VALUES (@parkID,'moon side',01,08,4200.00)
DECLARE @campgroundID int = (SELECT @@IDENTITY);

-- INSERT sample site
INSERT INTO site VALUES (@campgroundID,1,10,1,1000,1)
DECLARE @siteId int = (SELECT @@IDENTITY);

-- INSERT sample reservation
INSERT INTO reservation VALUES (@siteId,'Ken','2019-03-03','2019-05-01','2019-02-22')
DECLARE @reservationId int = (SELECT @@IDENTITY);

-- Return reservation ID
SELECT @reservationId as reservationId;

