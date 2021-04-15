var http = require('http');
var fs = require('fs');


http.createServer(function (req, res) {
  Connect_To_Server('localhost',11000);

  fs.readFile('index.php', function(err, data) {
    res.writeHead(200, {'Content-Type': 'text/html'});
    res.write(data);
    return res.end();
  });
  fs.readFile('register.php', function(err, data) {
    res.writeHead(200, {'Content-Type': 'text/html'});
    res.write(data);
    return res.end();
  });
    
  /*

  fs.readFile('index.js', function(err, data) {
      res.writeHead(200, {'Content-Type': 'script/javascript'});
      res.write(data);
      return res.end();
  });*/
}).listen(11000);

function Connect_To_Server (HOST,PORT) {
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


     const first_JSON = 
     {
         type:0, msgID:0
     }
     const jsonStr = JSON.stringify(first_JSON);
 }

