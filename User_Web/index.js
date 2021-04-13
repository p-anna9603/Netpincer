var net = require('net');

var client = new net.Socket();

console.log('First JSON to send : ');
const first_JSON = 
{
	type:0, msgID:0, clientID:0
}
const jsonStr = JSON.stringify(first_JSON);
console.log(jsonStr);

client.connect(11000, '127.0.0.1', function() {
	console.log('Connected');
	client.write('Hello, C# server! I am a Node.js client');
	//client.write(jsonStr);
});

client.on('data', function(data){
	console.log("Received : " + data);
})

process.on('uncaughtException', function (err) {
    console.log(err);
}); 

client.on('close', function() {
	console.log('Connection closed');
});

//client.write(JSON.stringify({ type: 0, msgID:0 }));
/*
client.on('data', function(data) {
	console.log('Received: ' + data);
	client.destroy(); // kill client after server's response




	{type: 0,      -> 0 - Basic Server Info
 *  msgID: 0,
 *  clientID: 0 }
});*/

