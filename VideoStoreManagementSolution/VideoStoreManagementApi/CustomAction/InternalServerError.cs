using Microsoft.AspNetCore.Mvc;
using System.Net;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.CustomAction
{
    public class InternalServerError<T>
    {
        public static ActionResult<T> Action(ErrorDTO errorDTO)
        {
            var result = new ObjectResult(errorDTO)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            return result;
        }
       
       
        
    }
}
