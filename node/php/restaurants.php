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
      </div>
    </div>

    <hr>
    <div class="container" >

      <div class="row mb-2">
        <?php 
        include("server.php");   
        Connect_To_Server("{ 'type':11, 'clientID': " . $_SESSION["user"]->clientID . "}"); 
        ?>
        <div class="row g-0 border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
          <div class="col p-4 d-flex flex-column position-static">
            <h2 class="mb-0"><%= Étterem["Name"] %></h2>
            <div class="mb-1 text-muted"> <%= Étterem["City"] %>   <%= Étterem["Line_1"] %>  <%= Étterem["Line_2"] %>   </div>
            <p class="card-text mb-auto">Leírás: <%= Étterem["RestaurantDesc"] %></p>
            <div class="mb-1 text-muted"> Stílus: <%= Étterem["Style"] %></div>


            <strong class="card-text mb-auto">Nyitvatartás: <%= Étterem["FromHour"] %> - <%= Étterem["ToHour"]%>: <%= Étterem["ToMinute"]%></strong>
            <a href="/restaurant?id=<%= Étterem['RestaurantID']%> " class="stretched-link">Innen rendelek</a>
          </div>
          <div class="col-auto d-none d-lg-block">
            <svg class="bd-placeholder-img" width="250" height="250" xmlns="http://www.w3.org/2000/svg" role="img" aria-label="Placeholder: Thumbnail" preserveAspectRatio="xMidYMid slice" focusable="false"><title>Placeholder</title><rect width="100%" height="100%" fill="#55595c"></rect><text x="50%" y="50%" fill="#eceeef" dy=".3em">Sample Image</text></svg>
  
          </div>
        </div>
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