<?php 
    session_start();  
    $var = 0;
    if(isset($_GET["id"]))
    {
        $_SESSION["curID"]  = $_GET["id"]-1;
        $var = $_SESSION["curID"];
    }

    if (!isset($_SESSION['log'])) {
        $_SESSION['msg'] = "You must log in first";
        header('location: index.php');
    }
    if (isset($_GET['logout'])) {
        session_destroy();
        unset($_SESSION['log']);
        header("location: index.php");
    }

    if ($_SESSION["log"] != true) {
        echo "You must log in first";
        header('location: index.php');
    }
    else
    {
        //{ type:11 , clientID: client_ID}
    }
?>
<!doctype html>
<html lang="en">
  <head>
   <?php  include("src/auth_head.php")?>
  </head>

  <body>

  <?php  include("src/auth_nav.php")?>
          
  
    <!-- Main-->
    <div class="container">
      <div class="jumbotron">
          <h1> Éttermek</h1>
          <hr>
          <p> AKA </p>
      </div>
    </div>

    <hr>

    <div class="container" >
      <div class="row py-lg-5">
        <div class="col col-4 col-lg-4"> 

          <div class="card" style="width: 18rem;">
            <img src="..." class="card-img-top" alt="logo">
            <div class="card-body">
              <h5 class="card-title fw-bold">  <?= $_SESSION["ettermek"]->restaurantList[$var]->name  ?> </h5>
              <p class="card-text">  <?= $_SESSION["ettermek"]->restaurantList[$_SESSION["curID"]]->restaurantDescription  ?> </p>
            </div>
            <ul class="list-group list-group-flush">
              <li class="list-group-item">Stílus: <?= $_SESSION["ettermek"]->restaurantList[$_SESSION["curID"]]->style  ?> </li>
              <li class="list-group-item fw-bold">Nyitvatartás: <?= $_SESSION["ettermek"]->restaurantList[$_SESSION["curID"]]->fromHour ?> -
              <?= $_SESSION["ettermek"]->restaurantList[$_SESSION["curID"]]->toHour ?> :<?= $_SESSION["ettermek"]->restaurantList[$var]->toMinute ?> </li>
              <li class="list-group-item text-center"></li>
            </ul>
            <div class="card-body">
              <a href="#" class="card-link">06 99 123 4567</a>
              <a href="#" class="card-link">x</a>
            </div>
          </div>
        </div>
        
        <div class="col col-lg-4 col-lg-8"> 
            <?php 
            ob_start();
            var_dump($var); echo $var;
            var_dump($_SESSION["curID"]);  var_dump($_SESSION["kaja"]->listFood); 
            var_dump(  $_SESSION["category_ids"]); 
            $_SESSION["cur_Rest"] = $var;
            include("server.php");   
            //echo "Az id = "; 
            //echo $_GET["id"];
            Connect_To_Server(" { type: 7, clientID: " . $_SESSION["user"]->clientID . ", restaurantID: " . $var. " } "); 
            //var_dump($_SESSION["category_ids"]);

            for ($i=0; $i < count($_SESSION["category_ids"]); $i++) 
            { 
                echo '<div class="row mb-2">';
                    echo '<div class="row g-0 border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative"> ';
                        echo '<div class="col p-4 d-flex flex-column position-static"> ';
                            echo ' <h2 class="mb-0"> ' .$_SESSION["category_names"][$i] .  ' </h2>';
                                echo '<div class="p-4 d-flex flex-column position-static "> ';
                                    Connect_To_Server(" { type: 9, clientID: " . $_SESSION["user"]->clientID . ", restaurantID: " . $var. ", categoryID: ".$_SESSION["category_ids"][$i] ." } "); 
                                    for ($j=0; $j <count($_SESSION["kaja"]->listFood)  ; $j++) { 
                                        echo "<form method='POST' action='addToList.php'>";
                                        echo '<div class="p-4 d-flex flex-column position-static g-0 border rounded overflow-hidden"> <b>';
                                            echo $_SESSION["kaja"]->listFood[$j]->Name . ' - ';    echo $_SESSION["kaja"]->listFood[$j]->Price; echo "Ft";
                                             echo '<input type="hidden" id="foodID" name="foodID" value=" '.  $_SESSION["kaja"]->listFood[$j]->FoodID. ' ">';
                                           //var_dump($_SESSION["kaja"]->listFood[$j]);
                                            echo " </b>  - ";
                                            if (isset($_SESSION["kaja"]->listFood[$j]->From)) {
                                                echo "Elérhető: "; echo $_SESSION["kaja"]->listFood[$j]->From . ' - '. $_SESSION["kaja"]->listFood[$j]->To ; 
                                            }
                                            else {echo "Elérhető <br>";}
                                         
                                            echo " Allergének: <br>";
                                            if (isset($_SESSION["kaja"]->listFood[$j]->Allergenes)) {
                                               
                                                for ($k=0; $k < count($_SESSION["kaja"]->listFood[$j]->Allergenes); $k++) { 
                                                    echo '‎ &nbsp; - ' . $_SESSION["kaja"]->listFood[$j]->Allergenes[$k] . '<br>';
                                                }
                                            }
                                            else{ echo "Nincs";}

                                            echo '<button type="submit" id="BTN_addFood" name="BTN_addFood" class="btn btn-primary">Kosárhoz adom</button>';
                                          
                                           
                                        echo "</div><br>";
                                        echo "</form>";
                                    }
                                    
                                echo '</div> ';
                            echo ' <strong class="card-text mb-auto"> </strong>';
                        echo '</div>';
                    echo '</div>';
                echo '</div>';
            }
            ?>
                <?php 

                    if ($_SERVER["REQUEST_METHOD"] == "POST") 
                    { 
                        if(isset($_POST["BTN_addFood"]))
                        { 

                            array_push($_SESSION["kosar"], $_POST["foodID"]);
                            var_dump($_SESSION["kosar"]);
                            //$_SESSION["kosár"] = $kosar;
                            //header("location: ./restaurant.php/ ". $_SESSION["curID"] ."");
                        }
                    }
                ?>

        </div>
        </div>
      </div>
    </div>
    <!-- Main-->

       
    <?php  
    include("src/footer.php");  ob_end_flush();
    ?>
    <!-- Main-->
    
</body>
</html>