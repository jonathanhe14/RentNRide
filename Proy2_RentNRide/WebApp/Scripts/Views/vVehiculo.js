function vVehiculo() {

	
	this.service = 'vehiculo';
	this.ctrlActions = new ControlActions();

	var markers = [];

	var posiciones = [];

	var map;

	this.Create = function () {
		var customerData = {};
		var idData = {};
		var documentoData = {};
		var UserOption = document.getElementById('drpTipoVehi').value;
		idData = this.ctrlActions.GetDataForm('idEdition');
		customerData = idData + UserOption + this.ctrlActions.GetDataForm('frmEdition');
		documentoData = idData + this.ctrlActions.GetDataForm('docuEdition');
		console.log(customerData);
		console.log(documentoData);
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.service + "/Post", customerData);

	}

	this.Update = function () {

		var customerData = {};
		customerData = this.ctrlActions.GetDataForm('frmEdition');
		//Hace el post al create
		this.ctrlActions.PutToAPI(this.service, customerData);
		//Refresca la tabla
		this.ReloadTable();

	}

	function initMap() {
		map = new google.maps.Map(document.getElementById('map'), {
			center: { lat: 9.932781, lng: -84.031195 },
			zoom: 18
		});
	}

		function crearMarcador(position) {
			posiciones.push(posiciones);
			console.log(posiciones.length)

			const marker = new google.maps.Marker({
				position,
				map,
			});

			markers.push(marker);

			//centrar el mapa a la posicion del parametro
			map.setCenter(position);

			console.log("Marcador creado");
		}

		function buscarUbicacion(lat, lon) {
			latitud = parseFloat(lat);
			longitud = parseFloat(lon);

			map.setCenter(new google.maps.LatLng(latitud, longitud));
			markers = { lat: latitud, lng: longitud };
			marker = new google.maps.Marker({
				position: markers,
				map: map,
			});
		}


	this.BindFields = function (data) {
		this.ctrlActions.BindFields('frmEdition', data);
	}
	

}

