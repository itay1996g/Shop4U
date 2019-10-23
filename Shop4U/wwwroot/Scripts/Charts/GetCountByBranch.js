function getBarChartData() {
    $.ajax('/Statistics/GetCountByBranch').then(function (data) {
        if (data && data.length) {
            // set the Properties of the graph size
            var margin = { top: 20, right: 20, bottom: 20, left: 40 },
                width = 600 - margin.left - margin.right,
                height = 350 - margin.top - margin.bottom;



            var x = d3.scaleBand()
                .range([0, width])
                .padding(0.1);


            var y = d3.scaleLinear()
                .range([height, 0]);



            // append the svg object to the body of the page
            // append a group element to svg and moves it to the top left margin
            var svg = d3.select("#barChartSection_GetCountByBranch").append("svg")
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom)
                .append("g")
                .attr("transform",
                    "translate(" + margin.left + "," + margin.top + ")");




            // Scale the range of the data
            x.domain(data.map(function (d) { return d.name; }));
            y.domain([0, d3.max(data, function (d) { return d.count; })]);



            // add and set the rectangles for the bar chart
            svg.selectAll(".bar")
                .data(data)
                .enter().append("rect")
                .attr("class", "bar")
                .attr("x", function (d) { return x(d.name); })
                .attr("width", x.bandwidth())
                .attr("y", function (d) { return y(d.count); })
                .attr("height", function (d) { return height - y(d.count); })
                .attr("fill", "blue");

            // add the x Axis
            svg.append("g")
                .attr("transform", "translate(0," + height + ")")
                .call(d3.axisBottom(x));

            // add the y Axis
            svg.append("g")
                .call(d3.axisLeft(y));
        }
    });
}

getBarChartData();