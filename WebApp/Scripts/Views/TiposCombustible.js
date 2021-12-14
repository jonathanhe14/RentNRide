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
		if (this.Validar()) {
			var tipoCombustible = {};
			tipoCombustible = this.ctrlActions.GetDataForm('frmTiposCombustible');
			//Hace el post al create
			this.ctrlActions.PostToAPI(this.adminService + "/PostTipoCombustible", tipoCombustible, function (data) {
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

	this.UpdTipoCombustible = function () {
		if (this.Validar()) {
			var tipoCombustibleData = {};
			tipoCombustibleData = this.ctrlActions.GetDataForm('frmTiposCombustible');
			//Hace el post al create
			this.ctrlActions.PutToAPI(this.adminService + "/PutTipoCombustible", tipoCombustibleData);
			//Refresca la tabla
			this.ReloadTable();
			this.Limpiar();
		} else {
			$('#alert').fadeTo(4000, 500).slideUp(500, function () {
				$('#alert').slideUp(500);
			});
		}

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
	this.Limpiar = function () {
		$("#txtIdTiposCombustible").val('');
		$("#txtNombreTiposCombustible").val('');
		$("#alert").fadeOut();

		$("#txtNombreTiposCombustible").attr("placeholder", "ej: Gasolina");

		this.TodoValido();
		$("#tblTiposCombustible > tbody > tr").each(function () {
			$(this).removeClass("seleccionado");
		})
	}

	this.TodoValido = function () {
		$("#txtIdTiposCombustible").css("border", "1px solid #ccc");
		$("#txtNombreTiposCombustible").css("border", "1px solid #ccc");

	}

	this.Validar = function () {
		var validar = true;
		let Letras = /^[A-Z]+$/i;
		let Numeros = /^\d+$/;
		let NumerosDecimales = /^[0-9]{1,2}([.][0-9]{1,2})?$/;

		var id = $("#txtIdMarca").val();
		var nombre = $("#txtNombreTiposCombustible").val();
		if (nombre == "" || !Letras.test(nombre)) {

			$("#txtNombreTiposCombustible").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		} else {
			$("#txtNombreTiposCombustible").css("border-color", "#0f0");
		}

		return validar;
	}
}

$(document).ready(function () {
	var tiposComb = new TiposCombustible();
	tiposComb.RetrieveAll();
	tiposComb.Limpiar();

});