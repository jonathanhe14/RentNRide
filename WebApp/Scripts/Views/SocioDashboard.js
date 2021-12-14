function SocioDashboard() {

	
	

	this.tblUsuariosId = 'tblVehiculos';
	this.usersService = 'vehiculo';
	this.socioService = 'socio';
	this.ctrlActions = new ControlActionsAdmin();

	

	this.Create = function () {
		
	}

	this.getDatas = function () {
		//U?correo=" + localStorage.getItem("Correo")
		this.ctrlActions.GetToApi(this.usersService + "/GetData?correo=" + localStorage.getItem("Correo"), function (data) {
			document.getElementById("vehiculoCant").innerHTML = data;
		});
		/*
		this.ctrlActions.GetToApi(this.socioService + "/GetMembresiaSocio?correo=" + localStorage.getItem("Correo"), function (data) {
			document.getElementById("nombreMem").innerHTML = data.Nombre;
			document.getElementById("montoMensual").innerHTML = data.MontoMensual;
			document.getElementById("ComTrans").innerHTML = data.ComisionTransaccion;
			document.getElementById("fechaCreate").innerHTML = data.FechaCreacion;
			document.getElementById("daysLeft").innerHTML = data.NumDias;
		});
		*/
		//et
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

	
		//cloud_name: "jherrerac@ucenfotec.ac.cr",
	
}


//----------------------------------------------------- ON DOCUMENT READY -----------------------------------------------------------
$(document).ready(function () {
	
	var vehi = new SocioDashboard();
	vehi.getDatas();

});


