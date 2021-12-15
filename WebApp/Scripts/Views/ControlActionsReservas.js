var dataR = {};
function ControlActionsReservas() {

	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.GetTableColumsDataName = function (tableId) {
		var val = $('#' + tableId).attr("ColumnsDataName");

		return val;
	}

	this.FillTable = function (service, tableId, refresh) {

		if (!refresh) {
			columns = this.GetTableColumsDataName(tableId).split(',');
			var arrayColumnsData = [];


			$.each(columns, function (index, value) {
				var obj = {};
				obj.data = value;
				arrayColumnsData.push(obj);

			});
			console.log(arrayColumnsData);
			$('#' + tableId).DataTable({
				"processing": true,
				"ajax": {
					"url": this.GetUrlApiService(service),
					dataSrc: 'Data'
				},
				"columns": arrayColumnsData
			});


		} else {
			//RECARGA LA TABLA
			$('#' + tableId).DataTable().ajax.reload();

		}

	}

	this.GetSelectedRow = function () {
		var data = sessionStorage.getItem(tableId + '_selected');

		return data;
	};




	this.BindFields = function (formId, data) {
		
		$('#' + formId + ' *').filter(':input').each(function (input) {
			var columnDataName = $(this).attr("ColumnDataName");
			this.value = data[columnDataName];
		});
	}

	this.GetDataForm = function (formId) {
		

		$('#' + formId + ' *').filter(':input').each(function (input) {
			var columnDataName = $(this).attr("ColumnDataName");
			dataR[columnDataName] = this.value;
		});

		console.log(dataR);
		return dataR;
	}
	this.PutToAPIReserva = function (data) {
		var jqxhr = $.post('http://localhost:52125/api/reserva/ActualizarEstadoReserva', data, function (response) {
			location.reload();
		})
			.fail(function (response) {
				var data = response.responseJSON;
				console.log(data);
			})
	};
	this.Aceptar = function () {
		this.GetDataForm('frmReservas')
		dataR["Solicitud"] = "PROCESO";
		this.PutToAPIReserva(dataR);
		
	}
	this.Rechazar = function () {
		this.GetDataForm('frmReservas')
		dataR["Solicitud"] = "RECHAZADA";
		this.PutToAPIReserva(dataR);
		
	}
	this.ShowMessage = function (type, message) {
		if (type == 'E') {
			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text(message);
		} else if (type == 'I') {
			$("#alert_container").removeClass("alert alert-danger alert-dismissable")
			$("#alert_container").addClass("alert alert-success alert-dismissable");
			$("#alert_message").text(message);
		}
		$('.alert').show();
	};

	this.PostToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActionsAdmin();
			ctrlActions.ShowMessage('I', response.Message);

			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActionsAdmin();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.PutToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.put(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActionsAdmin();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}

		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActionsAdmin();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.DeleteToAPI = function (service, data, callbackFunction) {
		var jqxhr = $.delete(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActionsAdmin();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActionsAdmin();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};


	this.GetToApi = function (service, callbackFunction) {
		var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
			console.log("Response " + response.Data);
			callbackFunction(response.Data);

		});
	}
	this.GetToApiFullData = function (service, callbackFunction) {
		var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
			console.log("Response " + response.Data);
			callbackFunction(response);
		});
	}
}