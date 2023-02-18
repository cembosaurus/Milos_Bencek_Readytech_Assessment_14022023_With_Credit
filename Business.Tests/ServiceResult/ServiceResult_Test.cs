using Business.Middlewares;
using Business.ServiceResult;
using Business.ServiceResult.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Net.Http;

namespace Business.Tests.ServiceResult
{
    internal class ServiceResult_Test
    {
        private IServiceResultFactory _serviceResultFact;

        [SetUp]
        public void SetUp()
        {
            _serviceResultFact = new ServiceResultFactory();
        }




        [Test]
        [TestCase("test data", true, "test string message")]
        [TestCase(3, false, "test integer message")]
        [TestCase(true, true, "test bool message")]
        public void ServiceResultFactory_WhenCalled_ShouldReturnDataStatusAndMessage<T>(T Data, bool Status, string Message)
        {
            var result = _serviceResultFact.Result(Data, Status, Message);


            Assert.That(result.Data, Is.TypeOf<T>());
            Assert.That(result.Data, Is.EqualTo(Data));
            Assert.That(result.Status, Is.TypeOf<bool>());
            Assert.That(result.Status, Is.EqualTo(Status));
            Assert.That(result.Message, Is.TypeOf<string>());
            Assert.That(result.Message, Is.EqualTo(Message));
        }
    }
}
