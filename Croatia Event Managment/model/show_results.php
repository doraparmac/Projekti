<?php

require_once __DIR__ . '/eventservice.class.php';


function sendJSONandEXIT($message){
    header('Content-type:application/json;charset=utf-8');
    echo json_encode($message);
    flush();
    exit(0);
}

if (!isset($_GET['send']))
    sendJSONandEXIT(['error' => 'Greska!']);

$message = [];
$message['show_results'] = [];
$ls = new EventService;
$temp = $ls->getAllEventsBySearch($_GET['send']);

foreach( $temp as $event ){
	array_push($message['show_results'], $event->naslov);
}

sendJSONandEXIT($message);

?>