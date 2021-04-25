
let User = class
{
    constructor(type,clientID,username, password, firstName, phoneNumber, city, zipcode, line1, line2, userType, email) {
        this.type = type;
        this.clientID = clientID;
        this.username = username;
        this.password = password;
        this.firstName = firstName;
        this.phoneNumber = phoneNumber;
        this.city = city;
        this.zipcode = zipcode;
        this.line1 = line1;
        this.line2 = line2;
        this.userType = userType;
        this.email = email;
      }   
}
let Restaurant = class {
    constructor(restaurantID, name, restaurantDesc, style, owner, phonenumber, city, zipcode, line1, line2, fromHour,toHour, toMinute)
    {
        this.RestaurantID=restaurantID;
        this.Name = name;
        this.RestaurantDesc = restaurantDesc;
        this.Style = style;
        this.Owner = owner;
        this.PhoneNumber = phonenumber;
        this.City = city;
        this.Zipcode = zipcode;
        this.Line_1 = line1;
        this.Line_2 = line2;
        this.FromHour = fromHour;
        this.ToHour = toHour;
        this.ToMinute = toMinute;
    }
}
const first_JSON = { type:0, msgID:0 }

// MUST-HAVE ééééés DEPENDENCIES
var net = require('net');
var client = new net.Socket();
var express = require('express');
var  app = express();
var bodyParser = require('body-parser');
    app.use(bodyParser.json());
    app.use(bodyParser.urlencoded({extended : true}));
    app.use(express.json());
var path = require('path');
// MUST-HAVE ééééés DEPENDENCIES


var session = require('express-session');
const { send } = require('process');
const { json } = require('body-parser');
const { parse } = require('path');
    //var sess;
var login_var = false;

let logged; // current user
let Ettermek = []; // étterem array amit átad a sessionben
let Étterem_JSON;

let Kategoriak = [];
var sess = {
    secret: 'secret keyboard cat ',
    resave: false,
    saveUninitialized: true,
    cookie: { maxAge: 60000000}, // ms-ben van -> 360000 = 6 min, 60millió az 16 óra, mert mér ne
    loggedIn: false
  }

app.use(session(sess));

app.set('view engine', 'ejs');

app.get('/', function (req, res) {
    if (req.session.loggedIn) {
        console.log(session);
        res.redirect('/home'); // ha be van jelentkezve, és még él a süti, akkor abból felállítja a rendszert
    } else {
        console.log(session);
        res.render('pages/index');
    }
    Connect_To_Server(first_JSON); 
 
})

app.get('/login', function(req, res) {
    res.render('pages/login');
});

app.get('/register', function(req, res) {
    res.render('pages/register');
});

app.get('/restaurant', async function(req, res) {
    if (req.session.loggedIn) 
    {
        
        let id = req.query.id;
        getCategory(id,req,res);
        res.render('pages/restaurant', { 'id': id, 'Éttermek' : JSON.stringify(Ettermek), 'Kategóriák': JSON.stringify(Kategoriak) });
    }
    else {
		res.send('Please login to view this page!');
	}
    
 });

 app.get('/futar_login', async function(req, res) {
    res.render('pages/futar_login');
    
 });

 app.get('/futar_register', async function(req, res) {
    res.render('pages/futar_register');
    
 });

// HOMEPAGE
app.get('/home', function(request,response){
    if (request.session.loggedIn) 
    {
        getRestaurants(request,response);
        response.render('pages/home', {'data' : request.session.username, 'session_var': request.session });
    }
    else {
		response.send('Please login to view this page!');
	}
    //

});
// HOMEPAGE

// RESTAURANT PAGE
app.get('/auth_restaurants', function(request,response){
    if (request.session.loggedIn) 
    {
        if (Ettermek.length == 0) {
            getRestaurants(request,response); // Bekéri az összes éttermet -> frissíteni kell az odalt, ha újat kap az adatbázis
            console.log("Ettermek : " + Ettermek);
            response.render('pages/auth_restaurants', {'Éttermek' : JSON.stringify(Ettermek), 'session_var': request.session, 'Étterem_JSON': Étterem_JSON }); // session-ben átadja madj az éttermeket egy objektum array-ként TODO#######
        }
        else{
            console.log("Ettermek : " + Ettermek); // kirakja Objektként, hogy mennyit ad át
            console.log("Étterem JSON : " + Étterem_JSON); // teljes JSON
            response.render('pages/auth_restaurants', {'Éttermek' : JSON.stringify(Ettermek), 'session_var': request.session, 'Étterem_JSON': Étterem_JSON  }); // session-ben átadja madj az éttermeket egy objektum array-ként TODO#######
        }
    }
    else {
		response.send('Please login to view this page!');
	}
});
// RESTAURANT PAGE

 // LOGIN
