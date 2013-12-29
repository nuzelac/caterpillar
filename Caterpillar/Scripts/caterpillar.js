$(function () {

    var addQuestion = function (q) {
        nodes.unshift({ text: q, head: false, id: nodes.length });
        if (nodes.length > 1) {
            links.push({ source: nodes[0], target: nodes[1] });
        }
        // console.log(nodes);
        update();
    }

    var addHead = function () {
        nodes.unshift({ text: "", head: true, id: nodes.length });
        update();
    }

    //setTimeout(function () {
    //    addQuestion("teeest");
    //}, 5000);

    var width = $('#caterpillar').width(),
      height = 800;

    var color = d3.scale.category20();

    var force = d3.layout.force()
        .charge(-1400)
        .linkDistance(100)
        .size([width, height]);

    var svg = d3.select("#caterpillar").append("svg")
        .attr("width", width)
        .attr("height", height)
        .attr("id", "caterpillar-svg");

    var chart = $("#caterpillar-svg"),
        aspect = chart.width() / chart.height(),
        container = chart.parent();

    $(window).on("resize", function () {
        var targetWidth = container.width();
        chart.attr("width", targetWidth);
        chart.attr("height", Math.round(targetWidth / aspect));
        force.size([$('#caterpillar').width(), height]);
    }).trigger("resize");

    var nodes = force.nodes();
    var links = force.links();



    var update = function () {
        var link = svg.selectAll(".link")
            .data(links);

        var linkEnter = link.enter().append("line")
            .attr("class", "link")
            .style("stroke-width", 0);

        link.exit().remove();

        var node = svg.selectAll("g")
                   .attr("class", "nodes")
                   .data(nodes, function (d) { return d.id; })
                   .on("click", function (d, i) { alert("Hello world"); });

        var nodeEnter = node.enter()
                   // Add one g element for each data node here.
                   .append("g")
                   .call(force.drag);

        // Add a circle element to the previously added g element.
        nodeEnter.append("circle")
              .attr("class", function (d) {
                  var classes = ["node"];
                  if (d.head === true) {
                      classes.push("head");
                  }
                  return classes.join(" ");
              })
              .attr("r", 70)
              .style("fill", "#fde0c8");
        ;

        var w = 90;
        var h = 95;

        nodeEnter.append("foreignObject")
          .attr({ width: w, height: h })
          .attr("x", function (d) {
              return -37;
          })
          .attr('y', function (d) {
              return -40;
          })
          .append("xhtml:div")
          .style({
              width: w + 'px',
              height: h + 'px',
              "font-size": "17px"
          })
            .html(function (d) {
                if (d.head !== true)
                    return d.text;
            });

        node.exit().remove();

        var headRadius = 137;
        var headBackgroundImage = svg.selectAll(".head-img")
                        .data(nodes, function (d) { return d.id });

        headBackgroundImage.enter().append("svg:image")
                        .filter(function (d) { return d.head === true })
                        .attr('class', 'head-img')
                        .attr('width', headRadius * 2)
                        .attr('height', headRadius * 2)
                        .attr("xlink:href", function (d) {
                            if (d.head === true)
                                return "/Content/img/wurm3.svg";
                        })

        headBackgroundImage.exit().remove();

        var headTicalaImage = svg.selectAll(".head-ticala-img")
                        .data(nodes, function (d) { return d.id });

        headTicalaImage.enter().append("svg:image")
                        .filter(function (d) { return d.head === true })
                        .attr('class', 'head-ticala-img')
                        .attr('width', headRadius * 2)
                        .attr('height', headRadius * 2)
                        .attr("xlink:href", function (d) {
                            return "/Content/img/wurm32.svg";
                        })

        headTicalaImage.exit().remove();

        node.on("click", function (e) {
            $('#kwl').modal(options);
        });

        force.on("tick", function (e) {

            link.attr("x1", function (d) { return d.source.x; })
                .attr("y1", function (d) { return d.source.y; })
                .attr("x2", function (d) { return d.target.x; })
                .attr("y2", function (d) { return d.target.y; });

            node.attr("transform", function (d) {
                return "translate(" + d.x + "," + d.y + ")";
            });

            var headOffset = -60;
            headBackgroundImage.attr("x", function (d) { return d.x - headRadius; })
                               .attr("y", function (d) { return d.y - headRadius + headOffset; });
            headTicalaImage.attr("x", function (d) { return d.x - headRadius; })
                           .attr("y", function (d) { return d.y - headRadius + headOffset; });

        });

        force.start();

        node.order();
    };

    update();

    addHead();
    for (i in questions) {
        addQuestion(questions[i]);
    }

});