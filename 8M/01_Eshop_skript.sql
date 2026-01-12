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
	-- Vrátí počet položek v jednotlivých kategoriích

------------------------------- DOTAZY PRO POKROCILE (tzn. BONUSOVE) -------------------------------

-- 25) Vypis jmeno a prijmeni uzivatelu, kteri si v e-shopu neco objednali.
	SELECT DISTINCT FirstName, LastName  FROM Orders INNER JOIN Users ON (Orders.UserID = Users.UserID);
	-- DISTINCT: aby se zobrazili dani uzivatele pouze jednou
	-- INNER JOIN ... ON ...: vytvori podmnozinu kartezskeho soucinu obou tabulek podle sdilene vlastnosti (zde to bylo uzivatelovo ID)
	
-- *26) Vypis e-maily vsech uzivatelu, kteri napsali nejakou recenzi.
        SELECT DISTINCT Email FROM Reviews INNER JOIN Users ON (Users.UserID = Reviews.UserID)
-- *27) Kolikrat se v objednavkach objevila polozka s nazvem "Mleko"? (zadejte s vyuzitim nazvu polozky, nedohledavejte si jeji ID)
        SELECT Count(*) FROM Orders INNER JOIN Items ON (Orders.ItemID = Items.ItemID) WHERE Items.Name = 'Mleko'
-- HODNE POKROCILE: *28) Vypis jmena uzivatelu, kteri si nic neobjednali, ale poslali recenzi.
        SELECT DISTINCT FirstName, LastName FROM Users JOIN Reviews ON (Users.UserID = Reviews.UserID) 
                                                       LEFT JOIN Orders ON (Users.UserID = Orders.UserID)
                                            WHERE Orders.OrderID IS NULL;
