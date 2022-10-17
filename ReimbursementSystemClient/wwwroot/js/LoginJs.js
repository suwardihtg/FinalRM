

function Login() {
    var obj = new Object();
        obj.email = $("#email").val(),
        obj.password = $("#password").val(),
            console.log(obj),
      
        $.ajax({
            type: "POST",
            url: "/Accounts/Auth",
            dataType: 'json',
            data: obj,
            success: function (result) {
                window.location.href = result;
 
            },
            error: function (error) {
                Swal.fire({
                    type: "error",
                    title: 'Oops...',
                    text: 'login Fail!'
                })
            }
             
        })
    $.LoadingOverlay("show");
    setTimeout(function () {
        $.LoadingOverlay("hide");
    }, 3000);
}

// Show full page LoadingOverlay



// Hide it after 3 seconds

