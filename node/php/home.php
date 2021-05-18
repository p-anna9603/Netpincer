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
            <div class="px-4 pt-5 my-5 text-center border-bottom">
                <h1 class="display-4 fw-bold"> Üdvözöllek <?= $_SESSION["user"]->username; ?> </h1>
                    <div class="col-lg-6 mx-auto">
                    <p class="lead mb-4"><i>Már kezdheted is a rendelést</i></p>
                    </div>

                    <div class="overflow-hidden" style="max-height: 30vh;">
                    <div class="container px-5">
                        <img src="bootstrap-docs.png" class="img-fluid border rounded-3 shadow-lg mb-4" alt="Example image" width="700" height="500" loading="lazy">
                    </div>
                    </div>
                </div>

      
  <div class="container">
        <div class="card">
                  <p class="lead mb-4">Tesztelődoboz</p>
      
                  Üdv!  <?= $_SESSION["user"]->username; ?>  <br>
                  Session tulajdonságai:<br>
                  átadott user: <?= $_SESSION["user"]->clientID; ?>  <br>
                  session email:  <?= $_SESSION["user"]->email; ?> <br>
                  session User : <%= session_var.user.firstname %> <br>

        </div>
    </div>
    
    
    <!-- Main-->
    <!-- Navbar -->
    <?php  
    session_start();
    include("src/navbar.php");
           include("server.php"); 
    ?>
    <!-- Main-->
    
</body>
</html>