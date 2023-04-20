using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Job_Board.Responses
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
        public static ContentResult CustomError(string custom)
        {

            var errorResponse = $"{custom}";
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            return new ContentResult
            {
                StatusCode = 404,
                ContentType = "application/json",
                Content = jsonErrorResponse
            };
        }


        public static ContentResult ErrorNoCandidate(string userInput)
        {

            var errorResponse = $"{userInput} has no Candidates with Interviews Scheduled.";
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            return new ContentResult
            {
                StatusCode = 404,
                ContentType = "application/json",
                Content = jsonErrorResponse
            };


        }

        public static ContentResult ErrorInputNotFound(string userInput)
        {
            var errorResponse = $"{userInput} was not found, please double check your input";
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            return new ContentResult
            {
                StatusCode = 404,
                ContentType = "application/json",
                Content = jsonErrorResponse
            };

        }
        public static ContentResult Error400()
        {

            var errorResponse = "There is a 400 Error";
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            return new ContentResult
            {
                StatusCode = 400,
                ContentType = "application/json",
                Content = jsonErrorResponse
            };


        }

        public static ContentResult Error500()
        {

            var errorResponse = "There is an Error with the Server";
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            return new ContentResult
            {
                StatusCode = 500,
                ContentType = "application/json",
                Content = jsonErrorResponse
            };

        }
        public static ContentResult ErrorUpdating(string T)
        {

            var errorResponse = $"There was an error updating the entry: {T}";
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
