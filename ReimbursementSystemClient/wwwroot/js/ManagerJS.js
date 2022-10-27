$(document).ready(function () {
    table = $("#tabelExpense").DataTable({
        "processing": true,
        "responsive": true,
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                text: '',
                className: 'buttonHide fa fa-copy btn-primary',
                exportOptions: { orthogonal: 'export' }
            },
            {
                extend: 'excelHtml5',
                text: '',
                className: 'buttonHide fa fa-download btn-default',
                exportOptions: { orthogonal: 'export' }
            },
            {
                extend: 'print',
                text: '',
                className: 'buttonHide fa fa-print btn-default',
                exportOptions: { orthogonal: 'export' }
            }
        ],
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
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["date"]);
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
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fa fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal"
                            onclick="Reject('${row['expenseId']}')" data-target="#UpdateModals" title="Reject">
                            <i class="fa fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-warning" data-toggle="modal" 
                            onclick="Approve('${row['expenseId']}')" title="Approve" data-target="#UpdateModals">
                            <i class="fa fa-check-circle"></i>
                            </button>`;
                }
            }
        ]
    });
});


function Reject(expenseid) {
    Swal.fire({
        title: 'Are you sure?',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, reject it!'
    }).then((result) => {
        console.log("test");
        if (result.value) {
            $.ajax({
                url: "/Expenses/Get/" + expenseid,
                type: "Get",
                success: function (result2) {
                    var obj = new Object();
                    obj.expenseId = result2.expenseId;
                    obj.approver = result2.approver;
                    obj.commentManager = result2.commentManager;
                    obj.commentFinace = result2.commentFinace;
                    obj.purpose = result2.purpose;
                    obj.description = result2.description;
                    obj.total = result2.total;
                    obj.employeeId = result2.employeeId;
                    obj.status = 5;
                    console.log(obj)
                    $.LoadingOverlay("show");
                    setTimeout(function () {
                        console.log("test");
                        $.ajax({
                            url: "/Expenses/Approval/" + 3,
                            type: "Put",
                            'data': obj,
                            'dataType': 'json',
                            success: function (result) {
                                console.log("test2");
                                $.LoadingOverlay("hide");
                                table.ajax.reload();
                                $("#exampleModal").modal('hide');
                                console.log(result);
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
        title: "Do you want to approve this request?",
        text: "You won't be able to revert this!",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Write something"
    }).then((result) => {
        console.log(result)
        if (result.value) {
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
                    obj.status = 3;
                    console.log(obj)

                    $.LoadingOverlay("show");
                    setTimeout(function () {
                        $.ajax({
                            url: "/Expenses/Approval/" + 4,
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
                    });
                    
                },
                error: function (error) {
                    console.log(error)
                }
            })


        }

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
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                text: '',
                className: 'buttonHide fa fa-copy btn-primary',
                exportOptions: { orthogonal: 'export' }
            },
            {
                extend: 'excelHtml5',
                text: '',
                className: 'buttonHide fa fa-download btn-default',
                exportOptions: { orthogonal: 'export' }
            },
            {
                extend: 'print',
                text: '',
                className: 'buttonHide fa fa-print btn-default',
                exportOptions: { orthogonal: 'export' }
            }
        ],
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
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["date"]);
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
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fa fa-info-circle"></i> 
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
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                text: '',
                className: 'buttonHide fa fa-copy btn-primary',
                exportOptions: { orthogonal: 'export' }
            },
            {
                extend: 'excelHtml5',
                text: '',
                className: 'buttonHide fa fa-download btn-default',
                exportOptions: { orthogonal: 'export' }
            },
            {
                extend: 'print',
                text: '',
                className: 'buttonHide fa fa-print btn-default',
                exportOptions: { orthogonal: 'export' }
            }
        ],
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
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["date"]);
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
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fa fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal"
                            onclick="getData2('${row['expenseId']}')" data-target="#exampleModal" data-placement="top" title="Reject">
                            <i class="fa fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="Approve('${row['expenseId']}')" title="Approve" data-target="#UpdateModals">
                            <i class="fa fa-check-circle"></i>
                            </button>`;
                }
            }
        ]
    });
}