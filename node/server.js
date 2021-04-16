const express = require('express');
const app = express();
app.set('view engine', 'ejs');
/*
var application_root = __dirname;
var session = require('express-session');
var bodyParser = require('body-parser');
var path = require('path');*/

app.get('/', function (req, res) {
    Connect_To_Server('localhost',11000); 
    res.render('pages/index');
})

app.get('/login', function(req, res) {
    res.render('pages/login');
});
app.get('/register', function(req, res) {
    res.render('pages/register');
});

app.post('/login', function(request, response) {
	var username = request.body.username;
	var password = request.body.password;
	if (username && password) {
		connection.query('SELECT * FROM accounts WHERE username = ? AND password = ?', [username, password], function(error, results, fields) {
			if (results.length > 0) {
				request.session.loggedin = true;
				request.session.username = username;
				response.redirect('/user');
			} else {
				response.send('Incorrect Username and/or Password!');
			}			
			response.end();
		});
	} else {
		response.send('Please enter Username and Password!');
		response.end();
	}
});


app.listen(8000);

function Connect_To_Server (HOST,PORT) {
    const first_JSON = 
    {
          type:0, msgID:0
    }
    const jsonStr = JSON.stringify(first_JSON);
    var net = require('net');
    var client = new net.Socket();
     
    client.connect(PORT,HOST, function() {
         console.log('Connected to: ' + HOST + ':' + PORT);
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