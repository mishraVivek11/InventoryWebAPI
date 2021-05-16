using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InventoryWebAPI.Middleware
{
    /// <summary>
    /// Custom exception handling middleware
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        /// Calls the next request delegate in the pipeline.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Handle exceptions and set appropriate return codes.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if (ex is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else if (ex is ArgumentNullException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            context.Response.StatusCode = (int)statusCode;
            var response = JsonConvert.SerializeObject(new { error = ex.Message });
            return context.Response.WriteAsync(response);
        }
    }
}
