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
    
                        <div class="row align-items-center g-5 py-5">
                                <div class="col-lg-7 text-center text-lg-start">
                                    <h1 class="display-4 fw-bold lh-1 mb-3">Bejelentkezés</h1>
                                    <p class="col-lg-10 fs-4">Lorem ipsum dolor sit amet consectetur adipisicing elit. Vitae laborum pariatur blanditiis vero culpa quas est, facilis quam commodi facere maxime atque impedit repellendus, odio consequuntur mollitia molestiae sed. Quam.</p>
                                </div>
                                    <div class="col-10 mx-auto col-lg-5">
                                            <form class="p-5 border rounded-3 bg-light">
                                                <div class="form-floating mb-3">
                                                    <input type="text" class="form-control" id="floatingInput" placeholder="name@example.com">
                                                    <label for="floatingInput">Felhasználónév</label>
                                                </div>
                                                <div class="form-floating mb-3">
                                                    <input type="password" class="form-control" id="floatingPassword" placeholder="Password">
                                                    <label for="floatingPassword">Jelszó</label>
                                                </div>
                                                <div class="checkbox mb-3">
                                                    <label>
                                                        <input type="checkbox" value="remember-me" disabled> Emlékezz rám
                                                    </label>
                                                </div>
                                                    <button class="w-100 btn btn-lg btn-primary" type="submit">Bejelentkezés</button>
                                                    <hr class="my-4">
                                                <small class="text-muted">Lorem ipsum dolor sit amet</small>
                                            </form>
                                    </div>
                        </div>   
           <!-- Vezetéknév
  Keresztnév
  Telefon
  Cím
  Felhnév
  email
  jelszo 1 2--> 

                        <div class="row align-items-center g-5 py-5">
                                    <div class="col-lg-7 text-center text-lg-start">
                                    <form class="p-5 border rounded-3 bg-light">
                                                    <div class="form-floating mb-3">
                                                        <input type="text" class="form-control" id="floating_first_name" placeholder="Keresztnév">
                                                        <label for="floatingInput">Keresztnév</label>
                                                    </div>
                                                    <div class="form-floating mb-3">
                                                        <input type="text" class="form-control" id="floating_last_name" placeholder="Vezetéknév">
                                                        <label for="floatingInput">Vezetéknév</label>
                                                    </div>

                                                    <div class="form-floating mb-3">
                                                        <input type="number" class="form-control" id="floating_phone" placeholder="Telefonszám"> <!-- pattern="[0-9]{2}[0-9]{2}[0-9]{3}[0-9]{4}" -->
                                                        <label for="floatingInput">Telefonszám</label>
                                                    </div>

                                                    <div class="form-row">
                                                        <div class="form-floating mb-3">
                                                            <input type="text" class="form-control" id="floating_city" placeholder="Város">
                                                            <label for="floatingInput">Város</label>
                                                        </div>
                                                        <div class="form-floating mb-3">
                                                            <input type="text" class="form-control" id="floating_city" placeholder="Irányítószám">
                                                            <label for="floatingInput">Irányítószám</label>
                                                        </div>
                                                    </div>

                                                    <div class="form-floating mb-3">
                                                        <input type="text" class="form-control" id="floating_adrress" placeholder="Utcanév">
                                                        <label for="floatingInput">Utca</label>
                                                    </div>

                                                    <div class="form-row">
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputAddress">Address</label>
                                                            <input type="text" class="form-control" id="inputAddress" placeholder="1234 Main St">
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputAddress2">Address 2</label>
                                                            <input type="text" class="form-control" id="inputAddress2" placeholder="Apartment, studio, or floor">
                                                        </div>
                                                        <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                            <label for="inputCity">City</label>
                                                            <input type="text" class="form-control" id="inputCity">
                                                            </div>
                                                            <div class="form-group col-md-4">
                                                            <label for="inputState">State</label>
                                                            <select id="inputState" class="form-control">
                                                                <option selected>Choose...</option>
                                                                <option>...</option>
                                                            </select>
                                                            </div>
                                                            <div class="form-group col-md-2">
                                                            <label for="inputZip">Zip</label>
                                                            <input type="text" class="form-control" id="inputZip">
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="form-check">
                                                            <input class="form-check-input" type="checkbox" id="gridCheck">
                                                            <label class="form-check-label" for="gridCheck">
                                                                Check me out
                                                            </label>
                                                            </div>
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