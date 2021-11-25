function PerfilSocio() {

	this.tblUsuariosId = 'tblUsuarios';
	this.usersService = 'usuarios';
	this.ctrlActions = new ControlActionsAdmin();


	this.RetrieveAll = function () {
		//U?correo=" + localStorage.getItem("Correo")
		this.ctrlActions.FillTable(this.usersService + "/GetU?correo=" + localStorage.getItem("Correo"), this.tblUsuariosId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.usersService + "/GetU?correo=" + localStorage.getItem("Correo"), this.tblUsuariosId, true);
	}

	this.Desactivar = function () {

	}

	this.BindFieldsUsers = function (data) {
		this.ctrlActions.BindFields('frmUsers', data);
	}


}

//ON DOCUMENT READY
$(document).ready(function () {
	var usuarios = new PerfilSocio();
	usuarios.RetrieveAll();

});
