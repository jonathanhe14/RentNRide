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
    public class ListManager : BaseManager
    {

        private Dictionary<string, List<VehiOpcion>> dicListOptions;
        private static ListManager _instance;
        //private ListCrudFactory crudCustomer;

        private ListManager()
        {
            LoadDictionary();
            //crudCustomer = new ListCrudFactory();
        }

        public static ListManager GetInstance()
        {
            if (_instance == null)
                _instance = new ListManager();

            return _instance;
        }


        private void LoadDictionary()
        {
            dicListOptions = new Dictionary<string, List<VehiOpcion>>();
            /*
            //TODO: ESTO DEBE VENIR DE ELA BASE DE DATOS
            var crudList = new ListOptionCrudFactory();

            var lists = crudList.RetrieveAll<VehiOpcion>();

            var lstId = lists[0].ListId;
            var lstOptions = new List<VehiOpcion>();

            for (int i = 0; i < lists.Count; i++)
            {
                var l = lists[i];
                lstOptions.Add(new VehiOpcion { ListId = l.ListId, Value = l.Value, Description = l.Description });

                if (i == lists.Count - 1 || !lists[i + 1].ListId.Equals(l.ListId))
                {
                    dicListOptions[l.ListId] = lstOptions;
                    lstOptions = new List<VehiOpcion>();
                    lstId = l.ListId;
                }

            }
            */
        }

        public List<VehiOpcion> RetrieveById(string id)
        {

            try
            {
                
                    //    //BUSCA EN OTRO MANAGER

                    if (id.Equals("LST_tipoVehi"))
                    {
                        var crudTipoVehi = new TipoVehiCrudFactory();
                        var lst = crudTipoVehi.RetrieveAll<VehiOpcion>();

                        var lstResult = new List<VehiOpcion>();

                        foreach (var c in lst)
                        {
                            var newOption = new VehiOpcion { id = c.id,  nombre = c.nombre };
                            lstResult.Add(newOption);
                        }
                        return lstResult;

                    }
                if (id.Equals("LST_tipoCombu"))
                {
                    var crudTipo = new TipoCombuCrudFactory();
                    var lst = crudTipo.RetrieveAll<VehiOpcion>();

                    var lstResult = new List<VehiOpcion>();

                    foreach (var c in lst)
                    {
                        var newOption = new VehiOpcion { id = c.id, nombre = c.nombre };
                        lstResult.Add(newOption);
                    }
                    return lstResult;

                }

                if (id.Equals("LST_tipoMarca"))
                {
                    var crudTipo = new MarcaVehiCrudFactory();
                    var lst = crudTipo.RetrieveAll<VehiOpcion>();

                    var lstResult = new List<VehiOpcion>();

                    foreach (var c in lst)
                    {
                        var newOption = new VehiOpcion { id = c.id, nombre =  c.nombre };
                        lstResult.Add(newOption);
                    }
                    return lstResult;

                }

                if (id.Equals("LST_tipoModelo"))
                {
                    var crudTipo = new ModeloVehiCrudFactory();
                    var lst = crudTipo.RetrieveAll<VehiOpcion>();

                    var lstResult = new List<VehiOpcion>();

                    foreach (var c in lst)
                    {
                        var newOption = new VehiOpcion { id = c.id, nombre = c.nombre, id_extra = c.id_extra };
                        lstResult.Add(newOption);
                    }
                    return lstResult;

                }
                //    //retrieve 



            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return new List<VehiOpcion>(); ;
        }



    }
}
