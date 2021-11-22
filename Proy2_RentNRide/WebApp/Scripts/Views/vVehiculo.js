function vVehiculo() {

	
	this.service = 'vehiculo';
	this.ctrlActions = new ControlActions();

	

	

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

	

		


	this.BindFields = function (data) {
		this.ctrlActions.BindFields('frmEdition', data);
	}

	cloudinary.config({
		cloud_name: "jherrerac@ucenfotec.ac.cr",
		api_key: "YOUR_API_NAME",
		api_secret: "YOUR_API_SECRET"
	});

}

var marker

function initMap() {
	map = new google.maps.Map(document.getElementById('map'), {
		center: { lat: 9.932781, lng: -84.031195 },
		zoom: 18
	});

	

	var posiciones = [];

	

	map.addListener("click", (event) => {
	crearMarcador(event.latLng);
	});


  function crearMarcador(position) {
	posiciones.push(posiciones);
	  /*console.log(posiciones.length)*/
	  if (marker != null) {
		  marker.setMap(null);
	  }

	 marker = new google.maps.Marker({
		position,
		map,
	});

	//centrar el mapa a la posicion del parametro
	map.setCenter(position);
	  document.getElementById('selectPosit').innerHTML = position;
  }


}


//ON DOCUMENT READY
$(document).ready(function () {
	initMap();


});

//function crearMarcador(position) {
//			posiciones.push(posiciones);
//			console.log(posiciones.length)

//			const marker = new google.maps.Marker({
//				position,
//				map,
//			});

//			markers.push(marker);

//			//centrar el mapa a la posicion del parametro
//			map.setCenter(position);

//			console.log("Marcador creado");
//		}

		




function buscarUbicacion(lat, lon) {
	latitud = parseFloat(lat);
	longitud = parseFloat(lon);

	map.setCenter(new google.maps.LatLng(latitud, longitud));

	if (marker != null) {
		marker.setMap(null);
	}

	marker = new google.maps.Marker({
		position,
		map,
	});
}


$('#drpMarca').on('change', function () {
	var selected = $(this).val();
	$("#drpModelo option").each(function (item) {
		console.log(selected);
		var element = $(this);
		console.log(element.data("tag"));
		if (element.data("id") != selected) {
			element.hide();
		} else {
			element.show();
		}
	});

	$("#drpModelo").val($("#expertCat option:visible:first").val());

});