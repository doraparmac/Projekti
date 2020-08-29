<?php require_once 'header.php'; ?>

	<form action="index.php?rt=login/try_register" method="post">
    <div class="div-main" >
        <div class="div-table">
        <table>
		<tr>
			<td> ime: </td>
			<td><input type="text" name="txtIme" /></td>
        </tr>
        <tr>
			<td> prezime: </td>
			<td><input type="text" name="txtPrezime" /></td>
        </tr>
        <tr>
			<td> username: </td>
			<td><input type="text" name="txtUsername" /></td>
		</tr>
		<tr>
			<td> password: </td>
			<td> <input type="password" name="txtPassword" /> </td>
		</tr>
		<tr>
			<td> e-mail: </td>
			<td> <input type="text" name="txtEmail" /> </td>
        </tr>		
    </table>	
    <br> &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
    <button class="button" id="btnRegistracija" type="submit">REGISTRIRAJ SE</button>	
        </div>
    </div>
	<?php 
		echo '<label class="message">';
		echo $message;
		echo '</label>';
	?>
	</form>

<?php require_once 'footer.php';?>