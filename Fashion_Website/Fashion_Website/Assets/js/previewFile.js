function previewFile() {
    var fileInput = document.getElementById('file-input');
    var fileDisplay = document.getElementById('file-display');
    var hiddenField = document.getElementById('HinhSP');

    // Set the value of the hidden field to the file name
    hiddenField.value = fileInput.value.replace(/^.*\\/, "");

    // Display the file name
    fileDisplay.innerHTML = hiddenField.value;

    var preview = document.querySelector(".img");
    var file = document.querySelector('input[type=file]').files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}