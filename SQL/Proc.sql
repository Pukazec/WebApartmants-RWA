USE RwaApartmani
GO

--#######################		Admin web site		################################

-----------------------		Apartmants		------------------------------------

CREATE OR ALTER PROC LoadApartments
AS
BEGIN 
	SELECT A.Id, A.Guid, A.CreatedAt, A.DeletedAt, A.OwnerId, AO.Name AS OwnerName, A.TypeId, A.StatusId, S.Name AS StatusName, S.NameEng AS StatusNameEng, A.CityId, C.Name AS CityName, A.Address, A.Name, A.NameEng, A.Price, A.MaxAdults, A.MaxChildren, A.TotalRooms, A.BeachDistance ,
		(
			SELECT COUNT(*)
			FROM ApartmentPicture AS AP
			WHERE A.Id = AP.ApartmentId
		) AS NumOfPictures,
		(
			SELECT AP.Base64Content
			FROM ApartmentPicture AS AP
			WHERE A.Id = AP.ApartmentId AND AP.IsRepresentative = 1
		) AS ImgUrl
		FROM dbo.Apartment AS A 
		INNER JOIN City AS C ON A.CityId = C.Id
		INNER JOIN ApartmentOwner AS AO ON A.OwnerId = AO.Id
		INNER JOIN ApartmentStatus AS S ON A.StatusId = S.Id
		WHERE DeletedAt IS NULL
END
GO

CREATE OR ALTER PROC LoadFilteredApartments
	@rooms INT,
	@adults INT,
	@children INT,
	@cityId INT
AS
BEGIN 
	SELECT A.Id, A.Guid, A.CreatedAt, A.DeletedAt, A.OwnerId, A.TypeId, A.StatusId, A.CityId, C.Name AS CityName, A.Address, A.Name, A.NameEng, A.Price, A.MaxAdults, A.MaxChildren, A.TotalRooms, A.BeachDistance,
		(
			SELECT AP.Base64Content
			FROM ApartmentPicture AS AP
			WHERE A.Id = AP.ApartmentId AND AP.IsRepresentative = 1
		) AS ImgUrl
	FROM dbo.Apartment AS A 
	INNER JOIN City AS C ON A.CityId = C.Id
	INNER JOIN ApartmentStatus AS S ON A.StatusId = S.Id
	WHERE   DeletedAt IS NULL 
			AND StatusId != 1
			AND ( @rooms IS NULL OR TotalRooms = @rooms )
			AND ( @adults IS NULL OR MaxAdults = @adults )
			AND ( @children IS NULL OR MaxChildren = @children)
			AND ( @cityId IS NULL OR CityId = @cityId)
END
GO
exec LoadFilteredApartments 4, null, null, null
go
CREATE OR ALTER PROC LoadApartment
	@apartmantId INT
AS
BEGIN 
	SELECT A.Id, A.Guid, A.CreatedAt, A.DeletedAt, A.OwnerId, AO.Name AS OwnerName, A.TypeId, A.StatusId, S.Name AS StatusName, S.NameEng AS StatusNameEng, A.CityId, C.Name AS CityName, A.Address, A.Name, A.NameEng, A.Price, A.MaxAdults, A.MaxChildren, A.TotalRooms, A.BeachDistance ,
		(
			SELECT COUNT(*)
			FROM ApartmentPicture AS AP
			WHERE A.Id = AP.ApartmentId
		) AS NumOfPictures,
		(
			SELECT AP.Base64Content
			FROM ApartmentPicture AS AP
			WHERE A.Id = AP.ApartmentId AND AP.IsRepresentative = 1
		) AS ImgUrl
		FROM dbo.Apartment AS A 
		INNER JOIN City AS C ON A.CityId = C.Id
		INNER JOIN ApartmentOwner AS AO ON A.OwnerId = AO.Id
		INNER JOIN ApartmentStatus AS S ON A.StatusId = S.Id
		WHERE A.Id = @apartmantId
END
GO


