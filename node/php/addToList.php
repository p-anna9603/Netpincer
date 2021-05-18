<?php 
echo "hahó";
    if ($_SERVER["REQUEST_METHOD"] == "POST") 
    {  echo "b";
        if(isset($_POST["BTN_addFood"]))
        { 
            echo "c";
            array_push($kosar, $_POST["foodID"]);
            var_dump($kosar);
            $_SESSION["kosár"] = $kosar;
            header("location: ./restaurant.php/ ". $_SESSION["curID"] ."");
        }
    }
?>