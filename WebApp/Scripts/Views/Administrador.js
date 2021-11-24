﻿function Administrador() {

	this.tblMembresiasId = 'tblMembresias';
	this.tblMarcasId = "tblMarcas";
	this.tblModelosId = "tblModelos";
	this.tblTiposVehiculoId = "tblTiposVehiculo";
	this.tblTiposCombustibleId = "tblTiposCombustible";
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
		this.ctrlActions.FillTable(this.adminService + "/GetMarcas", this.tblMarcasId, false);
		this.ctrlActions.FillTable(this.adminService + "/GetModelos", this.tblModelosId, false);
		this.ctrlActions.FillTable(this.adminService + "/GetTipoCombustibles", this.tblTiposCombustibleId, false);
		this.ctrlActions.FillTable(this.adminService + "/GetTipoVehiculos", this.tblTiposVehiculoId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetMembresias", this.tblMembresiasId, true);
		this.ctrlActions.FillTable(this.adminService + "/GetMarcas", this.tblMarcasId, true);
		this.ctrlActions.FillTable(this.adminService + "/GetModelos", this.tblModelosId, true);
		this.ctrlActions.FillTable(this.adminService + "/GetTipoCombustibles", this.tblTiposCombustibleId, true);
		this.ctrlActions.FillTable(this.adminService + "/GetTipoVehiculos", this.tblTiposVehiculoId, true);
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

	//Marcas

	this.CreMarca = function () {
		var marca = {};
		marca = this.ctrlActions.GetDataForm('frmMarca');
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.adminService + "/PostMarca", marca, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});

	}

	this.UpdMarca = function () {

		var marcaData = {};
		marcaData = this.ctrlActions.GetDataForm('frmMarca');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.adminService + "/PutMarca", marcaData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelMarca = function () {

		var marca = {};
		marca = this.ctrlActions.GetDataForm('frmMarca');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.adminService + "/DeleteMarca/" + marca.Id, marca);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.BindFieldsMarcas = function (data) {
		this.ctrlActions.BindFields('frmMarca', data);
	}
	//Modelos

	this.CreModelo = function () {
		var modelo = {};
		modelo = this.ctrlActions.GetDataForm('frmModelo');
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.adminService + "/PostModelo", modelo, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});

	}

	this.UpdModelo = function () {

		var modeloData = {};
		modeloData = this.ctrlActions.GetDataForm('frmModelo');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.adminService + "/PutModelo", modeloData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelModelo = function () {

		var modelo = {};
		modelo = this.ctrlActions.GetDataForm('frmModelo');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.adminService + "/DeleteModelo/" + modelo.Id, modelo);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.BindFieldsModelos = function (data) {
		this.ctrlActions.BindFields('frmModelo', data);
	}

	//Tipos vehiculo

	this.CreTipoVehiculo = function () {
		var tipoVehiculo = {};
		tipoVehiculo = this.ctrlActions.GetDataForm('frmTiposVehiculo');
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.adminService + "/PostTipoVehiculo", tipoVehiculo, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});

	}

	this.UpdTipoVehiculo = function () {

		var tipoVehiculoData = {};
		tipoVehiculoData = this.ctrlActions.GetDataForm('frmTiposVehiculo');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.adminService + "/PutTipoVehiculo", tipoVehiculoData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelTipoVehiculo = function () {

		var tipoVehiculo = {};
		tipoVehiculo = this.ctrlActions.GetDataForm('frmTiposVehiculo');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.adminService + "/DeleteTipoVehiculo/" + tipoVehiculo.Id, tipoVehiculo);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.BindFieldsTiposVehiculos = function (data) {
		this.ctrlActions.BindFields('frmTiposVehiculo', data);
	}


	//Tipos combustible

	this.CreTipoCombustible = function () {
		var tipoCombustible = {};
		tipoCombustible = this.ctrlActions.GetDataForm('frmTiposCombustible');
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.adminService + "/PostTipoCombustible", tipoCombustible, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});

	}

	this.UpdTipoCombustible = function () {

		var tipoCombustibleData = {};
		tipoCombustibleData = this.ctrlActions.GetDataForm('frmTiposCombustible');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.adminService + "/PutTipoCombustible", tipoCombustibleData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelTipoCombustible = function () {

		var tipoCombustible = {};
		tipoCombustible = this.ctrlActions.GetDataForm('frmTiposCombustible');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.adminService + "/DeleteTipoCombustible/" + tipoCombustible.Id, tipoCombustible);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.BindFieldsTiposCombustibles = function (data) {
		this.ctrlActions.BindFields('frmTiposCombustible', data);
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
