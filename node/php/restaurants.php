<?php 
    session_start();


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
          <p> Jó étvágyat kívánunk </p>
      </div>
    </div>

    <hr>
    <div class="container" >

      <div class="row mb-2">
        <?php 
        include("server.php");   
        $_SESSION["kosar"] = array();
        Connect_To_Server("{ 'type':11, 'clientID': " . $_SESSION["user"]->clientID . "}"); 
        //var_dump($_SESSION["ettermek"]->restaurantList );
        for ($i=0; $i < $_SESSION["etterem_db"]; $i++) { 
          echo '<div class="row g-0 border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">';
            echo '<div class="col p-4 d-flex flex-column position-static">';
              echo '<h2 class="mb-0"> '.  $_SESSION["ettermek"]->restaurantList[$i]->name .' </h2>';
              echo '<div class="mb-1 text-muted">' . $_SESSION["ettermek"]->restaurantList[$i]->line1 . " ". $_SESSION["ettermek"]->restaurantList[$i]->line2 .' </div>';
              echo '<p class="card-text mb-auto">' .  $_SESSION["ettermek"]->restaurantList[$i]->restaurantDescription . ' </p>';
              echo '<strong class="card-text mb-auto">Nyitvatartás: ' . $_SESSION["ettermek"]->restaurantList[$i]->fromHour . '-' .  $_SESSION["ettermek"]->restaurantList[$i]->toHour . ':' .  $_SESSION["ettermek"]->restaurantList[$i]->toMinute.  '</strong>';
          echo '<a href="restaurant.php/?id=' .$_SESSION["ettermek"]->restaurantList[$i]->restaurantID.' " class="stretched-link">Innen rendelek</a>';
          echo " </div>";
          echo '<div class="col-auto d-none d-lg-block">
          <svg class="bd-placeholder-img" width="250" height="250" xmlns="http://www.w3.org/2000/svg" role="img" aria-label="Placeholder: Thumbnail" preserveAspectRatio="xMidYMid slice" focusable="false"><title>Placeholder</title><rect width="100%" height="100%" fill="#55595c"></rect><text x="50%" y="50%" fill="#eceeef" dy=".3em">Sample Image</text></svg>

        </div>';
          echo "</div>";
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