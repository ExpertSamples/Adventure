$(document).ready(function () {
    $("#pedidos").click(function (e) {
        var pedido = e.target;
        $("#numPedido").val(pedido.id);
        //var url = document.location.origin + '/Proveedor/GetProductos?provID=' + proveedor.id;
        //$.ajax(url).done(function (data) {
        //    $("#productos").empty();
        //    $("#productos").html(data);
        //});
    });
});