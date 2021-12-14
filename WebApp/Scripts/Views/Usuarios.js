function Usuarios() {

	this.tblUsuariosId = 'tblUsuarios';
	this.usersService = 'usuarios';
	this.ctrlActions = new ControlActionsAdmin();


	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.usersService + "/Get", this.tblUsuariosId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.usersService + "/Get", this.tblUsuariosId, true);
	}

	this.Desactivar = function () {

    }

	this.BindFieldsUsers = function (data) {
		this.ctrlActions.BindFields('frmUsers', data);
	}

	this.Limpiar = function () {
		$("#txtCedula").val('');
		$("#txtNombre").val('');
		$("#txtApellidos").val('');
		$("#txtCorreo").val('');
		$("#txtEdad").val('');
		$("#txtTelefono").val('');
		$("#txtEstado").val('');
		$("#alert").fadeOut();


		$("#tblUsuarios > tbody > tr").each(function () {
			$(this).removeClass("seleccionado");
		})

	}
}

//ON DOCUMENT READY
$(document).ready(function () {
	var usuarios = new Usuarios();
	usuarios.RetrieveAll();

});

