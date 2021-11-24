function Solicitudes() {

	this.tblUsuariosId = 'tblUsuarios';
	this.usersService = 'usuarios';
	this.adminService = 'administrador';
	this.tblMembresiasId = 'tblMembresias'
	this.ctrlActions = new ControlActions();


	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.usersService + "/GetSolicitudes", this.tblUsuariosId, false);
		this.ctrlActions.FillTable(this.adminService + "/GetMembresias", this.tblMembresiasId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.usersService + "/GetSolicitudes", this.tblUsuariosId, true);
		this.ctrlActions.FillTable(this.adminService + "/GetMembresias", this.tblMembresiasId, true);
	}

	this.Rechazar = function () {
		var usuario = {};
		usuario = this.ctrlActions.GetDataForm('frmUsers');
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.usersService + "/Rechazar", usuario, function (data) {
			var solicitudes = new Solicitudes();
			solicitudes.ReloadTable();
		});
	}

	this.Aceptar = function () {
		var usrVacio = false;
		var membVacio = false;
		$('#frmUsers input[type="text"]').each(function () {
			if ($(this).val() == "") {
				usrVacio = true;
			}
		});

		$('#frmMembresia input[type="text"]').each(function () {
			if ($(this).val() == "") {
				membVacio = true;
			}
		});

		if (usrVacio || membVacio) {

		} else {
			var usuario = {};
			usuario = this.ctrlActions.GetDataForm('frmUsers');
			var membresia = {};
			membresia = this.ctrlActions.GetDataForm('frmMembresia');
			//Hace el post al create
			this.ctrlActions.PostToAPI(this.usersService + "/Aceptar/"+membresia.Id, usuario, function (data) {
				var solicitudes = new Solicitudes();
				solicitudes.ReloadTable();
			});
        }


	}

	this.Limpiar = function () {
		$('#frmUsers input[type="text"]').each(function () {
			$(this).val("");
		});

		$('#frmMembresia input[type="text"]').each(function () {
			$(this).val("");
		});
    }

	this.BindFieldsUsers = function (data) {
		this.ctrlActions.BindFields('frmUsers', data);
	}

	this.BindFieldsMembresia = function (data) {
		this.ctrlActions.BindFields('frmMembresia', data);
	}

}

//ON DOCUMENT READY
$(document).ready(function () {
	var solicitudes = new Solicitudes();
	solicitudes.RetrieveAll();

});

