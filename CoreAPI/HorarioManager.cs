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

        public void Create(Horario horario)
        {
            try
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

                //Loop para crear las horas
                int hours = 0;
                int minutes = 0;
                int hoursFinal = 0;
                int minutesFinal = 0;

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
                            Id_Horario = id_horario,
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

                        crudHoras.Create(hora);
                    }


                }




            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
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
