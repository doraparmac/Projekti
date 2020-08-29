<?php

require_once __DIR__ . '/db.class.php';

create_table_users();
create_table_events();
create_table_komentari();

// ------------------------------------------
function create_table_users()
{
	$db = DB::getConnection();

	try
	{
		$st = $db->prepare(
			'CREATE TABLE IF NOT EXISTS users (' .
			'id int NOT NULL PRIMARY KEY AUTO_INCREMENT,' .
			'name varchar(50) NOT NULL,' .
			'surname varchar(50) NOT NULL,' .
			'username varchar(50) NOT NULL,' .
			'email varchar(50) NOT NULL,' .
			'password varchar(255) NOT NULL)'
		);

		$st->execute();
	}
	catch( PDOException $e ) { exit( "PDO error (create_table_users): " . $e->getMessage() ); }

	echo "Napravio tablicu users.<br />";
}


function create_table_events()
{
	$db = DB::getConnection();

	try
	{
		$st = $db->prepare(
			'CREATE TABLE IF NOT EXISTS events (' .
			'id int NOT NULL PRIMARY KEY AUTO_INCREMENT,' .
			'autor varchar(50) NOT NULL,' .
			'dolazi INT,' .
			'zanima INT,' .
			'mjesto varchar(50) NOT NULL,' .
			'kategorija varchar(50) NOT NULL,' .
			//'vrijeme varchar(50) NOT NULL,' .
			'vrijeme_pocetak TIME NOT NULL,'.
			'vrijeme_kraj TIME NOT NULL,'.
			'datum_pocetak DATE NOT NULL,'.
			'datum_kraj DATE NOT NULL,'.
			'title varchar(50) NOT NULL)'
		);

		$st->execute();
	}
	catch( PDOException $e ) { exit( "PDO error (create_table_events): " . $e->getMessage() ); }

	echo "Napravio tablicu events.<br />";
}


function create_table_komentari()
{
	$db = DB::getConnection();

	try
	{
		$st = $db->prepare(
			'CREATE TABLE IF NOT EXISTS komentari (' .
			'id int NOT NULL PRIMARY KEY AUTO_INCREMENT,' .
			'id_user INT NOT NULL,' .
			'id_event INT NOT NULL,' .
			'opis varchar(255) NOT NULL,' .
			'zvjezdice INT NOT NULL,' .
			'vrijeme_objave DATE NOT NULL)'
		);

		$st->execute();
	}
	catch( PDOException $e ) { exit( "PDO error (create_table_komentari): " . $e->getMessage() ); }

	echo "Napravio tablicu komentari.<br />";
}

?>
