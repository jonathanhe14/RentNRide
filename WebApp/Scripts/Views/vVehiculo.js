﻿function vVehiculo() {

	
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
		//revisa si algo esta empty
		//if (!document.getElementById("image")) {
		//	console.log("pee");
		//} else {
		//	console.log("poo");
  //      }

		if (document.getElementById("Tipo").value == "null" || document.getElementById("Combustible").value == "null" ||
			document.getElementById("Marca").value == "null" || document.getElementById("Modelo").value == "null") {

			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text("Alguna seleccion no ha sido seleccionada");
			$('.alert').show();

		} else if (document.getElementById("txtKilometraje").value == "" ||
			document.getElementById("txtcKmExcedido").value == "" ||
			document.getElementById("txtcMalEstado").value == "" ||
			document.getElementById("txtcLugarDiferente").value == "" ||
			document.getElementById("txtTarifa").value == "" ||
			document.getElementById("Latitud").value == "" ||
			document.getElementById("Longitud").value == "" ||
			!document.getElementById("image")) {


			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text("Falta informacion del vehiculo");
			$('.alert').show();

		} else if (document.getElementById("txtMarchamo").src == "https://localhost:44383/Home/null" || document.getElementById("txttituloPropiedad").src == "https://localhost:44383/Home/null" ||
			document.getElementById("txtRiteve").src == "https://localhost:44383/Home/null" || document.getElementById("txtderechoCirculacion").src == "https://localhost:44383/Home/null") {

			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text("Falta subir documentos del vehiculo");
			$('.alert').show();
			/*
		} else if (document.forms["txtDia"].value == "" ||
			!document.getElementById("txthoraInicio").innerHTML.replace(/\s/g, '').length ||
			!document.getElementById("txthoraFinal").innerHTML.replace(/\s/g, '').length) {

			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text("Falta informacion de horario");
			$('.alert').show();
			*/
		} else {
			
            var ranId = Math.floor((Math.random() * 100000) + 1);
			idData = { Id: ranId };
			this.CreateTwo(idData);
        }
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
		var imges = "";
		switch (imgNums) {
			case 1:
				imges = document.getElementById("image").src;
				break;
			case 2:
				imges = document.getElementById("image").src + " , " + document.getElementById("image2").src;
				break;
			case 3:
				imges = document.getElementById("image").src + " , " + document.getElementById("image2").src
					+ " , " + document.getElementById("image3").src;
				break;
			case 4:
				imges = document.getElementById("image").src + " , " + document.getElementById("image2").src
					+ " , " + document.getElementById("image3").src + " , " + document.getElementById("image4").src;
				break;
			case 5:
				imges = document.getElementById("image").src + " , " + document.getElementById("image2").src
					+ " , " + document.getElementById("image3").src + " , " + document.getElementById("image4").src
					+ " , " + document.getElementById("image5").src;
				break;
			default:
			// code block
		}

		var loggedUser = this.ctrlActions.GetCurrentEmail();

		var opcionesChoice = {
			Tipo: document.getElementById("Tipo").value, Combustible: document.getElementById("Combustible").value,
			Marca: document.getElementById("Marca").value, Modelo: document.getElementById("Modelo").value,
			AccptInmediata: document.getElementById("txtAccptInmediata").value, Estado: document.getElementById("txtEstado").value,
			Imagen: imges, Latitud: document.getElementById("Latitud").innerHTML,
			Longitud: document.getElementById("Longitud").innerHTML, idUsuario: loggedUser
		};
		vehiculoData = this.ctrlActions.GetDataFormVehi('frmEdition');
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

		documentoData = {
			Marchamo: document.getElementById("txtMarchamo").src, tituloPropiedad: document.getElementById("txttituloPropiedad").src,
			Riteve: document.getElementById("txtRiteve").src, derechoCirculacion: document.getElementById("txtderechoCirculacion").src
        }
		var idDataTwo = { idVehi : idData.Id};
		Object.assign(finalDocumento, documentoData, idDataTwo);

		this.ctrlActions.PostToAPI(this.serviceTwo + "/Post", finalDocumento, function (data) {
			var vcustomer = new vVehiculo();
			vcustomer.CreateHora(idData);
		});
	}

	

	this.CreateHora = function (idData) {
		horarioOne = this.ctrlActions.GetDataFormVehi('horarioEdition');
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
	  var str = document.getElementById('selectPosit').innerHTML;
      var partsOfStr = str.replace(/[()]/g, '');
	  var pieces = partsOfStr.split(',');
	  console.log(pieces);
	  document.getElementById('Latitud').innerHTML = pieces[0];
	  document.getElementById('Longitud').innerHTML = pieces[1];
  }


}




var urlImage;
var urlDocumento;
var imgNums = 0;

function checkUploadResult (resultEvent) {
	if (resultEvent && resultEvent.event === "success") {
		urlImage = resultEvent.info.secure_url;
		this.showImage(urlImage);
	}
};