app.post('/authentication', function(request, response) 
{
	var got_username = request.body.auth_name;
	var got_password = request.body.auth_pass;
    const login_JSON = { type:1, clientID: 0, username: got_username, password: got_password, userType: 0 }
    const jsonStr = JSON.stringify(login_JSON);
    console.log("JSON to send: "  + jsonStr);
    login_var = sendData(login_JSON, request, response);
    sleep(3000);
    console.log("Most: " + request.session.loggedIn );
});
// LOGIN

// REGISTER
app.post('/register_authentication', function(request, response) 
{
    var got_username = request.body.floating_username;
    var got_email = request.body.floating_email;
    var got_password_1 = request.body.floating_password;
    var got_password_2 = request.body.floating_password_2;

    var got_firstname = request.body.floating_first_name;
    var got_lastname = request.body.floating_last_name;
    var got_phonenumber = request.body.floating_phone;

    var got_city = request.body.floating_city;
    var got_ZIP = request.body.floating_ZIP;
    var got_street = request.body.floating_street + " " + request.body.floating_housenumber ;
    //var got_housenumber = ;
    var got_adressline_2 = request.body.floating_address_line_2;
    // type ID 4 a register user

    const reg_JSON = { type:4, clientID: 0, username: got_username, password: got_password_1, lastName: got_lastname, firstName: got_firstname, phoneNumber: got_phonenumber, 
         city: got_city, zipcode: got_ZIP, line1: got_street, line2: got_adressline_2 ,userType: 0, email: got_email, GetUserType: 0, Username:got_username }
    const jsonStr = JSON.stringify(reg_JSON);
    console.log("JSON to send: "  + jsonStr);
    login_var = sendData(reg_JSON, request, response);
    sleep(3000);
    console.log("Most: " + request.session.loggedIn );
});
// REGISTER

app.listen(8000);

//CONNECT TO SERVE FUNCTION
function Connect_To_Server (json) 
{
    const jsonStr = JSON.stringify(json);
    client.connect(11000,'localhost', function() {
         console.log('Connected to: ' + 'localhost' + ':' + 11000);
     });
    client.on('connect', function (connect) {
         console.log('First JSON to send : ');
         console.log(jsonStr);
         client.write(jsonStr);
     })

    client.on('data', function(data){
        var parsed_JSON = jsonParser(data);
         if (parsed_JSON["type"] == 0) {
            console.log("Received first data " + data);
            console.log("Handshake -> Type: 0");
         }
         else if (parsed_JSON["type"] == 99) {
            console.log("Received false login data : " + data);
            console.log("User not found");
            login_var = false;
        }
     })

    };
//CONNECT TO SERVE FUNCTION

//SEND_DATA FUNCTION   
function sendData(json_Object, request, response)
{
    const jsonStr = JSON.stringify(json_Object);
    client.write(jsonStr);

    client.on('data', function(data){
        var parsed_JSON = jsonParser(data);
        console.log(data);
        console.log(parsed_JSON);
        //var obj = JSON.parse(data);
        if (parsed_JSON["type"] == 1) { // get User Data
            console.log("Received login data : " + data);
            console.log("Handshake -> Type: 1 <- User Login");
            request.session.loggedIn = userParser(data);
            if (request.session.loggedIn  == true) 
            {
                //request.sess.loggedIn = true;
                request.session.username = logged.username;
                request.session.user = logged;
                response.redirect('/home');
                response.end();
            } else {
                //response.redirect('/login');
                response.end();
            }
        }
        else if(parsed_JSON["type"] == 4)
         {
            console.log("Received register data : " + data);
            console.log("Handshake -> Type: 4 <- User Login");
        }
        else if(parsed_JSON["Type"] == 7) // getCategories
        {
            console.log("Received Category data : " + data);
            console.log("Handshake -> Type: 7 <- Got Category");
            parsed_JSON["listOfCategoryNames"].forEach(element => {
                     etterem = RestaurantParser(element);
                     Ettermek.push(etterem);
                 });
        }
        else if(parsed_JSON["restaurantList"].length != 0)
                { 
                    Ettermek = [];
                    console.log(">> Received Restaurants");
                    console.log(data);
                    parsed_JSON["restaurantList"].forEach(element => {
                        etterem = RestaurantParser(element);
                        Ettermek.push(etterem);
                    });
                    Étterem_JSON = data;
                    const fs = require('fs'); 
                    let restik = JSON.stringify(Ettermek,null,2);
                    fs.writeFile('restaurants.json', restik, (err) => {
                            if (err) throw err;
                            console.log('>>> Restaurants have been written to file');
                        });
                
                }
            
        
        else
        {
            console.log("Hiba: " + error);
            console.log("Received Unknown Data :" + data);
        }
     })
    client.on('error', function(err) {
        console.log(err)
     })
}
//SEND_DATA FUNCTION   

