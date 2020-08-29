<?php require_once 'header.php'; ?>

    <div class="div-image" id="div-image">
        <div class="div-text">
            <h1 style="font-size:50px">CROATIA EVENT CALENDAR</h1>
            <input type="text" placeholder="Search..">

            <!-- ode bi tribala ici tablica s prikazom svih evenata i ono sortiranje -->
            <!-- za sortiranje po gradu i temi mozemo ucitavat iz ovog txtboxa iznad, a za vrijeme mozda oni kalendar sta iskoci -->

        </div>
    </div>
	<table>
    <tr>
        <th>Event</th>
    </tr>
	<?php
		$i=0;
		foreach( $eventList as $event ){
        		echo '<tr>' .
            		'<td class="popup" onmouseenter="obradi('.$i.')" onmouseleave="obradi('.$i.')" >'.$event->naslov.'<span class="popuptext" id="'.$i.'" >'.$event->opis.'</span></td>' .
             		'</tr>';
			$i++;
		}  
	?>
	</table>

	
<?php require_once 'footer.php';?>

<script>

$(document).ready(function(){

});

function obradi(x){
	var popup = document.getElementById(x);
  	popup.classList.toggle("show");
}

</script>