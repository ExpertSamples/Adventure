
$(document).ready(function () {
    $("proveedores").click(function () {
        var proveedor = $(this).attr('id');
        var url = document.location.origin + '/Proveedor/GetProductos?id='+proveedor;
        $.ajax(url).done(function (data) {
            $("#productos").empty();
            $("#productos").html(data);
        });
    });
});