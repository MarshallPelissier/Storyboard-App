$(document).ready(function () {
    $('.edit-project').click(function () {
        var url = "/projects/editproject"; // the url to the controller
        var id = $(this).attr('data-id'); // the id that's given to each button in the list
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
});