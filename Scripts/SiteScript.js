
$('#employeeModal').on('shown.bs.modal', function () {
    //$('#myInput').trigger('focus')
})


$('#positionModal').on('shown.bs.modal', function () {
    //$('#myInput').trigger('focus')
})

$(document).on('click', '#employeeSave', function() {
    console.log('employeeSave');
    $('#employeeModal').modal('toggle');
});


$(document).on('click', '#positionSave', function () {
    console.log('positionSave');
    $('#positionModal').modal('toggle');
});