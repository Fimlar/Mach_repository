<?php session_start();
include "db.php"; // spojení s databází
include "header.html"; // vložení e-shopové hlavičky s navigačním menu
?>
<h1 class="products-header">Naše nabídka</h1>
<h2 class="products-header">Řazení a filtry zboží</h2>

<div class="products-grid">
    <?php
        $sql = "SELECT * FROM Items";
        $result = $conn->query($sql); // spuštěním SQL dotazu z databáze získáme všechny produkty


        while ($item = $result->fetch_assoc()) {       
            // s využitím while cyklu a funkce fetch_assoc() může procházet řádek po řádku výsledek SQL dotazu 
            // a přistupovat k hodinotám v jednotlivých sloupcích

            // následující podmínkou hledáme produkty, které už nejsou skladem 
            // a přidáme jim stylovací třídu "sold-out", aby se zobrazily šedé
            if($item["Pieces"] > 0) {  ?> 
                <div class="item"> <!-- Počáteční otevírací tag obalovače por každou jednu položku -->
    <?php   } else { ?>
                <div class="item sold-out">
    <?php   }
    ?>
                    <!-- Zde probíhá vytváření jednotlivých položek uvnitř divu s class="item" -->
                    <img src="products/<?php echo $item["ItemID"] ?>.jpg" > <!-- Vezmeme obrázek označen ID produktu  -->
                    <div class="item-text">
                        <h2><?php echo $item["Name"] ?></h2> <!-- Přidáme název položky -->
                        <p><?php echo $item["Price"] ?> Kč</p> <!-- Přidáme cenu položky -->               
                    </div>    
                    <?php 
                        // Pokud je na položku sleva, přidáme speciální paragraf
                        if($item["Sale"]>1) { 
                            $sale_id = $item["Sale"];
                            $sql = "SELECT * FROM Sales Where SaleID = $sale_id"; // procenta a typ slevy získáme z tabulky Sales pomocí konkrétního ID slevy z tabulky Items
                            $sale = $conn->query($sql); 
                            $sale_info = $sale->fetch_assoc();
                        ?>
                        <p class="sale"><?php echo "-" . $sale_info["Percentage"] . "% (" . $sale_info["Name"] . ")"; ?></p>        
                <?php } ?>
                </div>

    <?php
        }
    ?>
</div>
</body> <!-- V includnuté header.html otevřeme <body> i <html>, tady je zase poctivě zavřeme. -->
</html>