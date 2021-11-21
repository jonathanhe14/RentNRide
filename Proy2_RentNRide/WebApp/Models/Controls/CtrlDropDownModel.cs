using Entities_POJO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlDropDownModel : CtrlBaseModel 
    {
        public string Label { get; set; }
        public string ListId { get; set; }

        //public Boolean isOptDependant { get; set; }

        private string URL_API_LISTs = "http://localhost:52125/api/List/Get/";

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
                        htmlOptions += "<option value='" + option.id + "' id='"+ option.id_marca +"'>" + option.nombre + "</option>";
                    }
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
            var response = client.DownloadString(URL_API_LISTs + ListId);
            var options = JsonConvert.DeserializeObject<List<VehiOpcion>>(response);
            return options;
        }

        private List<ModelosVehi> GetOptionsFromAPIDepend()
        {
            var client = new WebClient();
            var response = client.DownloadString(URL_API_LISTs + ListId);
            var options = JsonConvert.DeserializeObject<List<ModelosVehi>>(response);
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