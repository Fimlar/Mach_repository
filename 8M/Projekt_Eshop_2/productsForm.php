<?php 
// Start session abychom se dostali k datům uživatele (cože????)
session_start();
include "db.php";

// Čeknutí přihlášení
if (!isset($_SESSION["id"])) {
    header("Location: login_page.php");
    exit();
}

$category = $_POST["categories"];
$order = $_POST["order"];
$stock = $_POST["stock"];

?>