function SocioVehiculos() {

	
	

	this.tblUsuariosId = 'tblVehiculos';
	this.usersService = 'vehiculo';
	this.listService = 'List';
	this.ctrlActions = new ControlActionsAdmin();
	

	this.Create = function () {
		
	}

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
					default:
						break;
				}
			}
		}

    }

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(this.usersService + "/GetV?correo=" + localStorage.getItem("Correo"), this.tblUsuariosId, true, function (data) {
			this.replaceEach();
		});
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
	
	var usuarios = new SocioVehiculos();
	usuarios.RetrieveAll();

});


