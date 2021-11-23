function vVehiculo() {

	
	this.service = 'vehiculo';
	this.serviceTwo = 'documento';
	this.serviceTree = 'horario';
	this.ctrlActions = new ControlActions();
	var vehiculoData = {};
	var finalData = {};
	var documentoData = {};
	var finalDocumento = {};
	var horarioOne = {};
	var finalHorario = {};
	

	

	this.Create = function () {
		
		
        var ranId = Math.floor((Math.random() * 100000) + 1);
		idData = { Id: ranId };
		this.CreateTwo(idData);
		/*
		var datareci = this.CheckifTrue(idData);
		setTimeout(() => {

		if (datareci == true) {
			 ranId = Math.floor((Math.random() * 100000) + 1);
			 idData = { Id: ranId };
				this.CheckifTrue(idData);
			setTimeout(() => {
				 this.CreateTwo(idData);
			}, 5000);
		} else {
			this.CreateTwo(idData);
        }

		}, 5000);
		*/
		
         
	}

	this.CheckifTrue = function (idData) {
         this.ctrlActions.GetToApiId(this.service + "/GetCheck", idData, function (data) {
			 datareci = data;
			 return datareci;
		 });
    }

	this.CreateTwo = function (idData) {
		var opcionesChoice = {
			Tipo: document.getElementById("Tipo").value, Combustible: document.getElementById("Combustible").value,
			Marca: document.getElementById("Marca").value, Modelo: document.getElementById("Modelo").value
		};
		vehiculoData = this.ctrlActions.GetDataForm('frmEdition');
		Object.assign(finalData, idData, opcionesChoice, vehiculoData);

		//console.log(customerData);
		//console.log(documentoData);
		//Hace el post al create
		this.ctrlActions.PostToAPI(this.service + "/Post", finalData, function (data) {
			var vcustomer = new vVehiculo();
			vcustomer.CreateDocum(idData);
		});


		
	}

	this.CreateDocum = function (idData) {

		documentoData = this.ctrlActions.GetDataForm('docuEdition');
		var idDataTwo = { idVehi : idData.Id};
		Object.assign(finalDocumento, documentoData, idDataTwo);

		this.ctrlActions.PostToAPI(this.serviceTwo + "/Post", finalDocumento, function (data) {
			var vcustomer = new vVehiculo();
			vcustomer.CreateHora(idData);
		});
	}


	this.CreateHora = function () {
        horarioOne = this.ctrlActions.GetDataForm('horarioEdition');
		var idDataTree = {Id : idData.Id};
		Object.assign(finalHorario, horarioOne, idDataTree);

		this.ctrlActions.PostToAPI(this.serviceTree + "/Post", finalHorario);
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
  }


}


function buscarUbicacion(lat, lon) {
	latitud = parseFloat(lat);
	longitud = parseFloat(lon);

	map.setCenter(new google.maps.LatLng(latitud, longitud));

	if (marker != null) {
		marker.setMap(null);
	}
	var uluru = { lat: lat, lng: lon};
	marker = new google.maps.Marker({
		uluru,
		map,
	});
}

//ON DOCUMENT READY
$(document).ready(function () {
	initMap();

	document.getElementById("txtImagen").addEventListener("click", function () {
		myWidget.open();
	}, false);
	/*
	document.querySelector('Marca').addEventListener("change", function () {
		var selected = document.getElementById("Marca").value;
		for (var option of document.getElementById("Modelo")) {
			var elementsOfIt = option.value;
			if (elementsOfIt != selected) {
				element.hide();
			} else {
				element.show();
			}

		}

	});
	*/
	

	
	$('#Marca').on('change', function () {
		var selected = $(this).val();
		$("#Modelo option").each(function (item) {
			console.log(selected);
			var element = $(this);
			console.log(element.data("tag"));
			if (element.data("tag") != selected) {
				element.hide();
			} else {
				element.show();
			}
		});

		$("#Modelo").val($("#Modelo option:visible:first").val());
	});
	
	

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

		






var urlImage;
var urlDocumento;


var imagenVehiculo = cloudinary.createUploadWidget({
	cloudName: 'cenfotec2021',
	uploadPreset: 'Cenfo_preset'
}, (error, result) => {
	if (!error && result && result.event === "success") {
		urlImage = result.info.secure_url;
		this.showImage(urlImage);
	}
}
);

function showImage(urlImage) {
	document.getElementById("image").src = urlImage;
}
/*
var DocumentosFiles = cloudinary.createUploadWidget({
	cloudName: 'cenfotec2021',
	uploadPreset: 'Cenfo_preset'
}, (error, result) => {
	if (!error && result && result.event === "success") {
		urlDocumento = result.info.secure_url;
		this.ctrlActions.SubirDocumento(urlDocumento);
	}
}
);





var
	file = (el[0].files ? el[0].files[0] : el[0].value || undefined),
	supportedFormats = ['image/jpg', 'image/gif', 'image/png'];

if (file && file.type) {
	if (0 > supportedFormats.indexOf(file.type)) {
		alert('unsupported format');
	}
	else {
		upload();
	}
}
*/
