<?php

require_once __DIR__ . '/../model/eventservice.class.php';
require_once __DIR__ . '/../app/database/db.class.php';

class EventController{
	
	public function index(){
        $message = '';
		require_once __DIR__ . '/../view/main.php';
	}

	public function event($event_id){
	    $ls = new EventService;
		if( isset($_POST['comment'] ) ){
			$ls->sendComment( $ls->getIdByUsername($_SESSION['username']), $event_id, $_POST['comment'], $_POST['ocjena']);
		}
		if( isset($_POST['dolazim']) ){
			$ls->userIsComing( $event_id, $ls->getIdByUsername($_SESSION['username']) );
		}
		if( isset($_POST['ne_dolazim']) ){
			$ls->userIsNotComing( $event_id, $ls->getIdByUsername($_SESSION['username']) );
		}

			$title = $ls->getEventTitle($event_id);
		if (isset($_SESSION['username'])){
			$current_user_id = $ls->getIdByUsername($_SESSION['username']);
			$coming = $ls->checkIfComing($event_id, $current_user_id);
		}
		$userListTemp = $ls->getAllUsers();
		$commentList = $ls->getAllComments($event_id);
		$userList = array();
		foreach( $commentList as $comment ){
			foreach( $userListTemp as $user ){
				if( $comment->id_user == $user->id ){
					array_push( $userList, $user->username );
				}
			}
		}

		$event = $ls->getEventById($event_id);

        require_once __DIR__ . '/../view/event.php';
    }

	public function logout(){
		$message = 'Uspjesno ste se odjavili!';
		session_unset();
        session_destroy();
		require_once __DIR__ . '/../view/prijava.php';
	}

	public function show_events(){
		$ls = new EventService;
		if (isset($_POST['search'])){
			$eventList = $ls->getAllEventsBySearch($_POST['search']);
		}
		else if (isset($_POST['category']) || isset($_POST['city']) || isset($_POST['date'])){
			$eventList = $ls->getAllEvents();
			$eventList = $ls->getFilteredEvents($_POST['category'], $_POST['city'], $_POST['date'], $eventList);
		}
		else{
			$eventList = $ls->getAllEvents();
		}
		$categoryList = $ls->getAllCategories();
		$cityList = $ls->getAllCities();
		$dateList = $ls->getAllDates();
		$message = '';
		require_once __DIR__ . '/../view/show_events.php';
	}

	public function search(){
		$ls = new EventService;
		$eventList = $ls->getAllEventsBySearch($_POST['search']);
		require_once __DIR__ . '/../view/show_searched_events.php';
	}

	public function try_add_event(){
		$message = '';
		require_once __DIR__ . '/../view/add_event.php';
	}

	public function add_event(){
		$message = '';
		$ls = new EventService;
		$ls->insertEvent($ls->getIdByUsername($_SESSION['username']), 0, $_POST['mjesto'], $_POST['grad'],
						$_POST['kategorija'], $_POST['vrijeme_pocetak'], $_POST['vrijeme_kraj'],
						$_POST['datum_pocetak'], $_POST['datum_kraj'], $_POST['naslov'], $_POST['opis']);
		require_once __DIR__ . '/../view/main.php';	
	}
	
	public function try_delete_event(){
		$message = '';
		$ls = new EventService;
		$eventList = $ls->getAllEvents();
		require_once __DIR__ . '/../view/delete_event.php';
	}

	public function delete_event(){
		$message = '';
		$ls = new EventService;
		$ls->deleteEvent($_POST['delete']);
		$eventList = $ls->getAllEvents();
		$message = "Uspjesno ste izbrisali event!";
		require_once __DIR__ . '/../view/delete_event.php';
	}

	public function try_delete_user(){
		$message = '';
		$ls = new EventService;
		$userList = $ls->getAllUsers();
		require_once __DIR__ . '/../view/delete_user.php';
	}

	public function delete_user(){
		$message = '';
		$ls = new EventService;
		$ls->deleteUser($_POST['delete']);
		$userList = $ls->getAllUsers();
		$message = "Uspjesno ste izbrisali korisnika!";
		require_once __DIR__ . '/../view/delete_user.php';
	}

	public function my_events(){
		$message = '';
		$ls = new EventService;
		$categoryList = $ls->getAllCategories();
		$cityList = $ls->getAllCities();
		$dateList = $ls->getAllDates();
		$id = $ls->getIdByUsername($_SESSION['username']);
		$eventList = $ls->getEventsById($id);
		require_once __DIR__ . '/../view/show_events.php';
	}

	public function o_nama(){
		$message = '';
		require_once __DIR__ . '/../view/o_nama.php';
	}

	public function uvjeti(){
		$message = '';
		require_once __DIR__ . '/../view/uvjeti.php';
	}

}
?>