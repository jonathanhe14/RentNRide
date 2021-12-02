function TiposCombustible() {

	this.tblTiposCombustibleId = "tblTiposCombustible";
	this.adminService = 'administrador';
	this.ctrlActions = new ControlActionsAdmin();

	//Marcas

	this.RetrieveAll = function () {

		this.ctrlActions.FillTable(this.adminService + "/GetTipoCombustibles", this.tblTiposCombustibleId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetTipoCombustibles", this.tblTiposCombustibleId, true);

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

}

$(document).ready(function () {
	var tiposComb = new TiposCombustible();
	tiposComb.RetrieveAll();

});