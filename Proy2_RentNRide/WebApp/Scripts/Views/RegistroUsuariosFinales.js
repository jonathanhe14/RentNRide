function RegistroUsuariosFinales() {


	this.service = 'usuarios';
	this.ctrlActions = new ControlActions();
	this.columns = "Nombre,Apellidos,Cedula,Correo,FechaNacimiento,Telefono,ContrassenaActual";


	this.Create = function () {
		var usuariosData = {};
		usuariosData = this.ctrlActions.GetDataForm('frmRegistro');
		console.log("Hola")
		console.log("Hola  "+usuariosData)
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.service +"/Post", usuariosData);

	}

	this.Update = function () {

		var customerData = {};
		customerData = this.ctrlActions.GetDataForm('frmRegistro');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.service, customerData);


	}

	this.Delete = function () {

		var customerData = {};
		customerData = this.ctrlActions.GetDataForm('frmRegistro');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.service, customerData);


	}

	this.BindFields = function (data) {
		this.ctrlActions.BindFields('frmRegistro', data);
	}
}


