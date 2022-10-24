$.ajax({
    url: "/Expenses/GetExpense",
    type: "Get",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})

/*$(document).ready(function () {
    $.ajax({
        url: "/Expenses/ExpenseCall",
        success: function (result) {
            $(".expense-title span").html(result);

            $.ajax({
                url: "/Expenses/GetExpense",
                type: "Get",
                success: function (result) {
                    console.log(result);
                    if (result.length != 0) {
                        $("#Approver").html(result[0].approver)
                    }
                },
                error: function (error) {
                    $("#Approver").html("~~")
                }
            })

            *//*table = $("#Formtable").DataTable({
                responsive: true,
                "ajax": {
                    "url": "/forms/getform/" + result,
                    type: "Get",
                    dataSrc: ""
                },
                "columnDefs": [
                    { "className": "dt-center", "targets": "_all" }
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
                            if (row["description"] == null) {
                                return "No Description"
                            }
                            return row["description"];
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
                            return `<button type="button" class="btn btn-primary " data-toggle="modal" 
                            onclick="getDataForm('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-danger hide-btn" data-toggle="modal" onclick="Delete('${row['formId']}')" data-placement="top" title="Delete">
                            <i class="fas fa-trash-alt"></i> 
                            </button>
                            <button type="button" class="btn btn-info hide-btn" data-toggle="modal"
                            onclick="EditForm('${row['formId']}')" title="Edit" data-target="#UpdateModals">
                            <i class="fas fa-edit"></i>
                            </button>`;
                        }
                    }
                ],
                initComplete: function () {
                    $.ajax({
                        url: "/Expenses/Get/" + result,
                        type: "Get",
                        data: "",
                        success: function (result) {
                            console.log(result)
                            $("#Status").html(status(result.status))
                            if ($("#Approver").text() == "") {
                                $("#Approver").html(result.approver)
                            }
                            $("#Description").html(result.description)
                            $("#Purpose").attr("value", result.purpose)
                            if (result.status != 0) {
                                $("#Description").prop('disabled', true);
                                $("#Purpose").prop('disabled', true);
                                $(".hide-btn").hide();
                            } else {
                                $("#Description").removeAttr('disabled');
                                $("#Purpose").removeAttr('disabled');
                                $(".hide-btn").show();
                            }
                        },
                        error: function (error) {
                            console.log(error)
                        }
                    })
                }
            });*//*

            $.ajax({
                url: "/forms/TotalExpenseForm/" + result,
                type: "Get",
                success: function (result) {
                    $("#Total").val(result.total)
                },
                error: function (error) {
                    console.log(error)
                }
            })

            $.ajax({
                url: "/Expenses/GetHistory/" + result,
                type: "Get",
                success: function (result) {
                    console.log(result)
                    var text = ``;
                    for (var i = 0; i < result.length; i++) {
                        text += `${result[i].message}\n`
                    }
                    $("#History").html(text)
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
});

function InsertForm() {
    var obj = new Object();
    obj.approver = $("#Approver").text();
    obj.expenseId = parseInt($(".expense-title span").text());
    obj.purpose = $("#Purpose").val();
    obj.description = $("#Description").val();
    obj.total = $("#Total").val();
    obj.status = 0;
    $.ajax({
        url: "/Expenses/Submit/" + 11,
        type: "Put",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            $.ajax({
                url: "/Forms/NewForm/",
                success: function (result) {
                    window.location.href = result;
                },
                error: function (error) {
                    console.log(error)
                }
            });
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Fail!'
            })
        }
    })
}*/

function InsertExpense() {
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
            window.location.href = "/Reimbusments/Expense";
            //$("#addRequest").modal("toggle");
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
    /*let btn = document.getElementById("buttonNext");
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        let obj = new Object();
        obj.name = $("#addName").val();
        obj.region_Id = $("#addRegionId").val();
        $.ajax({
            url: "/Country/Post",
            type: "POST",
            data: obj
        }).done((result) => {
            console.log(result);
            if (result == 200) {
                Swal.fire(
                    'Good Job!',
                    'Your data has been saved.',
                    'success'
                )
                $("#addCountry").modal("toggle");
                $('#dataTable').DataTable().ajax.reload();
            }
            else if (result == 400) {
                Swal.fire(
                    'Watch Out!',
                    'Duplicate Data!',
                    'error'
                )
            }
        }).fail((error) => {
            console.log(error);
        })
    })*/
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
                    return dateConversion(row["submitted"]);
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
                    if (row["purpose"] == null) {
                        return "No Purpose"
                    }
                    return row["purpose"];
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
                            <i class="fa fa-trash-alt"></i>
                            </button>
                            <button type="button" class="btn btn-info"
                            onclick="EditExpense('${row['expenseId']}')" title="Edit" >
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
        obj.status = 4;
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



