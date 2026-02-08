-- Pořadí vyhodnocování SQL dotazu
--  1. FROM - z které (kterých v případě JOINů) tabulek bereme data
--  2. WHERE - vyfiltrujeme data
--  3. GROUP BY - seskupíme data
--  4. HAVING - vyfiltrujeme seskupená data
--  5. SELECT - vyberou se dané sloupce
--  6. ORDER BY - seřazení dat
--  7. LIMIT - určuje počet řádků, které se zobrazí (někdy nás zajímají třeba jen první tři nebo prvních 10)

-- 1. Zobrazte cenu nejdražší položky z kategorie č. 2 (Potraviny).
    SELECT Max(Price) FROM Items GROUP BY Category HAVING Category = 2;
-- 2. Zobrazte cenu nejdražší položky za každou kategorii:
    SELECT Max(Price) FROM Items GROUP BY Category;
-- 3. Zobrazte cenu nejdražší položky za každou kategorii. Výsledek je seřazen podle těch maximálních cen:
    SELECT Max(Price) FROM Items GROUP BY Category ORDER BY Max(Price) desc;
-- 4. Zobrazte cenu nejdražší položky za každou kategorii. Výsledek je seřazen primárně dle kategorií, sekundárně dle jména:
    SELECT Max(Price) FROM Items GROUP BY Category ORDER BY Category,Name;
-- 5. Zobrazte průměrnou cenu položek za každou kategorii, pokud je ta průměrná cena vyšší než 100 Kč. 
    SELECT Avg(Price) FROM Items GROUP BY Category having Avg(Price)>100;
-- 6. Zobrazte ID kategorií a počty* položek (*různé položky, ne Pieces) v dané kategorii.
    
-- 7. Zobrazte průměrnou cenu položek dle kategorií.

-- 8. Zobrazte ID kategorie a průměrnou cenu položek dle kategorií, které mají alespoň 3* položky. 

-- 9. Zobrazte ID kategorií a počty* položek v dané kategorii seřazené od nejzastoupenější kategorie po tu nejméně zastoupenou.

-- 10. Vypište z recenzí: ID recenze, e-mail toho, kdo ji napsal, a kolik má hvězdiček.

-- 11. Vypište z objednávek id objednávky, název objednané položky a cenu položky (za kus).

-- 12. Vypište názvy všech produktů, které si objednal uživatel s Id = 1 a datum, kdy si to objednal.

-- 13. Vypiš všechny osobní údaje o uživatelích, kteří si něco objednali a zároveň napsali recenzi.

-- 14. Vypiš součty cen produktů (za kus) seskupených podle dne objednání seřazené od dne, kde je součet nejvyšší. Konkrétní datumy také vypište.

-- 15. Vypište e-mailové adresy všech uživatelů, kteří chtějí newsletter, ale neposlali recenzi.

-- 16. Ukaž všechny objednávky z dubna. (využijte LIKE: https://www.w3schools.com/sql/sql_like.asp)

-- 17. Ukaž všechny objednávky z dubna. (využijte Month(): https://www.w3schools.com/sql/func_sqlserver_month.asp)

