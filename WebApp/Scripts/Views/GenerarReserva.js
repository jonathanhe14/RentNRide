var latitudOriginal;

var longitudOriginal;

var ubi = [];

var data = [];

latitudOriginal = $('.latitud').attr('value');
longitudOriginal = $('.longitud').attr('value');

function ConsultarHorarios() {

	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.service = 'reserva';

	this.Create = function () {
		var usuario = $("#usuario").val();
		var socio = $("#socio").val();
		var totalTarifa = $("#total-tarifa").text();
		var costoEntrega = $("#total-difer").text();
		var latitud = $(".latitud").val();
		var longitud = $(".longitud").val();
		var aceptacion = $("#modalidadAcept").text();
		var solicitud = "";
		if (aceptacion.localeCompare("Si") == 0) {
			solicitud = "EN PROCESO";
		} else {
			solicitud = "PENDIENTE"
		}

		$('#tabla-consultas > tbody > tr').each(function () {
			if ($(this).find('.consulta').is(":checked")) {
				var idVehiculo = Number($(this).find('#id-vehiculo').text());
				var today = new Date();
				var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
				var fechaReserva = $(this).find('#fecha-reserva').text();
				//var date_test = new Date(fechaReserva);
				console.log(fechaReserva);

				var horaInicio = $(this).find('#hora-inicio').text();
				var horaFinal = $(this).find('#hora-fin').text();

				data.push(
					{
						Id_Vehiculo: idVehiculo,
						Fecha: date,
						FechaReserva: fechaReserva,
						HoraInicio: horaInicio,
						HoraFin: horaFinal,
						Usuario: usuario,
						Socio: socio,
						Tarifa: totalTarifa,
						Comision: 0,
						MalEstado: 0,
						Entrega: costoEntrega,
						KmExcedido: 0,
						KmInicial: 15000,
						KmFinal: 0,
						Latitud: latitud,
						Longitud: longitud,
						Solicitud: solicitud,
						CalifSocio: 0,
						CalifUsuario: 0,
						CodigoQR: "https://res.cloudinary.com/cenfotec2021/image/upload/v1637813540/rhfyfpomvedv65amiqxd.jpg",
					}
				);
			}
			/*function toDate(dateStr) {
				var parts = dateStr.split("-")
				return new Date(parts[2], parts[1] - 1, parts[0])
			}*/

		});

		var datos = JSON.stringify(data);

		console.log(datos);

		this.PostToAPI_Json(this.service + "/CrearReserva", datos);
	}

	this.ShowMessage = function (type, mensaje) {
		if (type == 'E') {
			console.log("lanzar alerta error")

			$('#modalErr').modal('show');
			$('#modalErr .modal-body').text(mensaje);
			//this.Limpiar();
		} else if (type == 'I') {
			//console.log("lanzar alerta")
			$('#modalSuccess').modal('show');
			$('#modalSuccess .modal-body').text(mensaje);
		}

	};

	this.PostToAPI_Json = function (service, data, callBackFunction) {
		$.ajax({
			url: this.GetUrlApiService(service),
			type: "POST",
			data: data,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {
				var ctrlActions = new ConsultarHorarios();
				ctrlActions.ShowMessage('I', response.Message);
				if (callBackFunction) {
					callBackFunction(response.Data);
				}
			},
			error: function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ConsultarHorarios();
				console.log(data.ExceptionMessage);
				console.log(response.Message);
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
			}
		})
	};
}

$(document).ready(function () {

	var rowCount = $('#tabla-consultas > tbody > tr').length;
	if (rowCount < 1) {
		$('#btnCreate').attr('disabled', 'disabled');
	} else {
		$('#btnCreate').removeAttr('disabled');
	}

	$('#ingresarParametros').click(function () {
		window.location.href = "https://localhost:44383/Home/BusquedaVehiculos";
	});

	$('#irMonedero').click(function () {
		window.location.href = "https://localhost:44383/Home/BusquedaVehiculos";
	});

	$("input[type='checkbox']").change(function (e) {
		if ($(this).is(":checked")) { //If the checkbox is checked
			$(this).closest('tr').addClass("alert-success");

			var numHoras = contarFilas()

			function contarFilas() {
				var resultado = 0;
				$('#tabla-consultas > tbody > tr').each(function () {
					if ($(this).find('.consulta').is(":checked")) {
						resultado = resultado + 1;
					}

				});
				return resultado;
			}

			var q_lugar = parseFloat($("#q-lugar-diferente").text());
			var p_lugar = parseFloat($("#precio-difer").text());
			var total = q_lugar * p_lugar;
			$("#total-difer").text(total.toString());


			$("#q-horas").text(numHoras.toString());
			var q_horas = parseFloat($("#q-horas").text());
			var p_horas = parseFloat($("#precio-tarifa").text());
			var total_horas = q_horas * p_horas;
			$("#total-tarifa").text(total_horas.toString());

			var total_maximo = total + total_horas;
			$("#total-general").text(total_maximo.toString());

			numhoras = 0;

		} else {
			$(this).closest('tr').removeClass("alert-success");

			var numHoras = contarFilas()

			function contarFilas() {
				var resultado = 0;
				$('#tabla-consultas > tbody > tr').each(function () {
					if ($(this).find('.consulta').is(":checked")) {
						resultado = resultado + 1;
					}

				});
				return resultado;
			}

			var q_lugar = parseFloat($("#q-lugar-diferente").text());
			var p_lugar = parseFloat($("#precio-difer").text());
			var total = q_lugar * p_lugar;
			$("#total-difer").text(total.toString());

			$("#q-horas").text(numHoras.toString());
			var q_horas = parseFloat($("#q-horas").text());
			var p_horas = parseFloat($("#precio-tarifa").text());
			var total_horas = q_horas * p_horas;
			$("#total-tarifa").text(total_horas.toString());

			var total_maximo = total + total_horas;
			$("#total-general").text(total_maximo.toString());

			numhoras = 0;
		}
	});




})