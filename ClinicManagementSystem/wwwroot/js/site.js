let formToSubmit = null;

function confirmDelete(event) {
    event.preventDefault(); 
    formToSubmit = event.target;

    const modal = new bootstrap.Modal(document.getElementById('confirmDeleteModal'));
    modal.show();

    document.getElementById('confirmDeleteBtn').onclick = function () {
        modal.hide();
        formToSubmit.submit();
    };
}


