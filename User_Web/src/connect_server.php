<?php 
/*
if(!($sock = socket_create(AF_INET, SOCK_STREAM, SOL_TCP)))
{
    $errorcode = socket_last_error();
    $errormsg = socket_strerror($errorcode);
     
    die("Couldn't create socket: [$errorcode] $errormsg \n");
}
 
echo "Socket created";
if(!socket_connect($sock , '127.0.0.1' , 1433))
{
    $errorcode = socket_last_error();
    $errormsg = socket_strerror($errorcode);
     
    die("Could not connect: [$errorcode] $errormsg \n");
}
 
echo "Connection established \n";


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
echo $buf;*/

$ip = "127.0.0.1";
$port = "1433";
$sendData = "pls mukodj";

$socket = socket_create(AF_INET, SOCK_STREAM, SOL_TCP);

socket_connect($socket,$ip, $port);

socket_write($socket, "1");
?>