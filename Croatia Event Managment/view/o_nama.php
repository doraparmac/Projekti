<?php require_once 'header.php'; ?>

	<form action="index.php?rt=login/try_login" method="post">
    <div class="div-main" >
        <div class="div-table">
        <table>
            <tr>
            <td>
            Mi smo skupina mladih ljudi koja je uvijek u potrazi za događajima u Hrvatskoj, 
            te smo odlučili obuhvatiti sve događaje na jednom mjestu!
</td>
            </tr>
            <tr>
                <td>
                Kroz naš sustav, možete imati uvid u sva aktualna događanja u Hrvatskoj, 
                kreirati vlastite događaje i time okupiti sve prijatelje i poznanike na jednom mjestu.
                </td>
            </tr>

            <tr>
                <td>
                Dostupni smo na adresi: Ulica izmisljena 4
                </td>
            </tr>
            
            <tr>
                <td>
                I na broju telefona: +385/91 000 00 00
                </td>
            </tr>

		</tr>

    </table>	
	
        </div>
    </div>
	</form>
	<?php echo $message ?>
<?php require_once 'footer.php';?>