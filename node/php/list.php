<?php 
    session_start();
    if (!isset($_SESSION["kosar"])) {
        array_push($_SESSION["kosar"],"A kosarad üres");
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
          <h1> Kosár</h1>
          <hr>
          <p> Jó étvágyat kívánunk </p>
      </div>
    </div>

    <hr>
    <div class="container" >

      <div class="row mb-2">
      <form action="" method="POST">
        <?php 
        include("server.php");   
        $kosar = array();
        //var_dump($_SESSION["kosar"]);
        if ($_SESSION["kosar"][0] == "A kosarad üres") {
            # code...
        }
        else{
            echo "Rendelés innen: ";
            echo $_SESSION["ettermek"]->restaurantList[$_SESSION["cur_Rest"]+1]->name;
            echo "<br> A kosaradban jelenleg ennyi termék van:<br> "; $osszeg = 0;
            for ($i=0; $i < count($_SESSION["kosar"]); $i++) { 
                Connect_To_Server("{ 'type':14, 'clientID': " . $_SESSION["user"]->clientID . ", foodID: " . $_SESSION["kosar"][$i].  "}"); 
                //var_dump($_SESSION["kaja"]);
                echo " - " . $_SESSION["kaja"]->Name . " - " . $_SESSION["kaja"]->Price . "Ft"; $osszeg +=$_SESSION["kaja"]->Price;
                array_push($kosar,$_SESSION["kaja"]->FoodID);
            }
           echo "<br><b>Összesen: " . $osszeg . "Ft </b>";
        }
        
        //var_dump($_SESSION["ettermek"]->restaurantList );
      
        ?>
        <button type="submit" class="btn btn-success" id="BTN_rendel" name="BTN_rendel">Megrendelem</button>
    </form>
    <?php
        if (isset($_POST["BTN_rendel"])) {
            echo "Rendelés leadás"; $string = " " . $kosar[0];
            for ($i=0; $i < count($kosar); $i++) { 
               $string= $string . "," . $kosar[$i]; # code...
            }
            echo $_SESSION["cur_Rest"]+1;
            Connect_To_Server("{ 'type':18, 'clientID': " . $_SESSION["user"]->clientID . ",'restID':" . $_SESSION["cur_Rest"]+1 . ", 'username':'"
                 . $_SESSION["user"]->username . "','foods': '" . $string .  "','price': " . $osszeg ."}"); 
        }
    ?>

       
        <!-- GetRestauranList() -->
    </div>
   
    </div>
    <!-- Main-->
    <!-- Main-->
    <!-- Navbar -->
    <?php  
    include("src/footer.php");
    ?>
    <!-- Main-->
    
</body>
</html>