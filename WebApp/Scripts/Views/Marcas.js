function Marcas() {

	this.tblMarcasId = "tblMarcas";
	this.adminService = 'administrador';
	this.ctrlActions = new ControlActionsAdmin();

	//Marcas

	this.RetrieveAll = function () {
	
		this.ctrlActions.FillTable(this.adminService + "/GetMarcas", this.tblMarcasId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetMarcas", this.tblMarcasId, true);
		
	}

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

}

//ON DOCUMENT READY
$(document).ready(function () {
	var marcas = new Marcas();
	marcas.RetrieveAll();

});


