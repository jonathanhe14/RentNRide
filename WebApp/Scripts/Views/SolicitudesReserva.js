function SolicitudesReserva() {
	this.reservaService = 'reserva';
	this.tblReservasId = 'tblReservas'
	this.tblReservasIdPEndientes = 'tblReservasPendientes'
	this.ctrlActions = new ControlActionsReservas();

	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(this.reservaService + "/ReservasPorSocio?Correo="+localStorage.getItem('Correo'), this.tblReservasId, false);

	}
	this.RetrieveAllPendientes = function () {
		this.ctrlActions.FillTable(this.reservaService + "/ReservasPorSocioPendientes?Correo="+localStorage.getItem('Correo'), this.tblReservasIdPEndientes, false);

	}
	this.BindFieldsReservasPendientes = function (data) {
		$("#Modificar").show();
		this.ctrlActions.BindFields('frmReservas', data);
	}

}

$(document).ready(function () {
	var ctrSolicitudesReserva = new SolicitudesReserva();
	ctrSolicitudesReserva.RetrieveAll();
	ctrSolicitudesReserva.RetrieveAllPendientes();

});