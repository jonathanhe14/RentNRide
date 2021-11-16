function vVehiculo() {

	
	this.service = 'vehiculo';
	this.ctrlActions = new ControlActions();

	this.Create = function () {
		var customerData = {};
		var idData = {};
		var documentoData = {};
		idData = this.ctrlActions.GetDataForm('idEdition');
		customerData = idData + this.ctrlActions.GetDataForm('frmEdition');
		documentoData = idData + this.ctrlActions.GetDataForm('docuEdition');
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.service, customerData, function (data) {
			var vcustomer = new vCustomers();
		});

	}

	this.Update = function () {

		var customerData = {};
		customerData = this.ctrlActions.GetDataForm('frmEdition');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.service, customerData);
		//Refresca la tabla
		this.ReloadTable();

	}


	this.BindFields = function (data) {
		this.ctrlActions.BindFields('frmEdition', data);
	}

}

