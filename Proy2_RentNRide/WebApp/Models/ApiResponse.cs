using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ApiResponse
    {
        public string Message { get; set; }
        //Soporta cualquier objeto.
        public object Data { get; set; }
    }
}