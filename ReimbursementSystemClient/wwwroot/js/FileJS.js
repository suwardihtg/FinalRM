function SaveExit() {
    var formData = new FormData();
    formData.append("Attachments", $('[name="file"]')[0].files[0]);

    console.log(formData)
    var obj = new Object();
    convertimagefile($("#files").val())
    //$.ajax({
    //    url: "/Forms/SingleUpload",
    //    type: "Post",
    //    data: formData,
    //    processData: false,
    //    contentType: false,
    //    success: function (result) {
    //        console.log(result)
    //    },
    //    error: function (error) {
    //        console.log(error)
    //    }
    //})
    //return false;
}

