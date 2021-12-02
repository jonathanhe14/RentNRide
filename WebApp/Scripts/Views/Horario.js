function Horario() {

	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.service = 'horario';

	this.Create = function () {
		var data = {};

		/*data["Id"] = 30041;
		data["Dia"] = "12/31/2001";
		data["horaInicio"] = "05:00";
		data["horaFinal"] = "06:00";*/


		//Hace el post al create
		this.PostToAPI(this.service + "/CrearHorario", data);
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


	this.PostToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			var horario = new Horario();
			horario.ShowMessage('I');

			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var horario = new Horario();
				horario.ShowMessage('E');
			})
	};

}


$(document).ready(function () {
	$("#btn-agregar-fila").click(function () {
		var txtHoraInicio = "<input type='time' class='txtHoraInicio form-control'>";
		var txtHoraFinal = "<input type='time' class='txtHoraFinal form-control' disabled>";
		var opciones = "<button>nada</button>";
		var lunes = "<input type = 'checkbox' class='lunes form-control' value='1'";
		var martes = "<input type = 'checkbox' class='martes form-control' value='2'";
		var miercoles = "<input type = 'checkbox' class='miercoles form-control' value='3'";
		var jueves = "<input type = 'checkbox' class='jueves form-control' value='4'";
		var viernes = "<input type = 'checkbox' class='viernes form-control' value='5'";
		var sabado = "<input type = 'checkbox' class='sabado form-control' value='6'";
		var domingo = "<input type = 'checkbox' class='domingo form-control' value='7'";
		var eliminar = "<button class='btnDelete'>Eliminar</button>";
		var markup = "<tr><td> " + txtHoraInicio + "</td><td>" + txtHoraFinal + "</td><td>" + opciones + "</td><td>" + lunes + "</td><td>" + martes + "</td><td>" + miercoles + "</td><td>" + jueves + "</td><td>" + viernes + "</td><td>" + sabado + "</td><td>" + domingo + "</td><td>" + eliminar + "</td></tr>";
		$("#tbl-horarios").append(markup);
	});

	$("#tbl-horarios").on('click', '.btnDelete', function () {
		$(this).closest('tr').remove();
	});

	$("#tbl-horarios").on('change', '.txtHoraInicio', function () {
		$(this).closest('tr').find('.txtHoraFinal').prop('disabled', false);
		var fecha = new Date(this.valueAsDate);
		var h = fecha.getHours() + 6;
		var m = fecha.getMinutes();
		if (h < 10) h = '0' + h;
		if (m < 10) m = '0' + m;
		$(this).closest('tr').find('.txtHoraFinal').attr({ 'value': h + ':' + m }, { 'min': h + ':' + m });
	});

	var data = [];

	$("#btn-datos").click(function () {
		$('#tbl-horarios > tbody > tr').each(function () {

			var hora_Inicio = $(this).find('.txtHoraInicio').val();
			var hora_Final = $(this).find('.txtHoraFinal').val();

			$(this).find('input[type="checkbox"]').each(function () {
				if ($(this).prop("checked") && hora_Inicio && hora_Final) {
					data.push({
						Dia: $(this).val(),
						horaInicio: hora_Inicio,
						horaFinal: hora_Final
					});
				}
			});

		});

	});

	$("#btn-imprimir").click(function () {
		console.log(data);
	});

});