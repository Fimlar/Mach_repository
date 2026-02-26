<?php 
include "db.php";

$email= $_POST["email"];
$pwd = $_POST["pwd"];


//ověříme, zda zadal správné heslo:
$sql = "SELECT * FROM Users WHERE Email='$email' AND Password='$pwd'"; 
$result = $conn->query($sql); // spustíme SQL dotaz na naší databázi
if ($result->num_rows == 0){ // dotaz nevrátil žádné řádky -> uživatelské jméno a heslo se neshodují

    //vrátíme se na přihlašování + předáme paramter pwd, že heslo nebylo v pořádku
    Header("Location:login_page.php?pwd=false"); 
    
}
else // dotaz vrátil nějaký řádek -> shoduje se username a zadané heslo
{
    //poznamenáme si, co se bude hodit vědět o přihlášeném uživateli do $_SESSION
    $row=$result->fetch_assoc(); // rozsekáme řádek po sloupcích do asociativního pole $row
    if(session_id() == ''){ //pokud ještě není žádná session založena (např. uživatel už není přihlášený)
        session_start();
        $_SESSION["id"]=$row["UserID"]; 
        $_SESSION["first-name"]=$row["FirstName"];
        $_SESSION["last-name"]=$row["LastName"];
    }
    
    // Přejdeme na hlavní stránku e-shopu
    Header("Location:main_page.php"); 
}
?>