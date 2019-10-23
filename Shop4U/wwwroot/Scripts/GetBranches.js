function GeBranches() {
    $.ajax('/Home/GeBranches').then(function (data) {
        initMap(data);

        //Go over the JSON by JQUERY
        /*
        $.each(data, function (index) {
            alert(data[index].lat);
            alert(data[index].lng);
        });*/


    });
}

GeBranches();