function RegistroUsuariosFinales() {




	this.service = 'usuarios';
	this.ctrlActions = new ControlActions();
	this.columns = "Nombre,Apellidos,Cedula,Correo,FechaNacimiento,Telefono,ContrassenaActual,Latitud,Longitud";


	this.Create = function () {
		this.TodoValido();
		if (this.Validar()) {

			console.log("Se valido")
			
			var usuariosData = {};
			usuariosData = this.ctrlActions.GetDataForm('frmRegistro');

			//Hace el post al create
			this.ctrlActions.PostToAPI(this.service + "/CrearUsuario", usuariosData);
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

}
$(document).ready(function () {
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


