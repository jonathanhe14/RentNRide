dataU = {};
function PerfilSocio() {
	this.URL_API = "http://localhost:52125/api/";
	this.usersService = 'usuarios';
	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

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
	this.GetToApiUsuarioEstado = function (service, callbackFunction) {

		var jqxhr = $.get("http://localhost:52125/api/" + "usuarios" + service, function (response) {
			console.log("http://localhost:52125/api/" + "usuarios" + service)
			dataU = response.Data;
			var EstadoMembresia = dataU[0].Estado;
			var EstadoOTP = dataU[0].Comprobacion;
			if (EstadoMembresia == "PENDIENTE") {

				if (EstadoOTP == "FALSE") {
					window.location.href = "https://localhost:44383/Home/Verificacion";

				}

			}

			//data = JSON.stringify(response.Data);

		});
	}
	this.Validar = function () {
		this.GetToApiUsuarioEstado("/GetU?correo=" + localStorage.getItem("Correo"));




	}

}

//ON DOCUMENT READY
$(document).ready(function () {
	this.usuarios = new PerfilSocio();
	this.usuarios.RetrieveAll();
	this.usuarios.Validar();


});
