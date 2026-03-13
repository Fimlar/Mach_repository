<?php 
include "db.php";

$FirstName = $_POST["first-name"];
$LastName = $_POST["last-name"];
$Email = $_POST["email"];
$Password = $_POST["pwd"];

if($_POST["remember"] == "on"){
    $Newsletter = "TRUE";
}
else{
    $Newsletter = "FALSE";
}

$sql = "SELECT * FROM users WHERE Email = '$Email'";

$result = $conn->query($sql);

if ($result->num_rows != 0){
    echo "<script>alert('Tento email už je registrovaný');</script>";
    Header("Location:login_page.php");
}


$sql = "INSERT INTO users (FirstName, LastName, Email, Newsletter, Password)
        SELECT '$FirstName', '$LastName', '$Email', $Newsletter, '$Password'
        WHERE NOT EXISTS (
            SELECT 1 FROM users WHERE Email = '$Email'
        )";

$result = $conn->query($sql);

Header("Location:login_page.php");
?>