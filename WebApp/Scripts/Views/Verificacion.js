var data = {};
function Verificacion() {

	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

    this.service = 'usuarios';
	this.ctrlActions = new ControlActionsRegistro();
	this.columns = "OTP,OTPSMS,Correo,Telefono";




    this.Enviar = function () {
        var codigos = {};
        codigos = this.ctrlActions.getDataFormVerificacion('formEnvio');

       this.PostToAPI(this.service + "/ComprobarOTP", codigos);


    }


	this.Reenviar = function () {
		var data = {}
		data["Telefono"] = localStorage.getItem("Telefono");
		data["Correo"] = localStorage.getItem("Correo");
		this.PostToAPI2(this.service + "/ReenviarCodigos", data);
    }
	this.ShowMessage = function (type) {
		if (type == 'E') {
			console.log("fallo");
			$('#txtOTP').css("border-color", "rgba(255, 0, 5, 0.7)");
			$('#txtOTPSMS').css("border-color", "rgba(255, 0, 5, 0.7)");
			$('#alert').fadeIn();
		} else if (type == 'I') {
		
			window.location.href = "https://localhost:44383/Home/InicioSesion";
			window.localStorage.clear();
		}
		
	};

	this.PostToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			var ctrVerificar = new Verificacion();
			ctrVerificar.ShowMessage('I');

			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var ctrVerificar = new Verificacion();
				ctrVerificar.ShowMessage('E');

			})
	};

	this.ShowMessage2 = function (type) {
		if (type == 'E') {

			$('#alertReenviarFallo').fadeIn();
		} else if (type == 'I') {
			$('#alertReenviar').fadeIn();
		}

	};

	this.PostToAPI2 = function (service, data, callBackFunction) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			var ctrVerificar = new Verificacion();
			ctrVerificar.ShowMessage2('I');

			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var ctrVerificar = new Verificacion();
				ctrVerificar.ShowMessage2('E');

			})
	};
}





$(document).ready(function () {
	
	$('#ReenviarA').click(function () {
		this.ctrVerificar = new Verificacion();
		this.ctrVerificar.Reenviar();
	});
});