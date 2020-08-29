<?php require_once 'header.php'; ?>
    <div class="div-main" >
        <div class="div-table">
	<form action="index.php?rt=event/delete_event" method="post">
		<select name="delete">
			<option value = 'null' selected = "true">Odaberi event</option>
				<?php
					for($i = 0; $i < count($eventList); $i++){
						echo '<option value="'.$eventList[$i]->id. '">('.$eventList[$i]->id.')'.$eventList[$i]->naslov.'</option>';
					}
				?>
		</select>
		<button class="button" id="btnDelete" type="submit">OBRISI EVENT</button>
	</form>

    <br> &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
    	</div>
    </div>
	<?php echo $message ?>
<?php require_once 'footer.php';?>
