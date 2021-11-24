var urlPersoneria;
var urlPermisoOperaciones;
var validarContrasenna = false;
function RegistroSocios() {

	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.service = 'usuarios';
	this.ctrlActions = new ControlActionsRegistro();
	this.columns = "Nombre,Cedula,Correo,Telefono,ContrassenaActual,Latitud,Longitud,PersoneriaJuridica,PermisoOperaciones";




	var PeroneriaJuridica = cloudinary.createUploadWidget({
		cloudName: 'cenfotec2021',
		uploadPreset: 'Cenfo_preset'
	}, (error, result) => {
		if (!error && result && result.event === "success") {
			urlPersoneria = result.info.secure_url;
			this.ctrlActions.SubirPersoneria(urlPersoneria);
			$('#alertVerificarPersoneria').fadeIn();
		}
	}
	);

	var PermisoOperaciones = cloudinary.createUploadWidget({
		cloudName: 'cenfotec2021',
		uploadPreset: 'Cenfo_preset'
	}, (error, result) => {
		if (!error && result && result.event === "success") {
			urlPermisoOperaciones = result.info.secure_url;
			this.ctrlActions.SubirOperaciones(urlPermisoOperaciones);
			$('#alertVerificarOperaciones').fadeIn();
		}
	}
	);

	this.Create = function () {
		var documentos = true;
		
		if (urlPersoneria == undefined) {
			$('#alertPersoneria').fadeIn();
			
			documentos = false;
		}
		
		if (urlPermisoOperaciones == undefined) {
			$('#alertOperaciones').fadeTo(4000, 500).slideUp(500, function () {
				$('#alertOperaciones').slideUp(500);
			});
			docuementos = false
		}
		if (this.Validar() && documentos == true) {


			console.log("se valido")
			var usuariosData = {};
			usuariosData = this.ctrlActions.GetDataFormUsuarios('frmRegistro');

			//Hace el post al create
			this.PostToAPI(this.service + "/CrearSocio", usuariosData);


		} else {
			$('#alert').fadeTo(4000, 500).slideUp(500, function () {
				$('#alert').slideUp(500);
			});
		}
	}


	this.SubirPersoneria = function () {
		PeroneriaJuridica.open();
	}
	this.SubirOperaciones = function () {
		PermisoOperaciones.open();
	}
	this.BindFields = function (data) {
		this.ctrlActions.BindFields('frmRegistro', data);
	}

	this.Limpiar = function () {
		$("#txtNombre").val('');
		$("#txtCedula").val('');
		$("#txtTelefono").val('');
		$("#txtCorreo").val('');
		$("#txtContrasenna").val('');
		$("#txtContrasenna2").val('');
		$("#alert").fadeOut();
		$("#alertPersoneria").fadeOut();
		$("#alertOperaciones").fadeOut();
		urlPersoneria = undefined;
		urlPermisoOperaciones = undefined;
		TodoValido();
	}
	this.TodoValido = function () {
		$("#txtNombre").css("border", "1px solid #ccc");
		$("#txtCedula").css("border", "1px solid #ccc");
		$("#txtTelefono").css("border", "1px solid #ccc");
		$("#txtCorreo").css("border", "1px solid #ccc");
		$("#txtContrasenna").css("border", "1px solid #ccc");
		$("#txtContrasenna2").css("border", "1px solid #ccc");
	}

	this.Validar = function () {
		var validar = true;
		let Letras = /^[A-Z]+$/i;
		let Numeros = /^\d+$/;
		let Correos = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;




		var nombre = $("#txtNombre").val();
		var cedula = $("#txtCedula").val();
		var telefono = $("#txtTelefono").val();
		var correo = $("#txtCorreo").val();
		var contrasenna = $("#txtContrasenna").val();
		var contrasenna2 = $("#txtContrasenna2").val();
		if (nombre == "" || !Letras.test(nombre)) {

			$("#txtNombre").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (cedula == "" || !Numeros.test(cedula)) {

			$("#txtCedula").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (telefono == "" || !Numeros.test(telefono)) {

			$("#txtTelefono").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (correo == "" || !Correos.test(correo)) {

			$("#txtCorreo").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (contrasenna == "") {

			$("#txtContrasenna").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (contrasenna2 == "") {

			$("#txtContrasenna").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (!validarContrasenna) {
			validar = false;
		}




		return validar;
	}



	this.ShowMessage = function (type) {
		if (type == 'E') {


			$('#modalErr').modal('show');
			this.Limpiar();
		} else if (type == 'I') {
			$('#modalSuccess').modal('show');
		}

	};


	this.PostToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			var RegistroSocioss = new RegistroSocios();
			RegistroSocioss.ShowMessage('I');

			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var RegistroSocioss = new RegistroSocios();
				RegistroSocioss.ShowMessage('E');
			})
	};
}



$(document).ready(function () {
	$('#cambioPagina').click(function () {
		window.location.href = "https://localhost:44383/Home/Verificacion";
	});

	var Contrasenna = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*.,])(?=.{8,})");

	$('#txtContrasenna').keyup(function () {
		if (!Contrasenna.test($('#txtContrasenna').val())) {

			$('#ContrasennaSegura').fadeIn();
			validarContrasenna = false;
		} else {
			console.log("Se tuvo que quitar la alerta")
			$('#ContrasennaSegura').fadeOut();
			$("#txtContrasenna").css("border", "1px solid #ccc");
			validarContrasenna = true;
		}
	});


	$('#txtContrasenna2').keyup(function () {
		var pass1 = $('#txtContrasenna').val();
		var pass2 = $('#txtContrasenna2').val();
		if (pass1 == pass2) {
			$("#txtContrasenna2").css("border-color", "rgba(0, 255, 76, 0.8)");

		} else {
			$("#txtContrasenna2").css("border-color", "rgba(255, 0, 5, 0.7)");

		}
		if (pass2 == "") {
			$("#txtContrasenna2").css("border", "1px solid #ccc");
		}
	});
});