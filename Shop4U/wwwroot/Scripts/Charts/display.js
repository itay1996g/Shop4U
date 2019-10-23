


$(function () { /* DOM ready */
    $("#myselect").change(function () {

        if ($(this).val() == "PieChartSection_GetCountByCategory") {
            $("#PieChartSection_GetCountByCategory").show();
            $("#barChartSection_GetCountByBranch").hide();
            $("#barChartSection_GetCountByCityUsers").hide();
            $("#barChartSection_GetCountByUsersGender").hide();


        }
        else if ($(this).val() == "barChartSection_GetCountByBranch") {
            $("#PieChartSection_GetCountByCategory").hide();
            $("#barChartSection_GetCountByBranch").show();
            $("#barChartSection_GetCountByCityUsers").hide();
            $("#barChartSection_GetCountByUsersGender").hide();

        }
        else if ($(this).val() == "barChartSection_GetCountByCityUsers") {
            $("#PieChartSection_GetCountByCategory").hide();
            $("#barChartSection_GetCountByBranch").hide();
            $("#barChartSection_GetCountByCityUsers").show();
            $("#barChartSection_GetCountByUsersGender").hide();

        }

        else if ($(this).val() == "barChartSection_GetCountByUsersGender") {
            $("#PieChartSection_GetCountByCategory").hide();
            $("#barChartSection_GetCountByBranch").hide();
            $("#barChartSection_GetCountByCityUsers").hide();
            $("#barChartSection_GetCountByUsersGender").show();

        }

    });
});