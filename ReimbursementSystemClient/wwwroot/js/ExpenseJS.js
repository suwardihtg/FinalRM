$(document).ready(function () {
    $.ajax({
        url: "/Expenses/ExpenseCall",
        success: function (result) {
            $(".expense-title span").html(result);

            $.ajax({
                url: "/Expenses/GetExpense",
                type: "Get",
                success: function (result) {
                    console.log(result);
                    if (result.length != 0 ) {
                        $("#Approver").html(result[0].approver)
                    }    
                },
                error: function (error) {
                    $("#Approver").html("~~")
                }
            })

            table = $("#Formtable").DataTable({
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
            });

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

function Submit() {
    var obj = new Object();
    obj.approver = $("#Approver").text();
    obj.expenseId = parseInt($(".expense-title span").text());
    obj.purpose = $("#Purpose").val();
    obj.description = $("#Description").val();
    obj.total = $("#Total").val();
    obj.status = 1;
    $.ajax({
        url: "/Expenses/Submit/" + 1,
        type: "Put",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            Swal.fire(
                'Good job!',
                'Your data has been Submitted!',
                'success',    
            ).then((result2) => {
                if (result2) {
                    window.location.href = "/Reimbusments/Reimbusment"
                }
            })

        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Fail!'
            })
        }
    })
    $.LoadingOverlay("show");
    setTimeout(function () {
        $.LoadingOverlay("hide");
    }, 3000);
}

function SaveExit() {
    var obj = new Object();
    obj.approver = $("#Approver").text();
    obj.expenseId = parseInt($(".expense-title span").text());
    obj.purpose = $("#Purpose").val();
    obj.description = $("#Description").val();
    obj.total = $("#Total").val();
    obj.status = 0;
    $.ajax({
        url: "/Expenses/Submit/" + 2,
        type: "Put",
        'data': obj,
        'dataType': 'json',
        success: function (result) {    
            Swal.fire(
                'Good job!',
                'Your data has been saved!',
                'success',
            ).then((result2) => {
                if (result2) {
                    //need to close expense session first
                    window.location.href = "/Reimbusments/Reimbusment"
                }
            })
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Fail!'
            })
        }
    })
}

function Delete(id) {
    console.log(id)
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Forms/Delete/" + id,
                type: "Delete",
                success: function (result) {
                    console.log(result)
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    table.ajax.reload()
                },
                error: function (error) {
                    alert("Delete Fail");
                }
            });
        }
    })
}

function getDataForm(id) {
    $.ajax({
        url: "/Forms/Get/" + id,
        data: "",
        success: function (result) {
            console.log(result)
            var text = ""
            text =
                `
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Receipt Date : <span id="date"> ${dateConversion(result.receipt_Date)} </span>  </label>
                </div>
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Category : <span id="cat"> ${cata(result.category)} </span>  </label>
                </div>
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Payee : <span id="total"> ${result.payee} </span>  </label>
                </div>
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Total : <span id="total"> ${result.total} </span>  </label>
                </div>
                `
            $("#info").html(text);
            $("#desc").html(result.description)
            $.ajax({
                url: "/Forms/Getatc/" + result.attachments,
                type: "GET",
                data: "",
                success: function (result2) {
                    console.log(result2)
                    $("#attc").attr("src", convertimagefileshow(result2.name))
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
}

function EditForm(formid) {
    console.log(formid)
    $.ajax({
        url: "/Forms/EditForm/" + formid,
        success: function (result) {
            console.log(result)
            window.location.href = "/Reimbusments/Form";

        },
        error: function (error) {
            console.log(error)
        }
    })
}