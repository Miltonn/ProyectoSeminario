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