function RegistroSocios() {


	this.service = 'usuarios';
	this.ctrlActions = new ControlActions();
	this.columns = "Nombre,Cedula,Correo,Telefono,ContrassenaActual,Latitud,Longitud,PersoneriaJuridica,PermisoOperaciones";

	var urlPersoneria;
	var urlPermisoOperaciones;


	var PeroneriaJuridica = cloudinary.createUploadWidget({
		cloudName: 'cenfotec2021',
		uploadPreset: 'Cenfo_preset'
	}, (error, result) => {
		if (!error && result && result.event === "success") {
			urlPersoneria = result.info.secure_url;
			this.ctrlActions.SubirPersoneria(urlPersoneria);
		}
	}
	);

	var PermisoOperaciones = cloudinary.createUploadWidget({
		cloudName: 'cenfotec2021',
		uploadPreset: 'Cenfo_preset'
	}, (error, result) => {
		if (!error && result && result.event === "success") {
			urlPermisoOperaciones = result.info.secure_url;
			this.ctrlActions.SubirOperaciones(urlPermisoOperaciones);
		}
	}
	);

	this.Create = function () {
		var usuariosData = {};
		usuariosData = this.ctrlActions.GetDataForm('frmRegistro');

		//Hace el post al create
		this.ctrlActions.PostToAPI(this.service + "/CrearSocio", usuariosData);

	}

	this.SubirPersoneria = function () {
		PeroneriaJuridica.open();
	}
	this.SubirOperaciones = function () {
		PermisoOperaciones.open();
	}
	this.BindFields = function (data) {
		this.ctrlActions.BindFields('frmRegistro', data);
	}



}