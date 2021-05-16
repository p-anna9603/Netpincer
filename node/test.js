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
let Category = class
{
    constructor(names,IDs)
    {
        this.listOfCategoryNames = names;
        this.listOfCategoryIDs = IDs;
    }
    getList()
    {
        return this.listOfCategoryNames;
    }
}
let Food = class 
{
    constructor(foodID, name, price, rating, pictureID, allergenes,avaibleFrom, avaibleTo, restaurantID, categoryID )
    {
        this.FoodID = foodID;
        this.Name = name;
        this.Price = price;
        this.Rating = rating;
        this.PictureID =  pictureID;
        this.Allergenes = allergenes;
        this.CategoryID = categoryID;
        this.RestaurantID = restaurantID;
        this.AvaibleFrom = avaibleFrom;
        this.AvaibleTo = avaibleTo;
    }

}
// Classok
var net = require('net');
var client = new net.Socket();
var express = require('express');
var  app = express();

// MUST-HAVE ééééés DEPENDENCIES
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

var client_ID;  // ebben van tárolva a client id-ja
let logged; // current user
let Ettermek = []; // étterem array amit átad a sessionben
let Étterem_JSON; // Étterem JSON amit átad a session-ben
let Kajak = []; //kaják amit átad a session-ben

let Kategoriak; // kategóriák
var Bev_lista = []; // bevásárló lista <- ebbe mennek majd a kaják.

var sess = { // session változó
    secret: 'secret keyboard cat ', // titkos kulcs - nem tudom mire kell, de kell
    resave: false, // ötletem nincs, de kell
    saveUninitialized: true, // szintén
    cookie: { maxAge: 60000000}, // ms-ben van -> 360000 = 6 min, 60millió az 16 óra, mert mér ne
    loggedIn: false, // ezt majd változtatjuk
    kliens: client_ID // itt kapja meg a kliens ID-t
  }



const first_JSON = { type:0, msgID:0 } // a legelső JSON amit küld a szervernek

function jsonParser(object) { 
    var parsed_JSON = JSON.parse(object);
    return parsed_JSON;
} // be parse-ol egy kapott json filet

client.connect(11000,'localhost', function() { // csatlakozás a socketServerhez
    console.log('Connected to: ' + 'localhost on port' + ': ' + 11000);
});

client.on('connect', function () { // ha csatlakozott a socketServerhez, akkor elküldi a legelső JSON-t
    const jsonStr = JSON.stringify(first_JSON);
    console.log('JSON to send : ' + jsonStr); // kidumpolja a consol-ba
    client.write(jsonStr); // ezzel küldi el
})

client.on('data', function(data) { // ha a klienstől adatot kap
    var parsed_JSON = jsonParser(data); // jsonParser- meghívása
    if (parsed_JSON["type"] == 0) { // ha a kapott JSON-ben a type 0 <- legelső JSON amit kaphat
        console.log("Received client data: " + data);
        client_ID = parsed_JSON["clientID"];
        console.log("Current Client ID :" + client_ID);
     }
	client.destroy(); // kill client after server's response
});

client.on('close', function() {
	console.log('Connection closed'); // kiírja, hogy bezárt a kapcsolat
});

function sendData(json_Object,request, response)
{
    const jsonStr = JSON.stringify(json_Object); // átalakítom az objektet
    client = new net.Socket(); // új socket
    client.connect(11000,'localhost'); // csatlakozás
    client.write(jsonStr); // jsonstr küldése a socket-nek
    client.on('data', function(data) { // ha a klienstől adatot kap
        var parsed_JSON = jsonParser(data); // jsonParser- meghívása

         if (parsed_JSON["type"] == 1) { // get User Data
            console.log("Received login data : " + data); 
            request.session.loggedIn = userParser(data);
            if (request.session.loggedIn  == true) 
            {
                request.session.username = logged.username;
                request.session.user = logged;
                response.redirect('/home');
                response.end();
            } else {
                response.end();
            }
        }
        else if (parsed_JSON["type"] == 99) { // rossz bejelentkezési adatok
            console.log("Received false login data : " + data);
            console.log("User not found");
            login_var = false;
        }
        else if(parsed_JSON["type"] == 4) // regisztrációs adatok
        {
           console.log("Received register data : " + data);
        }
        else if(parsed_JSON["Type"] == 7) // getCategories
        {
           console.log("Received Category data" + data);
           Kategoriak= CategoryParser(parsed_JSON);
           console.log("Kategoriak feltöltve: " + Kategoriak.listOfCategoryNames);
        }
        else if(parsed_JSON["Type"] == 9) // getFoods
        {
            Kajak = [];
            let kaja;
            console.log("Received Food data : " + data);
            parsed_JSON["listFood"].forEach(element => {
               console.log(element);
               kaja = FoodParser(element);
               Kajak.push(kaja);
            });
            console.log("> FoodParser() kész!");
            console.log(Kajak);
           /* {"Type":9,"listFood":[{"Type":9,"FoodID":3,"Name":"Sajtkrem leves","Price":800.0,"Rating":0.0,"PictureID":0,"Allergenes":["Laktoz"],"AvailableFrom":"","AvailableTo":"","RestaurantID":1,"CategoryID":2},{"Type":9,"FoodID":4,"Name":"Gulyas leves","Price":1000.0,"Rating":0.0,"PictureID":0,"Allergenes":[],"AvailableFrom":"","AvailableTo":"","RestaurantID":1,"CategoryID":2}]}*/
       }
       else if(parsed_JSON["restaurantList"][0]["Type"] == 11)
               { 
                   Ettermek = [];
                   console.log(">> Received Restaurants" + data); var i = 1;
                   parsed_JSON["restaurantList"].forEach(element => {
                       etterem = RestaurantParser(element,i);
                       Ettermek.push(etterem);
                       i++;
                   });
                   console.log("> RestaurantParser() kész!");
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
            console.log("Hiba: ");
            console.log("Received Unknown Data :" + data);
        }
    
        client.destroy(); // kill client after server's response
    });
}

