function Usuarios() {

	this.tblUsuariosId = 'tblUsuarios';
	this.usersService = 'usuarios';
	this.ctrlActions = new ControlActions();


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

	
}

//ON DOCUMENT READY
$(document).ready(function () {
	var usuarios = new Usuarios();
	usuarios.RetrieveAll();

});