CREATE OR ALTER PROC AddApartmant
	@aName NVARCHAR(250),
	@aNameEng NVARCHAR(250),
	@maxAdults INT,
	@maxChildren INT,
	@totalRooms INT,
	@address NVARCHAR(250),
	@cityID INT,
	@price MONEY,
	@beachDistance INT,
	@ownerId INT,
	@guid UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO Apartment (CreatedAt, Guid, Name, NameEng, MaxAdults, MaxChildren, TotalRooms, Address, CityId, Price, BeachDistance, OwnerId, TypeId, StatusId)
	VALUES (GETDATE(), @guid, @aName, @aNameEng, @maxAdults, @maxChildren, @totalRooms, @address, @cityID, @price, @beachDistance, @ownerId, 999, 3)
END
GO

CREATE OR ALTER PROC UpdateApartmant
	@apartmantID INT,
	@aName NVARCHAR(250),
	@aNameEng NVARCHAR(250),
	@maxAdults NVARCHAR(250),
	@maxChildren NVARCHAR(250),
	@totalRooms INT,
	@address NVARCHAR(250),
	@cityID INT,
	@price MONEY,
	@ownerId INT,
	@beachDistance INT
AS
BEGIN
	UPDATE Apartment 
	SET	Name = @aName,
		NameEng = @aNameEng,
		MaxAdults = @maxAdults,
		MaxChildren = @maxChildren,
		TotalRooms = @totalRooms,
		Address = @address,
		CityId = @cityID,
		Price = @price,
		OwnerId = @ownerId,
		BeachDistance = BeachDistance
	WHERE Id = @apartmantID
END
GO

CREATE OR ALTER PROC DeleteApartmant
	@apartmantID INT
AS
BEGIN
	UPDATE Apartment
	SET DeletedAt = GETDATE()
	WHERE Id = @apartmantID
END
GO


EXEC LoadApartments
GO

-----------------------		Apartmants review		------------------------------------

CREATE OR ALTER PROC LoadRating
	@apartmantId INT
AS
BEGIN
	SELECT *
	FROM ApartmentReview
	WHERE ApartmentId = @apartmantId
END
GO

CREATE OR ALTER PROC AddReview
	@apartmantId INT,
	@userId INT,
	@details NVARCHAR(250),
	@stars INT,
	@guid UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO ApartmentReview(CreatedAt, Guid, ApartmentId, UserId, Details, Stars)
	VALUES (GETDATE(), @guid, @apartmantId, @userId, @details, @stars)
END

EXEC LoadRating 3
GO
----------------------------		Apartmant Pictures		------------------------------------

CREATE OR ALTER PROC LoadPictures
	@apartmantId INT
AS
BEGIN
	SELECT *, p.Base64Content AS Bytes
	FROM ApartmentPicture AS P
	WHERE ApartmentId = @apartmantId
END
GO

CREATE OR ALTER PROC LoadPicture
	@pictureId INT
AS
BEGIN
	SELECT *, p.Base64Content AS Bytes
	FROM ApartmentPicture AS P
	WHERE p.Id = @pictureId
END
GO

CREATE OR ALTER PROC AddPicture
	@name NVARCHAR(250),
	@bytes NVARCHAR(MAX),
	@apartmantId INT,
	@guid UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO ApartmentPicture(CreatedAt, Guid, Name, ApartmentId, Base64Content, IsRepresentative)
	VALUES (GETDATE(), @guid, @name, @apartmantId, @bytes, 0)
END
GO

CREATE OR ALTER PROC UpdateImage
	@imageID INT,
	@name NVARCHAR(250)
AS
BEGIN
	UPDATE ApartmentPicture 
	SET	Name = @name
	WHERE Id = @imageID
END
GO

CREATE OR ALTER PROC SetRepresentative
	@pictureId INT,
	@apartmantId INT
AS
BEGIN
	UPDATE ApartmentPicture
	SET IsRepresentative = 0
	WHERE ApartmentId = @apartmantId
	UPDATE ApartmentPicture 
	SET	IsRepresentative = 1
	WHERE Id = @pictureId
END
GO

CREATE OR ALTER PROC DeletePicture
	@pictureID INT
AS
BEGIN
	UPDATE ApartmentPicture
	SET DeletedAt = GETDATE()
	WHERE Id = @pictureID
END
GO

----------------------------		Apartmant Reservations		------------------------------------

CREATE OR ALTER PROC LoadApartmantReservationE
	@apartmantId INT