app.listen(8000); // LEGFONTOSABB RÉSZ - DO NOT DELETE

app.use(session(sess)); // az app a session változót fogja használni

app.set('view engine', 'ejs'); // beállítja a view engine-nek az ejs-t

app.get('/', function (req, res) { // a főoldalt nézi
    if (req.session.loggedIn) { // megnézi. hogy van e jelenlegi session, ergo be vagyunk e jelentkezve
        console.log("Session elkezdődött" + session);
        res.redirect('/home'); // ha be van jelentkezve, és még él a süti, akkor abból felállítja a rendszert és a /home-ba navigál
    } else {
       // Connect_To_Server(first_JSON); // ha nincs bejelentkezve, akkor csatlakozunk a szerverhez
       res.redirect('/index'); // majd redirektáljuk az index page-r
    }
})

app.get('/index', function (req, res) { // le rendereli a views/pages/index.ejs fájlt
    res.render('pages/index');
})

// LOGIN
app.get('/login', function(req, res) { // le rendereli a views/pages/login.ejs fájlt
    res.render('pages/login');
});

app.post('/authentication', function(request, response)  // megcsinálja a login felület metódusait
 {
     var got_username = request.body.auth_name; // input username
     var got_password = request.body.auth_pass; // input password
     const login_JSON = { type:1, clientID: client_ID, username: got_username, password: got_password, userType: 0 } // ezt fogja elküldeni a szervernek
     const jsonStr = JSON.stringify(login_JSON); // stringé alakítja
     console.log("LOGIN INITIATED -> JSON to send: "  + jsonStr); // kidumploja a log-ba
    sendData(login_JSON, request, response); // 
 });
// LOGIN

// REGISTER
app.get('/register', function(req, res) { // le rendereli a views/pages/register.ejs fájlt
    res.render('pages/register');
});
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
     var got_adressline_2 = request.body.floating_address_line_2;
     // type ID 4 a register user
 
     const reg_JSON = { type:4, clientID: client_ID, username: got_username, password: got_password_1, lastName: got_lastname, firstName: got_firstname, phoneNumber: got_phonenumber, 
          city: got_city, zipcode: got_ZIP, line1: got_street, line2: got_adressline_2 ,userType: 0, email: got_email, GetUserType: 0, Username:got_username }
     const jsonStr = JSON.stringify(reg_JSON);
     console.log("JSON to send: "  + jsonStr);
     sendData(reg_JSON, request, response);
 });
 // REGISTER

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

app.get('/restaurant', async function(req, res) { // le rendereli a views/pages/restaurant.ejs fájlt
    if (req.session.loggedIn) // megnézi, hogy be vagyunk-e jelentkezve
    {
        if (Kategoriak == null) { // ha a kategóriák nincs feltöltve
            let id = req.query.id;
            getCategory(id,req,res);
            res.render('pages/restaurant', { 'id': id, 'Éttermek' : JSON.stringify(Ettermek), 'Kategóriák': JSON.stringify(Kategoriak) });
        }
        else{ // ha fel van töltve akkor simán rendereli, és átadja adolgokat
            let id = req.query.id;
            res.render('pages/restaurant', { 'id': id, 'Éttermek' : JSON.stringify(Ettermek), 'Kategóriák': JSON.stringify(Kategoriak.listOfCategoryNames), 'IDk': JSON.stringify(Kategoriak.listOfCategoryIDs) });
        }
    }
    else { // ha nem vagyunk, akor küld egy hibát
		res.send('Please login to view this page!');
	}
    
 });
