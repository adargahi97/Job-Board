using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Job_Board.Responses
{
    public class SuccessResponses
    {
        public static ContentResult DeleteSuccessful(string del)
        {

            var successResponse = $"{del} has been successfully deleted.";
            var jsonsuccessResponse = JsonConvert.SerializeObject(successResponse);
            return new ContentResult
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = jsonsuccessResponse
            };
        }
        public static ContentResult CreateSuccessful(string created)
        {

            var successResponse = $"{created} has been successfully created.";
            var jsonsuccessResponse = JsonConvert.SerializeObject(successResponse);
            return new ContentResult
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = jsonsuccessResponse
            };
        }
        public static ContentResult GetAllSuccessful(IEnumerable obj)
        {
            var jsonsuccessResponse = JsonConvert.SerializeObject(obj);
            return new ContentResult
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = jsonsuccessResponse,
            };
        }
        public static ContentResult GetObjectSuccessful(Object T)
        {
            var jsonsuccessResponse = JsonConvert.SerializeObject(T, Formatting.Indented);
            return new ContentResult
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = jsonsuccessResponse,
            };
        }
        public static ContentResult UpdateObjectSuccessful(string obj)
        {
            var successResponse = $"{obj} has been successfully updated.";
            var jsonsuccessResponse = JsonConvert.SerializeObject(successResponse, Formatting.Indented);
            return new ContentResult
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = jsonsuccessResponse,
            };
        }
    }
}
