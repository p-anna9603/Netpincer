<?php 

error_reporting(E_ALL);

    /* Get the port for the WWW service. */
    $service_port = 11000;

    /* Get the IP address for the target host. */
    $address = gethostbyname('localhost');

    /* Create a TCP/IP socket. */
    $socket = socket_create(AF_INET, SOCK_STREAM, SOL_TCP);
    if ($socket === false) {
        echo "socket_create() failed: reason: " . socket_strerror(socket_last_error()) . "\n";
    } else {
        echo "OK.<br>";
    }

    echo "Attempting to connect to '$address' on port '$service_port'...";
    $result = socket_connect($socket, $address, $service_port);
    if ($result === false) {
        echo "socket_connect() failed.\nReason: ($result) " . socket_strerror(socket_last_error($socket)) . "\n";
    } else {
        echo "OK.<br>";
    }

$in="1";

echo "Sending request...";
socket_write($socket, $in, strlen($in));
echo "OK.<br>";


echo "Reading response:\n\n";
while ($out = socket_read($socket, 2048)) {
    echo $out;
}

    echo "Closing socket...";
    socket_close($socket);
    echo "OK.\n\n";

/*

$ip = "127.0.0.1";
$port = "1433";
$sendData = "pls mukodj";

if(!($sock = socket_create(AF_INET, SOCK_STREAM,0)))
{
    $errorcode = socket_last_error();
    $errormsg = socket_strerror($errorcode);
     
    die("Couldn't create socket: [$errorcode] $errormsg \n");
}
else
{
    echo "Socket created <br>";
}
 

if(!socket_connect($sock , $ip, 0))
{
    $errorcode = socket_last_error();
    $errormsg = socket_strerror($errorcode);
     
    die("Could not connect: [$errorcode] $errormsg \n");
}
else
{
    echo "Connection established <br>";
}



$message = "GET / HTTP/1.1\r\n\r\n";
 
//Send the message to the server
if( ! socket_send ( $sock , $message , strlen($message) , 0))
{
    $errorcode = socket_last_error();
    $errormsg = socket_strerror($errorcode);
     
    die("Could not send data: [$errorcode] $errormsg \n");
}
 
echo "Message send successfully <br>";

 
//Now receive reply from server
if(socket_recv ( $sock , $buf , 80 , MSG_WAITALL ) === FALSE)
{
    $errorcode = socket_last_error();
    $errormsg = socket_strerror($errorcode);
     
    die("Could not receive data: [$errorcode] $errormsg \n");
}
 
//print the received message
echo $buf;



$socket = socket_create(AF_INET, SOCK_STREAM, SOL_TCP);

//socket_connect($socket,$ip, $port);

socket_write($socket, "1");*/
?>