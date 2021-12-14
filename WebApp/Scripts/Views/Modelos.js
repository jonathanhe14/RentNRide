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
		if (this.Validar()) {
			var modelo = {};
			modelo = this.ctrlActions.GetDataForm('frmModelo');
			//Hace el post al create
			this.ctrlActions.PostToAPI(this.adminService + "/PostModelo", modelo, function (data) {
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

	this.UpdModelo = function () {
		if (this.Validar()) {
			var modeloData = {};
			modeloData = this.ctrlActions.GetDataForm('frmModelo');
			//Hace el post al create
			this.ctrlActions.PutToAPI(this.adminService + "/PutModelo", modeloData);
			//Refresca la tabla
			this.ReloadTable();
			this.Limpiar();
		} else {
			$('#alert').fadeTo(4000, 500).slideUp(500, function () {
				$('#alert').slideUp(500);
			});
		}
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
	this.Limpiar = function () {
		$("#txtIdModelo").val('');
		$("#txtNombreModelo").val('');
		$("#alert").fadeOut();

		$("#txtNombreModelo").attr("placeholder", "ej: Yaris");
	

		this.TodoValido();
		$("#tblModelos > tbody > tr").each(function () {
			$(this).removeClass("seleccionado");
		})
	}

	this.TodoValido = function () {
		$("#txtIdModelo").css("border", "1px solid #ccc");
		$("#txtNombreModelo").css("border", "1px solid #ccc");
		
	}

	this.Validar = function () {
		var validar = true;
		let Letras = /^[A-Z]+$/i;
		let Numeros = /^\d+$/;
		let NumerosDecimales = /^[0-9]{1,2}([.][0-9]{1,2})?$/;

		var id = $("#txtIdModelo").val();
		var nombre = $("#txtNombreModelo").val();

		if (nombre == "" || !Letras.test(nombre)) {

			$("#txtNombreModelo").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		} else {
			$("#txtNombreModelo").css("border-color", "#0f0");
		}
		

		return validar;
	}


}

$(document).ready(function () {
	var modelos = new Modelos();
	modelos.RetrieveAll();
	modelos.Limpiar();

});