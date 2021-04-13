JS_connect();
var net = require('net');
var client = new net.Socket();

var PORT = 11000;
var HOST = '127.0.0.1';

client.connect(PORT,HOST, function() {
	console.log('Connected to: ' + HOST + ':' + PORT);
});

client.on('data', function(data){
	console.log("Received : " + data);
	
})

client.on('connect', function (connect) {
	console.log('First JSON to send : ');
	console.log(jsonStr);
	client.write(jsonStr);
})


const first_JSON = 
{
	type:0, msgID:0
}
const jsonStr = JSON.stringify(first_JSON);