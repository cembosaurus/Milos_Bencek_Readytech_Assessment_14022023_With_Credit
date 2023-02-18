using Microsoft.AspNetCore.Http;

namespace Business.Middlewares
{
    public class Http418HandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _month;
        private readonly int _day;

        public Http418HandlerMiddleware(int month, int day, RequestDelegate next)
        {
            _month = month;
            _day = day;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if(_month == DateTime.Now.Month && _day == DateTime.Now.Day)
            {
                var response = context.Response;

                response.ContentType = "application/json";
                response.StatusCode = StatusCodes.Status418ImATeapot;

                await response.WriteAsync("");
            }
            else
                await _next(context);
        }

    }
}
