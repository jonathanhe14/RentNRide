using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Entities_POJO;

namespace WebApp.Models.Controls
{
    public class CtrlDropDownModel : CtrlBaseModel 
    {
        public string Label { get; set; }
        public string ListId { get; set; }

        private string URL_API_LISTs = "http://localhost:52125/api/List/";


        public string ListOptions
        {
            get
            {

                var htmlOptions = "";
                if (ListId == "LST_tipoModelo")
                {
                    var lst = GetOptionsFromAPIDepend();
                    foreach (var option in lst)
                    {
                        htmlOptions += "<option value='" + option.id + "' data-tag='" + option.id_extra + "'>" + option.nombre + "</option>";
                    }
                    return htmlOptions;
                }
                else if (ListId == "LST_aceptInmd")
                {

                    htmlOptions += "<option value='Si'>Si</option>";
                    htmlOptions += "<option value='no'>No</option>";

                    return htmlOptions;
                }
                else if (ListId == "LST_Estado")
                {
                    htmlOptions += "<option value='Buen estado'>Buen estado</option>";
                    htmlOptions += "<option value='Un poco dañado'>Un poco dañado</option>";
                    htmlOptions += "<option value='Dañado'>Dañado</option>";
                    htmlOptions += "<option value='Bastante dañado'>Bastante dañado</option>";
                    htmlOptions += "<option value='Terrible'>Terrible</option>";
                    return htmlOptions;
                }
                else
                {
                    var lst = GetOptionsFromAPI();
                    foreach (var option in lst)
                    {
                        htmlOptions += "<option value='" + option.id + "'>" + option.nombre + "</option>";
                    }
                    return htmlOptions;
                }


            }
            set
            {

            }
        }


        private List<VehiOpcion> GetOptionsFromAPI()
        {
            var client = new WebClient();
            var response = client.DownloadString("http://localhost:52125/api/List/Get/" + ListId);
            var options = JsonConvert.DeserializeObject<List<VehiOpcion>>(response);
            return options;
        }

        private List<VehiOpcion> GetOptionsFromAPIDepend()
        {
            var client = new WebClient();
            var response = client.DownloadString("http://localhost:52125/api/List/Get/" + ListId);
            var options = JsonConvert.DeserializeObject<List<VehiOpcion>>(response);
            return options;
        }

        /*public string ListOptions
        {
            get
            {
                var htmlOptions = "";
                var lst = GetOptionsFromAPI();

                foreach (var option in lst)
                {
                    htmlOptions += "<option value='" + option.Value + "'>" + option.Description + "</option>";
                }
                return htmlOptions;
            }
            set
            {

            }
        }


        private List<OptionList> GetOptionsFromAPI()
        {            
            var client = new WebClient();
            var response = client.DownloadString(URL_API_LISTs + ListId);
            var options = JsonConvert.DeserializeObject<List<OptionList>>(response);
            return options;
        }*/



        public CtrlDropDownModel()
        {
            ViewName = "";
        }

    }
}