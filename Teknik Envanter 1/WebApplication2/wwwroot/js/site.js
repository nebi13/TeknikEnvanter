// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//== Class Definition
var NB = function () {
    var selector;
    var dataSource;
    var columns;

    return {
        generateGridList: function (s, d, c) {
            columns = c;
            dataSource = d;
            selector = s;

            $(selector).empty();

            $(selector).append("<div class='table-responsive'><table class='table'></table></div>");


            var Head = "";

            $.each(columns, function (key, value) {
                Head = Head + "<th>" + value.title + "</th>";
            });

            var tableHead = "<tr>" + Head + "</tr>";

            

            $(selector + " table").append(tableHead);

            for (i = 0; i < dataSource.length; i++) {
                var tableData = "";

                for (var j = 0; j < columns.length; j++) {
                    if (j == 0) {
                        tableData = tableData + "<input type='hidden'></input>";
                        tableData = tableData + "<th><a href='javascript:;' style='text-decoration: none; ' value='" + dataSource[i].id + "' >+<a/> " + dataSource[i][columns[j].value] + "</th>";

                    } else {
                        switch (columns[j].value) {
                            case "phone":
                                tableData = tableData + "<th><a href='javascript:;' onclick='Popups.openCallingPopUp(" + dataSource[i][columns[j].value] + ")'>" + dataSource[i][columns[j].value] + "</a></th>";
                                break;
                            case "tckn":
                                tableData = tableData + "<th><a href='javascript:;' onclick='Popups.openPersonInfoPopUp(" + JSON.stringify(dataSource[i]) + ")'>" + dataSource[i][columns[j].value] + "</a></th>";
                                break;
                            default:
                                tableData = tableData + "<th>" + dataSource[i][columns[j].value] + "</th>";
                                break;
                        }

                    }
                   
                }

                
                var tableRow = (i % 2 == 0 ? "<tr class='table-info'>" : "<tr>") + tableData + "</tr>";
                var table = tableRow;
                $(selector + ' tr:last').after(table);
                $(selector + ' tr:last').find('input').val(dataSource[i].id);
            }

            $(selector + " th a").click(function () {

                if ($(this).html() == "+") {
                    $(this).html('-');
                    var lineId = $(this).closest('tr').find('input').val();
                    var detailModel = dataSource.filter(x => x.id == lineId)[0].People;
                    for (i = 0; i < detailModel.length; i++) {
                        var tableData = "";

                        for (var j = 0; j < columns.length; j++) {
                            if (j == 0) {
                                tableData = tableData + "<input class='detail-data-1' type='hidden'></input>";
                            } 
                            switch (columns[j].value) {
                                case "phone":
                                    tableData = tableData + "<th><a href='javascript:;' onclick='Popups.openCallingPopUp(" + detailModel[i][columns[j].value] + ")'>" + detailModel[i][columns[j].value] + "</a></th>";
                                    break;
                                case "tckn":
                                    tableData = tableData + "<th><a href='javascript:;' onclick='Popups.openPersonInfoPopUp(" + JSON.stringify(detailModel[i]) + ")'>" + detailModel[i][columns[j].value] + "</a></th>";
                                    break;
                                default:
                                    tableData = tableData + "<th" + (j == 0 ? " style='text-align: right;'" : "") + ">" + detailModel[i][columns[j].value] + "</th>";
                                    break;
                            }
                            
                        }


                        var tableRow = (i % 2 == 0 ? "<tr class='table-info detail-data'>" : "<tr>") + tableData + "</tr>";
                        var table = tableRow;
                        $(this).closest('tr').after(table);
                        $('input.detail-data-1').each(function (index, elem) {
                            if (!$(elem).val()) {
                                $(elem).val(lineId);
                            }
                        });
                    }
                } else if ($(this).html() == "-"){
                    $(this).html('+');
                    var inputValueToDelete = $(this).closest('tr').find('input').val()
                    $('input.detail-data-1').each(function (index, elem) {
                        if ($(elem).val() == inputValueToDelete) {
                            $(elem).closest('tr').remove();
                        }
                    });
                   

                }
                
            });


           
        }
    }
}();


var Popups = function () {
    return {
       
        openCallingPopUp: function (number) {
            $("#callModal").remove();
            var target = '/Home/CallModal?number=' + number;

            $.get(target, function (html) {
                $(html).appendTo("body");

                $("#callModal").modal("show");

            });
        },
        openPersonInfoPopUp: function (model) {
            $("#personInfoModal").remove();
            $.post("/Home/PersonInfoModal", model, function (html) {
                $(html).appendTo("body");
                $("#personInfoModal").modal("show");
            });

        }

    }
}();





