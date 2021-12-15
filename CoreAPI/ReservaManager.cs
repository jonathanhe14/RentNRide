using DataAccess.Crud;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class ReservaManager : BaseEntity
    {
        private ReservaCrudFactory crudReserva;
        private FacturaCrudFactory crudFactura;
        private UsuariosCrudFactory crudUsuarios;
        private VehiculoCrudFactory crudVehiculos;


        public ReservaManager()
        {
            crudReserva = new ReservaCrudFactory();
            crudFactura = new FacturaCrudFactory();
            crudUsuarios = new UsuariosCrudFactory();
            crudVehiculos = new VehiculoCrudFactory();
        }

        public void Create(Reserva reserva, int horas)
        {
            try
            {
                if (!reserva.Solicitud.Equals("PENDIENTE"))
                {
                    Usuarios usuario = new Usuarios
                    {
                        Correo = reserva.Usuario,
                        Telefono = ""
                    };
                    usuario = crudUsuarios.Retrieve<Usuarios>(usuario);
                    var mng = new UsuariosManagement();
                    Monedero monedero = new Monedero();
                    monedero = mng.RetrieveMonedero(usuario.Correo);
                    if ((reserva.Tarifa * horas + reserva.Entrega) > monedero.Saldo)
                    {
                        throw new BussinessException(7);
                    }
                    crudReserva.Create(reserva);
                    monedero.Saldo = monedero.Saldo - (reserva.Tarifa * horas + reserva.Entrega);
                    mng.PutMonedero(monedero);
                }
                else
                {
                    crudReserva.Create(reserva);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void CrearFactura(List<Reserva> lstReservas)
        {
            Reserva reserva = lstReservas[0];
            Vehiculo vehiculoReserva = new Vehiculo();
            vehiculoReserva.Id = reserva.Id_Vehiculo;
            Vehiculo vehiculo = crudVehiculos.Retrieve<Vehiculo>(vehiculoReserva);
            Usuarios usuario = new Usuarios();
            usuario.Correo = reserva.Usuario;
            usuario.Telefono = "";
            usuario = crudUsuarios.Retrieve<Usuarios>(usuario);
            Factura factura = new Factura
            {
                FechaEmision = DateTime.Now,
                Identificacion = usuario.Cedula,
                Correo = reserva.Usuario,
                Nombre = usuario.Nombre + usuario.Apellidos,
                Clave = "clave",
                Consecutivo = "consecutivo",
                Monto = reserva.Tarifa * lstReservas.Count + reserva.Entrega + reserva.Comision + reserva.KmExcedido + reserva.MalEstado
            };
            crudFactura.Create(factura);
            List<Factura> lstFacturas = crudFactura.RetrieveAll<Factura>();
            var buscarFactura = from F in lstFacturas
                                where F.Consecutivo.Equals("consecutivo")
                                select F;

            int idFactura = 0;

            foreach (var item in buscarFactura)
            {
                idFactura = item.IdFactura;
            }
            factura.IdFactura = idFactura;
            factura.Consecutivo = "001" + "00001" + "01" + generarConsecutivo(idFactura);
            factura.Clave = "506" + factura.FechaEmision.Day.ToString() + factura.FechaEmision.Month.ToString() + factura.FechaEmision.Year.ToString() + factura.Identificacion + factura.Consecutivo + "1";
            crudFactura.Update(factura);

            NotificacionesManager notificaciones = new NotificacionesManager();
            //notificaciones.generarFactura(factura, reserva, lstFacturas.Count, vehiculo);


        }

        public string generarConsecutivo(int id_factura)
        {
            string id = id_factura.ToString();
            int largoId = id.Count();
            string cadena = "";
            for (int i = 0; i < 10; i++)
            {
                cadena += "0";
                if (10 - largoId == i)
                {
                    cadena += id;
                    break;
                }
            }

            return cadena;
        }

        public List<Reserva> RetrieveAll()
        {
            try
            {
                return crudReserva.RetrieveAll<Reserva>();
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }

        }

        public Reserva RetrieveById(Reserva reserva)
        {
            try
            {
                return crudReserva.Retrieve<Reserva>(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public Reserva RetrieveReservaById(Reserva reserva)
        {
            try
            {
                return crudReserva.RetrieveReserva<Reserva>(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public List<Reserva> RetrieveAllById(Reserva reserva)
        {
            try
            {
                return crudReserva.RetrieveByUser<Reserva>(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public List<Reserva> RetrieveAllByIdSocio(Reserva reserva)
        {
            try
            {
                return crudReserva.RetrieveBySocio<Reserva>(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public List<Reserva> RetrieveAllByIdSocioPendientes(Reserva reserva)
        {
            try
            {
                return crudReserva.RetrieveByPendientes<Reserva>(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public void Update(Reserva reserva)
        {
            try
            {
                crudReserva.Update(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public void UpdateEstado(Reserva reserva)
        {
            try
            {
                crudReserva.UpdateEstado(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }

        public void Delete(Reserva reserva)
        {
            try
            {
                crudReserva.Delete(reserva);
            }
            catch (Exception ex)
            {

                throw new BussinessException(0);

            }
        }

        public List<ConsultaReserva> RetrieveDisponibility(List<ConsultaReserva> listaConsultas)
        {
            try
            {
            List<ConsultaReserva> resultadoFinal = new List<ConsultaReserva>();

            foreach (var item in listaConsultas)
            {
                List<ConsultaReserva> resultadoConsulta = crudReserva.RetrieveDisponibility<ConsultaReserva>(item);
                foreach (var consulta in resultadoConsulta)
                {
                    resultadoFinal.Add(consulta);
                }
                resultadoConsulta.Clear();
            }

            return resultadoFinal;

            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }


    }
}
