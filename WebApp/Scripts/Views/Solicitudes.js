function Solicitudes() {

	this.tblUsuariosId = 'tblUsuarios';
	this.usersService = 'usuarios';
	this.adminService = 'administrador';
	this.tblMembresiasId = 'tblMembresias'
	this.ctrlActions = new ControlActionsAdmin();


	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.usersService + "/GetSolicitudes", this.tblUsuariosId, false);
		this.ctrlActions.FillTable(this.adminService + "/GetMembresias", this.tblMembresiasId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.usersService + "/GetSolicitudes", this.tblUsuariosId, true);
		this.ctrlActions.FillTable(this.adminService + "/GetMembresias", this.tblMembresiasId, true);
		location.reload();
	}

	this.Rechazar = function () {
		var usuario = {};
		usuario = this.ctrlActions.GetDataForm('frmUsers');
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.adminService+ "/Rechazar", usuario, function (data) {
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
			//"<div class='alert alert-warning alert-dismissible fade show'>< button type = 'button' class= 'btn-close' data-bs-dismiss='alert' ></button ><strong>Los campos no pueden estar vacíos</strong></div >"
			$('#alerta').append("<div class='alerta-d'> Los campos no pueden estar vacíos </div>")
		} else {
			$('#alerta').empty();
			var usuario = {};
			usuario = this.ctrlActions.GetDataForm('frmUsers');
			var membresia = {};
			membresia = this.ctrlActions.GetDataForm('frmMembresia');
			//Hace el post al create
			this.ctrlActions.PostToAPI(this.adminService+ "/Aceptar/"+membresia.Id, usuario, function (data) {
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
		$("#tblUsuarios > tbody > tr").each(function () {
			$(this).removeClass("seleccionado");
		})
		$("#tblMembresias > tbody > tr").each(function () {
			$(this).removeClass("seleccionado");
		})


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

