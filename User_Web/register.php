<!doctype html>
<html lang="en">
  <head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link rel="stylesheet" type="text/css" href="//fonts.googleapis.com/css?family=Raleway" />
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-eOJMYsd53ii+scO/bJGFsiCZc+5NDVN2yr8+0RDqr0Ql0h+rP48ckxlpbzKgwra6" crossorigin="anonymous">
    <link href="src/style.css" rel="stylesheet">

    <title>Netpincér</title>
  </head>

  
  <body> 
    <!-- Navbar -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">Netpincér</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item">
              <a class="nav-link "  href="index.php">Home</a>
            </li>
            <li class="nav-item">
              <a class="nav-link active" aria-current="page"href="#">Felhaszáló</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Futár</a>
            </li>
            <li class="nav-item">
              <a class="nav-link disabled" href="#" tabindex="-1" aria-disabled="true">Étterem</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <!-- Navbar -->
   
    <!-- Main-->



   
    <div class="container col-xl-10 col-xxl-8 px-4 py-5">
        <div class="accordion" id="accordion_Login_Reg">
            <div class="accordion-item">
                <h2 class="accordion-header" id="headingOne">
                <button class="accordion-button" type="button" data-toggle="collapse" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                    Bejelentkezés
                </button>
                </h2>
                <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                    <div class="accordion-body">
                        <div class="row align-items-center g-5 py-5">
                                <div class="col-lg-7 text-center text-lg-start">
                                    <h1 class="display-4 fw-bold lh-1 mb-3">Bejelentkezés</h1>
                                    <p class="col-lg-10 fs-4">Lorem ipsum dolor sit amet consectetur adipisicing elit. Vitae laborum pariatur blanditiis vero culpa quas est, facilis quam commodi facere maxime atque impedit repellendus, odio consequuntur mollitia molestiae sed. Quam.</p>
                                </div>
                                    <div class="col-10 mx-auto col-lg-5">
                                            <form class="p-5 border rounded-3 bg-light">
                                                <div class="form-floating mb-3">
                                                    <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com">
                                                    <label for="floatingInput">Felhasználónév</label>
                                                </div>
                                                <div class="form-floating mb-3">
                                                    <input type="password" class="form-control" id="floatingPassword" placeholder="Password">
                                                    <label for="floatingPassword">Jelszó</label>
                                                </div>
                                                <div class="checkbox mb-3">
                                                    <label>
                                                        <input type="checkbox" value="remember-me"> Emlékezz rám
                                                    </label>
                                                </div>
                                                    <button class="w-100 btn btn-lg btn-primary" type="submit">Bejelentkezés</button>
                                                    <hr class="my-4">
                                                <small class="text-muted">Lorem ipsum dolor sit amet</small>
                                            </form>
                                    </div>
                        </div>   
                    </div>
                </div>
            </div>
            <div class="accordion-item">
                <h2 class="accordion-header" id="headingTwo">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                        Regisztráció
                    </button>
                </h2>
                <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                    <div class="accordion-body">
                        <div class="row align-items-center g-5 py-5">
                                    <div class="col-lg-7 text-center text-lg-start">
                                    <form class="p-5 border rounded-3 bg-light">
                                                    <div class="form-floating mb-3">
                                                        <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com">
                                                        <label for="floatingInput">Felhasználónév</label>
                                                    </div>
                                                    <div class="form-floating mb-3">
                                                        <input type="password" class="form-control" id="floatingPassword" placeholder="Password">
                                                        <label for="floatingPassword">Jelszó</label>
                                                    </div>
                                                    <div class="checkbox mb-3">
                                                        <label>
                                                            <input type="checkbox" value="remember-me"> Emlékezz rám
                                                        </label>
                                                    </div>
                                                        <button class="w-100 btn btn-lg btn-primary" type="submit">Bejelentkezés</button>
                                                        <hr class="my-4">
                                                    <small class="text-muted">Lorem ipsum dolor sit amet</small>
                                                </form>
                                       </div>
                                        <div class="col-10 mx-auto col-lg-5">
                                        <h1 class="display-4 fw-bold lh-1 mb-3">Regisztráció</h1>
                                        <p class="col-lg-10 fs-4">Lorem ipsum dolor sit amet consectetur adipisicing elit. Vitae laborum pariatur blanditiis vero culpa quas est, facilis quam commodi facere maxime atque impedit repellendus, odio consequuntur mollitia molestiae sed. Quam.</p>
                                    
                                        </div>
                        </div>     
                    </div>
                </div>
            </div>
            <div class="accordion-item">
                <h2 class="accordion-header" id="headingThree">
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                    Accordion Item #3
                </button>
                </h2>
                <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                <div class="accordion-body">
                    <strong>This is the third item's accordion body.</strong> It is hidden by default, until the collapse plugin adds the appropriate classes that we use to style each element. These classes control the overall appearance, as well as the showing and hiding via CSS transitions. You can modify any of this with custom CSS or overriding our default variables. It's also worth noting that just about any HTML can go within the <code>.accordion-body</code>, though the transition does limit overflow.
                </div>
                </div>
            </div>
        </div>


       
    </div>

  