//GET RESTAURANTS - 11-ES KÓD
const getRestaurants  = async(request, response) => 
{
  const Get_Restaurant_JSON = { type:11, clientID: client_ID}
  const jsonStr = JSON.stringify(Get_Restaurant_JSON);
  console.log("Sent Restaurant JSON -> " + jsonStr)
  sendData(Get_Restaurant_JSON,request,response);
}
//GET RESTAURANTS - 11-ES KÓD

// RESTAURANT PAGE
app.get('/auth_restaurants', function(request,response){
    if (request.session.loggedIn) 
    {
        if (Ettermek.length == 0) {
            await(getRestaurants(request,response)); // Bekéri az összes éttermet -> frissíteni kell az odalt, ha újat kap az adatbázis
            Kategoriak = null;
            response.render('pages/auth_restaurants', {'Éttermek' : JSON.stringify(Ettermek), 'session_var': request.session, 'Étterem_JSON': Étterem_JSON }); // session-ben átadja madj az éttermeket egy objektum array-ként TODO#######
        }
        else{
            response.render('pages/auth_restaurants', {'Éttermek' : JSON.stringify(Ettermek), 'session_var': request.session, 'Étterem_JSON': Étterem_JSON  }); // session-ben átadja madj az éttermeket egy objektum array-ként TODO#######
        }
    }
    else {
		response.send('Please login to view this page!');
	}
});
// RESTAURANT PAGE

//GET CATEGORY - 7-es KÓD

app.get('/categories', async function(req, res) {

    if (req.session.loggedIn) 
    {
        let id = req.query.id; //macska id
        let esziID = req.query.restID; // eszi ID
        getFoods(esziID,id,req,res);
        res.render('pages/categories', { 'kajak' : JSON.stringify(Kajak) });
    }
    else
    {
        res.send('Please login to view this page!');
    }

    
 });


function getCategory(restaurant_ID, request, response)
{
    const Get_Category_JSON = { type: 7, clientID: client_ID, restaurantID: restaurant_ID};
    const jsonStr = JSON.stringify(Get_Category_JSON);
    console.log("Sent Category JSON -> " + jsonStr);
    sendData(Get_Category_JSON,request,response);
}

function getFoods(ID, macska, request, response)
{
    const Get_Foods_JSON = { type: 9, clientID: client_ID, restaurantID: ID, categoryID: macska};
    const jsonStr = JSON.stringify(Get_Foods_JSON);
    console.log("Sent Food JSON -> " + jsonStr);
    sendData(Get_Foods_JSON,request,response);
}














//PARSER FUNKCIÓK 

//USER PARSER FUNCTION 
function userParser (object)
{
   try {
       var p = jsonParser(object);
       logged = new User(p["type"],p["clientID"],p["username"],p["password"], p["firstName"], p["phoneNumber"], p["city"], p["zipcode"], p["line1"], p["line2"], p["userType"], p["email"]);
      
       session.loggedIn = true;
   } catch (error) {
       console.log(error);
       session.loggedIn = false;
       return false;
   }
   console.log("Current user: " + logged["username"]);
   return true;

}
//USER PARSER FUNCTION 

//RESTAURANT PARSER FUNCTION 
 function RestaurantParser(p,i){
    try{
        console.log("Rest: " + p["restaurantID"] + i);
        rest = new Restaurant (i,p["name"],p["restaurantDescription"],p["style"], p["owner"], p["phonenumber"], p["city"], p["zipcode"], p["line1"], p["line2"], p["fromHour"], p["toHour"], p["toMinute"]);
        //console.log(rest);
    } catch (error){
       console.log(error);
       return null;
    }

    return rest;
}
//RESTAURANT PARSER FUNCTION 

//CATEGORY PARSER
function CategoryParser(p){
    try{
        macska = new Category (p["listOfCategoryNames"], p["listOfCategoryIDs"]);
    } catch (error){
       console.log(error);
       return null;
    }
    console.log("> CategoryParser() kész!");
    return macska;
}
//CATEGORY PARSER

//FOOD PARSER
function FoodParser(p){  //foodID, name, price, rating, pictureID, allergenes, categoryID, restaurantID, avaibleFrom, avaibleTo
    console.log("Foodparser p -> " + p);
    try{
        fiszfasz = new Food(p["FoodID"],p["Name"],p["Price"], p["Rating"],p["PictureID"],p["Allergenes"], p["AvaibleFrom"],p["AvaibleTo"], p["RestaurantID"], p["CategoryID"]);
    } catch (error){
       console.log(error);
       return null;
    }
    return fiszfasz;
}
//FOOD PARSER