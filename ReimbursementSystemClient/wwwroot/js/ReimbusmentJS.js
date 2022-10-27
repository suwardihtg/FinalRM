﻿function InsertExpense() {
    var obj = new Object();
    obj.Status = 0;
    console.log(obj);
    $.ajax({
        url: "/Expenses/NewExpense",
        type: "Post",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            console.log(result)
            window.location.href = "/Reimbusments/Form";
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
}

$(document).ready(function () {
    table = $("#Expense-table").DataTable({
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
            "url": "/Expenses/GetExpense",
            dataSrc: ""
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        "columns": [
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["submittedDate"]);
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return row["expenseId"];
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
                    return status(row["status"])
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    var draft = `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fa fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Delete('${row['expenseId']}')" data-placement="top" title="Delete">
                            <i class="fa fa-trash"></i>
                            </button>
                            <button type="button" class="btn btn-warning" 
                            onclick="EditExpense('${row['expenseId']}')" title="Edit">
                            <i class="fa fa-edit"></i>
                            </button>`
                    var nondraft = `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fa fa-info-circle"></i>
                            </button>`
                    if (row["status"] != 0) {
                        return nondraft
                    } else {
                        return draft
                    }
                }
            }
        ],
        success: function (result) {
            console.log(result)
        },
        error: function (error) {
            console.log(error)
        }
    });
});
   
function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        var obj = new Object();
        obj.approver = $("#Approver").text();
        obj.expenseId = parseInt($(".expense-title span").text());
        obj.purpose = $("#Purpose").val();
        obj.description = $("#Description").val();
        obj.total = $("#Total").val();
        obj.expenseId = id;
        obj.status = 2;
        $.ajax({
            url: "/Expenses/Submit/" + 12,
            type: "Put",
            'data': obj,
            'dataType': 'json',
            success: function (result) {
                table.ajax.reload()
                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )
            },
            error: function (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Delete Fail!'
                })
            }
        })
    })
}