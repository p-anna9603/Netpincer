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
              <a class="nav-link active" aria-current="page" href="#">Home</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Felhaszáló</a>
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
    <div class="px-4 pt-5 my-5 text-center border-bottom">
  <h1 class="display-4 fw-bold">Netpincér</h1>
  <div class="col-lg-6 mx-auto">
    <p class="lead mb-4"><i>"Ahol az étel házhoz jön"</i></p>
    <div class="d-grid gap-2 d-sm-flex justify-content-sm-center mb-5">
      <button type="button" class="btn btn-primary btn-lg px-4 me-sm-3">Lorem</button>
      <button type="button" class="btn btn-outline-secondary btn-lg px-4">Ipsum</button>
    </div>
  </div>
  <div class="overflow-hidden" style="max-height: 30vh;">
    <div class="container px-5">
      <img src="bootstrap-docs.png" class="img-fluid border rounded-3 shadow-lg mb-4" alt="Example image" width="700" height="500" loading="lazy">
    </div>
  </div>
</div>
    <!-- Main-->
    <div class="container">
        <div class="row">
        <div class="col-sm-4">
          <div class="card">
            <div class="card-body">
              <h5 class="card-title text-center">Vásárló</h5>
              <p class="card-text">Lorem, ipsum dolor sit amet consectetur adipisicing elit. Voluptatem earum, explicabo autem libero alias asperiores eius? Est odio soluta labore sit, dolores omnis sapiente in hic tempore quod incidunt expedita!</p>
              <a href="#" class="btn btn-primary">Kezdj vásárolni</a>
            </div>
          </div>
        </div>
        <div class="col-sm-4">
          <div class="card">
            <div class="card-body ">
              <h5 class="card-title text-center" >Futár</h5>
              <p class="card-text">Lorem ipsum dolor sit amet consectetur adipisicing elit. Quos iusto molestias sapiente consectetur. Fuga aut, delectus ducimus dolore optio assumenda vero velit ex. Nulla repellat velit accusantium doloremque, non sunt!</p>
              <a href="#" class="btn btn-primary">Kezdj Dolgozni</a>
            </div>
          </div>
        </div>
        <div class="col-sm-4">
          <div class="card">
            <div class="card-body">
              <h5 class="card-title text-center">Étterem</h5>
              <p class="card-text">Lorem ipsum dolor, sit amet consectetur adipisicing elit. Hic adipisci eligendi quas doloribus laudantium suscipit aut sequi sit molestiae amet deleniti provident rerum fugit, veniam, omnis at quis atque error!.</p>
              <a href="#" class="btn btn-primary btn-lg btn-block">Hirdesd meg</a>
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