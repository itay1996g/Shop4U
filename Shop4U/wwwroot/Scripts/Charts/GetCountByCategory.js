
function getPieChartData() {
    $.ajax('/Statistics/GetCountByCategory').then(function (data) {
        if (data && data.length) {
            var formattedData = data.map(function (obj) {
                return {
                    value: obj.count,
                    label: obj.name
                }
            });

            // set the Properties for the appearance of the graph
            var width = 400,
                height = 400,
                radius = Math.min(width, height) / 2;

            var color = d3.scaleOrdinal(d3.schemeCategory20);

            var arc = d3.arc()
                .outerRadius(radius - 10)
                .innerRadius(0);

            var labelArc = d3.arc()
                .outerRadius(radius - 60)
                .innerRadius(radius - 40);

            var pie = d3.pie()
                .value(function (d) { return d.value; })(formattedData);

            // append the svg object to the body of the page
            // append a group element to svg 
            var svg = d3.select("#PieChartSection_GetCountByCategory").append("svg")
                .attr("width", width)
                .attr("height", height)
                .append("g")
                .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

            var g = svg.selectAll(".arc")
                .data(pie)
                .enter().append("g")
                .attr("class", "arc");

            //define colors to the paths
            g.append("path")
                .attr("d", arc)
                .style("fill", function (d) { return color(d.data.label); });

            //add text to the chart 
            g.append("text")
                .attr("transform", function (d) { return "translate(" + labelArc.centroid(d) + ")"; })
                .attr("dy", ".35em")
                .style("text-anchor", "middle")
                .text(function (d) { return d.data.label + '-' + d.data.value; });

        }
    });
}

getPieChartData();