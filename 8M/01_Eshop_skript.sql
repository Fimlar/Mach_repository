------------------------------- VYTVORENI TABULEK -------------------------------

-- Tento příkaz vytvoří novou tabulku s názvem "Users"
CREATE TABLE Users (
    
    -- "UserID" je číselný identifikátor uživatele (unikátní pro každého)
    -- "INTEGER" = celé číslo
    -- "AUTO_INCREMENT" = hodnota se automaticky zvýší pro každého nového uživatele (1, 2, 3, ...)
    UserID INTEGER AUTO_INCREMENT,
    
    -- "FirstName" = křestní jméno uživatele
    -- "VARCHAR(100)" = textový řetězec s maximální délkou 100 znaků
    -- "NOT NULL" = toto pole musí být vždy vyplněné (nesmí být prázdné)
    FirstName VARCHAR(100) NOT NULL,
    
    -- "LastName" = příjmení uživatele
    -- Stejně jako u FirstName: text do 100 znaků, nesmí být prázdné
    LastName VARCHAR(100) NOT NULL,
    
    -- "Email" = e-mailová adresa uživatele
    -- Také text do 100 znaků, musí být vyplněná
    Email VARCHAR(100) NOT NULL,
    
    -- "Newsletter" = informace, zda si uživatel přeje dostávat newsletter
    -- "BOOLEAN" = logická hodnota, může být TRUE (ano) nebo FALSE (ne)
    Newsletter BOOLEAN,
    
    -- Určujeme, že primární klíč tabulky (unikátní identifikátor každého záznamu)
    -- bude sloupec "UserID". To zajišťuje, že každý uživatel má jedinečné ID.
    PRIMARY KEY (UserID)
);


CREATE TABLE Categories (
	CategoryID INTEGER AUTO_INCREMENT,
	Name VARCHAR(100) NOT NULL,
	PRIMARY KEY(CategoryID)
);

CREATE TABLE Sales (
	SaleID INTEGER AUTO_INCREMENT,
	Name VARCHAR(100),
	Percentage INTEGER,
	PRIMARY KEY(SaleID)
);


CREATE TABLE Items (
    ItemID INTEGER AUTO_INCREMENT ,
    Name VARCHAR(255) NOT NULL,

    -- "Price" = cena položky
    -- "DECIMAL(10,2)" = číslo s přesností 10 číslic celkem, z toho 2 desetinná místa
    -- např. 12345.67
    -- "NOT NULL" = cena musí být vždy uvedena
    Price DECIMAL(10,2) NOT NULL,

    -- "Pieces" = počet kusů skladem
    -- "INTEGER" = celé číslo
    -- "DEFAULT 0" = výchozí hodnota je 0, pokud není zadána jiná
    Pieces INTEGER DEFAULT 0,

    Sale INTEGER,
    Category INTEGER,
    PRIMARY KEY(ItemID),
    -- "FOREIGN KEY(Category)" = definuje cizí klíč,
    -- který propojuje sloupec "Category" v této tabulce s "CategoryID" v tabulce "Categories"
    FOREIGN KEY(Category) REFERENCES Categories(CategoryID) 

        -- "ON DELETE SET NULL" = pokud se z tabulky "Categories" smaže daná kategorie,
        -- tak se ve sloupci "Category" nastaví hodnota NULL (nepřiřazeno)
        ON DELETE SET NULL

        -- "ON UPDATE CASCADE" = pokud se změní ID kategorie v tabulce "Categories",
        -- změna se automaticky projeví i v této tabulce
        ON UPDATE CASCADE,

    -- Druhý cizí klíč: "Sale" je propojen s tabulkou "Sales" přes sloupec "SaleID"
    FOREIGN KEY(Sale) REFERENCES Sales(SaleID) 

        -- Pokud se smaže sleva, nastaví se zde hodnota NULL
        ON DELETE SET NULL

        -- Pokud se změní ID slevy, aktualizuje se i zde
        ON UPDATE CASCADE
);

CREATE TABLE Orders (
	OrderID INTEGER AUTO_INCREMENT,
	UserID INTEGER NOT NULL,
	ItemID INTEGER NOT NULL,
	Amount INTEGER NOT NULL, 
	DateOfOrder DATE NOT NULL,
	PRIMARY KEY (OrderID),
	FOREIGN KEY (UserID) REFERENCES Users(UserID)
        ON UPDATE CASCADE,
	FOREIGN KEY (ItemID) REFERENCES Items(ItemID)
        ON UPDATE CASCADE
);

