$(document).ready(function () {
    $('.edit-project').click(function () {
        debugger;
        var url = "/projects/edit"; // the url to the controller
        var id = $(this).attr('data-id'); // the id that's given to each button in the list
        console.log(id);
        $.get(url + '/' + id, function (data) {
            $('#edit-project-container').html(data);
            $('#edit-project').modal('show');
        });
    });
});