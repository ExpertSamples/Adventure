var currentSalesPerson;
var currentProduct;
var auxSalesPerson;
var auxProduct;


$(document).ready(function () {
    $("#proveedores").click(function (e) {
        var lista = document.getElementById(proveedores);
        var proveedor = e.target;
        currentSalesPerson = proveedor.id;
        $("#" + auxSalesPerson).css("background-color", "#FFFFFF");
        $("#" + currentSalesPerson).css("background-color", "#58ACFA");
        auxSalesPerson = currentSalesPerson;
        var url = document.location.origin + '/Proveedor/GetProductos?id=' + proveedor.id;
        $.ajax(url).done(function (data) {
            $("#productos").empty();
            $("#productos").html(data);
        });
    });
});

//CAMBIAR CLICK BOTON
$(document).ready(function () {
    $("#productos").click(function (e) {
        var producto = e.target;
        var url = document.location.origin + '/Proveedor/ModificarProducto?ProductID=' + producto.id;
        window.location = url;
    });
});

$(document).ready(function () {
    $("#productos").click(function (e) {
        var proveedor = e.target;
        var url = document.location.origin + '/Proveedor/ModificarProducto?ProductID=' + proveedor.id;
        window.location = url;
    });
});