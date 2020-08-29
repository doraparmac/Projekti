<?php

//require_once '../../model/db.class.php';
require_once 'db.class.php';

$db = DB::getConnection();

try
{
	$st = $db->prepare(
		'CREATE TABLE IF NOT EXISTS users (' .
		'id int NOT NULL PRIMARY KEY AUTO_INCREMENT,' .
		'name varchar(50) NOT NULL,' .
		'surname varchar(50) NOT NULL,' .
		'username varchar(50) NOT NULL,' .
		'e-mail varchar(50) NOT NULL,' .
		'password varchar(255) NOT NULL)'
	);

	$st->execute();
}
catch( PDOException $e ) { exit( "PDO error #1: " . $e->getMessage() ); }

echo "Napravio tablicu users.<br />";

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
catch( PDOException $e ) { exit( "PDO error #2: " . $e->getMessage() ); }

echo "Napravio tablicu events.<br />";


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
catch( PDOException $e ) { exit( "PDO error #3: " . $e->getMessage() ); }

echo "Napravio tablicu komentari.<br />";


// Ubaci neke korisnike unutra
try
{
	$st = $db->prepare( 'INSERT INTO users(name, surname, password, username, email) VALUES (:name, :surname, :password, :username, :email)' );

	$st->execute( array( 'name' => 'Pero', 'surname' => 'Perić', 'username' => 'pperic', 'email' => 'pero.peric@gmail.com', 'password' => password_hash( 'perinasifra', PASSWORD_DEFAULT ) ) );
	$st->execute( array( 'name' => 'Mirko', 'surname' => 'Mirić', 'username' => 'mmiric', 'email' => 'mirko.miric@gmail.com', 'password' => password_hash( 'mirkovasifra', PASSWORD_DEFAULT ) ) );
	$st->execute( array( 'name' => 'Slavko', 'surname' => 'Slavić', 'username' => 'sslavic', 'email' => 'slavko.slavic@gmail.com', 'password' => password_hash( 'slavkovasifra', PASSWORD_DEFAULT ) ) );
	$st->execute( array( 'name' => 'Ana', 'surname' => 'Anić', 'username' => 'aanic', 'email' => 'ana.anic@gmail.com', 'password' => password_hash( 'aninasifra', PASSWORD_DEFAULT ) ) );
	$st->execute( array( 'name' => 'Maja', 'surname' => 'Majić', 'username' => 'mmajic', 'email' => 'maja.majic@gmail.com', 'password' => password_hash( 'majinasifra', PASSWORD_DEFAULT ) ) );
}
catch( PDOException $e ) { exit( "PDO error #4: " . $e->getMessage() ); }

echo "Ubacio korisnike u tablicu users.<br />";


