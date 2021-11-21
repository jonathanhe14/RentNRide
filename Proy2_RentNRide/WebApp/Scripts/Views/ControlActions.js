var ubi = [];
var PersoneriaJuridica = "NULL";
var PermisoOperaciones = "NULL";
function ControlActions() {


	this.URL_API = "http://localhost:52125/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}





	this.BindFields = function (formId, data) {
		console.log(data);

		$('#' + formId + ' *').filter(':input').each(function (input) {
			var columnDataName = $(this).attr("ColumnDataName");
			this.value = data[columnDataName];
		});
	}



	this.Ubicacion = function (lat, lng) {
		ubi.push(lat);
		ubi.push(lng);
	}

	this.SubirPersoneria = function (url) {
		PersoneriaJuridica = url;
	}

	this.SubirOperaciones = function (url) {
		PermisoOperaciones = url;
	}
	this.GetDataForm = function (formId) {

		var data = {};

		$('#' + formId + ' *').filter(':input').each(function (input) {
			var columnDataName = $(this).attr("ColumnDataName");
			data[columnDataName] = this.value;
			//console.log(columnDataName, this.value);
		});


		data["Latitud"] = ubi[0];
		data["Longitud"] = ubi[1];
		data["PersoneriaJuridica"] = PersoneriaJuridica;
		data["PermisoOperaciones"] = PermisoOperaciones;


		//console.log(data);

		return data;
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
			var ctrlActions = new ControlActions();
			ctrlActions.ShowMessage('I', response.Message);

			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.PutToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.put(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActions();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}

		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.DeleteToAPI = function (service, data, callbackFunction) {
		var jqxhr = $.delete(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActions();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};


	this.GetToApi = function (service, callbackFunction) {
		var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
			console.log("Response " + response);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}

		});
	}
}
//Custom jquery actions