function checkUploadResult1(resultEvent) {
	if (resultEvent && resultEvent.event === "success") {
		urlImage = resultEvent.info.secure_url;
		this.showImage1(urlImage);
	}
};

function checkUploadResult2(resultEvent) {
	if (resultEvent && resultEvent.event === "success") {
		urlImage = resultEvent.info.secure_url;
		this.showImage2(urlImage);
	}
};

function checkUploadResult3(resultEvent) {
	if (resultEvent && resultEvent.event === "success") {
		urlImage = resultEvent.info.secure_url;
		this.showImage3(urlImage);
	}
};

function checkUploadResult4(resultEvent) {
	if (resultEvent && resultEvent.event === "success") {
		urlImage = resultEvent.info.secure_url;
		this.showImage4(urlImage);
	}
};

//image
function showImage(urlImage) {
	
	if (imgNums == 0) {
		var img = document.createElement('img');
		img.src = urlImage;
		img.id = 'image';
		document.getElementById('extraImages').appendChild(img);
		imgNums++
	} else if (imgNums < 4) {
		//createimagethingindivide
		imgNums++
		var img = document.createElement('img');
		img.src = urlImage;
		img.id = 'image' + imgNums;
		document.getElementById('extraImages').appendChild(img);
	} else if (imgNums == 4) {
		//remove button
		imgNums++
		var img = document.createElement('img');
		img.src = urlImage;
		img.id = 'image' + imgNums;
		document.getElementById('extraImages').appendChild(img);
		document.getElementById("txtImagen").remove();
    }
}

//the four documents for the vehiculo
function showImage1(urlImage) {
	document.getElementById("txtMarchamo").src = urlImage;
}

function showImage2(urlImage) {
	document.getElementById("txttituloPropiedad").src = urlImage;
}

function showImage3(urlImage) {
	document.getElementById("txtRiteve").src = urlImage;
}

function showImage4(urlImage) {
	document.getElementById("txtderechoCirculacion").src = urlImage;
}

let myWidget = window.cloudinary.createUploadWidget({
		cloudName: 'ucenfotecp2last21',
		uploadPreset: 'ImageOnlyVehiculo'
}, (error, result) => { this.checkUploadResult(result) })

let myWidget1 = window.cloudinary.createUploadWidget({
	cloudName: 'ucenfotecp2last21',
	uploadPreset: 'DocumentOnlyVehi'
}, (error, result) => { this.checkUploadResult1(result) })

let myWidget2 = window.cloudinary.createUploadWidget({
	cloudName: 'ucenfotecp2last21',
	uploadPreset: 'DocumentOnlyVehi'
}, (error, result) => { this.checkUploadResult2(result) })

let myWidget3 = window.cloudinary.createUploadWidget({
	cloudName: 'ucenfotecp2last21',
	uploadPreset: 'DocumentOnlyVehi'
}, (error, result) => { this.checkUploadResult3(result) })

let myWidget4 = window.cloudinary.createUploadWidget({
	cloudName: 'ucenfotecp2last21',
	uploadPreset: 'DocumentOnlyVehi'
}, (error, result) => { this.checkUploadResult4(result) })




//ON DOCUMENT READY
$(document).ready(function () {
	initMap();


	
	//4 documents de vehiculo tambien

	document.getElementById("txtImagen").addEventListener("click", function () {
		myWidget.open();
	}, false);

	document.getElementById("doc1").addEventListener("click", function () {
		myWidget1.open();
	}, false);

	document.getElementById("doc2").addEventListener("click", function () {
		myWidget2.open();
	}, false);

	document.getElementById("doc3").addEventListener("click", function () {
		myWidget3.open();
	}, false);

	document.getElementById("doc4").addEventListener("click", function () {
		myWidget4.open();
	}, false);

	

	//set it so it orders things of marca con modelo
	$('#Marca').on('change', function () {
		var selected = $(this).val();
		$("#Modelo option").each(function (item) {
			//console.log(selected);
			var element = $(this);
			//console.log(element.data("tag"));
			if (element.data("tag") != selected) {
				element.hide();
			} else {
				element.show();
			}
		});

		$("#Modelo").val($("#Modelo option:visible:first").val());
	});

	//llena los eelects con una opcion nula para empezar el codigo de arriba propiamente
	
	fillEmptySelect();

	$(function () {
		var dtToday = new Date();

		var month = dtToday.getMonth() + 1;
		var day = dtToday.getDate();
		var year = dtToday.getFullYear();

		if (month < 10)
			month = '0' + month.toString();
		if (day < 10)
			day = '0' + day.toString();

		var maxDate = year + '-' + month + '-' + day;
		$('#txtDia').attr('min', maxDate);
	});
});


function fillEmptySelect() {
	var selectsToEmptyFill = [Tipo, Combustible, Marca, Modelo];
	for (var i = 0; i < selectsToEmptyFill.length; i++) {
		var select = selectsToEmptyFill[i];
		var opt = document.createElement('option');
		opt.value = null;
		opt.defaultSelected = true;
		opt.innerHTML = "-- seleccione una opcion --";
		select.appendChild(opt);
	}
}

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