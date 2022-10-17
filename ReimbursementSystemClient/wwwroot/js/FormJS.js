function ValidateEmployee(int) {
    var forms = document.querySelectorAll('.needs-validation')
    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            if (form.checkValidity() === true) {
                if (int == 1) {
                    SaveExit()
                } else if (int == 2) {
                    AddAnother()
                }
            }
            else {
                form.classList.add('was-validated');
                event.preventDefault();
                event.stopPropagation();
            }
        })
}

function SaveExit() {
    var formData = new FormData();
    var data = $('#Attachments')[0].files[0]
    formData.append("Attachments", $('#Attachments')[0].files[0]);

    var obj = new Object();
    obj.Receipt_Date = $("#Receipt_Date").val();
    obj.Start_Date = $("#Start_Date").val();
    obj.End_Date = $("#End_Date").val();
    obj.Category = $("#Category").val();
    obj.Payee = $("#Payee").val();
    obj.Description = $("#Description").val();
    obj.Total = $("#Total").val();
    obj.Attachments = convertimagefile($("#Attachments").val())
    console.log(obj)

    $.ajax({
        url: "/Forms/SingleUpload",
        type: "Post",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            console.log(result)
        },
        error: function (error) {
            console.log(error)
        }
    })

    $.ajax({
        url: "/Forms/InsertForm",
        type: "Post",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            window.location.href = "/Reimbusments/Expense"
        },
        error: function (error) {
            console.log(error)
        }
    })

    return false;
}

function Update() {
    $.ajax({
        url: "/Forms/FormCall",
        success: function (result) {
            var formData = new FormData();
            var data = $('#Attachments')[0].files[0]
            formData.append("Attachments", $('#Attachments')[0].files[0]);


            var obj = new Object();
            obj.fromId = result;
            obj.Receipt_Date = $("#Receipt_Date").val();
            obj.Start_Date = $("#Start_Date").val();
            obj.End_Date = $("#End_Date").val();
            obj.Category = $("#Category").val();
            obj.Payee = $("#Payee").val();
            obj.Description = $("#Description").val();
            obj.Total = $("#Total").val();
            obj.Attachments = convertimagefile($("#Attachments").val());
            console.log(obj)
            $.ajax({
                url: "/Forms/PutEditFrom",
                type: "Put",
                'data': obj,
                'dataType': 'json',
                success: function (result) {
                    window.location.href = "/Reimbusments/Expense"
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

function AddAnother() {
    var obj = new Object();
    var formData = new FormData();
    var data = $('#Attachments')[0].files[0]
    formData.append("Attachments", $('#Attachments')[0].files[0]);

    obj.Receipt_Date = $("#Receipt_Date").val();
    obj.Start_Date = $("#Start_Date").val();
    obj.End_Date = $("#End_Date").val();
    obj.Category = $("#Category").val();
    obj.Payee = $("#Payee").val();
    obj.Description = $("#Description").val();
    obj.Total = $("#Total").val();
    obj.Attachments = convertimagefile($("#Attachments").val());

    console.log(obj)

    $.ajax({
        url: "/Forms/InsertForm",
        type: "Post",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            window.location.href = "/Reimbusments/Form"
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
}

function Cancel() {
    window.location.href = "/Reimbusments/Expense"
}

$(document).ready(function () {
    $.ajax({
        url: "/Forms/FormCall",
        success: function (result) {
            console.log(result)
            if (result != null) {
                $(".Save-Exit").attr("onclick", "return Update()")
                $(".Save-Exit").html("Update")
            } 
           
            $.ajax({
                url: "/Forms/Get/" + result,
                type: "Get",
                data: "",
                success: function (result) {
                    $("#Receipt_Date").attr("value", dateInputConversion(result.receipt_Date))
                    $("#Start_Date").attr("value", dateInputConversion(result.start_Date))
                    $("#End_Date").attr("value", dateInputConversion(result.end_Date))
                    Category(result.category)
                    $("#Payee").attr("value", result.payee)
                    $("#Description").html(result.description)
                    $("#Total").attr("value", result.total)
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

    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('.profile-image').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
});

function Category(selected) {
   
    $("#Category option").each(function (i) {
        if (i-1 == selected) {
            $("#Category").val(i - 1).attr('selected', 'selected');
        }
    });
}

function dateInputConversion(dates) {
    if (dates == null) {
        return null
    }
    var date = new Date(dates)
    var newDate = date.getFullYear() + '-' + ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '-' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate()))
    console.log(newDate)
    return newDate
}

