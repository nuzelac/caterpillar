function caterpillar(selector, options) {
    window.nodeType = {
        HEAD: 0,
        NORMAL: 1,
        TAIL: 2
    }

    window.addQuestion = function (entry) {
        nodes.unshift({ text: entry.entry, nodeType: nodeType.NORMAL, id: entry.id });
        if (nodes.length > 1) {
            links.push({ source: nodes[0], target: nodes[1] });
        }
        update();
    }

    window.addHead = function () {
        nodes.unshift({ text: "", nodeType: nodeType.HEAD, id: -1 });
        update();
    }

    window.addTail = function () {
        nodes.unshift({ text: "", nodeType: nodeType.TAIL, id: -2 });
        if (nodes.length > 1) {
            links.push({ source: nodes[0], target: nodes[1] });
        }
        update();
    }

    window.removeLast = function () {
        nodes.shift();
        links.pop();
        update();
    }

    window.width = $(selector).width(),
      height = 800;

    window.color = d3.scale.category20();

    window.force = d3.layout.force()
        .charge(-1400)
        .linkDistance(100)
        .size([width, height]);

    window.svg = d3.select(selector).append("svg")
        .attr("width", width)
        .attr("height", height)
        .attr("id", "caterpillar-svg");

    window.chart = $("#caterpillar-svg"),
        aspect = chart.width() / chart.height(),
        container = chart.parent();

    $(window).on("resize", function () {
        var targetWidth = container.width();
        chart.attr("width", targetWidth);
        chart.attr("height", Math.round(targetWidth / aspect));
        force.size([$(selector).width(), height]);
    }).trigger("resize");

    window.nodes = force.nodes();
    window.links = force.links();

    window.update = function () {
        var link = svg.selectAll(".link")
            .data(links);

        var linkEnter = link.enter().append("line")
            .attr("class", "link")
            .style("stroke-width", 0);

        link.exit().remove();

        var node = svg.selectAll("g")
                   .attr("class", "nodes")
                   .data(nodes, function (d) { return d.id; });

        node
            .on("click", function (d) {
                if (d3.event.defaultPrevented) return; // prevents onClick behavior on drag

                if (d.nodeType === nodeType.NORMAL) {
                    if (options.nodeOnClick != false)
                        (options.nodeOnClick).call(self, d);
                    //$('#new-response-submit').click(function (e) {
                    //    e.preventDefault();
                    //    console.log($('#new-response-form').serialize());
                        
                    //    $.post($('#new-response-form').attr('action'),
                    //       $('#new-response-form').serialize(), 
                    //       function (data, status, xhr) {
                    //           if (data.success === true) {
                    //               $('#kwl').modal('hide');
                    //           }
                    //         // do something here with response;
                    //       });
                        
                    //});

                    //$('#kwl #tekst-pitanja').html(d.text);
                    //$('#kwl #question-id').val(d.id);
                    //$('#kwl #Response1').val('');
                    //$('#kwl').modal('show');
                } else if (d.nodeType === nodeType.TAIL) {
                    if (options.tailOnClick != false)
                        (options.tailOnClick).call(self, d);

                    //$('#new-entry-submit').click(function (e) {
                    //    e.preventDefault();
                    //    console.log($('#new-entry-form').serialize());

                    //    $.post($('#new-entry-form').attr('action'),
                    //       $('#new-entry-form').serialize(),
                    //       function (data, status, xhr) {
                    //           if (data.success === true) {
                    //               $('#kwl-new').modal('hide');
                    //           }
                    //           // do something here with response;
                    //       });

                    //});
                    //$('#kwl-new #Entry').val('');
                    //$('#kwl-new').modal('show');
                }
            });

        var nodeEnter = node.enter()
                   // Add one g element for each data node here.
                   .append("g")
                   .call(force.drag);

        // Add a circle element to the previously added g element.
        nodeEnter.append("circle")
              .attr("class", function (d) {
                  var classes = ["node"];
                  if (d.nodeType === nodeType.HEAD) {
                      classes.push("head");
                  }
                  if (d.nodeType === nodeType.TAIL) {
                      classes.push("tail");
                  }
                  return classes.join(" ");
              })
              .attr("r", 70);
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
                if (d.nodeType !== nodeType.HEAD)
                    return d.text;
            });

        node.exit().remove();

        var headRadius = 137;
        var headBackgroundImage = svg.selectAll(".head-img")
                        .data(nodes, function (d) { return d.id });

        headBackgroundImage.enter().append("svg:image")
                        .filter(function (d) { return d.nodeType === nodeType.HEAD })
                        .attr('class', 'head-img')
                        .attr('width', headRadius * 2)
                        .attr('height', headRadius * 2)
                        .attr("xlink:href", function (d) {
                            return "/Content/img/wurm3.svg";
                        })

        headBackgroundImage.exit().remove();

        var headTicalaImage = svg.selectAll(".head-ticala-img")
                        .data(nodes, function (d) { return d.id });

        headTicalaImage.enter().append("svg:image")
                        .filter(function (d) { return d.nodeType === nodeType.HEAD })
                        .attr('class', 'head-ticala-img')
                        .attr('width', headRadius * 2)
                        .attr('height', headRadius * 2)
                        .attr("xlink:href", function (d) {
                            return "/Content/img/wurm32.svg";
                        })

        headTicalaImage.exit().remove();

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

    window.update();

    window.addHead();
    for (i in questions) {
        window.addQuestion(questions[i]);
    }
    window.addTail();

    window.update();
};