function Administrador() {

	this.tblMembresiasId = 'tblMembresias';
	this.tblMarcasId = "tblMarcas";
	this.tblModelosId = "tblModelos";
	this.tblTiposVehiculoId = "tblTiposVehiculo";
	this.tblTiposCombustibleId = "tblTiposCombustible";
	this.service = 'administrador';
	this.ctrlActions = new ControlActions();
	

	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.service + "/GetMembresias", this.tblMembresiasId, false);
		this.ctrlActions.FillTable(this.service + "/GetMarcas", this.tblMarcasId, false);
		this.ctrlActions.FillTable(this.service + "/GetModelos", this.tblModelosId, false);
		this.ctrlActions.FillTable(this.service + "/GetTipoCombustibles", this.tblTiposCombustibleId, false);
		this.ctrlActions.FillTable(this.service + "/GetTipoVehiculos", this.tblTiposVehiculoId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.service + "/GetMembresias", this.tblMembresiasId, true);
		this.ctrlActions.FillTable(this.service + "/GetMarcas", this.tblMarcasId, true);
		this.ctrlActions.FillTable(this.service + "/GetModelos", this.tblModelosId, true);
		this.ctrlActions.FillTable(this.service + "/GetTipoCombustibles", this.tblTiposCombustibleId, true);
		this.ctrlActions.FillTable(this.service + "/GetTipoVehiculos", this.tblTiposVehiculoId, true);
	}

	this.CreMembresia = function () {
		var membresia = {};
		membresia = this.ctrlActions.GetDataForm('frmMembresia');
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.service+"/PostMembresia", membresia, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});
		
	}

	this.UpdMembresia = function () {

		var membresiaData = {};
		membresiaData = this.ctrlActions.GetDataForm('frmMembresia');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.service + "/PutMembresia", membresiaData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelMembresia = function () {

		var membresia = {};
		membresia = this.ctrlActions.GetDataForm('frmMembresia');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.service + "/DeleteMembresia/"+membresia.Id, membresia);
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
		this.ctrlActions.PostToAPI(this.service + "/PostMarca", marca, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});

	}

	this.UpdMarca = function () {

		var marcaData = {};
		marcaData = this.ctrlActions.GetDataForm('frmMarca');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.service + "/PutMarca", marcaData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelMarca = function () {

		var marca = {};
		marca = this.ctrlActions.GetDataForm('frmMarca');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.service + "/DeleteMarca/" + marca.Id, marca);
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
		this.ctrlActions.PostToAPI(this.service + "/PostModelo", modelo, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});

	}

	this.UpdModelo = function () {

		var modeloData = {};
		modeloData = this.ctrlActions.GetDataForm('frmModelo');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.service + "/PutModelo", modeloData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelModelo = function () {

		var modelo = {};
		modelo = this.ctrlActions.GetDataForm('frmModelo');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.service + "/DeleteModelo/" + modelo.Id, modelo);
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
		this.ctrlActions.PostToAPI(this.service + "/PostTipoVehiculo", tipoVehiculo, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});

	}

	this.UpdTipoVehiculo = function () {

		var tipoVehiculoData = {};
		tipoVehiculoData = this.ctrlActions.GetDataForm('frmTiposVehiculo');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.service + "/PutTipoVehiculo", tipoVehiculoData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelTipoVehiculo = function () {

		var tipoVehiculo = {};
		tipoVehiculo = this.ctrlActions.GetDataForm('frmTiposVehiculo');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.service + "/DeleteTipoVehiculo/" + tipoVehiculo.Id, tipoVehiculo);
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
		this.ctrlActions.PostToAPI(this.service + "/PostTipoVehiculo", tipoCombustible, function (data) {
			var administrador = new Administrador();
			administrador.ReloadTable();
		});

	}

	this.UpdTipoCombustible = function () {

		var tipoCombustibleData = {};
		tipoCombustibleData = this.ctrlActions.GetDataForm('frmTiposCombustible');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.service + "/PutTipoCombustible", tipoCombustibleData);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.DelTipoCombustible = function () {

		var tipoCombustible = {};
		tipoCombustible = this.ctrlActions.GetDataForm('frmTiposCombustible');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.service + "/DeleteTipoCombustible/" + tipoCombustible.Id, tipoCombustible);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.BindFieldsTiposCombustibles = function (data) {
		this.ctrlActions.BindFields('frmTiposCombustible', data);
	}
}

//ON DOCUMENT READY
$(document).ready(function () {
	var administrador = new Administrador();
	administrador.RetrieveAll();

});

