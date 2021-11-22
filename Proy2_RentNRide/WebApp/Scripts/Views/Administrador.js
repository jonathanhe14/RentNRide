function Administrador() {

	this.tblMembresiasId = 'tblMembresias';
	this.service = 'administrador';
	this.ctrlActions = new ControlActions();
	this.columns = "Id,Nombre,MontoMensual,ComisionTransaccion,FechaCreacion,NumDias";

	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.service + "/GetMembresias", this.tblMembresiasId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.service + "/GetMembresias", this.tblMembresiasId, true);
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
}

//ON DOCUMENT READY
$(document).ready(function () {
	var administrador = new Administrador();
	administrador.RetrieveAll();

});