AS
BEGIN
	SELECT P.Id, P.Guid, P.CreatedAt, P.ApartmentId, P.UserId, P.Details, U.UserName AS UserName, U.Email AS UserEmail, U.PhoneNumber AS UserPhone, U.Address AS UserAddress
	FROM ApartmentReservation AS P
	JOIN AspNetUsers AS U ON P.UserId = U.Id
	WHERE ApartmentId = @apartmantId
END
GO

CREATE OR ALTER PROC LoadApartmantReservationA
	@apartmantId INT
AS
BEGIN
	SELECT P.Id, P.Guid, P.CreatedAt, P.ApartmentId, P.Details, P.UserId, P.UserName, P.UserEmail, P.UserPhone, P.UserAddress
	FROM ApartmentReservation AS P
	WHERE ApartmentId = @apartmantId AND UserId IS NULL
END
GO

--exec LoadApartmantReservationE 3
--exec LoadApartmantReservationA 3

CREATE OR ALTER PROC AddReservationExistingUser
	@details NVARCHAR(250),
	@apartmantId INT,
	@userId INT,
	@guid UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO ApartmentReservation(CreatedAt, Guid, UserId, ApartmentId, Details)
	VALUES (GETDATE(), @guid, @userId, @apartmantId, @details)
END
GO

CREATE OR ALTER PROC AddReservationAnonimusUser
	@details NVARCHAR(250),
	@apartmantId INT,
	@name NVARCHAR(250),
	@email NVARCHAR(250),
	@phone NVARCHAR(250),
	@address NVARCHAR(250),
	@guid UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO ApartmentReservation(CreatedAt, Guid, ApartmentId, Details, UserName, UserEmail, UserPhone, UserAddress)
	VALUES (GETDATE(), @guid, @apartmantId, @details, @name, @email, @phone, @address)
END
GO

----------------------------		Tags		------------------------------------

CREATE OR ALTER PROC LoadTags
AS
BEGIN 
	SELECT T.CreatedAt, T.Id, T.Guid, T.Name, T.NameEng, T.TypeId, TT.Name AS TypeName, TT.NameEng AS TypeNameEng, 
	(
		SELECT COUNT(*)
		FROM TaggedApartment AS TA
		WHERE TA.TagId = T.Id
	) AS TaggedApartmants
	FROM Tag AS T
	INNER JOIN TagType AS TT ON T.TypeId = TT.Id
END
GO

CREATE OR ALTER PROC LoadTag
	@tagId INT
AS
BEGIN 
	SELECT T.CreatedAt, T.Id, T.Guid, T.Name, T.NameEng, T.TypeId, TT.Name AS TypeName, TT.NameEng AS TypeNameEng, 
	(
		SELECT COUNT(*)
		FROM TaggedApartment AS TA
		WHERE TA.TagId = T.Id
	) AS TaggedApartmants
	FROM Tag AS T
	INNER JOIN TagType AS TT ON T.TypeId = TT.Id
	WHERE T.Id = @tagId
END
GO


CREATE OR ALTER PROC LoadTagTypes
AS
BEGIN 
	SELECT *
	FROM TagType AS T	
END
GO

CREATE OR ALTER PROC LoadTagType
	@typeId INT
AS
BEGIN 
	SELECT *
	FROM TagType AS T
	WHERE T.Id = @typeId
END
GO

CREATE OR ALTER PROC LoadTaggedApartmant
	@apartmantId INT
AS
BEGIN
	SELECT *
	FROM TaggedApartment
	WHERE ApartmentId = @apartmantId
END
GO	

CREATE OR ALTER PROC AddTag
	@tagName NVARCHAR(250),
	@tagNameEng NVARCHAR(250),
	@typeId int,
	@guid UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO Tag (Name, NameEng, TypeId, Guid)
	VALUES (@tagName, @tagNameEng, @typeId, @guid)
END
GO

CREATE OR ALTER PROC UpdateTags
	@tagID INT,
	@tagName NVARCHAR(250),
	@tagNameEng NVARCHAR(250),
	@typeId int
AS
BEGIN
	UPDATE Tag
	SET	Name = @tagName,
		NameEng = @tagNameEng,
		TypeId = @typeId
	WHERE Id = @tagID
