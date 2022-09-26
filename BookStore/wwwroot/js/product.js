var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-100 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-info mx-2">
                                <i class="bi bi-pencil-square"></i> &nbsp; Edit
                            </a>
                            <a onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-warning mx-2">
                                <i class="bi bi-trash"></i> &nbsp; Delete
                            </a>
                        </div>
                    `;
                }
            }
        ]
    });
}

function Delete(endPointUrl) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: endPointUrl,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        Swal.fire(
                            'Deleted!',
                            'Product has been deleted.',
                            'success'
                        )
                    }
                    else {
                        Swal.fire(
                            'Error!',
                            'Could not delete. Try again later!',
                            'error'
                        )
                    }
                }
            })
        }
    })
}