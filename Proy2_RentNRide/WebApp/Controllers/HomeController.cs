﻿using Entities_POJO;
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

                    Session["UserID"] = user;
                    Session["FullName"] = user.FullName;
                    return View("About");
                }
                else
                {
                    return View(objUser);
                }
            }
            return View(objUser);
        }

        public ActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnvioOTPCorreo(Usuarios objUser)
        {
            if (ModelState.IsValid)
            {
                string jsonString = JsonConvert.SerializeObject(objUser);

                HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:52125/api/userprofile/RecuperarClaveCorreo", c).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
                    var user = JsonConvert.DeserializeObject<Usuarios>(apiResponse.Data.ToString());

                    TempData["usuario"] = user.Correo;

                    return new RedirectResult("IngresarOTP");

                    //return View("IngresarOTP");                    
                }
                else
                {
                    return View(objUser);
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


                string jsonString = JsonConvert.SerializeObject(objUser);

                HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:52125/api/userprofile/RecuperarClaveSMS", c).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
                    var user = JsonConvert.DeserializeObject<Usuarios>(apiResponse.Data.ToString());
                    string correo = user.Correo;
                    TempData["usuario"] = correo;


                    return View("IngresarOTP");
                }
                else
                {
                    return View(objUser);
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
        public ActionResult IngresarOTP(string correo, string otp)
        {
            if (ModelState.IsValid)
            {
                Usuarios objUser = new Usuarios();
                objUser.Correo = correo;
                objUser.OTP = Convert.ToInt32(otp);

                string jsonString = JsonConvert.SerializeObject(objUser);

                HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:52125/api/userprofile/ComprobarOTP", c).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
                    if (apiResponse.Message == "éxito")
                    {
                        var user = JsonConvert.DeserializeObject<Usuarios>(apiResponse.Data.ToString());
                        TempData["usuario"] = correo;
                        return new RedirectResult("RestablecerClave");

                        //return View("RestablecerClave"); 
                    }
                    else
                    {
                        TempData["usuario"] = correo;
                        return View();
                    }                 

                }
                else
                {
                    return View(objUser);
                }
            }
            return View("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestablecerClave(string correo, string nuevaClave, string confirmarClave)
        {
            if (ModelState.IsValid)
            {
                if (nuevaClave.Equals(confirmarClave))
                {
                    Hasher encriptado = new Hasher();
                    Usuarios objUser = new Usuarios();
                    objUser.Correo = correo;
                    objUser.ContrassenaActual = encriptado.MD5(nuevaClave);

                    string jsonString = JsonConvert.SerializeObject(objUser);

                    HttpContent c = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("http://localhost:52125/api/userprofile/CambiarClave", c).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;

                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
                        if (apiResponse.Message == "Su contraseña ha sido cambiada éxitosamente")
                        {
                            var user = JsonConvert.DeserializeObject<Usuarios>(apiResponse.Data.ToString());
                            return new RedirectResult("InicioSesion");

                        }
                        else
                        {
                            TempData["usuario"] = correo;
                            return View();
                        }

                    }
                    else
                    {
                        return View(objUser);
                    }

                }
                else
                {
                    ViewBag.Message = "Las contraseñas no coinciden";
                    TempData["usuario"] = correo;
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult VehiculoInfo()
        {
            
            if (Session["UserID"] != null)
            {
                ViewBag.Message = "Vehiculo ingreso!";

                return View();
            }
            else
            {
                return RedirectToAction("InicioSesion");
            }
        }
    }
}