CREATE TABLE Reviews (
ReviewID INTEGER AUTO_INCREMENT ,
UserID INTEGER NOT NULL,
Stars INTEGER NOT NULL,
TextOfReview VARCHAR(256) NOT NULL,
PRIMARY KEY (ReviewID),
FOREIGN KEY (UserID) REFERENCES Users(UserID)
        ON UPDATE CASCADE
);


------------------------------- VLOZENI DAT -------------------------------

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Jan", "Novak", "novak.jan@gmail.com", TRUE); 

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Sara", "Mala", "mala.sara@gmail.com", TRUE); 

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Miroslav", "Langmajer", "mira008@gmail.com", FALSE); 

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Jakub", "Jirchar", "kubakubikula@gmail.com", TRUE); 

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Nadezda", "Vyborna", "vyborna.nadezda@gmail.com", FALSE); 

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Lenka", "Chvatalova", "chvatalova98@gmail.com", FALSE); 

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Danuse", "Novakova", "dend@gmail.com", TRUE); 

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Simon", "Jarni", "jarnaky@gmail.com", TRUE); 

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Michal", "Michalek", "michalekm@gmail.com", FALSE); 

INSERT INTO Users(FirstName, LastName, Email, Newsletter)
VALUES ( "Jitka", "Petrakova", "npetrakova@gmail.com", TRUE); 



INSERT INTO Categories (Name)
VALUES ("Elektronika"); 

INSERT INTO Categories (Name)
VALUES ("Potraviny"); 

INSERT INTO Categories (Name)
VALUES ("Obleceni"); 

INSERT INTO Categories (Name)
VALUES ("Papirnictvi"); 

INSERT INTO Categories (Name)
VALUES ("Hobby"); 



INSERT INTO Sales (Name, Percentage)
VALUES ("Zadna", 0);

INSERT INTO Sales (Name, Percentage)
VALUES ("Vanocni", 20);

INSERT INTO Sales (Name, Percentage)
VALUES ("Valentyn", 14);

INSERT INTO Sales (Name, Percentage)
VALUES ("Propagacni", 10);



INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Sukne", 299, 27, 1, 3); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Kalhoty", 399, 16, 1, 3); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Samsung Galaxy", 5899, 6, 1, 1); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Mleko", 23, 254, 1, 2); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Kecup", 39, 158, 1, 2); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Jatra", 89, 98, 1, 2); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Syr", 56, 112, 1, 2); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Nuzky", 35, 78, 1, 4); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Lepenka", 35, 56, 1, 4); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Ruze", 24, 78, 3, 5); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Vanocni kalendar", 99, 150, 2, 2); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Klavesnice", 999, 45, 4, 1); 

INSERT INTO Items(Name, Price, Pieces, Sale, Category)
VALUES ( "Konev", 149, 0, 1, 5); 



INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 1, 2, 1, "2022-03-05"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 1, 3, 2, "2022-05-25"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 3, 5, 1, "2022-06-17"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 5, 8, 4, "2022-04-18"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 6, 1, 1, "2022-07-15"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 6, 2, 1, "2022-07-15"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 6, 3, 1, "2022-07-15"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 6, 4, 1, "2022-07-15"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 10, 11, 5, "2022-09-27"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 9, 7, 1, "2022-11-15"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 5, 9, 1, "2023-01-09"); 

INSERT INTO Orders(UserID, ItemID, Amount, DateOfOrder)
VALUES ( 6, 4, 1, "2022-09-15"); 



INSERT INTO Reviews(UserID, Stars, TextOfReview)
VALUES ( 1, 2, "Zbozi mi prislo po dvou tydnech. Poskozene..."); 

INSERT INTO Reviews(UserID, Stars, TextOfReview)
VALUES ( 2, 2, "Nic moc e-shop."); 

INSERT INTO Reviews(UserID, Stars, TextOfReview)
VALUES ( 3, 5, "Mam s timto obchodem pouze pozitivni zkusenost."); 

INSERT INTO Reviews(UserID, Stars, TextOfReview)
VALUES ( 6, 4, "Muj druhy nejoblibenejsi e-shop. Skvela komunikace!"); 

INSERT INTO Reviews(UserID, Stars, TextOfReview)
VALUES ( 1, 3, "Zbozi mi prislo po tydnu a v poradku. Zlepsuji si sve mineni :)"); 

INSERT INTO Reviews(UserID, Stars, TextOfReview)
VALUES ( 5, 5, "UZASNE!!!!"); 


------------------------------- DOTAZY -------------------------------

-- 1) Ukaz vsechno z tabulky uzivatele.
	SELECT * FROM Users; -- * znamena vsechny sloupce

-- *2) Ukaz vsechno z tabulky polozky.
        SELECT * FROM Items;