<!-- Main -->


  <div class="container">
  <div class="card">
            <p class="lead mb-4">Tesztelődoboz</p>

            <script src="/socket.io/socket.io.js"></script>
            <script>
                  const socket = io("localhost:11000");
                  socket.connect();
                
                  socket.on("connect", () => {
                    console.log(socket.id); // x8WIv7-mJelg7on_ALbx
                  });
                  socket.on('data',() => {
                    console.log("received data" );
                  });
           
            </script>
            </div>
    
  </div>

  <!-- Footer -->
<footer class="bg-light text-center text-lg-start">
  <!-- Grid container -->
  <div class="container p-4">
    <!--Grid row-->
    <div class="row">
      <!--Grid column-->
      <div class="col-lg-6 col-md-12 mb-4 mb-md-0">
        <h5 class="text-uppercase">Készítették</h5>

        <p>
          Pauló Anna <br>
          Bokor Klaudia<br>
          Soós Péter<br>
          Szabó Marcell
        </p>
      </div>
      <!--Grid column-->

      <!--Grid column-->
      <div class="col-lg-3 col-md-6 mb-4 mb-md-0">
        <h5 class="text-uppercase">Lorem</h5>

        <ul class="list-unstyled mb-0">
          <li>
            <a href="#!" class="text-dark">Ipsum</a>
          </li>
          <li>
            <a href="#!" class="text-dark">dolor</a>
          </li>
          <li>
            <a href="#!" class="text-dark">sit</a>
          </li>
          <li>
            <a href="#!" class="text-dark">amet</a>
          </li>
        </ul>
      </div>
      <!--Grid column-->

      <!--Grid column-->
      <div class="col-lg-3 col-md-6 mb-4 mb-md-0">
        <h5 class="text-uppercase mb-0">Links</h5>

        <ul class="list-unstyled">
          <li>
            <a href="#!" class="text-dark">Link 1</a>
          </li>
          <li>
            <a href="#!" class="text-dark">Link 2</a>
          </li>
          <li>
            <a href="#!" class="text-dark">Link 3</a>
          </li>
          <li>
            <a href="#!" class="text-dark">Link 4</a>
          </li>
        </ul>
      </div>
      <!--Grid column-->
    </div>
    <!--Grid row-->
  </div>
  <!-- Grid container -->

  <!-- Copyright -->
  <div class="text-center p-3" style="background-color: rgba(0, 0, 0, 0.2);">
    © 2020 Copyright:
    <a class="text-dark" href="https://mdbootstrap.com/">MDBootstrap.com</a>
  </div>
  <!-- Copyright -->
</footer>
<!-- Footer -->

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>

  </body>
</html>