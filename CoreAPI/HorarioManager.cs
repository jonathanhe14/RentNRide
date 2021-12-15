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
    public class HorarioManager
    {

        private HorarioCrudFactory crudHorario;
        private HorasCrudFactory crudHoras;


        public HorarioManager()
        {
            crudHorario = new HorarioCrudFactory();
            crudHoras = new HorasCrudFactory();
        }

        public void CrearComprobacion(List<Horas> listaGeneralHoras)
        {
            try
            {
                if (ComprobarHorasActual(listaGeneralHoras))
                {
                    throw new BussinessException(8);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Create(Horario horario)
        {
            try
            {
                //Loop para crear las horas
                List<Horas> listaHoras = GenerarHoras(horario);

                if (!ComprobarHorasActual(listaHoras))
                {
                    listaHoras = GenerarHoras(horario);
                    if (!ComprobarHorasBase(listaHoras))
                    {
                        crudHorario.Create(horario);

                        //Buscar el horario para obtener el Id del horario y ponérselo a las horas que se van a crear
                        List<Horario> horarios = RetrieveById(horario);

                        var buscarHorario = from h in horarios
                                            where h.horaInicio == horario.horaInicio &
                                                   h.horaFinal == horario.horaFinal &
                                                   h.Disponibilidad == horario.Disponibilidad &
                                                   h.DiaInicial == horario.DiaInicial &
                                                   h.DiaFinal == horario.DiaFinal
                                            select h;

                        int id_horario = 0;

                        foreach (var item in buscarHorario)
                        {
                            id_horario = item.Id;
                        }

                        foreach (var hora in listaHoras)
                        {
                            hora.Id_Horario = id_horario;
                            crudHoras.Create(hora);
                        }
                    }
                    else
                    {
                        throw new BussinessException(9);
                    }

                }
                else
                {
                    throw new BussinessException(8);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        private Boolean ComprobarHorasBase(List<Horas> listaHoras)
        {

            List<Horas> lstHorasBase = crudHoras.RetrieveByCar<Horas>(listaHoras[0]);
            foreach (var item in listaHoras)
            {
                foreach (var hora in lstHorasBase)
                {
                    if (item.Id_Vehiculo == hora.Id_Vehiculo &
                        item.Dia == hora.Dia &
                        item.Hora_Inicio.Equals(hora.Hora_Inicio) &
                        item.Hora_Final.Equals(hora.Hora_Final))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Boolean ComprobarHorasActual(List<Horas> listaHoras)
        {
            List<Horas> copiarListado = listaHoras;

            int i = 0;

            while (i < copiarListado.Count)
            {
                Horas horaBase = copiarListado[i];

                for (int k = 1; k < copiarListado.Count; k++)
                {
                    if (horaBase.Id_Vehiculo == listaHoras[k].Id_Vehiculo &
                    horaBase.Dia == listaHoras[k].Dia &
                    horaBase.Hora_Inicio.Equals(listaHoras[k].Hora_Inicio) &
                    horaBase.Hora_Final.Equals(listaHoras[k].Hora_Final))
                    {
                        return true;
                    }
                }
                copiarListado.RemoveAt(i);
            }
            return false;
        }

        public List<Horas> GenerarHoras(Horario horario)
        {

            List<Horas> listaHoras = new List<Horas>();
            int hours;
            int minutes;
            int hoursFinal;
            int minutesFinal;

            if (horario.DiaFinal < horario.DiaInicial)
            {
                hours = Convert.ToInt32(horario.horaInicio.Split(':')[0]);
                minutes = Convert.ToInt32(horario.horaInicio.Split(':')[1]);
                hoursFinal = Convert.ToInt32(horario.horaFinal.Split(':')[0]);
                minutesFinal = Convert.ToInt32(horario.horaFinal.Split(':')[1]);

                if (horario.Disponibilidad.Equals("CONTINUO"))
                {

                    DateTime fechaBase = new DateTime(2021, 8, 1);
                    DateTime fechaFinal = generarFechaFinal(fechaBase, horario.DiaFinal);
                    DateTime nuevaFechaFinal = new DateTime(fechaFinal.Year, fechaFinal.Month, fechaFinal.Day, hoursFinal, minutesFinal, 0);
                    DateTime fechaInicial = generarFechaInicial(fechaBase, horario.DiaInicial);
                    DateTime nuevaFechaInicial = new DateTime(fechaInicial.Year, fechaInicial.Month, fechaInicial.Day, hours, minutes, 0);

                    while (nuevaFechaInicial != nuevaFechaFinal)
                    {
                        Horas hora = new Horas
                        {
                            Id_Vehiculo = horario.Id_Vehiculo,
                            Id_Horario = horario.Id,
                            Dia = ((int)nuevaFechaInicial.DayOfWeek) + 1,
                            Hora_Inicio = nuevaFechaInicial.ToString("HH:mm"),
                            Hora_Final = nuevaFechaInicial.AddHours(1).ToString("HH:mm"),
                            Disponibilidad = "LIBRE",
                            Estado = "ACTIVO"
                        };
                        listaHoras.Add(hora);
                        nuevaFechaInicial = nuevaFechaInicial.AddHours(1);
                    }

                    return listaHoras;
                }
                else
                {
                    DateTime fechaBase = new DateTime(2021, 8, 1);
                    DateTime fechaFinal = generarFechaFinal(fechaBase, horario.DiaFinal);
                    DateTime fechaInicial = generarFechaInicial(fechaBase, horario.DiaInicial);
                    List<DateTime> listaFechas = new List<DateTime>();

                    while (fechaInicial <= fechaFinal)
                    {
                        listaFechas.Add(fechaInicial);
                        fechaInicial = fechaInicial.AddDays(1);
                    }

                    foreach (var fecha in listaFechas)
                    {
                        DateTime fechaNuevaInicial = new DateTime(fecha.Year, fecha.Month, fecha.Day, hours, minutes, 0);
                        DateTime fechaNuevaFinal = new DateTime(fecha.Year, fecha.Month, fecha.Day, hoursFinal, minutesFinal, 0);

                        while (fechaNuevaInicial != fechaNuevaFinal)
                        {
                            Horas hora = new Horas
                            {
                                Id_Vehiculo = horario.Id_Vehiculo,
                                Id_Horario = horario.Id,
                                Dia = ((int)fechaNuevaInicial.DayOfWeek) + 1,
                                Hora_Inicio = fechaNuevaInicial.ToString("HH:mm"),
                                Hora_Final = fechaNuevaInicial.AddHours(1).ToString("HH:mm"),
                                Disponibilidad = "LIBRE",
                                Estado = "ACTIVO"
                            };
                            listaHoras.Add(hora);
                            fechaNuevaInicial = fechaNuevaInicial.AddHours(1);
                        }
                    }
                    return listaHoras;
                }

            }
            else
            {
                for (int dia = horario.DiaInicial; dia <= horario.DiaFinal; dia++)
                {
                    //Definir y convertir la hora inicial y la final
                    hours = Convert.ToInt32(horario.horaInicio.Split(':')[0]);
                    minutes = Convert.ToInt32(horario.horaInicio.Split(':')[1]);
                    hoursFinal = Convert.ToInt32(horario.horaFinal.Split(':')[0]);
                    minutesFinal = Convert.ToInt32(horario.horaFinal.Split(':')[1]);
                    //Controlar aquí si la hora final es media noche
                    if (hoursFinal == 0)
                    {
                        hoursFinal = 24;
                    }

                    if (horario.Disponibilidad == "CONTINUO")
                    {
                        hoursFinal = 24;
                        if (dia == horario.DiaFinal)
                        {
                            hoursFinal = Convert.ToInt32(horario.horaFinal.Split(':')[0]);
                        }
                        if (dia == horario.DiaInicial)
                        {
                            hours = Convert.ToInt32(horario.horaInicio.Split(':')[0]);
                        }
                        else
                        {
                            hours = 0;
                        }
                    }

                    for (int inicial = hours; inicial < hoursFinal; inicial++)
                    {

                        Horas hora = new Horas
                        {
                            Id_Vehiculo = horario.Id_Vehiculo,
                            Id_Horario = horario.Id,
                            Dia = dia,
                            Disponibilidad = "LIBRE",
                            Estado = "ACTIVO"
                        };

                        if (minutes == 0)
                        {
                            if (inicial + 1 == 24)
                            {
                                hora.Hora_Inicio = Convert.ToString(inicial) + ":" + Convert.ToString(minutes) + "0";
                                hora.Hora_Final = Convert.ToString("00") + ":" + Convert.ToString(minutes) + "0";
                            }
                            else
                            {
                                hora.Hora_Inicio = Convert.ToString(inicial) + ":" + Convert.ToString(minutes) + "0";
                                hora.Hora_Final = Convert.ToString(inicial + 1) + ":" + Convert.ToString(minutes) + "0";
                            }
                        }
                        else
                        {
                            if (inicial + 1 == 24)
                            {
                                hora.Hora_Inicio = Convert.ToString(inicial) + ":" + Convert.ToString(minutes);
                                hora.Hora_Final = Convert.ToString("00") + ":" + Convert.ToString(minutes);
                            }
                            else
                            {
                                hora.Hora_Inicio = Convert.ToString(inicial) + ":" + Convert.ToString(minutes);
                                hora.Hora_Final = Convert.ToString(inicial + 1) + ":" + Convert.ToString(minutes);
                            }
                        }

                        listaHoras.Add(hora);
                    }

                }
            }



            return listaHoras;
        }

        public DateTime generarFechaFinal(DateTime fechaBase, int dia)
        {
            switch (dia - 1)
            {
                case ((int)DayOfWeek.Sunday):
                    return fechaBase;
                    break;
                case ((int)DayOfWeek.Monday):
                    return fechaBase.AddDays(1);
                    break;
                case ((int)DayOfWeek.Tuesday):
                    return fechaBase.AddDays(2);
                    break;
                case ((int)DayOfWeek.Wednesday):
                    return fechaBase.AddDays(3);
                    break;
                case ((int)DayOfWeek.Thursday):
                    return fechaBase.AddDays(4);
                    break;
                case ((int)DayOfWeek.Friday):
                    return fechaBase.AddDays(5);
                    break;
                case ((int)DayOfWeek.Saturday):
                    return fechaBase.AddDays(6);
                    break;
                default:
                    return fechaBase;
                    break;
            }
        }

        public DateTime generarFechaInicial(DateTime fechaBase, int dia)
        {
            switch (dia - 1)
            {
                case ((int)DayOfWeek.Sunday):
                    return fechaBase;
                    break;
                case ((int)DayOfWeek.Monday):
                    return fechaBase.AddDays(-6);
                    break;
                case ((int)DayOfWeek.Tuesday):
                    return fechaBase.AddDays(-5);
                    break;
                case ((int)DayOfWeek.Wednesday):
                    return fechaBase.AddDays(-4);
                    break;
                case ((int)DayOfWeek.Thursday):
                    return fechaBase.AddDays(-3);
                    break;
                case ((int)DayOfWeek.Friday):
                    return fechaBase.AddDays(-2);
                    break;
                case ((int)DayOfWeek.Saturday):
                    return fechaBase.AddDays(-1);
                    break;
                default:
                    return fechaBase;
                    break;
            }
        }


        public List<Horario> RetrieveAll()
        {
            return crudHorario.RetrieveAll<Horario>();
        }

        public List<Horario> RetrieveById(Horario horario)
        {
            List<Horario> c = null;
            try
            {
                c = crudHorario.RetrieveByCar<Horario>(horario);
                if (c == null)
                {
                    throw new BussinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public void Update(Horario horario)
        {
            crudHorario.Update(horario);
        }

        public void Delete(Horario horario)
        {
            crudHorario.Delete(horario);
        }

    }
}
