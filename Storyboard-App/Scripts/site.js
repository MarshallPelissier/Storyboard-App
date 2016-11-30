$(document).ready(function () {
    //Project Dialog Functions
    $('.edit-project').click(function () {
        var url = "/projects/editproject"; // the url to the controller
        var id = $(this).attr('data-id'); // the id that's given to each button
        $.get(url + '/' + id, function (data) {
            $('#edit-dialog-container').html(data);
            $('#edit-dialog').modal('show');

            var labelled = $('#project-modal-label').attr('name');
            if (labelled != "edit-project-label")
            {
                $('#project-modal-label').attr("name","edit-project-label");
                $('#project-modal-label').append("Edit Project");
            }
        });
    });

    $('.new-project').click(function () {
        var url = "/projects/newproject"; // the url to the controller
        $.get(url, function (data) {
            $('#edit-dialog-container').html(data);
            $('#edit-dialog').modal('show');

            var labelled = $('#project-modal-label').attr('id');
            if (labelled != "new-project-label") {
                $('#project-modal-label').attr("name", "new-project-label");
                $('#project-modal-label').append("New Project");
            }
        });
    });

    //Page Dialog Functions
    $('.edit-page').click(function () {
        debugger;
        var url = "/projects/editpage"; // the url to the controller
        var num = $(this).attr('data-num'); // the id that's given to each button
        var project = $(this).attr('data-project'); 
        $.get(url + '/' + project + '/' + num, function (data) {
            $('#edit-dialog-container').html(data);
            $('#edit-dialog').modal('show');

            var labelled = $('#project-modal-label').attr('name');
            if (labelled != "edit-project-label") {
                $('#project-modal-label').attr("name", "edit-project-label");
                $('#project-modal-label').append("Edit Project");
            }
        });
    });

    $('.new-page').click(function () {
        var url = "/projects/newpage"; // the url to the controller
        var name = $(this).attr('data-name'); // the id that's given to each button
        $.get(url + '/' + name, function (data) {
            $('#edit-dialog-container').html(data);
            $('#edit-dialog').modal('show');

            var labelled = $('#project-modal-label').attr('id');
            if (labelled != "new-project-label") {
                $('#project-modal-label').attr("name", "new-page-label");
                $('#project-modal-label').append("New Page");
            }
        });
    });

    //Dialog Save Function
    $("#edit-dialog").on("submit", "#save-dialog", function (e) {
        e.preventDefault();  // prevent standard form submission
        var form = $(this);
        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),  // post
            data: form.serialize(),
            success: function (partialResult) {
                $("#edit-dialog-container").html(partialResult);
            }
        });
    });
    //Picture Save Function
    $(document).on("submit", "#save-picture", function (e) {
        e.preventDefault();
        debugger;
        var partialValue = $('#partial-image').val();  // get the value
        //var originalValue = $('#picture-form').val();
        //var hiddenElement = $('[id^=picture] [value="' + originalValue + '"]')
        var form = $(this);
        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),  // post
            data: form.serialize(),
            success: function (partialResult) {
                $("#edit-dialog-container").html(partialResult);
            }
        });
        var $hiddenElement = $(this).parent().parent();
        $($hiddenElement).val(partialValue);         // "save" to main view     
    });
});