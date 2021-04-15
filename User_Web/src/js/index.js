/*
	var net = require('net');
	var client = new net.Socket();
	var PORT = 11000;
	var HOST = '127.0.0.1';
	client.connect(PORT,HOST, function() {
		console.log('Connected to: ' + HOST + ':' + PORT);
	});
	// megkap
	client.on('data', function(data){
		console.log("Received : " + data);
		
	})
	client.on('connect', function (connect) {
		console.log('First JSON to send : ');
		console.log(jsonStr);
		client.write(jsonStr);
	})
	process.on('uncaughtException', function (err) {
		console.log(err);
	}); 
	
	client.on('close', function() {
		console.log('Connection closed');
	});

const first_JSON = 
{
	type:0, msgID:0
}
const jsonStr = JSON.stringify(first_JSON);

//client.write(JSON.stringify({ type: 0, msgID:0 }));
/*
client.on('data', function(data) {
	console.log('Received: ' + data);
	client.destroy(); // kill client after server's response




	{type: 0,      -> 0 - Basic Server Info
 *  msgID: 0,
 *  clientID: 0 }
});*/