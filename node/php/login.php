<!doctype html>
<html lang="en">
  <head>
  <!-- Required meta tags -->
  <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">


    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-eOJMYsd53ii+scO/bJGFsiCZc+5NDVN2yr8+0RDqr0Ql0h+rP48ckxlpbzKgwra6" crossorigin="anonymous">

    <title>Netpincér - Vásárlás</title>
  </head>

  <body>
    <!-- Navbar -->
    <?php  ob_start();
        session_start();
        include("src/navbar.php");
    ?>
    <div class="container col-xl-10 col-xxl-8 px-4 py-5">
    
                        <div class="row align-items-center g-5 py-5">
                                <div class="col-lg-7 text-center text-lg-start">
                                    <h1 class="display-4 fw-bold lh-1 mb-3">Bejelentkezés</h1>
                                    <p class="col-lg-10 fs-4">Lorem ipsum dolor sit amet consectetur adipisicing elit. Vitae laborum pariatur blanditiis vero culpa quas est, facilis quam commodi facere maxime atque impedit repellendus, odio consequuntur mollitia molestiae sed. Quam.
                                                              Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quam ullam eius totam repellendus omnis veritatis voluptatum odio fugiat? </p>
                                </div>
                                    <div class="col-10 mx-auto col-lg-5">
                                            <form class="p-5 border rounded-3 bg-light" method="POST" action="" >
                                                <div class="form-floating mb-3">
                                                    <input type="text" class="form-control" id="auth_name" name="auth_name" placeholder="Kiss Pista" required>
                                                    <label for="auth_name">Felhasználónév</label>
                                                </div>
                                                <div class="form-floating mb-3">
                                                    <input type="password" class="form-control" id="auth_pass" name="auth_pass" placeholder="Password" required>
                                                    <label for="auth_pass">Jelszó</label>
                                                </div>
                                                <div class="checkbox mb-3">
                                                    <label>
                                                        <input type="checkbox" value="remember-me" disabled> Emlékezz rám
                                                    </label>
                                                </div>
                                                    <button class="w-100 btn btn-lg btn-primary" id="BTN_login" name="BTN_login" type="submit" >Bejelentkezés</button>
                                                    <span class="text-red" id="error_log"> </span>
                                                    <hr class="my-4">
                                                <small class="text-muted">
                                                    <?php 
                                                        if (isset($_POST['BTN_login'])) 
                                                        {  include("server.php");  Connect_To_Server("{ type:0, msgId:0}"); 
                                                            Connect_To_Server("{ type:1, clientID: ".$_SESSION["clientID"] .", username:'" . $_POST["auth_name"]  ."', password: '". $_POST["auth_pass"] . "', userType: 0 }"); 
                                                            if (isset($_SESSION["user"]->type) && $_SESSION["user"]->type == 1) 
                                                            {
                                                                if (isset($_SESSION["user"]->username)  && $_SESSION["user"]->username == $_POST["auth_name"]  ) 
                                                                {
                                                                    if (isset($_SESSION["user"]->password)  && $_SESSION["user"]->password == $_POST["auth_pass"]) 
                                                                    {
                                                                        header('location: home.php');
                                                                    }
                                                                    else{ echo "Helytelen jelszó";}
                                                                }
                                                                else {  echo "Helytelen felhasználónév";}
                                                            }
                                                            else {  echo "Hiba";}
                                                        }
                                              
                                                    ?>
                                                </small>
                                            </form>
                                            

                                    </div>
                        </div>   
        </div>

  
    <!-- Main -->

    <!-- Footer -->
    <?php  include("src/footer.php");  
 ob_end_flush();?>
    <!-- Footer --> 

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>

  </body>
</html>