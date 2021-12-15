using Entities_POJO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        static HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InicioSesion(UserProfile objUser)
        {
            if (ModelState.IsValid)
            {
                Hasher encriptado = new Hasher();               
                                
                objUser.Password = encriptado.MD5(objUser.Password);
                string jsonString = JsonConvert.SerializeObject(objUser);

                HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:52125/api/userprofile/InicioSesion", c).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
                    var user = JsonConvert.DeserializeObject<UserProfile>(apiResponse.Data.ToString());

                    HttpResponseMessage responseRol = client.GetAsync("http://localhost:52125/api/userprofile/RolesPorUsuario?correo=" + user.UserName).Result;

                    if (responseRol.IsSuccessStatusCode)
                    {
                        var contentRol = responseRol.Content.ReadAsStringAsync().Result;
                        var apiResponseRol = JsonConvert.DeserializeObject<ApiResponse>(contentRol);
                        List<UsuariosRol> rol = JsonConvert.DeserializeObject<List<UsuariosRol>>(apiResponseRol.Data.ToString());

                        Session["Admin"] = verificarRol(rol, 1);
                        Session["Socio"] = verificarRol(rol, 2);
                        Session["Empresa"] = verificarRol(rol, 3);
                        Session["Usuario"] = verificarRol(rol, 4);
                        Session["UserID"] = user.UserName;
                        Session["FullName"] = user.FullName;

                        if (verificarRol(rol, 1) == "yes")
                        {
                            return View("Administrador");
                        }

                        if (verificarRol(rol, 2) == "yes" || verificarRol(rol, 3) == "yes")
                        {
                            return View("PerfilSocio");
                        }

                        if (verificarRol(rol, 4) == "yes")
                        {
                            return View("BusquedaVehiculos");
                        }
                    }
                    else
                    {
                        var contentRol = responseRol.Content.ReadAsStringAsync().Result;
                        var error = JsonConvert.DeserializeObject<RespuestaFallida>(contentRol);
                        ViewBag.Message = error.ExceptionMessage;
                        return View(objUser);
                    }

                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var error = JsonConvert.DeserializeObject<RespuestaFallida>(content);
                    ViewBag.Message = error.ExceptionMessage;
                    return View(objUser);
                }
            }
            return View(objUser);
        }

        public string verificarRol(List<UsuariosRol> roles, int id_rol)
        {
            string resultado = "no";

            foreach (var item in roles)
            {
                if (item.IdRol == id_rol)
                {
                    if (item.Estado.Equals("Activo"))
                    {
                        resultado = "yes";
                        break;
                    }
                }
            }

            return resultado;
        }

        public ActionResult InicioSesion()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnvioOTPCorreo(Usuarios objUser)
        {
            if (ModelState.IsValid)
            {
                objUser.Telefono = "";
                string jsonString = JsonConvert.SerializeObject(objUser);

                HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:52125/api/userprofile/RecuperarClaveCorreo", c).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
                    var user = JsonConvert.DeserializeObject<Usuarios>(apiResponse.Data.ToString());

                        TempData["correo"] = user.Correo;
                        TempData["telefono"] = user.Telefono;

                        return new RedirectResult("IngresarOTP");              
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var error = JsonConvert.DeserializeObject<RespuestaFallida>(content);
                    ViewBag.Message = error.ExceptionMessage;
                    return View("EnvioOTP");
                }
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnvioOTPTelefono(Usuarios objUser)
        {
            if (ModelState.IsValid)
            {
                objUser.Correo = "";
                string jsonString = JsonConvert.SerializeObject(objUser);

                HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:52125/api/userprofile/RecuperarClaveSMS", c).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
                    var user = JsonConvert.DeserializeObject<Usuarios>(apiResponse.Data.ToString());

                        TempData["correo"] = user.Correo;
                        TempData["telefono"] = user.Telefono;

                        return new RedirectResult("IngresarOTP");
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var error = JsonConvert.DeserializeObject<RespuestaFallida>(content);
                    ViewBag.Message = error.ExceptionMessage;
                    return View("EnvioOTP");
                }
            }
            return View("Index");
        }


        public ActionResult EnvioOTP()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IngresarOTP(string correo, string telefono, string otp)
        {
            if (ModelState.IsValid)
            {
                Usuarios objUser = new Usuarios();
                objUser.Correo = correo;
                objUser.Telefono = telefono;
                objUser.OTP = Convert.ToInt32(otp);

                string jsonString = JsonConvert.SerializeObject(objUser);

                HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:52125/api/userprofile/ComprobarOTP", c).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

                        var user = JsonConvert.DeserializeObject<Usuarios>(apiResponse.Data.ToString());
                        TempData["correo"] = correo;
                        TempData["telefono"] = telefono;
                        return new RedirectResult("RestablecerClave");
                }
                else
                {
                    TempData["correo"] = correo;
                    TempData["telefono"] = telefono;
                    var content = response.Content.ReadAsStringAsync().Result;
                    var error = JsonConvert.DeserializeObject<RespuestaFallida>(content);
                    ViewBag.Message = error.ExceptionMessage;
                    return View("IngresarOTP");
                }
            }
            return View("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestablecerClave(string correo, string telefono, string nuevaClave, string confirmarClave)
        {
            if (ModelState.IsValid)
            {
                if (nuevaClave.Equals(confirmarClave))
                {
                    
                    Usuarios objUser = new Usuarios();
                    objUser.Correo = correo;
                    objUser.ContrassenaActual = nuevaClave;
                    objUser.Telefono = telefono;

                    string jsonString = JsonConvert.SerializeObject(objUser);

                    HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("http://localhost:52125/api/userprofile/CambiarClave", c).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;

                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

                            var user = JsonConvert.DeserializeObject<Usuarios>(apiResponse.Data.ToString());
                            return new RedirectResult("InicioSesion");

                    }
                    else
                    {
                        TempData["correo"] = correo;
                        TempData["telefono"] = telefono;
                        var content = response.Content.ReadAsStringAsync().Result;
                        var error = JsonConvert.DeserializeObject<RespuestaFallida>(content);
                        ViewBag.Message = error.ExceptionMessage;
                        return View(objUser);
                    }
                }
                else
                {
                    ViewBag.Message = "Las contraseñas no coinciden";
                    TempData["correo"] = correo;
                    TempData["telefono"] = telefono;
                    return View();
                }              


            }
            return View("Index");
        }

        public ActionResult RestablecerClave()
        {
            return View();
        }


        public ActionResult IngresarOTP()
        {
            return View();
        }

        public ActionResult RegistroUsuariosFinales()
        {
            return View();
        }

        public ActionResult RegistroSocios()
        {
            return View();
        }

        public ActionResult About()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }
            else
            {
                return RedirectToAction("InicioSesion");
            }
        }

        public ActionResult Administrador() {
            return View();
        }

        public ActionResult Usuarios() {
            return View();
        }

        public ActionResult Finanzas() {
            return View();
        }

        public ActionResult Solicitudes() {
            return View();
        }


        public ActionResult VehiculoInfo() {
            if (Session["UserID"] != null)
            {
                ViewBag.Message = "Agregar vehiculo a rentar";

                return View();
            }
            else
            {
                return RedirectToAction("InicioSesion");
            }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Verificacion()
        {
            ViewBag.Message = "Pagina para Verificar";

            return View();
        }

        public ActionResult PerfilSocio()
        {
            ViewBag.Message = "Perfil Socio";

            return View();
        }

        public ActionResult Horario()
        {
            return View();
        }

        public ActionResult SocioDashboard()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.Message = "vehiculos del Socio";
                return View();
            }
            else
            {
                return RedirectToAction("InicioSesion");
            }
        }
        public ActionResult SocioVehiculos()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.Message = "vehiculos del Socio";
                return View();
            }
            else
            {
                return RedirectToAction("InicioSesion");
            }
        }

        public ActionResult BusquedaVehiculos()
        {
            //if (Session["UserID"] != null)
            //{
            //ViewBag.Message = "Búsqueda de vehículos";
            if (ModelState.IsValid)
            {
                HttpResponseMessage responseAutos = client.GetAsync("http://localhost:52125/api/vehiculo/GetAllVehicles").Result;
                var contentAutos = responseAutos.Content.ReadAsStringAsync().Result;
                var apiResponseAutos = JsonConvert.DeserializeObject<ApiResponse>(contentAutos);
                List<Vehiculo> listaVehiculos = JsonConvert.DeserializeObject<List<Vehiculo>>(apiResponseAutos.Data.ToString());
                string[] imagenes = new string[listaVehiculos.Count];
                int i = 0;
                foreach (var item in listaVehiculos)
                {
                    imagenes[i] = item.Imagen.Split(',')[0];
                    i++;
                }

                ViewData["imagenes"] = imagenes;
                return View(listaVehiculos);

            }
            return View();


            /*}
            else
            {
                return RedirectToAction("InicioSesion");
            }*/
        }

        public ActionResult PaginaVehiculo(int id_vehiculo)
        {
            //if (Session["UserID"] != null)
            //{
            if (ModelState.IsValid)
            {
                //////Información del vehículo                
                HttpResponseMessage responseAutos = client.GetAsync("http://localhost:52125/api/vehiculo/GetOneVehicle?id=" + id_vehiculo).Result;
                var contentVehiculo = responseAutos.Content.ReadAsStringAsync().Result;
                var apiResponseVehiculo = JsonConvert.DeserializeObject<ApiResponse>(contentVehiculo);
                Vehiculo vehiculo = JsonConvert.DeserializeObject<Vehiculo>(apiResponseVehiculo.Data.ToString());

                string[] listaImagenes = vehiculo.Imagen.Split(',');

                ViewData["listaImagenes"] = listaImagenes;
                ViewBag.vehiculo = id_vehiculo;

                //////TIPO VEHÍCULO 
                /*HttpResponseMessage responseTipoAutos = client.GetAsync("http://localhost:52125/api/list/Get?id=LST_tipoVehi").Result;
                var contentTipoAutos = responseTipoAutos.Content.ReadAsStringAsync().Result;
                var apiResponseTipoAutos = JsonConvert.DeserializeObject<ApiResponse>(contentTipoAutos);
                List<VehiOpcion> tipoAutos = JsonConvert.DeserializeObject<List<VehiOpcion>>(apiResponseTipoAutos.Data.ToString());
                ViewData["listaTipoAutos"] = tipoAutos;*/   


                return View(vehiculo);
            }
            return View();
            //}
            /*else
            {
                return RedirectToAction("InicioSesion");
           }*/
        }
        public ActionResult PagoMembresia()
        {
            
            return View();
        }
        public ActionResult Monedero()
        {
            return View();
        }
        public ActionResult EquipoPage()
        {
            return View();
        }

    }
}