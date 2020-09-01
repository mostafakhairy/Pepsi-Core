using Coupons.PepsiKSA.Api.Core;
using Coupons.PepsiKSA.Api.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Filters.ErrorHandlerFilter
{
    public class GlobalExceptionMiddleware
    {
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly RequestDelegate _next;
        private readonly ILog _logger;
        public GlobalExceptionMiddleware(RequestDelegate next, IStringLocalizer<Resource> localizer, ILog logger)
        {
            _next = next;
            _localizer = localizer;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.Error(exception, $"Error Message: {exception.Message}");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new
            {
                context.Response.StatusCode,
                Message = _localizer["ErrorOccurred"].Value
            }.ToString());
        }
    }
}
