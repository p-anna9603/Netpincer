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
            //echo $buf . "\n";
            $jsonobj = $buf;
            $obj = json_decode($jsonobj);
            var_dump($obj);

            if (isset( $obj->restaurantList[0]->Type)) {
                
                if ($obj->restaurantList[0]->Type == "11") 
                {
                    $_SESSION["ettermek"] = $obj;
                    $_SESSION["etterem_db"] = count($obj->restaurantList);
                }
            }
            else if(isset($obj->type) )
            {
                if($obj->type == "1")
                {
                    $_SESSION["clientID"] = $obj->clientID;
                }
                if ($obj->type == "1") 
                {
                    $_SESSION["log"] = true;
                    $_SESSION["user"] = $obj;
                }
                else if ($obj->type == "14") 
                {
                    var_dump($obj);
                    $_SESSION["lista"] = $obj;
                }
                else if ($obj->type == "18") 
                {
                    var_dump($obj);
                }
                else if ($obj->type == "99") 
                {
                    var_dump($obj);
                }
                
                
            }
            else if(isset($obj->Type))
            {
                
                if($obj->Type == "1")
                {
                    $_SESSION["clientID"] = $obj->clientID;
                }
                if($obj->Type == "7")
                {
                    $_SESSION["category_names"] = $obj->listOfCategoryNames;
                    $_SESSION["category_ids"] = $obj->listOfCategoryIDs;
                    var_dump($obj);
                }
                else if ($obj->Type == "9") 
                {
                    var_dump($obj);
                    $_SESSION["kaja"] = $obj;
                }
                else if ($obj->Type == "14") 
                {
                    var_dump($obj);
                    $_SESSION["lista"] = $obj;
                }
                else if ($obj->Type == "18") 
                {
                    var_dump($obj);
                }
            }
            else if(isset($obj->FoodID))
            {
                $_SESSION["kaja"] = $obj;
                //var_dump($obj);
            }

    }
          
?>