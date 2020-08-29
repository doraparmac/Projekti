<?php require_once 'header.php'; ?>

	<form action="index.php?rt=login/try_login" method="post">
    <div class="div-main" >
        <div class="div-table">
        <table>
            <tr>
            <td>
                Dopušteno je pretraživati događaje, te ih filtrirati.
                Možete se ulogirati, nakon registracije, te kreirati vlastite događaje i ostavljati 
                komentare na već postojećima.
            </td>
            </tr>
            <tr>
            <td>

                Admin zadržava pravo brisanja uvrijedljivih komentara, događaja, korisničkih računa.
            </td>
            </tr>
		</tr>

    </table>	
	
        </div>
    </div>
	</form>
	<?php echo $message ?>
<?php require_once 'footer.php';?>