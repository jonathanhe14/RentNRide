function SocioVehiculos() {




	this.tblUsuariosId = 'tblVehiculos';
	this.usersService = 'vehiculo';
	this.listService = 'List';
	this.service = 'vehiculo';
	this.serviceTwo = 'documento';
	var vehiculoData = {};
	var finalData = {};
	var documentoData = {};
	var finalDocumento = {};
	this.ctrlActions = new ControlActionsAdmin();
	//e


	this.Create = function () {
		if (this.checkIfError) {
			var ranId = Math.floor((Math.random() * 100000) + 1);
			idData = { Id: ranId };
			this.CreateTwo(idData);

			//Hace el post al create
			this.ctrlActions.PostToAPI(this.service + "/Post", finalData, function (data) {
				var vcustomer = new SocioVehiculos();
				vcustomer.CreateDocum(idData);
				vcustomer.SendCreateDocu();
			});
		}
	}

	this.SendCreateDocu = function () {
		this.ctrlActions.PostToAPI(this.serviceTwo + "/Post", finalDocumento, function (data) {
			var vcustomer = new SocioVehiculos();
			vcustomer.ReloadTable();
		});
	}

	this.SendUpdateVehi = function () {
		if (this.checkIfError) {
			if (currIdData == 0) {
				$("#alert_container").removeClass("alert alert-success alert-dismissable")
				$("#alert_container").addClass("alert alert-danger alert-dismissable");
				$("#alert_message").text("Para actualizar un vehiculo, por favor seleccione el vehiculo de la tabla que quiere actualizar");
				$('.alert').show();
			} else {
				idData = { Id: currIdData };
				this.CreateTwo(idData);

				//Hace el post al create
				this.ctrlActions.PutToAPI(this.service + "/Put", finalData, function (data) {
					var vcustomer = new SocioVehiculos();
					vcustomer.CreateDocum(idData);
					vcustomer.SendUpdateDocu();

				});
			}

		}
	}

	this.SendUpdateDocu = function () {
		this.ctrlActions.PutToAPI(this.serviceTwo + "/Put", finalDocumento, function (data) {
			var vcustomer = new SocioVehiculos();
			vcustomer.ReloadTable();
		});
		currIdData = 0;
	}

	this.checkIfError = function () {
		if (document.getElementById("Tipo").value == "null" || document.getElementById("Combustible").value == "null" ||
			document.getElementById("Marca").value == "null" || document.getElementById("Modelo").value == "null") {

			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text("Alguna seleccion no ha sido seleccionada");
			$('.alert').show();
			return false;

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

			return false;

		} else if (document.getElementById("txtMarchamo").src == "https://localhost:44383/Home/null" || document.getElementById("txttituloPropiedad").src == "https://localhost:44383/Home/null" ||
			document.getElementById("txtRiteve").src == "https://localhost:44383/Home/null" || document.getElementById("txtderechoCirculacion").src == "https://localhost:44383/Home/null") {

			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text("Falta subir documentos del vehiculo");
			$('.alert').show();
			return false;
		} else {


			return true;
		}
	}

	this.CreateTwo = function (idData) {
		var imges = "";

		var days = document.getElementById('extraImages').children;
		for (var u = 0; u < days.length; u++) {
			if (u == 0) {
			imges = days[u].src;
			} else {
			imges = imges +" , "+ days[u].src;
            }
		}

		var loggedUser = localStorage.getItem("Correo");

		var opcionesChoice = {
			Tipo: document.getElementById("Tipo").value, Combustible: document.getElementById("Combustible").value,
			Marca: document.getElementById("Marca").value, Modelo: document.getElementById("Modelo").value,
			AccptInmediata: document.getElementById("txtAccptInmediata").value, Estado: document.getElementById("txtEstado").value,
			Imagen: imges, Latitud: document.getElementById("Latitud").innerHTML,
			Longitud: document.getElementById("Longitud").innerHTML, idUsuario: loggedUser
		};
		vehiculoData = this.ctrlActions.GetDataForm('frmEdition');
		Object.assign(finalData, idData, opcionesChoice, vehiculoData);

		//console.log(customerData);
		//console.log(documentoData);
	}

	this.CreateDocum = function (idData) {

		documentoData = {
			Marchamo: document.getElementById("txtMarchamo").src, tituloPropiedad: document.getElementById("txttituloPropiedad").src,
			Riteve: document.getElementById("txtRiteve").src, derechoCirculacion: document.getElementById("txtderechoCirculacion").src
		}
		var idDataTwo = { idVehi: idData.Id };
		Object.assign(finalDocumento, documentoData, idDataTwo);


	}

	this.RemoveVehiculo = function () {
		if (currIdData == 0) {
			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text("Para eliminar un vehiculo, por favor seleccione el vehiculo de la tabla que quiere eliminar");
			$('.alert').show();
		} else {
			idData = { Id: currIdData };
			//Hace el post al create
			this.ctrlActions.PutToAPI(this.service + "/Delete", idData, function (data) {
				var vcustomer = new SocioVehiculos();
				vcustomer.ReloadTable();
			});
		}
    }

	//table stuuuuffffffffff
	this.RetrieveAll = function () {
		//U?correo=" + localStorage.getItem("Correo")
		this.ctrlActions.FillTable(this.usersService + "/GetV?correo=" + localStorage.getItem("Correo"), this.tblUsuariosId, false);
		//va uno por uno por las listas
		this.ctrlActions.GetToApiFullData(this.listService + "/Get?id=LST_tipoVehi", function (data) {
			var thisOne = new SocioVehiculos
			thisOne.RetrieveAll2(data);
		});
	}

	this.RetrieveAll2 = function (dataTipo) {
		this.ctrlActions.GetToApiFullData(this.listService + "/Get?id=LST_tipoCombu", function (data) {
			var thisOne = new SocioVehiculos
			thisOne.RetrieveAll3(dataTipo, data);
		});
	}

	this.RetrieveAll3 = function (dataTipo, dataCombu) {
		this.ctrlActions.GetToApiFullData(this.listService + "/Get?id=LST_tipoMarca", function (data) {
			var thisOne = new SocioVehiculos
			thisOne.RetrieveAll4(dataTipo, dataCombu, data);
		});
	}

	this.RetrieveAll4 = function (dataTipo, dataCombu, dataMarca) {
		this.ctrlActions.GetToApiFullData(this.listService + "/Get?id=LST_tipoModelo", function (data) {
			var thisOne = new SocioVehiculos
			thisOne.replaceEach(dataTipo, dataCombu, dataMarca, data);
		});
	}

	this.replaceEach = function (dataTipo, dataCombus, dataMarca, dataModel) {
		var table = document.getElementById("tblVehiculos");
		for (var ta = 0, row; row = table.rows[ta]; ta++) {

			for (var j = 0, col; col = row.cells[j]; j++) {
				/*
				if (col == "Tipo") {
					for (var i = 0; i < data1.length; i++) {
						if (col.innerHTML == data1[i].id) {
							col.innerHTML = data1[i].nombre;
						}
					}
				}
				*/
				switch (col.cellIndex) {
					case 0:
						for (var i = 0; i < dataTipo.length; i++) {
							if (col.innerHTML == dataTipo[i].id) {
								col.innerHTML = dataTipo[i].nombre;
							}
						}
						break;
					case 1:
						for (var i = 0; i < dataCombus.length; i++) {
							if (col.innerHTML == dataCombus[i].id) {
								col.innerHTML = dataCombus[i].nombre;
							}
						}
						break;
					case 2:
						for (var i = 0; i < dataModel.length; i++) {
							if (col.innerHTML == dataModel[i].id) {
								col.innerHTML = dataModel[i].nombre;
							}
						}
						break;
					case 3:
						for (var i = 0; i < dataMarca.length; i++) {
							if (col.innerHTML == dataMarca[i].id) {
								col.innerHTML = dataMarca[i].nombre;
							}
						}
						break;
					case 13:
						if (col.innerHTML != "Imagen") {
							var imgs = col.innerHTML;
							col.innerHTML = "";
							var cutted = imgs.split(" , ");
							for (var i = 0; i < cutted.length; i++) {
								var img = document.createElement('img');
								img.src = cutted[i];
								img.className = "imageRestr";
								img.id = imgs;
								col.appendChild(img);

							}
						}

						break;
					default:
						break;
				}
				data1 = dataTipo;
				data2 = dataCombus;
				data3 = dataMarca;
				data4 = dataModel;
			}
		}

	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.usersService + "/GetV?correo=" + localStorage.getItem("Correo"), this.tblUsuariosId, true);
		this.replaceEach(data1, data2, data3, data4);
	}





	this.BindFields = function (data) {
		//llena campos
		currIdData = data.Id;
		document.getElementById("Tipo").value = data.Tipo;
		document.getElementById("Combustible").value = data.Combustible;
		document.getElementById("Marca").value = data.Marca;
		document.getElementById("Modelo").value = data.Modelo;
		document.getElementById("txtKilometraje").value = data.Kilometraje;
		document.getElementById("txtcKmExcedido").value = data.cKmExcedido;
		document.getElementById("txtcMalEstado").value = data.cMalEstado;

		//console.log(pieces);
		document.getElementById('Latitud').innerHTML = data.Latitud;
		document.getElementById('Longitud').innerHTML = data.Longitud;

		document.getElementById("txtcLugarDiferente").value = data.cLugarDiferente;
		document.getElementById("txtTarifa").value = data.Tarifa;
		document.getElementById("txtAccptInmediata").value = data.AccptInmediata;
		document.getElementById("txtEstado").value = data.Estado;

		document.getElementById("extraImages").innerHTML = "";
		var imgs = data.Imagen;
		var cutted = imgs.split(" , ");
		for (var i = 0; i < cutted.length; i++) {
			//createimagethingindivide
			imgNums++
			var img = document.createElement('img');
			img.src = cutted[i];
			img.id = 'image' + imgNums;
			img.onclick = function() { selectImage(this); };
			img.className = 'imageRestr';
			document.getElementById('extraImages').appendChild(img);
		}

		this.ctrlActions.GetToApi(this.serviceTwo + "/Get?id=" + currIdData, function (data) {
			document.getElementById("txtMarchamo").src = data.Marchamo;
			document.getElementById("txttituloPropiedad").src = data.tituloPropiedad;
			document.getElementById("txtRiteve").src = data.Riteve;
			document.getElementById("txtderechoCirculacion").src = data.derechoCirculacion;
		});
	}


	//cloud_name: "jherrerac@ucenfotec.ac.cr",

}

