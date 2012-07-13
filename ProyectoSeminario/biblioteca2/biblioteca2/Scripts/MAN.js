/* File Created: junio 13, 2012 */
/*animacion del banner*/
$(document).ready(function () {
    $('#title1').imagecube({ full3D: false });
    
});
/*reloj*/
function startTime() {
    today = new Date();
    h = today.getHours();
    m = today.getMinutes();
    s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    document.getElementById('reloj').innerHTML = h + ":" + m + ":" + s;
    t = setTimeout('startTime()', 500);
}
function checkTime(i)
{ if (i < 10) { i = "0" + i; } return i; }
window.onload = function () { startTime(); }
/*login animado*/
$(document).ready(function () {
    $(".field1").click(function () {
        $(".field2").show("slow");
    });
    $(".field1").click(function () {
        $("#hide2").hide("slow");
    });
    $("#hide1").click(function () {
        $("#hide2").show("slow");
    });
    $("#hide1").click(function () {
        $(".field2").hide("slow");
    });
});

/*para los costados*/

$(document).ready(function(){
	
	var first = 0;
	var speed = 700;
	var pause = 3500;
	
		function removeFirst(){
			first = $('ul#listticker li:first').html();
			$('ul#listticker li:first')
			.animate({opacity: 0}, speed)
			.fadeOut('slow', function() {$(this).remove();});
			addLast(first);
		}
		
		function addLast(first){
			last = '<li style="display:none">'+first+'</li>';
			$('ul#listticker').append(last)
			$('ul#listticker li:last')
			.animate({opacity: 1}, speed)
			.fadeIn('slow')
		}
	
	interval = setInterval(removeFirst, pause);
});
