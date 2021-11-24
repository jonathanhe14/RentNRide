function RegistroUsuariosFinales() {


	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}
	
	this.service = 'usuarios';
	this.ctrlActions = new ControlActionsRegistro();
	this.columns = "Nombre,Apellidos,Cedula,Correo,FechaNacimiento,Telefono,ContrassenaActual,Latitud,Longitud,OTP,OTPSMS";


	this.Create = function () {
		
		if (this.Validar()) {
			
			console.log("Se valido")

			var usuariosData = {};
			usuariosData = this.ctrlActions.GetDataFormUsuarios('frmRegistro');

			//Hace el post al create
			this.PostToAPI(this.service + "/CrearUsuario", usuariosData);
			
			
			
			
		} else {
			this.MostrarAlerta();
		}
	}





	this.Limpiar = function () {
		$("#txtNombre").val('');
		$("#txtApellido").val('');
		$("#txtCedula").val('');
		$("#txtTelefono").val('');
		$("#txtCorreo").val('');
		$("#txtContrasenna").val('');
		$("#txtContrasenna2").val('');
		$("#txtFechaNacimiento").val('');
		$("#alert").fadeOut();
		TodoValido();
	}
	this.TodoValido = function () {
		$("#txtNombre").css("border", "1px solid #ccc");
		$("#txtApellido").css("border", "1px solid #ccc");
		$("#txtCedula").css("border", "1px solid #ccc");
		$("#txtTelefono").css("border", "1px solid #ccc");
		$("#txtCorreo").css("border", "1px solid #ccc");
		$("#txtContrasenna").css("border", "1px solid #ccc");
		$("#txtContrasenna2").css("border", "1px solid #ccc");
		$("#txtFechaNacimiento").css("border", "1px solid #ccc");
	}


	this.BindFields = function (data) {
		this.ctrlActions.BindFields('frmRegistro', data);
	}

	this.MostrarAlerta = function () {
		$('#alert').fadeTo(4000, 500).slideUp(500, function () {
			$('#alert').slideUp(500);
		});
	}
	this.Validar = function () {
		var validar = true;
		let Letras = /^[A-Z]+$/i;
		let Numeros = /^\d+$/;
		let Correos = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

		var nombre = $("#txtNombre").val();
		var apellido = $("#txtApellido").val();
		var cedula = $("#txtCedula").val();
		var telefono = $("#txtTelefono").val();
		var correo = $("#txtCorreo").val();
		var fecha = $("#txtFechaNacimiento").val();
		if (nombre == "" || !Letras.test(nombre)) {

			console.log("se ejecuto la prueba")
			$("#txtNombre").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (apellido == "" || !Letras.test(apellido)) {

			$("#txtApellido").css("border-color", "rgba(255, 0, 5, 0.7)");
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
		} if (fecha == "") {

			$("#txtFechaNacimiento").css("border-color", "rgba(255, 0, 5, 0.7)");
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
			var RegistroUsuariosFinaless = new RegistroUsuariosFinales();
			RegistroUsuariosFinaless.ShowMessage('I');

			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var RegistroUsuariosFinaless = new RegistroUsuariosFinales();
				RegistroUsuariosFinaless.ShowMessage('E');
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

	$('#alert').hide();
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



