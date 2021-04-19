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
const first_JSON = 
{
      type:0, msgID:0
}

var net = require('net');
var client = new net.Socket();
var express = require('express');
var  app = express();
var bodyParser = require('body-parser');
    app.use(bodyParser.json());
    app.use(bodyParser.urlencoded({extended : true}));
    app.use(express.json());
var path = require('path');
/*
    const fs = require('fs');
    const { json } = require('body-parser');
    const { response } = require('express');
*/



var session = require('express-session');
    //var sess;
var login_var = false;

let logged;
var sess = {
    secret: 'secret keyboard cat ',
    resave: false,
    saveUninitialized: true,
    cookie: { maxAge: 60000},
    loggedIn: false
  }

app.use(session(sess));

app.set('view engine', 'ejs');

app.get('/', function (req, res) {
    if (req.session.loggedIn) {
        console.log(session);
        res.redirect('/home');
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

app.get('/auth', function(request, response) {
        response.render('pages/auth');
});

app.get('/home', function(request,response){
    if (request.session.loggedIn) 
    {
        //response.send('Welcome back, ' + request.session.username + '!');
        response.render('pages/home');
    }
    else {
		response.send('Please login to view this page!');
	}
    //

});
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


app.listen(8000);

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

function sendData(json_Object, request, response)
{
    const jsonStr = JSON.stringify(json_Object);
    client.write(jsonStr);

    client.on('data', function(data){
        var parsed_JSON = jsonParser(data);
        if (parsed_JSON["type"] == 1) {
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
     })
    client.on('error', function(err) {
        console.log(err)
     })
}


function jsonParser(object) {

    var parsed_JSON = JSON.parse(object);
    return parsed_JSON;
 }

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
    console.log("Userparser kész:  :" + session.loggedIn)
    return true;

 }

 
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
