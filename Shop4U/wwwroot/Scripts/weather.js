


$(function () { /* DOM ready */
    $("#branches_list").change(function () {


        var url = "https://api.openweathermap.org/data/2.5/weather?q=" + $(this).val() + "&units=metric&mode=html&appid=a79bd08c5d0384ebc498be5b9b3b8270"

        $("#weather_iframe").attr("src", url );
        $("#weather_iframe").css("display", "block");



    });
});