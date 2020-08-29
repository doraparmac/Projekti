<?php require_once 'header.php'; ?>

	<form action="index.php?rt=event/add_event" method="post">
    <div class="div-main" >
        <div class="div-table-add">
        <table>
		<tr>
			<td> Ime eventa: </td>
			<td><input type="text" name="naslov" /></td>

			<td> Kategorija: </td>
			<td><input type="text" name="kategorija" /></td>
		</tr>
		<tr>
			<td> Opis: </td>
			<td> <textarea name="opis" rows = "10" cols = "50" placeholder="Ovdje napišite kratki opis..."></textarea> </td>
		</tr>
		<tr>
			<td> Mjesto održavanja: </td>
			<td><input type="text" name="mjesto" /></td>
		
			<td> Grad održavanja: </td>
			<td><input type="text" name="grad" /></td>
		</tr>
		<tr>
			<td> Datum početka događaja: </td>
			<td><input type="date" name="datum_pocetak" /></td>

			<td> Datum kraja događaja: </td>
			<td><input type="date" name="datum_kraj" /></td>
		</tr>
		<tr>
			<td> Vrijeme početka događaja: </td>
			<td><input type="time" name="vrijeme_pocetak" /></td>

			<td> Vrijeme kraja događaja: </td>
			<td><input type="time" name="vrijeme_kraj" /></td>
		</tr>
    	</table>
    <br> &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
    <button class="button" id="btnPrijava" type="submit" style="margin-left:6cm">KREIRAJ DOGAĐAJ</button>	
    	</div>
    </div>
	</form>
	<?php echo $message ?>
<?php require_once 'footer.php';?>