-- 3) Ukaz vsechno z tabulky uzivatele serazene podle prijmeni.
	SELECT * FROM Users ORDER BY LastName;

-- *4) Ukaz vsechno z tabulky polozky serazene dle ceny.
        SELECT * FROM Items ORDER BY Price;
-- 5) Ukaz vvsechno z tabulky recenze uživatele s ID = 1.
	SELECT * FROM Reviews WHERE UserID = 1;

-- *6) Ukaz vsechno z tabulky objednavky polozky s ID = 2.
        SELECT * FROM Sales WHERE SaleID = 2;
-- *7) Ukaz vsechno z tabulky uzivatele, kteri chteji newsletter.
        SELECT * FROM Users WHERE Newsletter = 1;
-- *8) Ukaz vsechno z tabulky recenze, kde uzivatel dal 5 hvezdicek.
        SELECT * FROM Reviews WHERE Stars = 5;
-- 9) Ukaz vsechno z tabulky polozky, kde je cena ostre vyssi nez 100 korun.
	SELECT * FROM Items WHERE Price > 100;

-- *10) Ukaz vsechno z tabulky polozky, ktere jsou skladem.
	SELECT * FROM Items WHERE Pieces > 0;
-- *11) Ukaz polozky, ktere nejsou skladem.
        SELECT * FROM Items WHERE Pieces = 0;
-- *12) Ukaz objednavky od kvetna 2022 az k dnesku (zadavani datumu viz dalsi dotaz).
        SELECT * FROM Orders WHERE DateOfOrder > "2022-05-01";
-- 13) Ukaz vsechny objednavky uzivatele s ID=6 a v den 15. 7. 2022.
	SELECT * FROM Orders WHERE UserID = 6 AND DateOfOrder = "2022-07-15";
	-- logické spojky: AND, OR, NOT

-- *14) Ukaz vsechny polozky, ktere jsou skladem a neni na ne sleva.
        SELECT * FROM Items WHERE Pieces = 0 AND Sale = 1;
-- *15) Ukaz vsechny objednavky z dubna.
        SELECT * FROM Orders WHERE Month(DateOfOrder) = 04;
-- *16) Ukaz vsechny objednavky z dubna nebo cervna.
        SELECT * FROM Orders WHERE Month(DateOfOrder) = 04 OR Month(DateOfOrder) = 06;
-- 17) Ukaz pouze nazvy polozek.
	SELECT Name FROM Items;

-- 18) Ukaz pouzev nazev a cenu polozek.
	SELECT Name, Price FROM Items;

-- *19) Ukaz pouze e-mailove adresy uzivatelu a serad je abecedne.
        SELECT Email FROM Users ORDER BY Email;
-- 20) Ukaz, kolik uzivatelu nechce newsletter.
	SELECT Count(*) FROM Users WHERE Newsletter = 0;
	-- Count je tzv. agregacni funkce, ktera pocita pocet radku, ktere by se danym dotazem zobrazily. Vysledkem dotazu tedy muze byt i cislo.

-- *21) Ukaz, na kolik polozek je nejaka sleva.
        SELECT Count(*) FROM Items WHERE Sale>1;
-- *22) Ukaz, kolik polozek je v kategorii obleceni. 
        SELECT Count(*) FROM Items WHERE Category=3;
-- *23) Ukaz vsechny polozky z kategorie potraviny serazene dle ceny.
        SELECT * FROM Items WHERE Category = 2 ORDER BY Price;
-- *24) Popiste, co vraci nasledujici dotaz:
	SELECT Count(*), Category FROM Items GROUP BY Category;
	-- 

------------------------------- DOTAZY PRO POKROCILE (tzn. BONUSOVE) -------------------------------

-- 25) Vypis jmeno a prijmeni uzivatelu, kteri si v e-shopu neco objednali.
	SELECT DISTINCT FirstName, LastName  FROM Orders INNER JOIN Users ON (Orders.UserID = Users.UserID);
	-- DISTINCT: aby se zobrazili dani uzivatele pouze jednou
	-- INNER JOIN ... ON ...: vytvori podmnozinu kartezskeho soucinu obou tabulek podle sdilene vlastnosti (zde to bylo uzivatelovo ID)
	
-- *26) Vypis e-maily vsech uzivatelu, kteri napsali nejakou recenzi.

-- *27) Kolikrat se v objednavkach objevila polozka s nazvem "Mleko"? (zadejte s vyuzitim nazvu polozky, nedohledavejte si jeji ID)

-- HODNE POKROCILE: *28) Vypis jmena uzivatelu, kteri si nic neobjednali, ale poslali recenzi.

