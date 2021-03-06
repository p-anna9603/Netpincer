<!doctype html>
<html lang="en">
  <head>
   <?php  include("src/auth_head.php")?>
  </head>

  <body>

    <!-- Navbar -->
    <?php  include("src/navbar.php");
           include("server.php"); 
    ?>
    <!-- Navbar-->
     
    <!-- Main-->
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
    
    <!-- Main-->

    
    <div class="container">
        <div class="card">
                <p class="lead mb-4">Tesztelődoboz</p>
        </div>
    </div>

    
    <!-- Footer -->
    <?php  include("src/footer.php"); ?>

  </body>
</html>