var currIdData = 0;
var data1 = "";
var data2 = "";
var data3 = "";
var data4 = "";


//----------------------------------------------------- ON DOCUMENT READY -----------------------------------------------------------
$(document).ready(function () {

	var usuarios = new SocioVehiculos();
	usuarios.RetrieveAll();

	initMap();

	document.getElementById("remImagen").addEventListener("click", removeImage);

	//4 documents de vehiculo tambien

	document.getElementById("txtImagen").addEventListener("click", function () {
		myWidget.open();
	}, false);

	document.getElementById("updImagen").addEventListener("click", function () {
		if (selectedImageId != "") {
            myWidgetUpd.open();
		} else {
			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text("Escoga una imagen para actualizar");
			$('.alert').show();
		}
		
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


let myWidget = window.cloudinary.createUploadWidget({
	cloudName: 'ucenfotecp2last21',
	uploadPreset: 'ImageOnlyVehiculo'
}, (error, result) => { this.checkUploadResult(result) })

let myWidgetUpd = window.cloudinary.createUploadWidget({
	cloudName: 'ucenfotecp2last21',
	uploadPreset: 'ImageOnlyVehiculo'
}, (error, result) => { this.checkUploadResultUpd(result) })

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

var urlImage;
var urlDocumento;
var imgNums = 0;

function checkUploadResult(resultEvent) {
	if (resultEvent && resultEvent.event === "success") {
		urlImage = resultEvent.info.secure_url;
		this.showImage(urlImage);
	}
};

function checkUploadResultUpd(resultEvent) {
	if (resultEvent && resultEvent.event === "success") {
		urlImage = resultEvent.info.secure_url;
		this.showImageUpd(urlImage);
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


//image sleelction
var imgProp = {

	'borderStyle': 'solid',
	'borderColor': '#2dff87'
};

var selectedImageId = "";

function selectImage(img) {
	var days = document.getElementById('extraImages').children;
	selectedImageId = img.id;
	for (var u = 0; u < days.length; u++) {
		var tempId = days[u].id;
		document.getElementById(tempId).style.border = 'none';
	}
	document.getElementById(selectedImageId).style.borderStyle = imgProp.borderStyle;
	document.getElementById(selectedImageId).style.borderColor = imgProp.borderColor;
	/*
	this.style.border = 'none';
	this.style.borderStyle = imgProp.borderStyle;
	this.style.borderColor = imgProp.borderColor;
	*/
}

function removeImage() {
	if (selectedImageId != "") {
		document.getElementById(selectedImageId).remove();
		selectedImageId = "";
		document.getElementById("txtImagen").style.display = "block";
	} else {
		$("#alert_container").removeClass("alert alert-success alert-dismissable")
		$("#alert_container").addClass("alert alert-danger alert-dismissable");
		$("#alert_message").text("Escoga una imagen para quitar");
		$('.alert').show();
	}
}


//image
function showImage(urlImage) {
	var element = document.getElementById("extraImages");
	var numberOfChildren = element.getElementsByTagName('*').length

	if (numberOfChildren < 4) {
		//createimagethingindivide
		imgNums++
		var img = document.createElement('img');
		img.src = urlImage;
		img.id = 'image' + imgNums;
		img.onclick = 'selectImage(this)';
		img.className = 'imageRestr';
		document.getElementById('extraImages').appendChild(img);
	} else if (numberOfChildren == 4) {
		//remove button
		imgNums++
		var img = document.createElement('img');
		img.src = urlImage;
		img.id = 'image' + imgNums;
		img.onclick = 'selectImage(this)';
		img.setAttribute("onclick", "selectImage(this);");
		img.className = 'imageRestr';
		document.getElementById('extraImages').appendChild(img);
		document.getElementById("txtImagen").style.display = "none";
	}
}

function showImageUpd(urlImage) {
	document.getElementById(selectedImageId).src = urlImage;
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
		//console.log(pieces);
		document.getElementById('Latitud').innerHTML = pieces[0];
		document.getElementById('Longitud').innerHTML = pieces[1];
	}


}

