using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MicroApi.Handlers
{
    public class HandlerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        protected HandlerBase(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected HttpContext HttpContext => httpContextAccessor.HttpContext;
        protected ModelStateDictionary ModelState { get; private set; }

        //protected async Task Ok(object obj) => await WriteResponse(StatusCodes.Status200OK, obj);
        
//        protected async Task BadRequest(string message)
//        {
//            await WriteResponse(StatusCodes.Status400BadRequest,
//                new ProblemDetails()
//                {
//                    Title = message
//                }
//            );
//        }
//
//        protected async Task BadRequest(ModelStateDictionary modelState)
//        {
//            await WriteResponse(StatusCodes.Status400BadRequest,
//                new ValidationProblemDetails(modelState));
//        }
        
        protected async Task InternalServerError(string message)
        {
            await WriteResponse(StatusCodes.Status500InternalServerError,
                new ProblemDetails()
                {
                    Title = message
                }
            );
        }

        protected T GetRouteValue<T>(string routeKey)
        {
            var routeValue = HttpContext.Request.RouteValues["id"].ToString();

            return (T)Convert.ChangeType(routeValue, typeof(T));
        }

        protected async Task<T> FromBody<T>()
        {
            using var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true);
            string data = await reader.ReadToEndAsync();
            T result = JsonSerializer.Deserialize<T>(data);
            
            List<ValidationResult> validationResults = new List<ValidationResult>();
            var isModelValid = Validator.TryValidateObject(result, new ValidationContext(result), validationResults);

            ModelState = new ModelStateDictionary();
            foreach (var validationResult in validationResults)
            {
                ModelState.AddModelError("", validationResult.ErrorMessage);
            }

            return result;
        }
        
        private async Task WriteResponse(int statusCode, object obj)
        {
            if (HttpContext.Response.HasStarted)
            {
                return;
            }
            
            HttpContext.Response.StatusCode = statusCode;
            HttpContext.Response.ContentType = "application/json";
            await HttpContext.Response.WriteAsync(JsonSerializer.Serialize(obj));
        }
    }
}