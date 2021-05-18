<?php 

    function Connect_To_Server($data_to_send)
    {
        $service_port = 11000;
        $address = gethostbyname('localhost');

        $socket = socket_create(AF_INET, SOCK_STREAM, SOL_TCP);
        if ($socket === false) {
            echo "socket_create() failed: reason: " . socket_strerror(socket_last_error()) . "\n";
        } 

        $result = socket_connect($socket, $address, $service_port);
        if ($result === false) {
            echo "socket_connect() failed.\nReason: ($result) " . socket_strerror(socket_last_error($socket)) . "\n";
        }

        $in = $data_to_send;
        $out = '';
            
           
            socket_write($socket, $in, strlen($in));
            if (false !== ($bytes = socket_recv($socket, $buf, 2048, MSG_WAITALL))) {
                // echo "Read $bytes bytes from socket_recv(). Closing socket...";
            }
            socket_close($socket);
            echo $buf . "\n";
            $jsonobj = $buf;
            $obj = json_decode($jsonobj);
            
            if ($obj->type == "1") {
                
                $_SESSION["log"] = true;
                $_SESSION["user"] = $obj;
            }
            else if ($obj->type == "2") 
            {

            }
            else if ($obj->type == "3") 
            {
                
            }
            else if ($obj->type == "4") 
            {
                
            }
            else if ($obj->type == "5") 
            {
                
            }
            else if ($obj->type == "6") 
            {
                
            }
            else if ($obj->type == "7") 
            {
                
            }
            else if ($obj->type == "9") 
            {
                
            }
            else if ($obj->type == "11") 
            {

                
            }
    }
          
?>