// Ubaci neke evente unutra
try
	{
		$st = $db->prepare( 'INSERT INTO events(autor, title, mjesto, kategorija, datum_pocetak, datum_kraj, vrijeme_pocetak, vrijeme_kraj, dolazi, zanima, opis) VALUES (:autor, :title, :mjesto, :kategorija, :datum_pocetak, :datum_kraj, :vrijeme_pocetak, :vrijeme_kraj, :dolazi, :zanima, :opis)' );

		$st->execute( array( 'autor' => 'Mirko Mirić', 'title' => 'Ljeto na Štrosu', 'mjesto' => 'Strossmayerovo šetalište, Zagreb', 'kategorija' => 'Zabava', 'datum_pocetak' => '2020-06-10', 'datum_kraj' => '2020-09-30', 'vrijeme_pocetak' => '16:00:00', 'vrijeme_kraj' => '22:00:00', 'dolazi' => '589', 'zanima' => '1443', 'opis' => 'Strossmayerovo šetalište jamči neke od najromantičnijih šetnji po Gornjem gradu i donosi duh prekrasnih pariških četvrti. U ljetnim mjesecima pretvara se u pozornicu za umjetničke instalacije i nostalgičnu glazbu uživo iz prošlog stoljeća. 30. lipnja održat će se i proslava pola Nove godine uz popratni vatromet.') );
		$st->execute( array( 'autor' => 'Slavko Slavić', 'title' => 'Dubrovačke ljetne igre', 'mjesto' => 'Dubrovnik', 'kategorija' => 'Glazba', 'datum_pocetak' => '2020-07-10', 'datum_kraj' => '2020-08-25', 'vrijeme_pocetak' => '15:00:00', 'vrijeme_kraj' => '24:00:00', 'dolazi' => '2001', 'zanima' => '3332', 'opis' => 'Dubrovačke ljetne igre i ove će godine okupiti ponajbolje dramske, glazbene, baletne, folklorne, likovne i filmske umjetnike iz cijelog svijeta. Zasnovane na bogatoj i živoj baštini grada Dubrovnika, Igre 71. godinu za redom u razdoblju od 10. srpnja do 25. kolovoza postaju sjecište hrvatskog i svjetskog duha i kulture.' ) );
		$st->execute( array( 'autor' => 'Ana Anić', 'title' => 'Forestland festival', 'mjesto' => 'Brezje, Međimurje', 'kategorija' => 'Glazba', 'datum_pocetak' => '2020-07-17', 'datum_kraj' => '2020-07-19', 'vrijeme_pocetak' => '21:00:00', 'vrijeme_kraj' => '24:00:00', 'dolazi' => '689', 'zanima' => '1827', 'opis' => 'Od bregovitog i vinorodnog dijela Međimurja sve do šumovitog Brezja od 2013. godine vlada divan sklad prirode i čovjeka. Tu se smjestio Forestland, Festival koji zbližava prirodu, umjetnost i ljude, stvara nova prijateljstva i ljubavi! ' ) );
		$st->execute( array( 'autor' => 'Maja Majić', 'title' => 'Tabor Film Festival', 'mjesto' => 'Veliki Tabor', 'kategorija' => 'Film', 'datum_pocetak' => '2020-07-10', 'datum_kraj' => '2020-07-12', 'vrijeme_pocetak' => '20:00:00', 'vrijeme_kraj' => '20:00:00', 'dolazi' => '75', 'zanima' => '269', 'opis' => 'U prekrasnom srednjovjekovnom dvorcu Veliki Tabor prikazat će se preko 200 kratkometražnih filmova iz preko 50 zemalja svijeta na 18. izdanju Tabor Film Festivala. Uz bogati filmski program, TFF ima i impresivni glazbeni program na čak 6 pozornica. Posjetitelji će moći uživati i u brojnim radionicama, predavanjima i promocijama knjiga, kao i ukusnoj domaćoj hrani i piću te kampu koji se nalazi u blizini samog dvorca.' ) );
		$st->execute( array( 'autor' => 'Pero Perić', 'title' => 'Biciklom kroz baštinu', 'mjesto' => 'Hvar', 'kategorija' => 'Sport', 'datum_pocetak' => '2020-09-06', 'datum_kraj' => '2020-09-09', 'vrijeme_pocetak' => '17:00:00', 'vrijeme_kraj' => '22:00:00', 'dolazi' => '25', 'zanima' => '84', 'opis' => 'Ove godine smo za Vas pripremili dječju biciklijadu, obiteljsku vožnju u potpuno novom obliku sa stajanjem po raznim turiskičkim lokalitetima te biciklističku utrku po  novim atraktivnim stazama u nekoliko kategorija i u dvije dužine od 27 i 50km s iznimno bogatim nagradnim fondom.' ) );
		$st->execute( array( 'autor' => 'Maja Majić', 'title' => 'Tko se boji Virginije Wolf?', 'mjesto' => 'Dubrovnik', 'kategorija' => 'Umjetnost', 'datum_pocetak' => '2020-08-06', 'datum_kraj' => '2020-08-06', 'vrijeme_pocetak' => '20:00:00', 'vrijeme_kraj' => '22:00:00', 'dolazi' => '40', 'zanima' => '3', 'opis' => 'Tko se boji Virginije Woolf,  komad koji već pola stoljeća s lakoćom osvaja kazališnu i filmsku publiku, ogleda se u humornim elementima crne, mračne, divlje komedije, ali s elementima i melodrame, tragedije, slapsticka pa i satire te teatarski proziva i poziva na propitivanje društvenih konvencija na originalan i snažan način koji ovu dramu svrstava u antologijska ostvarenja svjetske dramske literature.' ) );
		$st->execute( array( 'autor' => 'Slavko Slavić', 'title' => 'Utrka ludih plovila', 'mjesto' => 'Opuzen', 'kategorija' => 'Ostalo', 'datum_pocetak' => '2020-08-01', 'datum_kraj' => '2020-08-01', 'vrijeme_pocetak' => '18:00:00', 'vrijeme_kraj' => '20:00:00','dolazi' => '1', 'zanima' => '74', 'opis' => 'Osmislite i izradite što originalnije plovilo jer onom najkreativnijem slijedi nagrada.' ) );
		$st->execute( array( 'autor' => 'Slavko Slavić', 'title' => 'Advanced Applications of Developmental Functional Neurology', 'mjesto' => 'Split', 'kategorija' => 'Zdravlje', 'datum_pocetak' => '2020-09-16', 'datum_kraj' => '2020-09-20', 'vrijeme_pocetak' => '10:00:00', 'vrijeme_kraj' => '13:00:00', 'dolazi' => '17', 'zanima' => '169', 'opis' => 'Our dates are set! The 4 day European seminar experience with Drs Robert Melillo and Steve Williams will be held on September 16-20, 2020.' ) );
		$st->execute( array( 'autor' => 'Ana Anić', 'title' => 'Okusi Pelješca', 'mjesto' => 'Pelješac', 'kategorija' => 'Hrana', 'datum_pocetak' => '2020-07-25', 'datum_kraj' => '2020-07-25', 'vrijeme_pocetak' => '16:00:00', 'vrijeme_kraj' => '21:00:00', 'dolazi' => '32', 'zanima' => '180', 'opis' => 'U Okusima Pelješca na Stonskoj placi  uživati ćete u raznolikoj paleti jela pripremljenih od ribe i morskih plodova, uz najomiljenije zvukove dalmatinske glazbe i veliki izbor vrhunskih peljeških vina prezentiranih od lokalnih vinara. ' ) );
	}
