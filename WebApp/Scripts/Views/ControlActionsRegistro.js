var ubi = [];
var PersoneriaJuridica = "NULL";
var PermisoOperaciones = "NULL";
function ControlActionsRegistro() {

	//this.ctrVerificacion = new Verificacion();
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
	this.GetDataFormUsuarios = function (formId) {

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
		localStorage.setItem("Correo", data["Correo"]);
		localStorage.setItem("Telefono", data["Telefono"]);
		//console.log(data);

		return data;
	}
	this.getDataFormVerificacion = function (formId) {
		var data = {};

		$('#' + formId + ' *').filter(':input').each(function (input) {
			var columnDataName = $(this).attr("ColumnDataName");
			data[columnDataName] = this.value;

		});
		data["Telefono"] = localStorage.getItem("Telefono");
		data["Correo"] = localStorage.getItem("Correo");
		return data;
	}

	this.ShowMessage = function (type, message) {
		if (type == 'E') {
			//console.log("fallo")
			//$("#alert_container").removeClass("alert alert-success alert-dismissable")
			//$("#alert_container").addClass("alert alert-danger alert-dismissable");
			//$("#alert_message").text(message);
		} else if (type == 'I') {
			console.log("Todo bien");
			$('#modalSuccess').modal('show');
		}

	};

	this.PostToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActionsRegistro();
			ctrlActions.ShowMessage('I', response.Message);

			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActionsRegistro();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.PutToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.put(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActionsRegistro();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}

		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActionsRegistro();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.DeleteToAPI = function (service, data, callbackFunction) {
		var jqxhr = $.delete(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActionsRegistro();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActionsRegistro();
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

