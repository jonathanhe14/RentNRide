function Solicitudes() {

	this.tblUsuariosId = 'tblUsuarios';
	this.usersService = 'usuarios';
	this.ctrlActions = new ControlActions();


	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.usersService + "/GetSolicitudes", this.tblUsuariosId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.usersService + "/GetSolicitudes", this.tblUsuariosId, true);
	}

	this.Rechazar = function () {

	}

	this.Aceptar = function () {

    }

	this.BindFieldsUsers = function (data) {
		this.ctrlActions.BindFields('frmUsers', data);
	}


}

//ON DOCUMENT READY
$(document).ready(function () {
	var solicitudes = new Solicitudes();
	solicitudes.RetrieveAll();

});

