<?php require_once 'header.php'; ?>
    <div class="div-main" >
        <div class="div-table">
	<form action="index.php?rt=event/delete_user" method="post">
		<select name="delete" style="width:60%;" >
			<option value = 'null' selected = "true">Odaberi korisnika</option>
				<?php
					for($i = 0; $i < count($userList); $i++){
						if( $userList[$i]->username == $_SESSION['username'] ){ continue; }
						echo '<option value="'.$userList[$i]->id. '">('.$userList[$i]->id.')'.$userList[$i]->username.'</option>';
					}
				?>
		</select>
		<br>
		<button class="button" id="btnDelete" type="submit">OBRISI KORISNIKA</button>
	</form>

    <br> &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
    	</div>
    </div>
	<?php echo $message ?>
<?php require_once 'footer.php';?>
