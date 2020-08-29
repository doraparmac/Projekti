<?php require_once 'header.php'; ?>

	<form action="index.php?rt=login/try_login" method="post">
    <div class="div-main" >
        <div class="div-table">
        <table>
			<td> username: </td>
			<td><input type="text" name="txtUsername" /></td>
		</tr>
		<tr>
			<td> password: </td>
			<td> <input type="password" name="txtPassword" /> </td>
		</tr>
    </table>	
    <br> &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
    <button class="button" id="btnPrijava" type="submit">PRIJAVI SE</button>	
        </div>
    </div>
	</form>
	<?php 
	echo '<label class="message">';
	echo $message;
	echo '</label>';
	
	?>
<?php require_once 'footer.php';?>