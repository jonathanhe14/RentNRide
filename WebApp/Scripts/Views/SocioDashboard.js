function SocioDashboard() {

	
	

	this.tblUsuariosId = 'tblVehiculos';
	this.usersService = 'vehiculo';
	this.socioService = 'socio';
	this.ctrlActions = new ControlActionsAdmin();

	

	this.Create = function () {
		
	}

	this.getDatas = function () {
		//U?correo=" + localStorage.getItem("Correo")
		this.ctrlActions.GetToApi(this.usersService + "/GetData?correo=" + localStorage.getItem("Correo"), function (data) {
			document.getElementById("vehiculoCant").innerHTML = data;
		});

		this.ctrlActions.GetToApi(this.socioService + "/GetMembresiaSocio?correo=" + localStorage.getItem("Correo"), function (data) {
			document.getElementById("nombreMem").innerHTML = data.Nombre;
			document.getElementById("montoMensual").innerHTML = data.MontoMensual;
			document.getElementById("ComTrans").innerHTML = data.ComisionTransaccion;
			document.getElementById("fechaCreate").innerHTML = data.FechaCreacion;
			document.getElementById("daysLeft").innerHTML = data.NumDias;
		});
		//et
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

	
		//cloud_name: "jherrerac@ucenfotec.ac.cr",
	
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
	  var str = document.getElementById('selectPosit').innerHTML;
      var partsOfStr = str.replace(/[()]/g, '');
	  var pieces = partsOfStr.split(',');
	  console.log(pieces);
	  document.getElementById('Latitud').innerHTML = pieces[0];
	  document.getElementById('Longitud').innerHTML = pieces[1];
  }


}



//----------------------------------------------------- ON DOCUMENT READY -----------------------------------------------------------
$(document).ready(function () {
	
	var vehi = new SocioDashboard();
	vehi.getDatas();

});


