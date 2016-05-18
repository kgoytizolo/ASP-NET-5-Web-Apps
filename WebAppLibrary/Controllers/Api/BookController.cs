using Microsoft.AspNet.Mvc;
using System.Net;
using WebAppLibrary.Repositories.Interface;
using WebAppLibrary.ViewModels;
using WebAppLibrary.Services;
using AutoMapper;
using WebAppLibrary.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppLibrary.Controllers.Api
{
 
    [Route("api/books")]     //Specifies the root of the entire Controller. All methods will be called by this root route 
    public class BookController : Controller        //Inherits from Controller class - Creating Web API's
    {
        private ILibraryRepository _libRepository;
        private ILogger<BookController> _logger;            //using logger framework functionality (dependency injection) 
        private GeoLocationService _geoLocationService;     //using geoLocation services from app  (dependency injection)

        public BookController(ILibraryRepository libRepository, 
                              ILogger<BookController> logger,
                              GeoLocationService geoLocationService) {   //We generate a constructor to get Startup MVC app dependency injections
            _libRepository = libRepository;
            _logger = logger;
            _geoLocationService = geoLocationService;
        }

        //(by default, no attribute in case of Get)
        [HttpGet("")]                                                                               //Mapping this as a HTTPGET RESTFul service
        public JsonResult Get() {                                                                   //Simulating that we get information at JSON format - HTTP GET 
            var results = Mapper.Map<IEnumerable<BookViewModel>>(_libRepository.GetAllBooks());     //Get HTTP action also can work with mapping 
                                                                                                    //(From List of Books to ViewModel List)
            return Json(results);                                                                   //Returning DB information / from Entity Framework. Converts what comes from DB to JSON format
        }

        //Use of auto-mapping: To map between ViewModels and Models
        //A class is binded to the request (queryString parameters). [FromBody] is necessary for binding to Json to .NET types    
        //Using HTTP Verb POST after supplied data by a Form 
        //Using Async task due to async API calls, is more effective
        public async Task<JsonResult> Post([FromBody]BookViewModel vm) {            //Sends a request from body. You can also specify if comes from a Form or other 
            //Using try catch to avoid 500 internal error in HTML
            try {
                if (ModelState.IsValid)         //ModelState is an MVC class. Encapsulates the state of Model binding to a property of an action-method argument, or to the argument itself.
                {
                    var newBook = Mapper.Map<Book>(vm);                     //vm Is the sourcetype for mapping, destination (Book). Not all elements from Book will be used
                                                                            //Automapper asks where is the model to map? (destination - Book)

                    //Look for the coordinates before insert a new book
                    var coordResult = await _geoLocationService.Lookup(newBook.Location);

                    //If results are not correct, it will be shown to response attached
                    if (!coordResult.Sucess) {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Json(coordResult.Message);
                    }

                    string newLatitude = coordResult.Latitude.ToString();           
                    string newLongitude = coordResult.Longitude.ToString();         
                    newBook.Location = $"{newLatitude}, {newLongitude}";
                    
                    //Save to the Database  
                    _logger.LogError("Saving a new book into the Database");
                    _libRepository.AddBook(newBook);

                    //Results back to the client
                    if (_libRepository.SaveAll()) {
                        _logger.LogError("New book saved correctly");
                        Response.StatusCode = (int)HttpStatusCode.Created;      //Cast code to integer value - In case of correct validation through BookViewModel (ModelState)
                        return Json(Mapper.Map<BookViewModel>(newBook));        //Reverse mapping, like Startup component
                    }                  
                }
            }
            catch (Exception err) {
                _logger.LogError("Failed to save new book", err);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;                   //In case of wrong validation through BookViewModel (ModelState)
                return Json(new { Message = err.Message, ModelState = ModelState });      //Returns failed and error source message                                               
            }
            _logger.LogError("Failed to save new book due to bad request");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;                       //In case of wrong validation through BookViewModel (ModelState)
            return Json(new { Message = "failed", ModelState = ModelState });           //Returns failed and error source message                                               
        }

    }
}
