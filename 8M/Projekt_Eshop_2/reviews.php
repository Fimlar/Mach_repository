<?php session_start();
include "header.html";
include "db.php";
?>
    <!--Formulář na recenze-->
    <form id="form" action="reviewForm.php" method="post">
        <label>Write review</label>
        <input type="text" placeholder="Write review">

        <div>
                <button type="submit">Send</button>
                <!-- Nutné zde dát type="sumbit" jako signál pro vyhodnocení formuláře (pomocí action a method)-->
            </div>
    </form>
    <!--Zobrazení recenzí-->
    <?php
        $sql = "SELECT * FROM reviews LEFT JOIN users ON reviews.UserID = users.UserID;";
        $result = $conn->query($sql);

        while ($item = $result->fetch_assoc()) {
            ?> 
            <div class="review">
                <h3 class="review-author"><?php echo $item["FirstName"] . " " . $item["LastName"] ?></h2>
                <p class="review-text"><?php echo $item["TextOfReview"]?></p> <?php

                switch ($item["Stars"]) {
                    case 1:
                        echo '<p class="stars"> ★ ☆ ☆ ☆ ☆</p>';
                        break;
                    case 2:
                        echo '<p class="stars"> ★ ★ ☆ ☆ ☆</p>';
                        break;
                    case 3:
                        echo '<p class="stars"> ★ ★ ★ ☆ ☆</p>';
                        break;
                    case 4:
                        echo '<p class="stars"> ★ ★ ★ ★ ☆</p>';
                        break;
                    case 5:
                        echo '<p class="stars"> ★ ★ ★ ★ ★</p>';
                        break;
                }
                ?>
            </div><?php
        }
    ?>


</body>
</html>