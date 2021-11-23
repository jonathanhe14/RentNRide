﻿using CoreAPI;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class VehiculoController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/vehiculo
        // Retrieve
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new VehiculoManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        // GET api/
        // Retrieve by id
        public IHttpActionResult Get(int id)
        {
            try
            {
                var mng = new VehiculoManager();
                var customer = new Vehiculo
                {
                    Id = id
                };

                customer = mng.RetrieveById(customer);
                apiResp = new ApiResponse();
                apiResp.Data = customer;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult GetCheck(int id)
        {
            try
            {
                var mng = new VehiculoManager();
                var customer = new Vehiculo
                {
                    Id = id
                };

                var doesItExi = mng.checkIfExists(customer);
                apiResp = new ApiResponse();
                apiResp.Data = doesItExi;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // POST 
        // CREATE
        public IHttpActionResult Post(Vehiculo vehiculo)
        {

            try
            {
                var mng = new VehiculoManager();



                mng.Create(vehiculo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }

        // PUT
        // UPDATE
        public IHttpActionResult Put(Vehiculo vehiculo)
        {
            try
            {
                var mng = new VehiculoManager();
                mng.Update(vehiculo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE ==
        public IHttpActionResult Delete(Vehiculo vehiculo)
        {
            try
            {
                var mng = new VehiculoManager();
                mng.Delete(vehiculo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}