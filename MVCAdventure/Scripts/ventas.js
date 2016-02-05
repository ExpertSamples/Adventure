$(document).ready(function () {
    $("#pedidos").click(function (e) {
        var pedido = e.target;
        $("#numPedido").val(pedido.id);
    });
});

$(document).ready(function () {
    $("#filtrar").click(function (e) {       
        var nombre = $("#nombre").val();
        var apellido = $("#apellido").val();
        var fechaDesde = $("#fechaDesde").val();
        var fechaHasta = $("#fechaHasta").val();
        var url = document.location.origin + '/Ventas/FiltroPedidos?name=' + nombre + '&lastName=' + apellido + '&inicio=' + fechaDesde + '&final=' + fechaHasta;
        $.ajax(url).done(function (data) {
            $("#pedidos").empty();
            $("#pedidos").html(data);
        });

    });
});