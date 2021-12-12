var urlPersoneria;
var urlPermisoOperaciones;
var validarContrasenna = false;
var tipoUsuario = 1;
function RegistroSocios() {

	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.service = 'usuarios';
	this.ctrlActions = new ControlActionsRegistro();
	this.columns = "Nombre,Cedula,Correo,Telefono,ContrassenaActual,Latitud,Longitud,PersoneriaJuridica,PermisoOperaciones,Apellidos,FechaNacimiento,Rol";




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
		console.log(tipoUsuario);
		var documentos = true;



		if (tipoUsuario == 1) {
			if (urlPersoneria == undefined) {
				$('#alertPersoneria').fadeIn();

				documentos = false;
			}

			if (urlPermisoOperaciones == undefined) {
				$('#alertOperaciones').fadeIn();
				documentos = false
			}
			if (this.Validar() && documentos) {


				console.log("se valido")
				var usuariosData = {};
				usuariosData = this.ctrlActions.GetDataFormUsuarios('frmRegistro', 3);
				console.log(tipoUsuario);
				console.log(usuariosData);
				//Hace el post al create
				this.PostToAPI(this.service + "/CrearSocio", usuariosData);


			} else {
				$('#alert').fadeTo(4000, 500).slideUp(500, function () {
					$('#alert').slideUp(500);
				});
			}
		} else {


			if (this.Validar()) {

				console.log("se valido")
				var usuariosData = {};
				usuariosData = this.ctrlActions.GetDataFormUsuarios('frmRegistro', 2);
				console.log(tipoUsuario);
				console.log(usuariosData);
				//Hace el post al create
				this.PostToAPI(this.service + "/CrearSocio", usuariosData);


			} else {
				$('#alert').fadeTo(4000, 500).slideUp(500, function () {
					$('#alert').slideUp(500);
				});
			}
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
		$("#txtFecha").val('');
		$("#txtApellidos").val('');
		$("#alert").fadeOut();
		$("#alertPersoneria").fadeOut();
		$("#alertOperaciones").fadeOut();
		urlPersoneria = undefined;
		urlPermisoOperaciones = undefined;
		this.TodoValido();
	}
	this.TodoValido = function () {
		$("#txtNombre").css("border", "1px solid #ccc");
		$("#txtCedula").css("border", "1px solid #ccc");
		$("#txtTelefono").css("border", "1px solid #ccc");
		$("#txtCorreo").css("border", "1px solid #ccc");
		$("#txtContrasenna").css("border", "1px solid #ccc");
		$("#txtContrasenna2").css("border", "1px solid #ccc");
		$("#txtFecha").css("border", "1px solid #ccc");
		$("#txtApellidos").css("border", "1px solid #ccc");
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
		var Apellido = $('#txtApellidos').val();
		var Fecha = $('#txtFecha').val();


		if (nombre == "" || !Letras.test(nombre)) {
			console.log("1")
			$("#txtNombre").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (cedula == "" || !Numeros.test(cedula)) {
			console.log("2")
			$("#txtCedula").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (telefono == "" || !Numeros.test(telefono)) {
			console.log("3")
			$("#txtTelefono").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (correo == "" || !Correos.test(correo)) {
			console.log("4")
			$("#txtCorreo").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (tipoUsuario == 2) {
			if (Apellido == "" || !Letras.test(Apellido)) {
				console.log("5")
				$("#txtApellido").css("border-color", "rgba(255, 0, 5, 0.7)");
				validar = false;
			}
			if (Fecha == "") {
				console.log("6")
				$("#txtFecha").css("border-color", "rgba(255, 0, 5, 0.7)");
				validar = false;
			}
		}

		if (contrasenna == "") {
			console.log("7")
			$("#txtContrasenna").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		}
		if (contrasenna2 == "") {
			console.log("8")
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

	$('#botonEmpresa').click(function () {
		this.ctrRegistro = new RegistroSocios();
		$('#botonEmpresa').css("border-bottom", "2px solid black")
		$('#botonSocio').css("border-bottom", "none")
		$('#divDocumentos').css("display", "block")
		$('#lbNombre').html("Nombre de la Empresa")
		$('#lbCedula').html("Cedula Jurídica")
		$('#divApellido').css("display", "none")
		$('#divFecha').css("display", "none")
		this.ctrRegistro.Limpiar();
		tipoUsuario = 1;

	});

	$('#botonSocio').click(function () {
		this.ctrRegistro = new RegistroSocios();
		$('#botonSocio').css("border-bottom", "2px solid black");
		$('#botonEmpresa').css("border-bottom", "none");
		$('#divDocumentos').css("display", "none");
		$('#lbNombre').html("Nombre");
		$('#lbCedula').html("Cedula");
		$('#divApellido').css("display", "block");
		$('#divFecha').css("display", "block");
		this.ctrRegistro.Limpiar();
		tipoUsuario = 2;
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