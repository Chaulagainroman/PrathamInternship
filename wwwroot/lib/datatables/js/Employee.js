

let oTable = $('#employee').DataTable({
    paging: false,
    "order": []
});

$('#mysearch').keyup(function () {
    oTable.search($(this).val()).draw();
})