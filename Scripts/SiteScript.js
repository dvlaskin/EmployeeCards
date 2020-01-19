$(document).ready(function () {

    $('#employee-table').DataTable(
    {
            "sAjaxSource": '/Home/ReloadTable',
            "columns": [

                { "data": "RegisterId" },
                { "data": "Title" },
                { "data": "Fio" },
                { "data": "Salary" },
                { "data": "DateHired" },
                { "data": "DateFired" }  

            ]
    });

});


$('#employeeModal').on('shown.bs.modal', function () {
    $('#eMLastName').trigger('focus')
})

$('#positionModal').on('shown.bs.modal', function () {
    $('#pMPosition').trigger('focus')
})

$(document).on('click', '#employeeCancel', function () {
    resetEmployeeModal();
});

$(document).on('click', '#positionCancel', function () {
    resetPositionModal()
});

$(document).on('click', '#employeeSave', function() {

    const regCard = {
        Firstname: $('#eMFirstName').val(),
        Lastname: $('#eMLastName').val(),
        PositionId: $('#eMPosition').children(":selected").attr("id"),
        PositionTitle: $('#eMPosition').children(":selected").attr("value"), 
        Salary: parseFloat($('#eMSalary').val()),
        DateHired: $('#eMDateHired').val(),
        DateFired: $('#eMDateFired').val()
    }

    addNewEmployeeCard(regCard);
});

$(document).on('click', '#positionSave', function () {

    const positionTitle = $('#pMPosition').val();
    addNewPosition(positionTitle);

});

function resetEmployeeModal() {

    $('#eMLastName').val('');
    $('#eMFirstName').val('');
    $('#eMPosition').prop('selectedIndex', 0);
    $('#eMSalary').val('');
    $('#eMDateHired').val('');
    $('#eMDateFired').val('');
    $('#eMError').text('');
}

function resetPositionModal() {

    $('#pMPosition').val('');
    $('#pMError').text('');
}

function addNewPosition(positionTitle) {

    $.ajax({
        type: "POST",
        url: '/Home/AddPosition',
        data: {
            positionTitle: positionTitle
        }
    }).done(function (data) {

        console.log('Success');

        $('#eMPosition').append(`<option id="${data.PositionId}" value="${data.Title}">${data.Title}</option>`);

        $('#positionModal').modal('toggle');
        resetPositionModal();

    }).fail(function (err) {

        console.log('Error: ');
        console.log(err.responseJSON.error);
        $('#pMError').text(err.responseJSON.error);
    });

}

function addNewEmployeeCard(regCard) {

    $.ajax({
        type: "POST",
        url: '/Home/AddEmployeeCard',
        data: regCard
    }).done(function (data) {

        console.log('Success');

        dataTableReload();

        $('#employeeModal').modal('toggle');
        $('#eMError').text('');

        resetEmployeeModal();

    }).fail(function (err) {

        console.log('Error');
        console.log(err.responseJSON.error);
        $('#eMError').text(err.responseJSON.error);
    });
}

function dataTableReload() {

    $('#employee-table').DataTable().ajax.reload();
}