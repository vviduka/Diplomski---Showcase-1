$(document).ready(function () {
    $('.group').hide();
    $('#filterParam').change(function () {
        $('.group').hide();
        $('#' + $(this).val()).show();
    })
});