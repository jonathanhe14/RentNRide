var data = [];
var resultado = false;
var lista = [];

function Horario() {

	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.service = 'horario';

	this.Create = function () {
		$('#tbl-horarios > tbody > tr').each(function () {

			var hora_Inicio = $(this).find('.txtHoraInicio').val();
			var hora_Final = $(this).find('.txtHoraFinal').val();
			var dia_Inicial = $(this).find('.diaInicial').find(":selected").val();
			var dia_Final = $(this).find('.diaFinal').find(":selected").val();

			if (hora_Inicio && hora_Final && dia_Inicial && dia_Final) {
				data.push({
					Id_Vehiculo: 30041,
					horaInicio: hora_Inicio,
					horaFinal: hora_Final,
					Disponibilidad: "Libre",
					DiaInicial: Number(dia_Inicial),
					DiaFinal: Number(dia_Final)
				});
			}
		});

		//var prueba = this.buscarDuplicados(data);

		//if (!prueba) {
		datos = JSON.stringify(data);

		this.PostToAPI_Json(this.service + "/CrearHorario", datos);
		/*} else {
			alert("¡Cuidado! Existe un traslape de horarios.");
			while (data.length) {
				data.pop();
			}
		};*/
	}

	this.ShowMessage = function (type) {
		if (type == 'E') {
			console.log("lanzar alerta error")

			//$('#modalErr').modal('show');
			//this.Limpiar();
		} else if (type == 'I') {
			console.log("lanzar alerta")
			//$('#modalSuccess').modal('show');
		}

	};

	this.buscarDuplicados = function () {
		var i = 0;
		while (data.length != 0) {
			for (var k = 1; k < data.length; k++) {
				if (this.between(data[k].DiaInicial, data[i].DiaInicial, data[i].DiaFinal) &&
					this.between(data[k].DiaFinal, data[i].DiaInicial, data[i].DiaFinal) &&
					this.between(Number(data[k].horaInicio.match(/^[0-9]+/)[0]), Number(data[i].horaInicio.match(/^[0-9]+/)[0]), Number(data[i].horaFinal.match(/^[0-9]+/)[0])) &&
					this.between(Number(data[k].horaFinal.match(/^[0-9]+/)[0]), Number(data[i].horaInicio.match(/^[0-9]+/)[0]), Number(data[i].horaFinal.match(/^[0-9]+/)[0]))) {
					resultado = true;
					break;
				}
			}
			data.shift();
			i++;
		}
		return resultado;
	};

	this.between = function (x, min, max) {
		return x >= min && x <= max;
	};


	this.PostToAPI_Json = function (service, data, callBackFunction) {
		$.ajax({
			url: this.GetUrlApiService(service),
			type: "POST",
			data: data,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('I', response.Message);
				if (callBackFunction) {
					callBackFunction(response.Data);
				}
			},
			fail: function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			}
		})
	};
		

}


