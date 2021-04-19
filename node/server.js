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

var net = require('net');
var client = new net.Socket();
const express = require('express');
const app = express();
var session = require('express-session');
var bodyParser = require('body-parser');

app.use(bodyParser.json());
var path = require('path');

app.set('view engine', 'ejs');
var application_root = __dirname;
const first_JSON = 
{
      type:0, msgID:0
}

const fs = require('fs');
const { json } = require('body-parser');

app.get('/', function (req, res) {
    
    Connect_To_Server(first_JSON); 
    res.render('pages/index');
})

app.get('/login', function(req, res) {
    res.render('pages/login');
});
app.get('/register', function(req, res) {
    res.render('pages/register');
});

app.get('/auth', function(req, res) {
    res.render('pages/auth');
});

app.get('/class', function(req,res){
    res.render('src/class.js');
});
// Parse URL-encoded bodies (as sent by HTML forms)
app.use(bodyParser.urlencoded({extended : true}));

// Parse JSON bodies (as sent by API clients)
app.use(express.json());

app.use(session({
	secret: 'secret',
	resave: true,
	saveUninitialized: true
}));
var login_var = false;
app.post('/auth', function(request, response) {
  
	var got_username = request.body.auth_name;
	var got_password = request.body.auth_pass;

    const login_JSON = 
    {
        type:1, clientID: 0, username: got_username, password: got_password, userType: 0
    }

    const jsonStr = JSON.stringify(login_JSON);
    console.log("JSON to send: ");
    console.log(jsonStr);
    sendData(login_JSON);
	if (login_var == true) 
    {
        request.session.loggedin = true;
        request.session.username = got_username;
        response.redirect('/auth');

	} else {
		response.send('Please enter Username and Password!');
		response.end();
	}
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
         if (parsed_JSON["type"] == 1) {
            console.log("Received login data : " + data);
            console.log("Handshake -> Type: 1 <- User Login");
            userParser(data);
            login_var = true;
         }
         else if (parsed_JSON["type"] == 99) {
            console.log("Received false login data : " + data);
            console.log("User not found");
            login_var = false;
        }
     })

    };

function sendData(json_Object)
{
    const jsonStr = JSON.stringify(json_Object);
    client.write(jsonStr);
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
    var p = jsonParser(object);
    let logged = new User(p["type"],p["clientID"],p["username"],p["password"], p["firstName"], p["phoneNumber"], p["city"], p["zipcode"], p["line1"], p["line2"], p["userType"], p["email"]);
    console.log(logged);
    login_var = true;
 }