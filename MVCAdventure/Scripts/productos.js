var currentSalesPerson;
var currentProduct;
var auxSalesPerson;
var auxProduct;


$(document).ready(function () {
    $("#proveedores").click(function (e) {
        var proveedor = e.target;
        currentSalesPerson = proveedor.id;
        $("#" + auxSalesPerson).css("background-color", "#FFFFFF");
        $("#" + currentSalesPerson).css("background-color", "#58ACFA");
        auxSalesPerson = currentSalesPerson;
        var url = document.location.origin + '/Proveedor/GetProductos?provID=' + proveedor.id;
        $.ajax(url).done(function (data) {
            $("#productos").empty();
            $("#productos").html(data);
        });
    });
});


$(document).ready(function () {
    $("#productos").click(function (e) {
        var producto = e.target;
        currentProduct = producto.id;
        $("#" + auxProduct).css("background-color", "#FFFFFF");
        $("#" + currentProduct).css("background-color", "#58ACFA");
        auxProduct = currentProduct;
    });
});


$(document).ready(function () {
    $("#modificarProducto").click(function (e) {
        var url = document.location.origin + '/Proveedor/ModificarProducto?ProductID=' + currentProduct;
        window.location = url;
    });
});


$(document).ready(function () {
    $("#modificaProveedor").click(function (e) {
        var url = document.location.origin + '/Proveedor/ModificarProveedor?negocio=' + currentSalesPerson;
        window.location = url;
    });
});