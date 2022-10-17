$.ajax({
    "url": "/Expenses/GetExpenseSManager",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})

$(document).ready(function () {

    table = $("#tabelExpense").DataTable({
        "processing": true,
        "responsive": true,
        "ajax": {
            "url": "/Expenses/GetExpenseSManager",
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
                "data": "purpose"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-info"
                            onclick="EditExpense('${row['expenseId']}')" title="Open Form" >
                            <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal"
                            onclick="getData2('${row['expenseId']}')" data-target="#exampleModal" data-placement="top" title="Reject">
                            <i class="far fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-info" data-toggle="modal" 
                            onclick="Approve('${row['expenseId']}')" title="Approve" data-target="#UpdateModals">
                            <i class="far fa-check-circle"></i>
                            </button>`;
                }
            }
        ]
    });
});

function Reject() {
    var expenseid = parseInt($('#expenseId').text())
    var managercomment = $('textarea#managercomment').val();
    Swal.fire({
        title: 'Are you sure?',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Reject it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Expenses/Get/" + expenseid,
                type: "Get",
                success: function (result2) {
                    var obj = new Object();
                    obj.expenseId = expenseid;
                    obj.approver = result2.approver;
                    obj.commentManager = managercomment;
                    obj.commentFinace = result2.commentFinace;
                    obj.purpose = result2.purpose;
                    obj.description = result2.description;
                    obj.total = result2.total;
                    obj.employeeId = result2.employeeId;
                    obj.status = 7;
                    console.log(obj)

                    $.LoadingOverlay("show");
                    setTimeout(function () {
                        $.ajax({
                            url: "/Expenses/Approval/" + 7,
                            type: "Put",
                            'data': obj,
                            'dataType': 'json',
                            success: function (result2) {
                                $.LoadingOverlay("hide");
                                table.ajax.reload();
                                $("#exampleModal").modal('hide');
                            },
                            error: function (error) {
                                console.log(error)
                            }
                        })
                    });
                },
                error: function (error) {
                    console.log(error)
                }
            })
        }
    })
}

function Approve(expenseid) {
    swal({
        title: "Do you want to approvee this ??",
        text: "You can't revert this!!",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Write something"
    }).then((result) => {
        console.log(result)
        setTimeout(function () {
            if (result.value) {
                $.LoadingOverlay("show");
                $.ajax({
                    url: "/Expenses/Get/" + expenseid,
                    type: "Get",
                    success: function (result) {
                        var obj = new Object();
                        obj.expenseId = expenseid;
                        obj.approver = result.approver;
                        obj.commentManager = result.commentManager;
                        obj.commentFinace = result.commentFinace;
                        obj.purpose = result.purpose;
                        obj.description = result.description;
                        obj.total = result.total;
                        obj.employeeId = result.employeeId;
                        obj.status = 10;
                        if (result.total > 10000000) {
                            obj.status = 11;
                        }
                        console.log(obj)
                        $.ajax({
                            url: "/Expenses/Approval/" + 8,
                            type: "Put",
                            'data': obj,
                            'dataType': 'json',
                            success: function (result2) {
                                $.LoadingOverlay("hide");
                                table.ajax.reload();
                                console.log(result2);
                            },
                            error: function (error) {
                                console.log(error)
                            }
                        })
                    },
                    error: function (error) {
                        console.log(error)
                    }
                })
            }
        });
    })
}

function RejectTable() {
    $('.status').html("Action");

    if ($.fn.DataTable.isDataTable('#tabelExpense')) {
        $('#tabelExpense').DataTable().destroy();
    }
    $('#tabelExpense tbody').empty();

    $(".column-tab").html(column());

    $("#tabelExpense").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseManagerReject",
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
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>`;
                }
            }
        ]
    });

}

function RequestTable() {

    $('.status').html("Action");
    if ($.fn.DataTable.isDataTable('#tabelExpense')) {
        $('#tabelExpense').DataTable().destroy();
    }
    $('#tabelExpense tbody').empty();

    $(".column-tab").html(column());

    $("#tabelExpense").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseManager",
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
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-info"
                            onclick="EditExpense('${row['expenseId']}')" title="Open Form" >
                            <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal"
                            onclick="getData2('${row['expenseId']}')" data-target="#exampleModal" data-placement="top" title="Reject">
                            <i class="far fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-info" data-toggle="modal" 
                            onclick="Approve('${row['expenseId']}')" title="Approve" data-target="#UpdateModals">
                            <i class="far fa-check-circle"></i>
                            </button>`;
                }
            }


        ]
    });
}
