function SaveExit() {
    var formData = new FormData();
    formData.append("Attachments", $('[name="file"]')[0].files[0]);

    console.log(formData)
    var obj = new Object();
    convertimagefile($("#files").val())
}

