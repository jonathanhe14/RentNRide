function TiposVehiculo() {


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
		if (this.Validar()) {
			var tipoVehiculo = {};
			tipoVehiculo = this.ctrlActions.GetDataForm('frmTiposVehiculo');
			//Hace el post al create
			this.ctrlActions.PostToAPI(this.adminService + "/PostTipoVehiculo", tipoVehiculo, function (data) {
				var administrador = new Administrador();
				administrador.ReloadTable();
			});
			this.Limpiar();
		} else {
			$('#alert').fadeTo(4000, 500).slideUp(500, function () {
				$('#alert').slideUp(500);
			});
		}

	}

	this.UpdTipoVehiculo = function () {
		if (this.Validar()) {
			var tipoVehiculoData = {};
			tipoVehiculoData = this.ctrlActions.GetDataForm('frmTiposVehiculo');
			//Hace el post al create
			this.ctrlActions.PutToAPI(this.adminService + "/PutTipoVehiculo", tipoVehiculoData);
			//Refresca la tabla
			this.ReloadTable();
			this.Limpiar();
		} else {
			$('#alert').fadeTo(4000, 500).slideUp(500, function () {
				$('#alert').slideUp(500);
			});
		}
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
	this.Limpiar = function () {
		$("#txtIdTiposVehiculo").val('');
		$("#txtNombreTiposVehiculo").val('');
		$("#alert").fadeOut();

		$("#txtNombreTiposVehiculo").attr("placeholder", "ej: Manual");

		this.TodoValido();
		$("#tblTiposVehiculo > tbody > tr").each(function () {
			$(this).removeClass("seleccionado");
		})

	}

	this.TodoValido = function () {
		$("#txtIdTiposVehiculo").css("border", "1px solid #ccc");
		$("#txtNombreTiposVehiculo").css("border", "1px solid #ccc");

	}

	this.Validar = function () {
		var validar = true;
		let Letras = /^[A-Z]+$/i;
		let Numeros = /^\d+$/;
		let NumerosDecimales = /^[0-9]{1,2}([.][0-9]{1,2})?$/;

		var id = $("#txtIdTiposVehiculo").val();
		var nombre = $("#txtNombreTiposVehiculo").val();
		if (nombre == "" || !Letras.test(nombre)) {

			$("#txtNombreTiposVehiculo").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		} else {
			$("#txtNombreTiposVehiculo").css("border-color", "#0f0");
		}

		return validar;
	}
}

$(document).ready(function () {
	var tiposVeh = new TiposVehiculo();
	tiposVeh.RetrieveAll();
	tiposVeh.Limpiar();

});