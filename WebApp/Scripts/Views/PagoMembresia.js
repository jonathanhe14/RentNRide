data = {};
dataU = {};
dataMembresia ={};
var urlComprobante;
var Total;
var Estado = 0;
var documentoSubido = false;
$('#boton-sinpe').click(function () {
    if (Estado == 0) {
        Estado = 1;
        $('#div-sinpe').css('display','block');
    } else {
        Estado = 0;
        $('#div-sinpe').css('display','none');
    }
    
    
});

paypal.Buttons({
    style: {
        color: "blue",
        shape: "pill"
    },
    createOrder: function (data, actions) {
        return actions.order.create({
            purchase_units: [{


                amount: {
                    value: Total
                }
            }]
        });
    },
    onApprove: function (data, actions) {
        return actions.order.capture().then(function (details) {
            $('#modal-paypal').modal('show');
        })
    }

}).render('#paypal-payment-button');

function PagarMembresia() {

    this.URL_API = "http://localhost:52125/api/";
    this.usersService = 'usuarios';
    this.GetUrlApiService = function (service) {
        return this.URL_API + service;
    }
    this.Membresia = function () {
        this.GetToApiMembresia(this.usersService + "/GetMembresiaUsuario?correo=jonaherrera90@hotmail.com")
    }

    this.Usuario = function () {
        this.GetToApiUsuario(this.usersService + "/GetU?correo=jonaherrera90@hotmail.com")
    }

    this.GetToApiMembresia = function (service, callbackFunction) {
        
        var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
            data = response.Data;
            //data = JSON.stringify(response.Data);
            console.log(data);

            $('#Nombre').html("Nombre: " + data["Nombre"])
            $('#Precio').html("Precio: " + data["MontoMensual"] + "₡")
            $('#Total').html("Total : " + (parseInt(data["MontoMensual"]) * 1.13).toFixed(2) + "₡")
            Total = (parseInt(data["MontoMensual"]) * 1.13).toFixed(2);
            
            console.log(Total)
        });
    }
    this.GetToApiUsuario = function (service, callbackFunction) {

        var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
            dataU = response.Data;
            //data = JSON.stringify(response.Data);
            console.log(dataU);

            $('#NombreU').html("Nombre: " + dataU[0].Nombre)
            $('#Cedula').html("Cedula: " + dataU[0].Cedula)
            $('#Correo').html("Correo : " + dataU[0].Correo)
            $('#Telefono').html("Telefono : " + dataU[0].Telefono)
            
           
           

        });
    }
    this.PutToAPI = function (service, data) {
        var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {

        })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
            })
    };
    this.Enviar = function () {
        this.ctrPagar = new PagarMembresia();
        this.ctrPagar.PutToAPI("usuarios" + "/ComprobanteMembresia", dataMembresia);
    }
}

$(document).ready(function () {

    var ComprobanteSinpe = cloudinary.createUploadWidget({
        cloudName: 'cenfotec2021',
        uploadPreset: 'Cenfo_preset'
    }, (error, result) => {
        if (!error && result && result.event === "success") {
            urlComprobante = result.info.secure_url;
            $("#alert").fadeIn();
            dataMembresia["IdMembresia"] = data["Id"]
            dataMembresia["Estado"] = "ACTIVO";
            dataMembresia["IdUsuario"] = dataU[0].Id_usuario;
            dataMembresia["Comprobante"] = urlComprobante;
            documentoSubido = true;
           
            
        }
    }
    );
    this.membresia = new PagarMembresia();
    this.membresia.Membresia();
    this.membresia.Usuario();

    $("#Comprobante").click(function () {
        ComprobanteSinpe.open();
        $("#alertError").fadeOut();
        

    });
    $("#enviar").click(function () {
        if (documentoSubido) {
            this.ctrPagarMembresias = new PagarMembresia();
            this.ctrPagarMembresias.Enviar();
        } else {
            $("#alertError").fadeIn();
        }



    });

});


