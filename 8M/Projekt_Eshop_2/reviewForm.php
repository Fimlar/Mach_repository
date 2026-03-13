<?php 
// Start session abychom se dostali k datům uživatele (cože????)
session_start();
include "db.php";

// Jsme přihlášení? (další AI slop)
if (!isset($_SESSION["id"])) {
    // Pokud není přihlášený, nepustíme ho k zápisu recenze a pošleme ho na login (pokud není přihlášený tak si nezaslouží tady vůbec být)
    header("Location: login_page.php");
    exit();
}

$review = $_POST["review"];
$stars = $_POST["stars"];
$userId = $_SESSION["id"]; // Další AI magie, ale dává to smysl

$sql = "INSERT INTO Reviews (UserID, Stars, TextOfReview)
        VALUES ($userId, $stars, '$review')";

// Aby uživatel věděl, když se něco pokazí
if ($conn->query($sql)) {
    header("Location: reviews.php?status=success");
} else {
    echo "Chyba při ukládání: " . $conn->error;
}

// Původně jsem to napsal sám tak, že jsem prostě rovnou přistupoval k proměnné email
// Myslel jsem, že všechny proměnné jsou globální tak se na ní prostě můžu odkázat
// Ale prý se to musí dělat přes to session tak to už udělalo gemini
// Ale měl jsem to v podstatě stejně 💪

exit();
?>