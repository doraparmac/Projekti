<?php

require __DIR__ . '/../app/database/db.class.php';
require __DIR__ . '/user.class.php';
require __DIR__ . '/event.class.php';
require __DIR__ . '/comment.class.php';
require __DIR__ . '/dolazi.class.php';

class EventService{
    public function getAllEvents(){
        $events = [];

        $db = DB::getConnection();
        $st = $db->prepare( 'SELECT * FROM events' );
        $st->execute();

        while ($row = $st->fetch())
            $events[] = new Event ($row['id'], $row['id_user'], $row['dolazi'], $row['mjesto'], $row['grad'], $row['kategorija'], $row['vrijeme_pocetak'], $row['vrijeme_kraj'], $row['datum_pocetak'], $row['datum_kraj'], $row['title'], $row['opis']);
    
        return $events;
    }

    public function getAllUsers(){
        $users = [];

        $db = DB::getConnection();
        $st = $db->prepare('SELECT * FROM users');
        $st->execute();
        while( $row = $st->fetch() ){
            $users[] = new User($row['id'], $row['name'], $row['surname'], $row['username'], $row['email'], $row['password'], $row['registered_sequence'], $row['registered'], $row['admin']);
	}
        
        return $users;
    }

    public function getAllComments($id_event){
        $comments = [];

        $db = DB::getConnection();
        $st = $db->prepare('SELECT * FROM komentari WHERE id_event=:id_event ORDER BY vrijeme_objave ASC');
        $st->execute(['id_event' => $id_event]);

        while ($row = $st->fetch())
            $comments[] = new Comment($row['id'], $row['id_user'], $row['id_event'], $row['opis'], $row['zvjezdice'], $row['vrijeme_objave']);
    
        return $comments;
    }

    public function getEventTitle($id_event){
        $db = DB::getConnection();
		$st = $db->prepare('SELECT * FROM events WHERE id=:id_event');
        $st->execute(['id_event' => $id_event]);
        
        $row = $st->fetch();

        return $row['title'];
    }

    public function getEventById($id_event){
        $db = DB::getConnection();
		$st = $db->prepare('SELECT * FROM events WHERE id=:id_event');
        $st->execute(['id_event' => $id_event]);

        $row = $st->fetch();

        $event = new Event ($row['id'], $row['id_user'], $row['dolazi'], $row['mjesto'], $row['grad'], $row['kategorija'], $row['vrijeme_pocetak'], $row['vrijeme_kraj'], $row['datum_pocetak'], $row['datum_kraj'], $row['title'], $row['opis']);

        return $event;
    }


    public function sendComment($id_user, $id_event, $comment, $zvjezdice){
        date_default_timezone_set('Europe/Paris');
        $date = date('Y-m-d H:i:s', time());
        $db = DB::getConnection();
        $st = $db->prepare( 'INSERT INTO komentari(id_user, id_event, vrijeme_objave, opis, zvjezdice) VALUES (:id_user, :id_event, :vrijeme_objave, :opis, :zvjezdice)' );
        $st->execute(array('id_user' => $id_user, 'id_event' => $id_event, 'vrijeme_objave' => $date, 'opis' => $comment, 'zvjezdice' => $zvjezdice));
    }

    public function insertEvent($id_user, $dolazi, $mjesto, $grad, $kategorija, $vrijeme_pocetak, 
                    $vrijeme_kraj, $datum_pocetak, $datum_kraj, $naslov, $opis){
                        
        $db = DB::getConnection();
        $st = $db->prepare( 'INSERT INTO events(id_user, dolazi, mjesto, grad, kategorija,
                            vrijeme_pocetak, vrijeme_kraj, datum_pocetak, datum_kraj, title, opis)
                            VALUES (:id_user, :dolazi, :mjesto, :grad, :kategorija,
                            :vrijeme_pocetak, :vrijeme_kraj, :datum_pocetak, :datum_kraj, :title, :opis)');
        $st->execute(array('id_user' => $id_user, 'dolazi' => $dolazi, 
                        'mjesto' => $mjesto, 'grad' => $grad, 'kategorija' => $kategorija, 'vrijeme_pocetak' => $vrijeme_pocetak,
                        'vrijeme_kraj' => $vrijeme_kraj, 'datum_pocetak' => $datum_pocetak,
                        'datum_kraj' => $datum_kraj, 'title' => $naslov, 'opis' => $opis));    
    }

