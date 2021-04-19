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

// Parse URL-encoded bodies (as sent by HTML forms)
app.use(bodyParser.urlencoded({extended : true}));

// Parse JSON bodies (as sent by API clients)
app.use(express.json());

app.use(session({
	secret: 'secret',
	resave: true,
	saveUninitialized: true
}));

app.post('/auth', function(request, response) {
  
	var got_username = request.body.auth_name;
	var got_password = request.body.auth_pass;

    const login_JSON = 
    {
        type:1, clientID: 0, username: got_username, password: got_password, userType: 0
    }
    const jsonStr = JSON.stringify(login_JSON);
    console.log("JSON to send: ");
    console.log(login_JSON);
	if (true) 
    {
        request.session.loggedin = true;
        request.session.username = got_username;
        sendData(login_JSON);
      //  response.redirect('/auth');
	} else {
		response.send('Please enter Username and Password!');
		response.end();
	}
});

app.listen(8000);

function Connect_To_Server (json) 
{
    const jsonStr = JSON.stringify(json);
    //var net = require('net');
    //var client = new net.Socket();
     
    client.connect(11000,'localhost', function() {
         console.log('Connected to: ' + 'localhost' + ':' + 11000);
     });
    client.on('connect', function (connect) {
         console.log('First JSON to send : ');
         console.log(jsonStr);
         client.write(jsonStr);
     })

    client.on('data', function(data){
         console.log("Received : " + data);
         //console.log(data[0]);
     })

    };

function sendData(json_Object)
{
    const jsonStr = JSON.stringify(json_Object);
    client.write(jsonStr);

     client.on('data', function(data){
        console.log("Received : " + data);
        json_Object = data; //console.log(data[0]);
        return data;
    })

    client.on('error', function(err) {
        console.log(err)
     })

    
}