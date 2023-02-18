using Business.Middlewares;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Business.Tests.Middlewares
{

    [TestFixture]
    internal class Http418HandlerMiddleware_Test
    {

        private DefaultHttpContext _httpContext;
        private RequestDelegate _requestDelegate;
        private Http418HandlerMiddleware? _middleware;
        private int _month, _day;


        [SetUp]
        public void SetUp()
        {
            _httpContext = new DefaultHttpContext();

            _requestDelegate = new RequestDelegate((innerContext) => Task.FromResult(0));

            _month = DateTime.Now.Month;
            _day = DateTime.Now.Day;
        }





        [Test]
        public void Invoke_WhenCalledAtPresentDay_Returns418HttpResponse()
        {
            _middleware = new Http418HandlerMiddleware(_month, _day, _requestDelegate);


            _middleware?.Invoke(_httpContext);


            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(StatusCodes.Status418ImATeapot));
        }



        [Test]
        public void Invoke_WhenCalledAnyDayExceptPresentDay_Returns200HttpResponse()
        {
            _middleware = new Http418HandlerMiddleware(_month, _day + 1, _requestDelegate);


            _middleware?.Invoke(_httpContext);


            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

    }
}