    public function register($name, $surname, $username, $password, $email){
        $registration_sequence = '';
        for( $i = 0; $i < 20; ++$i )
            $registration_sequence .= chr( rand(0, 25) + ord( 'a' ) );
        
        $db = DB::getConnection();
        try{
            $st = $db->prepare('INSERT INTO users(name, surname, username, email, password, 
                            registered_sequence, registered, admin) VALUES (:name, :surname, :username, :email, :password, :registration_sequence, 0, 0)');
            $st->execute(array('name' => $name, 'surname' => $surname, 'username' => $username, 'email' => $email, 'password' => password_hash($password, PASSWORD_DEFAULT ), 'registration_sequence' => $registration_sequence ));
        }catch( PDOException $e ) { exit( 'PDO Error: ' . $e->getMessage() ); }
        $recipient = $email;
        $subject = 'Registracijski mail - Event Management';
        $message = 'Postovani ' . $username . '! Za dovrsetak registracije kliknite na sljedeci link: ';
        $message .= 'http://' . $_SERVER['SERVER_NAME'] . htmlentities( dirname( $_SERVER['PHP_SELF'] ) ) . '/index.php?rt=' . $registration_sequence . "\n";
        $headers  = 'From: rp2@studenti.math.hr' . "\r\n" .
		            'Reply-To: rp2@studenti.math.hr' . "\r\n" .
                    'X-Mailer: PHP/' . phpversion();
        
        $isOK = mail($recipient , $subject, $message, $headers);  
        return $isOK;   
    }

    public function getUserByUsername($username){
        $db = DB::getConnection();
	    $st = $db->prepare('SELECT * FROM users WHERE username=:username');
        $st->execute(['username' => $username]);
        
        $row = $st->fetch();

        $user = new User($row['id'], $row['name'], $row['surname'], $row['username'], $row['email'], $row['password'], $row['registered_sequence'], $row['registered'], $row['admin']);

        return $user;
    }

    public function getIdByUsername($username){
        $db = DB::getConnection();
	    $st = $db->prepare('SELECT id FROM users WHERE username=:username');
        $st->execute(['username' => $username]);

        $row = $st->fetch();
        
        return $row['id'];
    }

    public function final_register($email){
		$value=1;
        $db = DB::getConnection();
        $st = $db->prepare( 'UPDATE users SET registered=? WHERE email=?' );
		$st->bindParam(1,$value);
		$st->bindParam(2,$email, PDO::PARAM_STR);
        $st->execute();
    }

    public function getAllEventsBySearch($searched){
        $events = [];
        $db = DB::getConnection();
        $st = $db->prepare( "SELECT * FROM events WHERE title LIKE '%{$searched}%'");
        $st->execute();

        while ($row = $st->fetch())
            $events[] = new Event ($row['id'], $row['id_user'], $row['dolazi'], $row['mjesto'], $row['grad'], $row['kategorija'], $row['vrijeme_pocetak'], $row['vrijeme_kraj'], $row['datum_pocetak'], $row['datum_kraj'], $row['title'], $row['opis']);
        return $events;
    }
    
    public function getAllCategories(){
        $categories = [];
        $db = DB::getConnection();
        $st = $db->prepare( 'SELECT DISTINCT kategorija FROM events' );
        $st->execute();
    
        while ($row = $st->fetch()){
            $categories[] = $row['kategorija'];
        }

        return $categories;
    }

    public function getAllCities(){
        $cities = [];
        $db = DB::getConnection();
        $st = $db->prepare( 'SELECT DISTINCT grad FROM events' );
        $st->execute();
    
        while ($row = $st->fetch()){
            $cities[] = $row['grad'];
        }

        return $cities;
    }
    
    public function getAllDates(){
        $dates = [];
        $db = DB::getConnection();
        $st = $db->prepare( 'SELECT DISTINCT datum_pocetak FROM events' );
        $st->execute();
    
        while ($row = $st->fetch()){
            $dates[] = $row['datum_pocetak'];
        }

        return $dates;
    }

    public function getFilteredEvents($category, $city, $date, $eventList){
        if ($category != 'null'){
            for ($i = 0; $i < count($eventList); $i++){
                if ($eventList[$i]->kategorija != $category){
                    array_splice($eventList, $i, 1);
                    $i--;
                }
            }
        }

	if ($city != 'null'){
            for ($i = 0; $i < count($eventList); $i++){
                if ($eventList[$i]->mjesto != $city){
                    array_splice($eventList, $i, 1);
                    $i--;
                }
            }
        }

	if ($date != 'null'){
            for ($i = 0; $i < count($eventList); $i++){
                if ($eventList[$i]->datum_pocetak != $date){
                    array_splice($eventList, $i, 1);
                    $i--;
                }
            }
        }

        return $eventList;
    }

    public function checkAdminshipByUsername($username){
	$db = DB::getConnection();
	$st = $db->prepare('SELECT * FROM users WHERE username=:username');
        $st->execute(['username' => $username]);
        
        $row = $st->fetch();
	if( $row['admin'] == 1 ){
		return 1;
	}
	else return 0;
    }

    public function deleteEvent($id_event){
	$db = DB::getConnection();
	$st = $db->prepare( 'DELETE FROM events WHERE id=:id_event' );
	$st->execute(array('id_event' => $id_event));

	$st = $db->prepare( 'DELETE FROM komentari WHERE id_event=:id_event' );
	$st->execute(array('id_event' => $id_event));

	$st = $db->prepare( 'DELETE FROM dolazi WHERE id_event=:id_event' );
	$st->execute(array('id_event' => $id_event));

    }

