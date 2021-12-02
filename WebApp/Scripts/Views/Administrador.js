function Administrador() {

	this.tblMembresiasId = 'tblMembresias';
	this.adminService = 'administrador';
	this.usersService = 'usuarios';
	this.ctrlActions = new ControlActionsAdmin();
	this.usrChartId = 'usrChart';
	this.usrAdmin = 'lblAdmin';
	this.usrFinales = 'lblFinales';
	this.usrSocios = 'lblSocios';
	this.usrEmpresas = 'lblEmpresas';
	this.ingresos = 'lblIngresos';
	this.egresos = 'lblEgresos';
	this.ganancias = 'lblGanancias';


	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetMembresias", this.tblMembresiasId, false);

	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetMembresias", this.tblMembresiasId, true);

	}

	this.CreMembresia = function () {
		var membresia = {};
		membresia = this.ctrlActions.GetDataForm('frmMembresia');
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.adminService + "/PostMembresia", membresia, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});

	}

	this.UpdMembresia = function () {

		var membresiaData = {};
		membresiaData = this.ctrlActions.GetDataForm('frmMembresia');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.adminService + "/PutMembresia", membresiaData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelMembresia = function () {

		var membresia = {};
		membresia = this.ctrlActions.GetDataForm('frmMembresia');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.adminService + "/DeleteMembresia/" + membresia.Id, membresia);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.BindFieldsMembresia = function (data) {
		this.ctrlActions.BindFields('frmMembresia', data);
	}






	// Usuarios
	/*this.GetDataToChart = function (initializeChartFunction) {

		this.ctrlActions.GetToApi(this.service + '/Get', initializeChartFunction);
	};*/

	this.RetrieveAllUsuarios = function () {
		this.ctrlActions.GetToApi(this.usersService + '/GetRoles', function (data) {
			var admin = 0;
			var empresas = 0;
			var socios = 0;
			var finales = 0;

			for (var i = 0; i < data.length; i++) {
				if (data[i]["IdRol"] == 1) {
					admin - admin + 1;
				} else if (data[i]["IdRol"] == 2) {
					socios = socios + 1;
				} else if (data[i]["IdRol"] == 3) {
					empresas = empresas + 1;
				} else {
					finales = finales + 1;
				}
			}

			$("#lblAdmin").text(admin);
			$("#lblEmpresas").text(empresas);
			$("#lblSocios").text(socios);
			$("#lblFinales").text(finales);

		});

	}

	this.RetrieveFinanzas = function () {
		var ingresos = 6000;
		var egresos = 1750;
		var ganancias = ingresos - egresos;
		if (ganancias >= 0) {
			$("#lblGanancias").text("Ganancias");
		} else {
			$("#lblGanancias").text("Pérdidas");
		}

		$("#lblIngresos").text(ingresos);
		$("#lblEgresos").text(egresos);
		$("#lblGananciasMonto").text(ganancias);
	}
}

//ON DOCUMENT READY
$(document).ready(function () {
	var administrador = new Administrador();
	administrador.RetrieveAll();
	administrador.RetrieveAllUsuarios();
	administrador.RetrieveFinanzas();
});

