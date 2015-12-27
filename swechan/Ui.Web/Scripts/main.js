$(document).ready(function () {
    $("#forget #Email").typeahead({
        remote: "/User/LookupUser?mail=%QUERY"
    });
});