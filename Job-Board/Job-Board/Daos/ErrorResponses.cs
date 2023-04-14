using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Job_Board.Daos
{
    public class ErrorResponses
    {
        public static ContentResult Error404(string userInput)
        {

            var errorResponse = $"{userInput} is invalid, please check your input and try again.";
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            return new ContentResult
            {
                StatusCode = 404,
                ContentType = "application/json",
                Content = jsonErrorResponse
            };
        }

        public static ContentResult Error500()
        {

            var errorResponse = "dang ol shoot man not sure what happent..";
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            return new ContentResult
            {
                StatusCode = 500,
                ContentType = "application/json",
                Content = jsonErrorResponse
            };
        }
    }
}
