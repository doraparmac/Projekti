<?php 

session_start();

require_once __DIR__ . '/model/eventservice.class.php';

if( !isset( $_GET['rt'] ) ){
    $controller = 'login';
    $action = 'index';
}
else{
    $parts = explode( '/', $_GET['rt'] );

    if( isset( $parts[0] ) && preg_match( '/^[A-Za-z0-9]+$/', $parts[0] ) )
        $controller = $parts[0];
    else
        $controller = 'login';

    
    if( isset( $parts[1] )  )
        $action = $parts[1];
    else
        $action = 'index';

}
$ls = new EventService;
$userList = $ls->getAllUsers();

foreach($userList as $user){
	if($user->registration_sequence == $controller && $user->registered == 0){
		$ls->final_register($user->email);
		$message = 'Uspjesna verifikacija! Sada se mozete ulogirati.'; 
		require_once __DIR__ . '/view/prijava.php';
		exit(0);
	}
}


$controllerName = $controller . 'Controller';

if( !file_exists( __DIR__ . '/controller/' . $controllerName . '.php' ) )
    error_404();

require_once __DIR__ . '/controller/' . $controllerName . '.php';

if( !class_exists( $controllerName ) )
    error_404();

$con = new $controllerName();

if( $controller == 'event'){
	$eventList = $ls-> getAllEvents();
	foreach( $eventList as $event){
		if( $event->id == $action ){
			$con->event($event->id);
			exit(0);
		}
	}
}
$con->$action();

?>
