var http = require('http');
var fs = require('fs');
var async = require('async');





http.createServer(function (req, res) {
 
  startup();
  Connect_To_Server('localhost',11000);
 
  
/*
  fs.readFile('index.php', function(err, data) {
    res.writeHead(200, {'Content-Type': 'text/html'});
    res.write(data);
    return res.end();
  });
 */

}).listen(11000);

function startup() {
  fs.readFileSync('./'); 
}

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

  
 }