catch( PDOException $e ) { exit( "PDO error #5: " . $e->getMessage() ); }

echo "Ubacio evente u tablicu events.<br />";


// Ubaci neke komentare unutra 
try
{
	$st = $db->prepare( 'INSERT INTO komentari(id_user, id_event, vrijeme_objave, opis, zvjezdice) VALUES (:id_user, :id_event, :vrijeme_objave, :opis, :zvjezdice)' );

	$st->execute( array( 'id_user' => 3, 'id_event' => 1, 'vrijeme_objave' => '2020-15-07 00:38:54.840', 'opis' => 'Sve je bilo super, osim predugog reda za cugu...', 'zvjezdice' => '3') );
	$st->execute( array( 'id_user' => 4, 'id_event' => 2, 'vrijeme_objave' => '2020-06-02 10:15:54.840', 'opis' => 'SUper atmosfera!', 'zvjezdice' => '5') );
	$st->execute( array( 'id_user' => 1, 'id_event' => 4, 'vrijeme_objave' => '2020-08-01 18:54:54.840', 'opis' => 'Sve pohvale organizatorima, ali iduce godine osigurati vise camping mjesta.', 'zvjezdice' => '4') );
	$st->execute( array( 'id_user' => 1, 'id_event' => 10, 'vrijeme_objave' => '2020-07-26 07:06:54.840', 'opis' => 'Neorganizirano, losa prezentacija hrane.', 'zvjezdice' => '2') );
	$st->execute( array( 'id_user' => 2, 'id_event' => 9, 'vrijeme_objave' => '2020-10-01 19:23:54.840', 'opis' => 'Kvalitetan i poucan sadrzaj', 'zvjezdice' => '5') );
}
catch( PDOException $e ) { exit( "PDO error #5: " . $e->getMessage() ); }

echo "Ubacio komentare u tablicu komentari.<br />";

?> 
