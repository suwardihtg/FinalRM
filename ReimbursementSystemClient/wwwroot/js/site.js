function column() {
    return `<th scope="col" class="text-light text-center">Id</th>
            <th scope="col" class="text-light text-center">Name</th>
            <th scope="col" class="text-light text-center">Date Request</th>
            <th scope="col" class="text-light text-center">Total</th>
            <th scope="col" class="text-light text-center">Purpose</th>
            <th scope="col" class="text-light text-center status">Action</th>`
}

function remove(str) {
    // Get target th with the name you want to remove.
    var target = $('table').find('th[data-name="' + str + '"]');
    // Find its index among other ths

    var index = (target).index();
    // For each tr, remove all th and td that match the index.

    $('table tr').find('th:eq(' + index + '),td:eq(' + index + ')').remove();
}

function status(stat) {
    switch (stat) {
        case 0:
            return "Draft";
        case 1:
            return "Posted";
        case 2:
            return "Approved";
        case 3:
            return "Rejected";
        case 4:
            return "Canceled";
        case 5:
            return "Approved By Manager <br> Waiting for Finance Approval";
        case 6:
            return "Approved By Finance";
        case 7:
            return "Rejected At Phase 1"; 
        case 8:
            return "Rejected By Finance";
        case 9:
            return "Approved By Manager <br> Waiting for Senior Manager Approval";
        case 10:
            return "Approved By Senior Manager <br> Waiting for Finance Approval";
        case 11:
            return "Approved By Senior Manager <br> Waiting for Director Approval ";
        case 12:
            return "Approved By Director <br> Waiting for Finance Approval";
        default:
            return "Draft";
            break;
    }
}

function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
};

function EditExpense(expenseid) {
    console.log(expenseid)
    $.ajax({
        url: "/Expenses/EditExpense/" + expenseid,
        success: function (result) {
            console.log(result)
            window.location.href = "/Reimbusments/Expense";

        },
        error: function (error) {
            console.log(error)
        }
    })
}

function getData(id) {
    $.ajax({
        url: "/Expenses/Get/" + id,
        data: "",
        success: function (result) {
            console.log(result)
            var text = ""
            text =
                `
                <div class="form-group col-xl-6 col-sm-6 text-dark">
                    <label for="inputState">ExpenseId : <span id="Eid"> ${result.expenseId} </span>  </label>
                </div>

                <div class="form-group col-xl-6 col-sm-6 text-dark">
                    <label for="inputState">Status : <span id="stat"> ${status(result.status)} </span>  </label>
                </div>

                <div class="form-group col-xl-6 col-sm-6 text-dark">
                    <label for="inputState">Total : <span id="total"> ${result.total} </span>  </label>
                </div>
                <div class="form-group col-xl-6 col-sm-6 text-dark">
                    <label for="inputState">Submited : <span id="date"> ${dateConversion(result.submitted)} </span>  </label>
                </div>`
            $("#info").html(text);
            $("#desc").html(result.description)

            tableformdetail(result.expenseId)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function getData2(id) {
    $('textarea#managercomment').val('')
    $.ajax({
        url: "/Expenses/Get/" + id,
        data: "",
        success: function (result) {
            console.log(result)
            var text = ""
            text =
                `
                <div class="form-group col-xl-6 col-sm-6 text-dark">
                    <label for="inputState">ExpenseId : <span id="Eid"> ${result.expenseId} </span>  </label>
                </div>

                <div class="form-group col-xl-6 col-sm-6 text-dark">
                    <label for="inputState">Status : <span id="stat"> ${status(result.status)} </span>  </label>
                </div>

                <div class="form-group col-xl-6 col-sm-6 text-dark">
                    <label for="inputState">Total : <span id="total"> ${result.total} </span>  </label>
                </div>
                <div class="form-group col-xl-6 col-sm-6 text-dark">
                    <label for="inputState">Submited : <span id="date"> ${dateConversion(result.submitted)} </span>  </label>
                </div>
                `
            $(".reject-modal").html(text);
            console.log(result)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function tableformdetail(expenseid) {
    if ($.fn.DataTable.isDataTable('#dataTableForm')) {
        $('#dataTableForm').DataTable().destroy();
    }
    $('#dataTableForm tbody').empty();

    $("#dataTableForm").DataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "searching": false,
        "bAutoWidth": false,
        responsive: true,
        "ajax": {
            url: "/forms/getform/" + expenseid,
            type: "Get",
            dataSrc: ""
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            {
                "targets": [3, 4],
                "orderable": false
            }

        ],
        "columns": [
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["receipt_Date"])
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    switch (row["category"]) {
                        case 0:
                            return "Transportation";
                        case 1:
                            return "Parking";
                        case 2:
                            return "Medical";
                        case 3:
                            return "Lodging";
                        default:
                            return "~Empty~";
                            break;
                    }
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row["payee"] == null) {
                        return "~Empty~"
                    }
                    return row["payee"];
                }
            },  
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row["total"] == null) {
                        return "Rp. " + 0
                    }
                    return "Rp." + row["total"];
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="showimage('${row['attachments']}')" data-placement="top"
                            title="Attachments">
                            <i class="fas fa-images"></i></button>`;
                }
            }
        ]
    });

}

function convertimagefileshow(image) {
    var result = "https://localhost:44350/Images/" + image;
    return result
}

function showimage(link) {
    console.log(link)
    console.log(convertimagefileshow(link))
    $('#DetailModalimage').modal('show');
    $("#images").attr("src", convertimagefileshow(link));
}

function cata(cat) {
    switch (cat) {
        case 0:
            return "Transportation";
        case 1:
            return "Parking";
        case 2:
            return "Medical";
        case 3:
            return "Lodging";
        default:
            return "~Empty~";
            break;
    }
}

function convertimagefile(image) {
    var text = image
    var result = text.toString().replace("C:\\fakepath\\", "").replace(" ", "_").replace(" ", "_").replace(" ", "_").replace(" ", "_").replace(" ", "_");
    console.log(result.toString())
    return result
}

function AllTable() {

    if ($.fn.DataTable.isDataTable('#tabelExpense')) {
        $('#tabelExpense').DataTable().destroy();
    }

    $('#tabelExpense tbody').empty();

    if ($('.action').length == 0) {
        $('#tabelExpense').html()
    }

    var text = column() + `<th scope="col" class="text-light text-center action">Action</th>`
    $(".column-tab").html(text);

    $('.status').html("Status");

    $("#tabelExpense").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpensePosted",
            "type": "GET",
            "datatype": "json",
            dataSrc: ""
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        "columns": [
            {
                "data": "expenseId"
            },
            {
                "data": "name"
            },
            {
                //ganti submited date
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["dateTime"]);
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row["total"] == null) {
                        return "Rp. " + 0
                    }
                    return "Rp." + row["total"];
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row["description"] == null) {
                        return "No purpose"
                    }
                    return row["purpose"];
                }

            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return status(row["status"]);
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> `;
                }

            }

        ]
    });
}