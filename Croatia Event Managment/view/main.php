<?php require_once 'header.php'; ?>

    <div class="div-image" id="div-image">
        <div class="div-text" id="div">
            <h1 style="font-size:50px">CROATIA EVENT CALENDAR</h1>
            <form id="search_form" action="index.php?rt=event/show_events" method="post">
                <input type="text" id="search" name="search" onkeyup="show_results()" placeholder="Search.." autocomplete="off">

	<ul class="x" id="myUL">
		
	</ul>
	</form>
        </div>
	
	</div>
	<?php 
		echo '<label class="message">';
		echo $message;
		echo '</label>';
	?>

<?php require_once 'footer.php';?>

<script>

$(document).ready(function(){
    $(document).on('click', function(){
 	  $('#myUL').html('');
    });
});


function show_results(){
	input = document.getElementById("search");
	user_input = input.value;
	$.ajax({
			url: '../projekt/model/show_results.php',
			data:
			{
				send: input.value
			},
			type: "GET",
			dataType: "json",
			success: function(data){
				$('#myUL').html('');
				var length = data.show_results.length;
				if( length > 6 ){ length = 6; }
				for(i=0; i<length; i++){
					$('#myUL').append('<li style="cursor:pointer" class="search" onclick="document.getElementById(\'search_form\').submit();" id="'+i+'" onmouseenter="obradi_enter('+i+')" onmouseleave="obradi_leave('+i+')">'+data.show_results[i]+'</li><br>');
				}
		
			},
			error: function(xhr, status, error) {
 				alert(xhr.responseText);
			}
	});

}

function obradi_enter(x){
	document.getElementById(x).style.backgroundColor="blue";
	var li = document.getElementById(x);
	input = document.getElementById("search");
	input.value = li.innerText;
}

function obradi_leave(x){
	document.getElementById(x).style.backgroundColor="gray";
	input = document.getElementById("search");
	input.value = user_input;
}



</script>