    public function getEventsById($id){
        $events = [];
        $created_events_ids = [];
        $dolazi = [];
        $db = DB::getConnection();
        $st = $db->prepare( 'SELECT * FROM events WHERE id_user=:id' );
        $st->execute(array('id' => $id));
        while ($row = $st->fetch()){
            $events[] = new Event ($row['id'], $row['id_user'], $row['dolazi'], $row['mjesto'], $row['grad'], $row['kategorija'], $row['vrijeme_pocetak'], $row['vrijeme_kraj'], $row['datum_pocetak'], $row['datum_kraj'], $row['title'], $row['opis']);
            array_push($created_events_ids, $row['id']);
        }

        $st = $db->prepare( 'SELECT * FROM dolazi' );
        $st->execute();
        while ($row = $st->fetch())
                $dolazi[] = new Dolazi ($row['id_event'], $row['id_user'] );

        foreach( $dolazi as $dolaz ){
            if( $dolaz->id_user == $id && !in_array($dolaz->id_event, $created_events_ids) ){
                $st = $db->prepare( 'SELECT * FROM events WHERE id=:id' );
                $st->execute(array('id' => $dolaz->id_event));
                $row = $st->fetch();
                $events[] = new Event ($row['id'], $row['id_user'], $row['dolazi'], $row['mjesto'],  $row['grad'], $row['kategorija'], $row['vrijeme_pocetak'], $row['vrijeme_kraj'], $row['datum_pocetak'], $row['datum_kraj'], $row['title'], $row['opis']);
            }
        }
        return $events;
    }
   
    public function checkIfComing($id_event, $id_user){
	$dolazi = [];
        $coming = 0;
        $db = DB::getConnection();
        $st = $db->prepare( 'SELECT * FROM dolazi' );
        $st->execute();
        while ($row = $st->fetch())
            $dolazi[] = new Dolazi ($row['id_event'], $row['id_user'] );

        foreach( $dolazi as $dolaz ){
            if( $dolaz->id_user == $id_user && $dolaz->id_event == $id_event ){
                $coming = 1;
            }
        }

        return $coming;
    }

    public function userIsComing($id_event, $id_user){
        $db = DB::getConnection();
        $st = $db->prepare( 'INSERT INTO dolazi(id_event, id_user )
                                VALUES (:id_event, :id_user )');
        $st->execute(array('id_event' => $id_event, 'id_user' => $id_user ));

        $st = $db->prepare( 'SELECT dolazi FROM events WHERE id=:id' );
        $st->execute(array('id' => $id_event));

        $row = $st->fetch();
        $value = $row['dolazi'] + 1;

        $st = $db->prepare( 'UPDATE events SET dolazi=:val WHERE id=:id' );
        $st->execute(array('val' => $value, 'id' => $id_event));	
    }

    public function userIsNotComing($id_event, $id_user){
        $db = DB::getConnection();
        $st = $db->prepare( 'DELETE FROM dolazi WHERE id_event=:id_event AND id_user=:id_user' );
        $st->execute(array('id_event' => $id_event, 'id_user' => $id_user ));

        $st = $db->prepare( 'SELECT dolazi FROM events WHERE id=:id' );
        $st->execute(array('id' => $id_event));

        $row = $st->fetch();
        $value = $row['dolazi'] - 1;

        $st = $db->prepare( 'UPDATE events SET dolazi=:val WHERE id=:id' );
        $st->execute(array('val' => $value, 'id' => $id_event));	
    }

    public function deleteUser($id_user){
	$dolazi = [];

	$db = DB::getConnection();
	$st = $db->prepare( 'DELETE FROM users WHERE id=:id_user' );
	$st->execute(array('id_user' => $id_user));

	$st = $db->prepare( 'DELETE FROM komentari WHERE id_user=:id_user' );
	$st->execute(array('id_user' => $id_user));

	$st = $db->prepare( 'SELECT * FROM dolazi WHERE id_user=:id_user' );
	$st->execute(array('id_user' => $id_user));

	while ($row = $st->fetch())
            $dolazi[] = new Dolazi ($row['id_event'], $row['id_user'] );

	foreach( $dolazi as $dolaz ){
		$st = $db->prepare( 'SELECT dolazi FROM events WHERE id=:id' );
        	$st->execute(array('id' => $dolaz->id_event));

       		$row = $st->fetch();
        	$value = $row['dolazi'] - 1;

		$st = $db->prepare( 'UPDATE events SET dolazi=:val WHERE id=:id' );
		$st->execute(array('val' => $value, 'id' => $dolaz->id_event));
	}

	$st = $db->prepare( 'DELETE FROM dolazi WHERE id_user=:id_user' );
	$st->execute(array('id_user' => $id_user));

	$st = $db->prepare( 'DELETE FROM events WHERE id_user=:id_user' );
	$st->execute(array('id_user' => $id_user));

    }



}