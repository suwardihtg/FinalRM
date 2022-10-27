$(document).ready(function () {
    $.ajax({
        url: "/Expenses/ExpenseCall",
        success: function (result) {
            $(".expense-title span").html(result);

            table = $("#Formtable").DataTable({
                responsive: true,
                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'copyHtml5',
                        text: '',
                        className: 'buttonHide fa fa-copy btn-default',
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
                            return dateConversion(row["requestDate"])
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
                            return row["accountNumber"];
                        }
                    },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return row["bankName"];
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
                            <i class="fa fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-warning hide-btn" data-toggle="modal"
                            onclick="EditForm('${row['formId']}')" title="Edit" data-target="#UpdateModals">
                            <i class="fa fa-edit"></i>
                            </button>`;
                        }
                    }  
                ],
            });

            $.ajax({
                url: "/forms/getform/" + result,
                type: "Get",
                success: function (data) {
                    console.log(data)
                    $("#Total").val(data[0].total)
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
    obj.expenseId = parseInt($(".expense-title span").text());
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
                'Your data has been submitted!',
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
    /*$.LoadingOverlay("show");
    setTimeout(function () {
        $.LoadingOverlay("hide");
    }, 2000);*/
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
                    <label for="inputState">Date : <span id="date"> ${dateConversion(result.requestDate)} </span>  </label>
                </div>
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Category : <span id="cat"> ${cata(result.category)} </span>  </label>
                </div>
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Bank Account : <span id="bank"> ${result.bankName} ${result.accountNumber} </span>  </label>
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