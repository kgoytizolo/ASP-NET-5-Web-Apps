using System;
using Microsoft.AspNet.Mvc;
using WebAppLibrary.Repositories.Interface;
using Microsoft.Extensions.Logging;
using System.Net;
using AutoMapper;
using WebAppLibrary.ViewModels;
using WebAppLibrary.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppLibrary.Controllers.Api
{
    [Route("api/books/{salesId}/sales")]           //We use a parameter that replaces an object (a name for search) - we can reuse books as a logic tree to navigate on options
    public class SalesController : Controller
    {
        private ILibraryRepository _libRepository;
        private ILogger<BookController> _logger;        //using logger framework functionality (dependency injection) 
 
        public SalesController(ILibraryRepository libRepository, ILogger<BookController> logger) {
            _libRepository = libRepository;
            _logger = logger;
        }

        //
        [HttpGet("")]
        public JsonResult Get(int salesId)
        {
            try
            {
                var results = _libRepository.GetSalesById(salesId);
                if(results == null) return Json(null);
                return Json(Mapper.Map<ShoppingCartViewModel>(results));       //Mapper is also used to return sales per Id
            }
            catch (Exception err) {
                _logger.LogError($"Falló al tratar de obtener las compras por código de transacción {salesId}", err);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("El error ocurrió mientras buscaba el nombre del libro: " + err.Message + " / Err: " + err.Source + " / Tracking: " + err.StackTrace);
            }

        }

        // POST api/values
        [HttpPost("")]
        public JsonResult Post([FromBody]ShoppingCartViewModel vm)
        {
            //Using try catch to avoid 500 internal error in HTML
            try
            {
                if (ModelState.IsValid) {
                    //Map to the entity
                    var newTransaction = Mapper.Map<ShoppingCart>(vm);
                    //Guardar en la base de datos
                    _libRepository.AddShoppingCart(newTransaction);
                    if (_libRepository.SaveAll()) {
                        _logger.LogError("New transaction saved correctly");
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<ShoppingCartViewModel>(newTransaction));
                    }
                }

            }
            catch (Exception err) {
                _logger.LogError("Failed to save new shopping cart header - item", err);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;                   //In case of wrong validation through BookViewModel (ModelState)
                return Json(new { Message = err.Message, ModelState = ModelState });      //Returns failed and error source message                                               
            }
            _logger.LogError("Failed to save new shopping cart header - item due to bad request");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;                       //In case of wrong validation through BookViewModel (ModelState)
            return Json(new { Message = "failed", ModelState = ModelState });           //Returns failed and error source message 
        }

    }
}
