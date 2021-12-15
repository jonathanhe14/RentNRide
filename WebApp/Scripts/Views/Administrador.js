function Administrador() {

	this.tblMembresiasId = 'tblMembresias';
	this.adminService = 'administrador';
	this.usersService = 'usuarios';
	this.ctrlActions = new ControlActionsAdmin();
	this.usrChartId = 'usrChart';
	this.usrAdmin = 'lblAdmin';
	this.usrFinales = 'lblFinales';
	this.usrSocios = 'lblSocios';
	this.usrEmpresas = 'lblEmpresas';
	this.ingresos = 'lblIngresos';
	this.egresos = 'lblEgresos';
	this.ganancias = 'lblGanancias';


	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetMembresias", this.tblMembresiasId, false);
	
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.adminService + "/GetMembresias", this.tblMembresiasId, true);
	
	}
	this.ShowMessage = function (type) {
		if (type == 'E') {


			$('#modalErr').modal('show');
			this.Limpiar();
		} else if (type == 'I') {
			$('#modalSuccess').modal('show');
		}

	};

	this.CreMembresia = function () {
		if (this.Validar()) {
			var membresia = {};
			membresia = this.ctrlActions.GetDataForm('frmMembresia');
			//Hace el post al create
			this.ctrlActions.PostToAPI(this.adminService + "/PostMembresia", membresia, function (data) {
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

	this.UpdMembresia = function () {
		if (this.Validar()) {
			var membresiaData = {};
			membresiaData = this.ctrlActions.GetDataForm('frmMembresia');
			//Hace el post al create
			this.ctrlActions.PutToAPI(this.adminService + "/PutMembresia", membresiaData);
			//Refresca la tabla
			this.ReloadTable();
			this.Limpiar();
		} else {
			$('#alert').fadeTo(4000, 500).slideUp(500, function () {
				$('#alert').slideUp(500);
			});
        }

	}

	this.DelMembresia = function () {

		var membresia = {};
		membresia = this.ctrlActions.GetDataForm('frmMembresia');
		//Hace el post al create
		this.ctrlActions.DeleteToAPI(this.adminService + "/DeleteMembresia/" + membresia.Id, membresia);
		//Refresca la tabla
		this.ReloadTable();

	}

	this.BindFieldsMembresia = function (data) {
		this.ctrlActions.BindFields('frmMembresia', data);
	}

	
	
	this.Limpiar = function () {
		$("#txtIdMembresia").val('');
		$("#txtFechaCreacionMembresia").val('');
		$("#txtNombreMembresia").val('');
		$("#txtMontoMensualMembresia").val('');
		$("#txtComisionTransaccionMembresia").val('');
		$("#txtNumDiasMembresia").val('');
		$("#alert").fadeOut();

		$("#txtNombreMembresia").attr("placeholder", "ej: Oro");
		$("#txtMontoMensualMembresia").attr("placeholder", "ej: 5000");
		$("#txtComisionTransaccionMembresia").attr("placeholder", "ej: 7.54");
		$("#txtNumDiasMembresia").attr("placeholder", "ej: 22");
		
		this.TodoValido();

		$("#tblMembresias > tbody > tr").each(function () {
			$(this).removeClass("seleccionado");
		})

	}

	this.TodoValido = function () {
		$("#txtIdMembresia").css("border", "1px solid #ccc");
		$("#txtFechaCreacionMembresia").css("border", "1px solid #ccc");
		$("#txtNombreMembresia").css("border", "1px solid #ccc");
		$("#txtMontoMensualMembresia").css("border", "1px solid #ccc");
		$("#txtComisionTransaccionMembresia").css("border", "1px solid #ccc");
		$("#txtNumDiasMembresia").css("border", "1px solid #ccc");
	}

	this.Validar = function () {
		var validar = true;
		let Letras = /^[A-Z]+$/i;
		let Numeros = /^\d+$/;
		let NumerosDecimales = /^[0-9]{1,2}([.][0-9]{1,2})?$/;

		var id = $("#txtIdMembresia").val();
		var fecha = $("#txtFechaCreacionMembresia").val();
		var nombre = $("#txtNombreMembresia").val();
		var monto_mensual = $("#txtMontoMensualMembresia").val();
		var comision = $("#txtComisionTransaccionMembresia").val();
		var num_dias = $("#txtNumDiasMembresia").val();
		if (nombre == "" || !Letras.test(nombre)) {

			$("#txtNombreMembresia").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		} else {
			$("#txtNombreMembresia").css("border-color", "#0f0");
        }
		if (monto_mensual == "" || !Numeros.test(monto_mensual)) {

			$("#txtMontoMensualMembresia").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		} else {
			$("#txtMontoMensualMembresia").css("border-color", "#0f0");
		}

		if (comision == "" || !NumerosDecimales.test(comision)) {

			$("#txtComisionTransaccionMembresia").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		} else {
			$("#txtComisionTransaccionMembresia").css("border-color", "#0f0");
		}
		if (num_dias == "" || !Numeros.test(num_dias)) {

			$("#txtNumDiasMembresia").css("border-color", "rgba(255, 0, 5, 0.7)");
			validar = false;
		} else {
			$("#txtNumDiasMembresia").css("border-color", "#0f0");
		}



		return validar;
	}

	

	// Usuarios
	/*this.GetDataToChart = function (initializeChartFunction) {

		this.ctrlActions.GetToApi(this.service + '/Get', initializeChartFunction);
	};*/

	this.RetrieveAllUsuarios = function () {
		this.ctrlActions.GetToApi(this.usersService + '/GetRoles', function (data) {
			var admin = 0;
			var empresas = 0;
			var socios = 0;
			var finales = 0;
			
			for (var i = 0; i < data.length; i++) {
				if (data[i]["IdRol"] == 1) {
					admin - admin + 1;
				} else if (data[i]["IdRol"] == 2) {
					socios = socios + 1;
				} else if (data[i]["IdRol"] == 3) {
					empresas = empresas + 1;
				} else {
					finales = finales + 1;
				}
			}

			$("#lblAdmin").text(admin);
			$("#lblEmpresas").text(empresas);
			$("#lblSocios").text(socios);
			$("#lblFinales").text(finales);

		});
		
	}

	this.RetrieveFinanzas = function () {
		var ingresos = 400000;
		var egresos = 150000;
		var ganancias = ingresos - egresos;
		if (ganancias >= 0) {
			$("#lblGanancias").text("Ganancias");
		} else {
			$("#lblGanancias").text("Pérdidas");
		}

		$("#lblIngresos").text(ingresos);
		$("#lblEgresos").text(egresos);
		$("#lblGananciasMonto").text(ganancias);
    }
}

//ON DOCUMENT READY
$(document).ready(function () {
	var administrador = new Administrador();
	administrador.RetrieveAll();
	administrador.RetrieveAllUsuarios();
	administrador.RetrieveFinanzas();
	administrador.Limpiar();

});

