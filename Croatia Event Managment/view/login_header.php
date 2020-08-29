<!DOCTYPE html>
	<html>
	<head>
		<meta charset="utf8">
		<title>Croatia Event Calendar</title>
        <link rel="stylesheet" type="text/css" href="../css/style.css" />
        <script src='https://kit.fontawesome.com/a076d05399.js'></script>
        <script src="sidebar.js"></script>
        
		<ul>
            <li><i class='fas fa-align-justify' style='font-size:36px; margin-top:5px;margin-left:5px;'></i></li>
            <li><a href="main.php">EVENT CALENDAR</a></li>
            
            <?php if( isset( $_SESSION['username'] ) ){
                    echo '<li style="float:right"><a href="logout.php">Log out</a></li>'; 
                    echo '<li style="float:right"><i class="fa fa-user-circle" style="font-size:30px; margin-top:10px;"></i></li>';
                  }
                  else
                  {
		    echo '<form action="index.php?rt=login/login" method="post">';
		    echo '<button style="float:right" type="submit">Prijava</button></form>';
                    //echo '<li style="float:right"><a href="prijava.php">Prijava</a></li>';
                    //echo '<li style="float:right"><a href="registracija.php">Registracija</a></li>';
		    echo '<form action="index.php?rt=login/register" method="post">';
		    echo '<button style="float:right" type="submit">Registracija</button></form>';
                    echo '<li style="float:right"><i class="fa fa-user-circle" style="font-size:30px; margin-top:10px;"></i></li>';
                  }
            
            ?>
        </ul>
	</head>
	<body>
	