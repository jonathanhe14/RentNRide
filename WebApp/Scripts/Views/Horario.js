var data = [];
var resultado = false;
var lista = [];
var i = 0;


function Horario() {

	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.service = 'horario';

	this.Create = function () {
		$('#tbl-horarios > tbody > tr').each(function () {

			var idVehiculo = localStorage.getItem("idVehiculo");
			var hora_Inicio = $(this).find('.txtHoraInicio').val();
			var hora_Final = $(this).find('.txtHoraFinal').val();
			var dia_Inicial = $(this).find('.diaInicial').find(":selected").val();
			var dia_Final = $(this).find('.diaFinal').find(":selected").val();
			var disponibilidad = "";

			if ($(this).find('#repetir').is(':checked')) {
				disponibilidad = $(this).find('#repetir').val();
			}

			if ($(this).find('#continuo').is(':checked')) {
				disponibilidad = $(this).find('#continuo').val();
			}



			if (hora_Inicio && hora_Final && dia_Inicial && dia_Final) {
				data.push({
					//Id_Vehiculo: 30041,
					Id_Vehiculo: idVehiculo,
					horaInicio: hora_Inicio,
					horaFinal: hora_Final,
					Disponibilidad: disponibilidad,
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

	this.ShowMessage = function (type, mensaje) {
		if (type == 'E') {
			console.log("lanzar alerta error")
			data = [];
			i = 0;
			$('#modalErr').modal('show');
			$('#modalErr .modal-body').text(mensaje);
			//this.Limpiar();
		} else if (type == 'I') {
			//console.log("lanzar alerta")
			$('#modalSuccess').modal('show');
			$('#modalSuccess .modal-body').text(mensaje);
			localStorage.removeItem("idVehiculo");
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
				var ctrlActions = new Horario();
				ctrlActions.ShowMessage('I', response.Message);
				if (callBackFunction) {
					callBackFunction(response.Data);
				}
			},
			error: function (response) {
				var data = response.responseJSON;
				var ctrlActions = new Horario();
				console.log(data.ExceptionMessage);
				console.log(response.Message);
				console.log(data);
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
			}
		})
	};


}


$(document).ready(function () {

	$('#horarioExitoso').click(function () {
		window.location.href = "https://localhost:44383/Home/SocioVehiculos";
	});

	$('#horarioError').click(function () {
		window.location.href = "https://localhost:44383/Home/SocioVehiculos";
	})

	var counter = 0
	//Meter aquí un menú de opciones.
	$("#btn-agregar-fila").click(function () {
		var opciones = "<div><input type='radio' id='repetir' value='REPETIR' name='modalidad" + counter + "'>" + "<label for='#repetir'>Repetir</label></div>" +
			"<div><input type='radio' id='continuo' value='CONTINUO' name='modalidad" + counter + "'>" + "<label for='#continuo'>Continuo</label></div>";
		var diaInicial = "<select name='diaInicial' class='diaInicial form-control'>" +
			"<option value=2>Lunes</option>" +
			"<option value=3>Martes</option>" +
			"<option value=4>Miércoles</option>" +
			"<option value=5>Jueves</option>" +
			"<option value=6>Viernes</option>" +
			"<option value=7>Sábado</option>" +
			"<option value=1>Domingo</option>" +
			"</select > ";
		var txtHoraInicio = "<input type='time' class='txtHoraInicio form-control'>";
		var diaFinal = "<select name='diaFinal' class='diaFinal form-control'>" +
			"<option value=2>Lunes</option>" +
			"<option value=3>Martes</option>" +
			"<option value=4>Miércoles</option>" +
			"<option value=5>Jueves</option>" +
			"<option value=6>Viernes</option>" +
			"<option value=7>Sábado</option>" +
			"<option value=1>Domingo</option>" +
			"</select > ";
		var txtHoraFinal = "<input type='time' class='txtHoraFinal form-control' disabled>";
		var eliminar = "<button class='btnDelete'>Eliminar</button>";
		var alerta = "<div class='mensaje alert-danger'></div>"
		var markup = "<tr><td> " + opciones + "</td><td>" + diaInicial + "</td><td>" + txtHoraInicio +
			"</td><td>" + diaFinal + "</td><td>" + txtHoraFinal + "</td><td>" + eliminar + "</td><td>" + alerta + "</td></tr>";
		counter++;
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


	function between(x, min, max) {
		return x >= min && x <= max;
	}

});