function jsonParser(object) {

    var parsed_JSON = JSON.parse(object);
    return parsed_JSON;
 }

//USER PARSER FUNCTION 
 function userParser (object)
 {
    try {
        var p = jsonParser(object);
        logged = new User(p["type"],p["clientID"],p["username"],p["password"], p["firstName"], p["phoneNumber"], p["city"], p["zipcode"], p["line1"], p["line2"], p["userType"], p["email"]);
        console.log(logged);
        session.loggedIn = true;
    } catch (error) {
        console.log(error);
        session.loggedIn = false;
        return false;
    }
    console.log("> UserParser() kész!");
    return true;

 }
 //USER PARSER FUNCTION 

 //RESTAURANT PARSER FUNCTION 
 function RestaurantParser(p){
     try{
         //var p = jsonParser(object);
         //TODO - idk if it works
         rest = new Restaurant (p["restaurantID"],p["name"],p["restaurantDescription"],p["style"], p["owner"], p["phonenumber"], p["city"], p["zipcode"], p["line1"], p["line2"], p["fromHour"], p["toHour"], p["toMinute"]);
         //console.log(rest);
     } catch (error){
        console.log(error);
        return null;
     }
     console.log("> RestaurantParser() kész!");
     return rest;
 }
//RESTAURANT PARSER FUNCTION 

//SLEEP
async function slep(number) {
    console.log("slep begin");
    await sleep(number);
    console.log("slep end");
  }
  function sleep(ms) {
    return new Promise((resolve) => {
      setTimeout(resolve, ms);
    });
  }  
//SLEEP

//GET RESTAURANTS - 11-ES KÓD
  function getRestaurants(request, response)
  {
    const Get_Restaurant_JSON = { type:11, clientID: 0}
    const jsonStr = JSON.stringify(Get_Restaurant_JSON);
    console.log("Sent Restaurant JSON -> " + jsonStr)
    sendData(Get_Restaurant_JSON,request,response);
  }

//GET RESTAURANTS - 11-ES KÓD

  //GET CATEGORY - 7-es KÓD
function getCategory(restaurant_ID, request, response)
{
    const Get_Category_JSON = { type: 7, clientID: 0, restaurantID: restaurant_ID}
    const jsonStr = JSON.stringify(Get_Category_JSON);
    console.log("Sent Category JSON -> " + jsonStr)
    sendData(Get_Category_JSON,request,response);
}

   //GET CATEGORY - 7-es KÓD
   /*
   Received JSON: {
  "type": 7,
  "clientID": 0,
  "restaurantID": 1
}
   */


/*
GETFOODS
Received JSON: {
  "type": 9,
  "clientID": 0,
  "restaurantID": 1,
  "categoryID": 1

  1
2
3
4
5
6
7
10

12
3
4
5
6
7
10

12
FoodID: 1
Name: Hawaii pizza
Price: 1500
Rating: 0
PictureID: 0
CatID: 1
RestID: 1
From: 2021. 06. 01.
To: 2021. 09. 01.
Allergens:
FoodID: 2
Name: Magyaros pizza
Price: 1600
Rating: 0
PictureID: 0
CatID: 1
RestID: 1
From:
To:
Allergens:
}

*/