<!DOCTYPE html>
	<html>
	<head>
		<meta name="viewport" content="width=device-width, initial-scale=1" charset="utf-8">
	</head>
		<title>Croatia Event Calendar</title>
        <link rel="stylesheet" type="text/css" href="css/style.css" />
        <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    		<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
        <script src='https://kit.fontawesome.com/a076d05399.js'></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.js"></script>
        
		<ul>
		<div id="mySidebar" class="sidebar">
      <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&#10005</a>
      <a href = "../projekt/index.php">Početna</a>
      <form id = "se" action="index.php?rt=event/show_events" method="post">
      <a href="#" onclick="document.getElementById('se').submit();">Event kalendar</a></form>
      <?php
      if (isset($_SESSION['username'])){
        echo '<form id = "me" action="index.php?rt=event/my_events" method="post">';
        echo '<a href="#" onclick="document.getElementById(\'me\').submit();">Moji dogadaji</a></form>';
      }

      if (isset($_SESSION['username'])){
        echo '<form id = "ce" action="index.php?rt=event/try_add_event" method="post">';
        echo '<a href="#" onclick="document.getElementById(\'ce\').submit();">Kreiraj događaj</a></form>';
      }
      	
      if( isset($_SESSION['admin']) ){
	echo '<form id = "de" action="index.php?rt=event/try_delete_event" method="post">';
        echo '<a href="#" onclick="document.getElementById(\'de\').submit();">Obrisi event</a></form>';

      }
     if( isset($_SESSION['admin']) ){
	echo '<form id = "ze" action="index.php?rt=event/try_delete_user" method="post">';
        echo '<a href="#" onclick="document.getElementById(\'ze\').submit();">Obrisi korisnika</a></form>';

      }

    ?>
    </div>
      <div id="main">
  		  <button class="openbtn" onclick="openNav()">&#9776</button>  
      </div>
            <?php if( isset( $_SESSION['username'] ) ){
		                echo '<form action="index.php?rt=event/logout" method="post">';
                    echo '<li style="float:right"><button class = "openbtn" style="padding: 12px 15px" type="submit">Log out</button></li></form>'; 
                    echo '<li style="float:right"><i class="fa fa-user-circle" style="font-size:30px; margin-top: 10px; margin-right: 5px;"></i></li>';
                  }
                  else
                  {
                    echo '<form action="index.php?rt=login/login" method="post">';
                    echo '<button class = "openbtn" style="float:right; padding: 12px 15px;" type="submit">Prijava</button></form>';
                                //echo '<li style="float:right"><a href="prijava.php">Prijava</a></li>';
                                //echo '<li style="float:right"><a href="registracija.php">Registracija</a></li>';
                    echo '<form action="index.php?rt=login/register" method="post">';
                    echo '<button class = "openbtn" style="float:right; padding: 12px 15px;" type="submit">Registracija</button></form>';
                    echo '<li style="float:right"><i class="fa fa-user-circle" style="font-size:30px; margin-top: 10px; margin-right: 5px;"></i></li>';
                  }
            ?>
        </ul>
	</head>
<script>
function openNav() {
  document.getElementById("mySidebar").style.width = "250px";
  document.getElementById("main").style.marginLeft = "250px";
}

function closeNav() {
  document.getElementById("mySidebar").style.width = "0";
  document.getElementById("main").style.marginLeft= "0";
}
</script>
<body>
	