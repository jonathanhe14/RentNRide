var data = {};
function Verificacion() {
	let tiempo = 900;

	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.service = 'usuarios';
	this.ctrlActions = new ControlActionsRegistro();
	this.columns = "OTP,OTPSMS,Correo";




	this.Enviar = function () {
		var codigos = {};
		$('#alert').fadeOut();
		codigos = this.ctrlActions.getDataFormVerificacion('formEnvio');
		if (this.Validar()) {
			this.PostToAPI(this.service + "/ComprobarOTP", codigos, 1);
		} else {
			$('#alertError').fadeIn();
		}



	}
	this.EnviarCodigos = function () {
		var data = {}
		data["Correo"] = localStorage.getItem("Correo");
		$('#alertaTiempo').fadeOut();
		this.Limpiar();
		this.Temporizador();
		this.PostToAPI(this.service + "/EnvioCodigos", data, 0);
	}

	this.Temporizador = function () {
		let intervalo = setInterval(function () {
			const temporizador = document.getElementById('temporizador');
			const minutos = Math.floor(tiempo / 60);
			let segundos = tiempo % 60;

			segundos = segundos < 10 ? '0' + segundos : segundos;
			temporizador.innerHTML = `${minutos}:${segundos}`;
			tiempo--;
			console.log(tiempo)
			if (tiempo < 60) {
				$('#temporizador').css("color", "red")
			};
			if (tiempo < 0) {
				$('#alertaTiempo').fadeIn();
				$('#temporizador').val("00:00");
				clearInterval(intervalo);
			};
		}, 1000)
	}


	this.ShowMessage = function (type) {
		if (type == 'E') {
			$('#alert').fadeIn();
		} else if (type == 'I') {

			window.location.href = "https://localhost:44383/Home/InicioSesion";
			window.localStorage.clear();
		}

	};

	this.PostToAPI = function (service, data, type) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			if (type == 1) {
				var ctrVerificar = new Verificacion();
				ctrVerificar.ShowMessage('I');
			}
		})
			.fail(function (response) {
				var ctrVerificar = new Verificacion();
				ctrVerificar.ShowMessage('E');

			})
	};

	this.Validar = function () {
		var validar = true;
		otp = $('#txtOTP').val();
		otpsms = $('#txtOTPSMS').val()
		let Numeros = /^\d+$/;
		if (otp == "" || !Numeros.test(otp)) {
			$('#txtOTP').css("border-color", "rgba(255, 0, 5, 0.7)")
			validar = false;
		}
		if (otpsms == "" || !Numeros.test(otpsms)) {
			$('#txtOTPSMS').css("border-color", "rgba(255, 0, 5, 0.7)")
			validar = false;
		}
		return validar;
	}
	this.Limpiar = function () {
		$("#txtOTP").val('');
		$("#txtOTPSMS").val('');
		$("#txtOTP").css("border", "1px solid #ccc");
		$("#txtOTPSMS").css("border", "1px solid #ccc");
		$('#alertError').fadeOut();

	}

}





$(document).ready(function () {
	this.ctrVerificar = new Verificacion();
	this.ctrVerificar.Temporizador();

	$('#txtOTP').keyup(function () {
		$("#txtOTP").css("border", "1px solid #ccc");
		$("#txtOTPSMS").css("border", "1px solid #ccc");
		$('#alertError').fadeOut();
	});


	$('#ReenviarA').click(function () {
		this.ctrVerificar = new Verificacion();
		this.ctrVerificar.EnviarCodigos();
	});
});