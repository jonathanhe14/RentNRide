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
		if (this.Validar()) {
			var marca = {};
			marca = this.ctrlActions.GetDataForm('frmMarca');
			//Hace el post al create
			this.ctrlActions.PostToAPI(this.adminService + "/PostMarca", marca, function (data) {
				var administrador = new Administrador();
			})
			this.Limpiar();
			this.ReloadTable();
		}else {
				$('#alert').fadeTo(4000, 500).slideUp(500, function () {
					$('#alert').slideUp(500);
				});
			}
 
	}

	this.UpdMarca = function () {
		if (this.Validar()) {
			var marcaData = {};
			marcaData = this.ctrlActions.GetDataForm('frmMarca');
			//Hace el post al create
			this.ctrlActions.PutToAPI(this.adminService + "/PutMarca", marcaData);
			//Refresca la tabla
			this.ReloadTable();
			this.Limpiar();
		} else {
			$('#alert').fadeTo(4000, 500).slideUp(500, function () {
				$('#alert').slideUp(500);
			});
        }

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

	this.Limpiar = function () {
		$("#txtIdMarca").val('');
		$("#txtNombreMarca").val('');
		$("#alert").fadeOut();

		$("#txtNombreMarca").attr("placeholder", "ej: Toyota");

		this.TodoValido();
		$("#tblMarcas > tbody > tr").each(function () {
			$(this).removeClass("seleccionado");
		})
	}

	this.TodoValido = function () {
		$("#txtIdMarca").css("border", "1px solid #ccc");
		$("#txtNombreMarca").css("border", "1px solid #ccc");

	}

	this.Validar = function () {
		var validar = true;
		let Letras = /^[A-Z]+$/i;
		let Numeros = /^\d+$/;
		let NumerosDecimales = /^[0-9]{1,2}([.][0-9]{1,2})?$/;

		var id = $("#txtIdMarca").val();
		var nombre = $("#txtNombreMarca").val();
		if (nombre == "" || !Letras.test(nombre)) {

			$("#txtNombreMarca").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		} else {
			$("#txtNombreMarca").css("border-color", "#0f0");
		}

		return validar;
	}


}

//ON DOCUMENT READY
$(document).ready(function () {
	var marcas = new Marcas();
	marcas.RetrieveAll();
	marcas.Limpiar();

});


