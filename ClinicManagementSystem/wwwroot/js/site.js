//Confirm Delete

Notiflix.Notify.init({
    position: 'right-bottom',
    distance: '15px',
    timeout: 2500,
    cssAnimationStyle: 'fade',
});

function confirmDeleteVisit(e) {
    e.preventDefault();
    const form = e.target;
    const url = form.action;
    Notiflix.Confirm.show(
        'Confirm Deletion',
        'Are you sure you want to delete this visit?',
        'Yes, Delete',
        'Cancel',
        function okCb() {
            $.post(url, $(form).serialize())
                .done(function () {
                    Notiflix.Notify.success('Visit deleted successfully');
                    const row = $(form).closest('tr');
                    row.fadeOut(500, function () {
                        row.remove();
                        if ($('#visitsTable tbody tr').length === 0) {
                            const currentPage = parseInt('@ViewBag.CurrentPage');
                            const targetPage = currentPage > 1 ? currentPage - 1 : 1;
                            setTimeout(function () {
                                window.location.href = '@Url.Action("Index", "Visit")' + '?page=' + targetPage + '&searchTerm=@ViewBag.SearchTerm';
                            }, 1000);
                        }
                    });
                })
                .fail(function () {
                    Notiflix.Notify.failure('Error deleting visit');
                });
        },
        function cancelCb() {
            Notiflix.Notify.info('Deletion canceled');
        },
        {
            width: '320px',
            borderRadius: '8px',
            titleColor: '#e74c3c',
            okButtonBackground: '#e74c3c',
            cssAnimationStyle: 'fade',
        }
    );

    return false;
}


/* ==== Confirm Delete Alert Patient ==== */
function confirmDeletePatient(e) {
    e.preventDefault();
    const form = e.target;
    const url = form.action;

    Notiflix.Confirm.show(
        'Confirm Deletion',
        'Are you sure you want to delete this patient?',
        'Yes, Delete',
        'Cancel',
        function okCb() {
            $.post(url, $(form).serialize())
                .done(function () {
                    Notiflix.Notify.success('Patient deleted successfully');
                    const row = $(form).closest('tr');
                    row.fadeOut(500, function () {
                        row.remove();
                    });
                })
                .fail(function () {
                    Notiflix.Notify.failure('Error deleting patient');
                });
        },
        function cancelCb() {
            Notiflix.Notify.info('Deletion canceled');
        },
        {
            width: '320px',
            borderRadius: '8px',
            titleColor: '#e74c3c',
            okButtonBackground: '#e74c3c',
            cssAnimationStyle: 'fade',
        }
    );

    return false;
}


