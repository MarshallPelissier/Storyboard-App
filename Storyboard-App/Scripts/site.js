$(document).ready(function () {
    $('.edit-project').click(function () {
        var url = "/projects/editproject"; // the url to the controller
        var id = $(this).attr('data-id'); // the id that's given to each button in the list
        $.get(url + '/' + id, function (data) {
            $('#edit-project-container').html(data);
            $('#edit-project').modal('show');
        });
    });

    $('.new-project').click(function () {
        var url = "/projects/newproject"; // the url to the controller
        $.get(url, function (data) {
            $('#new-project-container').html(data);
            $('#new-project').modal('show');
        });
    });

    $("#edit-project").on("submit", "#project-save", function (e) {
        e.preventDefault();  // prevent standard form submission

        var form = $(this);
        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),  // post
            data: form.serialize(),
            success: function (partialResult) {
                $("#edit-project-container").html(partialResult);
            }
        });
    });
});