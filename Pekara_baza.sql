create database Pekara
use  Pekara

create table korisnik
(
id_korisnik int identity(1,1) primary key,
email nvarchar(100) not null,
lozinka nvarchar(100) not null
)

create table lokacija
(id_lokacija int identity(1,1) primary key,
adresa nvarchar(100),
radno_vreme_pocetak datetime,
radno_vreme_kraj datetime
)

create table radnik
(id_radnik int identity(1,1) primary key,
ime nvarchar(100),
prezime nvarchar(100),
JMBG int,
id_lokacije int,
tekuci_racun nvarchar(100)
)

create table proizvod
(id_proizvoda int identity(1,1) primary key,
ime nvarchar(100),
tip nvarchar(100),
cena int,
kalorije int
)

create table proizvodjac
(id_proizvodjac int identity(1,1) primary key,
ime nvarchar(100),
PIB int
)

create table lager
(id_lager int identity(1,1) primary key,
id_proizvoda int ,
id_lokacija int ,
kolicina int
)

create table kupac
( id_kupac int identity(1,1) primary key,
  tip_kupovine nvarchar(100),
  datum_dostave datetime
)
create table racun
(
	id_racun int identity(1,1) primary key,
	id_kupac int,
	cena int,
)

create table racun_proizvod
(id_racun_proizvod int identity(1,1) primary key,
id_proizvod int,
id_racun int,
kolicina int
)

create table uvoz
( id_uvoz int identity(1,1) primary key,
	id_proizvodjac int,
	id_proizvod int,
	id_lager int,
	kolicina int,
	
	cena_uvoza int
)


ALTER TABLE uvoz
ADD CONSTRAINT
FK_id_proizvod FOREIGN KEY
(id_proizvod) REFERENCES Proizvod(id_proizvoda)

ALTER TABLE uvoz
ADD CONSTRAINT
FK_id_proizvodjac FOREIGN KEY
(id_proizvodjac) REFERENCES Proizvodjac(id_proizvodjac)


ALTER TABLE uvoz
ADD CONSTRAINT
FK_id_lager_uvoz FOREIGN KEY
(id_lager) REFERENCES Lager(id_lager)

ALTER TABLE radnik
ADD CONSTRAINT
FK_id_lokacija FOREIGN KEY
(id_lokacije) REFERENCES Lokacija(id_lokacija)

ALTER TABLE lager
ADD CONSTRAINT
FK_id_proizvod_lager FOREIGN KEY
(id_proizvoda) REFERENCES Proizvod(id_proizvoda)

ALTER TABLE lager
ADD CONSTRAINT
FK_id_lokacija_lager FOREIGN KEY
(id_lokacija) REFERENCES Lokacija(id_lokacija)

ALTER TABLE racun
ADD CONSTRAINT
FK_id_kupac FOREIGN KEY
(id_kupac) REFERENCES Kupac(id_kupac)

ALTER TABLE racun_proizvod
ADD CONSTRAINT
FK_id_racun_1 FOREIGN KEY
(id_racun) REFERENCES Racun(id_racun)

ALTER TABLE racun_proizvod
ADD CONSTRAINT
FK_id_racun_2 FOREIGN KEY
(id_proizvod) REFERENCES Proizvod(id_proizvoda)









Create PROC dbo.Korisnik_Email
@email nvarchar(50),
@loz nvarchar(100)
AS
SET LOCK_TIMEOUT 3000;
BEGIN TRY
	IF EXISTS(SELECT TOP 1 email FROM Korisnik
	WHERE email = @email and lozinka=@loz)
	Begin
	RETURN 0
	end
	RETURN 1
END TRY
BEGIN CATCH
	RETURN @@error;
END CATCH
GO

CREATE PROC Korisnik_Insert
@email nvarchar(50),
@loz nvarchar(100)
AS
SET LOCK_TIMEOUT 3000;
BEGIN TRY
IF EXISTS (SELECT TOP 1 email  FROM Korisnik
	WHERE email = @email  )
	Return 1
	else
	Insert Into Korisnik(email,lozinka)
	Values(@email,@loz)
		RETURN 0;
END TRY
	
BEGIN CATCH
	RETURN @@ERROR;
END CATCH
GO

Create PROC dbo.Korisnik_Update
@email nvarchar(50),
@loz nvarchar(100)
AS
SET LOCK_TIMEOUT 3000;
BEGIN TRY
	IF EXISTS (SELECT TOP 1 email FROM dbo.Korisnik
	WHERE email = @email  )

	BEGIN
	
	Update Korisnik  Set lozinka=@loz  where email=@email 
		RETURN 0;
	END
	RETURN -1;
END TRY
BEGIN CATCH
	RETURN @@ERROR;
END CATCH
GO

Create PROC Proizvod_insert
@ime nvarchar(50),
@cena int
AS
SET LOCK_TIMEOUT 3000;
BEGIN TRY
	IF EXISTS (SELECT TOP 1 ime  FROM proizvod
	WHERE ime = @ime  )
	Return 1
	else
	Insert Into proizvod(ime,cena)
	Values(@ime,@cena)
		RETURN 0;
END TRY
BEGIN CATCH
	RETURN @@ERROR;
END CATCH