$(document).ready(function () {

	//Meter aquí un menú de opciones.
	$("#btn-agregar-fila").click(function () {
		var opciones = "<button>nada</button>";
		var diaInicial = "<select name='diaInicial' class='diaInicial form-control'>" +
			"<option value=1>Lunes</option>" +
			"<option value=2>Martes</option>" +
			"<option value=3>Miércoles</option>" +
			"<option value=4>Jueves</option>" +
			"<option value=5>Viernes</option>" +
			"<option value=6>Sábado</option>" +
			"<option value=7>Domingo</option>" +
			"</select > ";
		var txtHoraInicio = "<input type='time' class='txtHoraInicio form-control'>";
		var diaFinal = "<select name='diaFinal' class='diaFinal form-control'>" +
			"<option value=1>Lunes</option>" +
			"<option value=2>Martes</option>" +
			"<option value=3>Miércoles</option>" +
			"<option value=4>Jueves</option>" +
			"<option value=5>Viernes</option>" +
			"<option value=6>Sábado</option>" +
			"<option value=7>Domingo</option>" +
			"</select > ";
		var txtHoraFinal = "<input type='time' class='txtHoraFinal form-control' disabled>";
		var eliminar = "<button class='btnDelete'>Eliminar</button>";
		var alerta = "<div class='mensaje alert-danger'></div>"
		var markup = "<tr><td> " + opciones + "</td><td>" + diaInicial + "</td><td>" + txtHoraInicio +
			"</td><td>" + diaFinal + "</td><td>" + txtHoraFinal + "</td><td>" + eliminar + "</td><td>" + alerta + "</td></tr>";
		$("#tbl-horarios").append(markup);
	});

	$("#tbl-horarios").on('click', '.btnDelete', function () {
		$(this).closest('tr').remove();
	});

	$("#tbl-horarios").on('change', '.txtHoraInicio', function () {
		if ($(this).closest('tr').find('.txtHoraFinal').is(':disabled')) {
			$(this).closest('tr').find('.txtHoraFinal').prop('disabled', false);
			var fecha = new Date(this.valueAsDate);
			var h = fecha.getHours() + 6;
			var m = fecha.getMinutes();
			if (h < 10) h = '0' + h;
			if (m < 10) m = '0' + m;
			$(this).closest('tr').find('.txtHoraFinal').attr({ 'value': h + ':' + m }, { 'min': h + ':' + m });
		} else {
			var hora_final = Number($(this).closest('tr').find('.txtHoraFinal').val().match(/^[0-9]+/)[0]);
			var hora_inicial = Number($(this).closest('tr').find('.txtHoraInicio').val().match(/^[0-9]+/)[0]);
			var dia_inicial = Number($(this).closest('tr').find('.diaInicial').val());
			var dia_final = Number($(this).closest('tr').find('.diaFinal').val());
			var mensaje = $(this).closest('tr').find('.mensaje');

			if (dia_inicial == dia_final && hora_final <= hora_inicial) {
				mensaje.text("La hora final no puede ser anterior a la hora inicial en un mismo día");
			} else {
				mensaje.text("");
			}
		}

	});

	//Validar en la hora final
	$("#tbl-horarios").on('change', '.txtHoraFinal', function () {

		var hora_final = Number($(this).val().match(/^[0-9]+/)[0]);
		var hora_inicial = Number($(this).closest('tr').find('.txtHoraInicio').val().match(/^[0-9]+/)[0]);
		var dia_inicial = Number($(this).closest('tr').find('.diaInicial').val());
		var dia_final = Number($(this).closest('tr').find('.diaFinal').val());
		var mensaje = $(this).closest('tr').find('.mensaje');

		if (dia_inicial == dia_final && hora_final <= hora_inicial) {
			mensaje.text("La hora final no puede ser anterior a la hora inicial en un mismo día");
		} else {
			mensaje.text("");
		}
	});

	//Validar cambios en el día inicial
	$("#tbl-horarios").on('change', '.diaInicial', function () {
		var hora_final = Number($(this).closest('tr').find('.txtHoraFinal').val().match(/^[0-9]+/)[0]);
		var hora_inicial = Number($(this).closest('tr').find('.txtHoraInicio').val().match(/^[0-9]+/)[0]);
		var dia_inicial = Number($(this).closest('tr').find('.diaInicial').val());
		var dia_final = Number($(this).closest('tr').find('.diaFinal').val());
		var mensaje = $(this).closest('tr').find('.mensaje');

		if (dia_inicial == dia_final && hora_final <= hora_inicial) {
			mensaje.text("La hora final no puede ser anterior a la hora inicial en un mismo día");
		} else {
			mensaje.text("");
		}
	});

	//Validar cambios en el día final
	$("#tbl-horarios").on('change', '.diaFinal', function () {
		var hora_final = Number($(this).closest('tr').find('.txtHoraFinal').val().match(/^[0-9]+/)[0]);
		var hora_inicial = Number($(this).closest('tr').find('.txtHoraInicio').val().match(/^[0-9]+/)[0]);
		var dia_inicial = Number($(this).closest('tr').find('.diaInicial').val());
		var dia_final = Number($(this).closest('tr').find('.diaFinal').val());
		var mensaje = $(this).closest('tr').find('.mensaje');

		if (dia_inicial == dia_final && hora_final <= hora_inicial) {
			mensaje.text("La hora final no puede ser anterior a la hora inicial en un mismo día");
		} else {
			mensaje.text("");
		}
	});




	$("#btn-datos").click(function () {
		$('#tbl-horarios > tbody > tr').each(function () {

			var hora_Inicio = $(this).find('.txtHoraInicio').val();
			var hora_Final = $(this).find('.txtHoraFinal').val();
			var dia_Inicial = $(this).find('.diaInicial').find(":selected").val();
			var dia_Final = $(this).find('.diaFinal').find(":selected").val();

			if (hora_Inicio && hora_Final && dia_Inicial && dia_Final) {
				data.push({
					Id_Vehiculo: 30041,
					horaInicio: hora_Inicio,
					horaFinal: hora_Final,
					Disponibilidad: "Libre",
					DiaInicial: Number(dia_Inicial),
					DiaFinal: Number(dia_Final)
				});
			}
		});

	});

	$("#btn-imprimir").click(function () {
		var i = 0;
		var resultado = false;
		while (data.length != 0) {
			for (var k = 1; k < data.length; k++) {
				if (between(data[k].DiaInicial, data[i].DiaInicial, data[i].DiaFinal) &&
					between(data[k].DiaFinal, data[i].DiaInicial, data[i].DiaFinal) &&
					between(Number(data[k].horaInicio.match(/^[0-9]+/)[0]), Number(data[i].horaInicio.match(/^[0-9]+/)[0]), Number(data[i].horaFinal.match(/^[0-9]+/)[0])) &&
					between(Number(data[k].horaFinal.match(/^[0-9]+/)[0]), Number(data[i].horaInicio.match(/^[0-9]+/)[0]), Number(data[i].horaFinal.match(/^[0-9]+/)[0]))) {
					console.log("¡Cuidado! Existe un traslape de horarios. Caso 1");
					resultado = true;
					break;
				}
			}
			data.shift();
			i++;
		}

	});


	function between(x, min, max) {
		return x >= min && x <= max;
	}

});