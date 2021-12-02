//Modelos
function Modelos() {
	this.tblModelosId = "tblModelos";
	this.adminService = 'administrador';
	this.ctrlActions = new ControlActionsAdmin();

	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetModelos", this.tblModelosId, false);
		
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetModelos", this.tblModelosId, true);
	}

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
}

$(document).ready(function () {
	var modelos = new Modelos();
	modelos.RetrieveAll();

});