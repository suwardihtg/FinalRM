$(document).ready(function () {

    table = $("#tabelExpense").DataTable({

        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseFinance",
            "type": "GET",
            "datatype": "json",
            dataSrc: ""
        },
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
                            <button type="button" class="btn btn-primary" data-toggle="modal"
                            onclick="Approve('${row['expenseId']}')" title="Approve" data-target="#UpdateModals">
                            <i class="fa fa-check-circle"></i>
                            </button>`;
                }
            }
        ]
    });
});

function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
}

function Reject(expenseid) {
    //var expenseid = parseInt($('#expenseId').text())
    var finance = $('textarea#managercomment').val();
    Swal.fire({
        title: 'Are you sure?',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, reject it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Expenses/Get/" + expenseid,
                type: "Get",
                success: function (result2) {
                    var obj = new Object();
                    obj.expenseId = expenseid;
                    obj.approver = result2.approver;
                    obj.commentManager = result2.commentManager;
                    obj.commentFinace = finance;
                    obj.purpose = result2.purpose;
                    obj.description = result2.description;
                    obj.total = result2.total;
                    obj.employeeId = result2.employeeId;
                    obj.status = 6;
                    console.log(obj)
                    $.ajax({
                        url: "/Expenses/Approval/" + 5,
                        type: "Put",
                        'data': obj,
                        'dataType': 'json',
                        success: function (result2) {
                            table.ajax.reload();
                            $("#exampleModal").modal('hide');
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
            $.LoadingOverlay("show");
            setTimeout(function () {
                $.LoadingOverlay("hide");
            }, 3000);
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
                    obj.status = 4;
                    console.log(obj)
                    $.ajax({
                        url: "/Expenses/Approval/" + 6,
                        type: "Put",
                        'data': obj,
                        'dataType': 'json',
                        success: function (result2) {
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
        $.LoadingOverlay("show");
        setTimeout(function () {
            $.LoadingOverlay("hide");
        }, 3000);
    })
}

function Payment(expenseid) {
    swal({
        title: "Comfirm payment?",
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
                    obj.status = 7;
                    console.log(obj)
                    $.ajax({
                        url: "/Expenses/Approval/" + 8,
                        type: "Put",
                        'data': obj,
                        'dataType': 'json',
                        success: function (result2) {
                            Swal.fire(
                                'Reimbursement Complete!',
                                'Your data has been saved.',
                                'success'
                            )
                            table.ajax.reload();
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
        $.LoadingOverlay("show");
        setTimeout(function () {
            $.LoadingOverlay("hide");
        }, 3000);
    })
}

function PaymentTable() {
    $('.status').html("Action");

    if ($.fn.DataTable.isDataTable('#tabelExpense')) {
        $('#tabelExpense').DataTable().destroy();
    }
    $('#tabelExpense tbody').empty();

    $(".column-tab").html(column());

    $("#tabelExpense").DataTable({
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
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseFinancePayment",
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
                            <button type="button" class="btn btn-primary"
                            onclick="Payment('${row['expenseId']}')" data-placement="top" title="Confirm Payment Complete">
                            <i class="fa fa-credit-card"></i>`;
                }
            }
        ],
    });

}

function RejectTable() {
    $('.status').html("Action");

    if ($.fn.DataTable.isDataTable('#tabelExpense')) {
        $('#tabelExpense').DataTable().destroy();
    }
    $('#tabelExpense tbody').empty();

    $(".column-tab").html(column());

    $("#tabelExpense").DataTable({
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
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseFinanceReject",
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
        ],
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
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseFinance",
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
        ],
    });
    
}



