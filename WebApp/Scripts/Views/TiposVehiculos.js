function TiposVehiculos() {


    this.tblTiposVehiculoId = "tblTiposVehiculo";
    this.adminService = 'administrador';
    this.ctrlActions = new ControlActionsAdmin();


	this.RetrieveAll = function () {

		this.ctrlActions.FillTable(this.adminService + "/GetTipoVehiculos", this.tblTiposVehiculoId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetTipoVehiculos", this.tblTiposVehiculoId, true);

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

}

$(document).ready(function () {
	var tiposVeh = new TiposVehiculos();
	tiposVeh.RetrieveAll();

});