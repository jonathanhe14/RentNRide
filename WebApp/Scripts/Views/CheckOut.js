dataReserva = {};
dataM = {};
dataS = {};
var fecha = new Date;
var Total;
var Saldo;
var Estado;
var KilometrosExtra;
var contador;
function checkOut() {
    this.URL_API = "http://localhost:52125/api/";
    this.ReservaService = 'reserva';
    this.usersService ='usuarios'
    this.GetUrlApiService = function (service) {
        return this.URL_API + service;
    }
    this.GetToApiReserva = function (service, callbackFunction) {

        var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
            dataReserva = response.Data;
            //data = JSON.stringify(response.Data);
            console.log(dataReserva);
            $("#horaInicio").val(dataReserva["HoraInicio"])
            $("#horaFinal").val(dataReserva["HoraFin"])
            this.ctr = new checkOut();
            this.ctr.CalcularMonto();

        });
    }
    this.Reserva = function () {
        this.GetToApiReserva(this.ReservaService + "/GetReservasPorId?Correo=" +localStorage.getItem("Correo"))

    }
    this.CalcularMonto = function () {

        if ($("#selectEstado").val() == 'SinDannos') {
            Total = 0;
        } else if ($("#selectEstado").val() == 'AlgunDanno') {
            Total = 10000;
        } else if ($("#selectEstado").val() == 'MuchoDanno') {
            Total = 20000;
        } else {
            Total = 0;
        }
        console.log(dataReserva["KmFinal"] - $("#txtKilometrosFinal").val());
        if (($("#txtKilometrosFinal").val() - dataReserva["KmInicial"]) >= 1000) {
           
            Total = Total + 5000;
          
        }
        if (($("#txtKilometrosFinal").val() - dataReserva["KmInicial"]) >= 1000) {
            KilometrosExtra = ($("#txtKilometrosFinal").val() - dataReserva["KmInicial"]) - 1000;
            
        } else {
            KilometrosExtra = 0;
        }
        Estado = $("#selectEstado").val();
        $("#Total").html(Total + "₡");
    }

    this.GetToApiMonedero = function (service, callbackFunction) {

        var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
            dataM = response.Data;
            //data = JSON.stringify(response.Data);
            console.log(dataM);
            Saldo = dataM["Saldo"];
            $("#saldo").html(dataM["Saldo"] + "₡");
        });
    }
    this.GetToApiMonederoSocio = function (service, callbackFunction) {

        var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
            dataS = response.Data;
            //data = JSON.stringify(response.Data);

        });
    }
    this.PutToAPIReserva = function (data) {
        var jqxhr = $.post('http://localhost:52125/api/reserva/ActualizarReserva', data, function (response) {
            window.location.href = "https://localhost:44383/Home/PerfilSocio";
        })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
            })
    };
    this.PutToAPI = function (data) {
        var jqxhr = $.post('http://localhost:52125/api/usuarios/ActualizarMonedero', data, function (response) {

        })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
            })
    };
    this.PutToAPISocio = function (data) {
        var jqxhr = $.post('http://localhost:52125/api/usuarios/ActualizarMonedero', data, function (response) {

        })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
            })
    };

    this.Monedero = function () {
        this.GetToApiMonedero(this.usersService + "/GetMonedero?correo=" + localStorage.getItem('Correo'))

    }
    this.MonederoSocio = function () {
        console.log(this.usersService + "/GetMonedero?correo=" + dataReserva["Socio"]);
        this.GetToApiMonederoSocio(this.usersService + "/GetMonedero?correo=" + dataReserva["Socio"])

    }
    this.Validar = function () {
        this.MonederoSocio();
        if (Total <= dataM["Saldo"]) {
            $('#alertErr').fadeOut();
            $('#modalCalificar').modal('show');
        } else {
            $('#alertErr').fadeIn();
        }
       
    }
    this.ActualizarReserva = function () {
        if ($("#txtKilometrosFinal").val() != "") {
            $('#alertErr').fadeOut();
            dataReserva["CalifSocio"] = contador;
            dataReserva["KmExcedido"] = KilometrosExtra
            dataReserva["KmFinal"] = $("#txtKilometrosFinal").val();
            dataReserva["MalEstado"] = Estado;
            dataReserva["Solicitud"] = "FINALIZADA"
            this.PutToAPIReserva(dataReserva);
            
        } else {
            $('#alertErr').fadeIn();
        }
    }
    this.ActualizarMonedero = function () {
        console.log("Actualizar Monedero" + dataS["Saldo"]);
        console.log(Total +"Total")
        dataS["Saldo"] = dataS["Saldo"] + Total;
        dataM["Saldo"] = dataM["Saldo"] - Total;
        this.PutToAPI(dataM);
        this.PutToAPISocio(dataS);
    }
}

function Calificar(item) {

    contador = item.id[0];
    let nombre = item.id.substring(1);

    for (let i = 0; i < 5; i++) {
        if (i < contador) {
            document.getElementById((i + 1) + nombre).style.color = "orange";

        } else {
            document.getElementById((i + 1) + nombre).style.color = "black";
        }
    }
}

$(document).ready(function () {

    this.ctrCheck = new checkOut();
    this.ctrCheck.Reserva();
    this.ctrCheck.Monedero();
   
    
    $("#Enviar").click(function () {
        this.ctrCheck = new checkOut();
        this.ctrCheck.ActualizarMonedero();
        
        this.ctrCheck.ActualizarReserva();

    });
    $("#txtKilometrosFinal").keyup(function () {
        this.ctrCheck2 = new checkOut();
        this.ctrCheck2.CalcularMonto();
    });
    $("#txtKilometrosFinal").click(function () {
        this.ctrCheck2 = new checkOut();
        this.ctrCheck2.CalcularMonto();
    });

    $("#cambiarPagina").click(function () {
        window.location.href = "https://localhost:44383/Home/Monedero";
    });
});