END
GO

CREATE OR ALTER PROC DeleteTag
	@tagID INT
AS
BEGIN
	DELETE TAG
	WHERE Id = @tagID
END
GO

CREATE OR ALTER PROC DeletableTag
	@tagID INT
AS
BEGIN
	SELECT 
	(
		SELECT COUNT(*)
		FROM TaggedApartment AS A 
		WHERE T.Id = A.TagId
	)
	FROM Tag AS T
	WHERE T.Id = @tagID
END
GO

EXEC LoadTags
EXEC LoadTagTypes
EXEC DeletableTag 4
GO

----------------------------		Tagged apartmant		------------------------------------
CREATE OR ALTER PROC LoadTaggedApartmant
	@apartmantId INT
AS
BEGIN
	SELECT *
	FROM TaggedApartment
	WHERE ApartmentId = @apartmantId
END
GO	

CREATE OR ALTER PROC AddTaggedApartmant
	@tagId INT,
	@apartmantId INT,
	@guid UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO TaggedApartment(ApartmentId, TagId, Guid)
	VALUES (@apartmantId, @tagId, @guid)
END
GO

CREATE OR ALTER PROC DeleteTaggedApartmant
	@taggedApartmantID INT
AS
BEGIN
	DELETE TaggedApartment
	WHERE Id = @taggedApartmantID
END
GO


-----------------------		Registered Users		------------------------------------

CREATE OR ALTER PROC LoadUsers
AS
BEGIN 
	SELECT Id AS IdUser, Id, Guid, CreatedAt, DeletedAt, Email, EmailConfirmed, SecurityStamp, PhoneNumber
      ,PhoneNumberConfirmed, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, Address, PasswordHash AS Password
	FROM AspNetUsers
END
GO

CREATE OR ALTER PROC LoadUser
	@userId INT
AS
BEGIN 
	SELECT *, PasswordHash AS Password
	FROM AspNetUsers
	WHERE Id = @userId
END
GO

CREATE OR ALTER PROC AddUser
	@userName NVARCHAR(256),
	@address NVARCHAR(1000),
	@email NVARCHAR(250),
	@phoneNumber NVARCHAR(250),
	@password NVARCHAR(max),
	@guid UNIQUEIDENTIFIER
	
AS
BEGIN
	INSERT INTO AspNetUsers(Guid, CreatedAt, Email, EmailConfirmed, PasswordHash, PhoneNumber, PhoneNumberConfirmed, LockoutEnabled, AccessFailedCount, UserName, Address)
	VALUES (@guid, GETDATE(), @email, 1, @password, @phoneNumber, 1, 0, 0, @userName, @address)
END
GO

CREATE OR ALTER PROC AuthenticateUser
	@username NVARCHAR(50),
	@password NVARCHAR(128)
AS
BEGIN
	SELECT A.Id AS IdUser, *, PasswordHash as Password 
	FROM AspNetUsers AS A 
	WHERE Username = @username AND PasswordHash = @password 
END
GO

EXEC LoadUsers
GO

-----------------------		Owners		-----------------------------------------------

CREATE OR ALTER PROC LoadOwners
AS
BEGIN 
	SELECT *
	FROM ApartmentOwner
END
GO

CREATE OR ALTER PROC LoadOwner
	@ownerId INT
AS
BEGIN 
	SELECT *
	FROM ApartmentOwner
	WHERE Id = @ownerId
END
GO

EXEC LoadOwners
GO

-----------------------		Cities		------------------------------------

CREATE OR ALTER PROC LoadCities
AS
BEGIN
	SELECT *
	FROM City
END
GO

CREATE OR ALTER PROC LoadCity
	@cityId INT
AS
BEGIN
	SELECT *
	FROM City
	WHERE Id = @cityId
END
GO

EXEC LoadCities
GO

-----------------------		Statuses		------------------------------------

CREATE OR ALTER PROC LoadStatuses
AS
BEGIN
	SELECT *
	FROM ApartmentStatus
END
GO

CREATE OR ALTER PROC LoadStatus
	@statusId INT
AS
BEGIN
	SELECT *
	FROM ApartmentStatus
	WHERE Id = @statusId
END
GO

EXEC LoadStatuses
GO