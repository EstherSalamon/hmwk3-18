$(() => {

    const addModal = new bootstrap.Modal($('#add-modal')[0]);
    const editModal = new bootstrap.Modal($('#edit-modal')[0]);

    refreshTable()

    $("#add-person").on('click', function () {
        addModal.show();
    });

    $("table").on('click', '#edit', function () {
        const id = $(this).data('id');

        $.get(`/home/getbyid?id=${id}`, function (person) {
            $("#id-for-edit").val(id);
            $("#first-name-edit").val(person.firstName);
            $("#last-name-edit").val(person.lastName);
            $("#age-edit").val(person.age);

            editModal.show();
        });
    });

    $("table").on('click', '.btn-outline-danger', function () {
        const id = $(this).data('id');

        $.post(`/home/deleteperson?id=${id}`, function () {
            refreshTable();
        });
    });

    $("#save").on('click', function () {

        const person = {
            firstName: $("#firstName").val(),
            lastName: $("#lastName").val(),
            age: $("#age").val()
        };

        $.post('/home/addperson', person, function () {
            refreshTable();
            clearModal();
            addModal.hide();
        });
    });

    $("#update").on('click', function () {
        const updatedPerson = {
            id: $("#id-for-edit").val(),
            firstName: $("#first-name-edit").val(),
            lastName: $("#last-name-edit").val(),
            age: $("#age-edit").val()
        };

        $.post('/home/updateperson', updatedPerson, function () {
            refreshTable();
            clearModal();
            editModal.hide();
        });
    });

    $("#close").on('click', function () {
        clearModal();
    });

    function refreshTable() {
        $("#table-body tr").remove();

        $.get('/home/getallpeople', function (people) {
            people.forEach((person) => {
                $('tbody').append(`
                <tr>
                    <td>${person.id}</td>
                    <td>${person.firstName}</td>
                    <td>${person.lastName}</td>
                    <td>${person.age}</td>
                    <td>
                        <button class='btn btn-outline-warning' id='edit' data-id='${person.id}'>Edit</button>
                        <button class='btn btn-outline-danger' id='delete' data-id='${person.id}'>Delete</button>
                    </td>
                </tr>`);
            });
        });
    }

    function clearModal() {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
    }
});