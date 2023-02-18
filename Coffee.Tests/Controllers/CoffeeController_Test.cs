using Business.ServiceResult.Interfaces;
using Coffee.Controllers;
using Coffee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Coffee.Tests.Controllers
{

    [TestFixture]
    internal class CoffeeController_Test
    {

        private CoffeeController? _controller;
        private Mock<ICoffeeService> _coffeeService;
        private Mock<ICounterService> _counterService;
        private Mock<IServiceResult<string>> _coffeeServiceResult;


        [SetUp]
        public void Setup()
        {
            _counterService = new Mock<ICounterService>();            
            _coffeeService = new Mock<ICoffeeService>();
            _coffeeServiceResult = new Mock<IServiceResult<string>>();
            _coffeeServiceResult.Setup(csr => csr.Data).Returns(It.IsAny<string>());
            _coffeeServiceResult.Setup(csr => csr.Status).Returns(It.IsAny<bool>());
            _coffeeServiceResult.Setup(csr => csr.Message).Returns(It.IsAny<string>());
            _coffeeService.Setup(cfs => cfs.GetCoffee())
                .Returns(Task.FromResult(_coffeeServiceResult.Object));
        }




        [Test]
        public void GetCoffee_WhenCalled_ReturnsOkResult()
        {
            _counterService.Setup(cns => cns.IsFifth).Returns(false);

            _controller = new CoffeeController(_coffeeService.Object, _counterService.Object);


            var result = _controller.GetCoffee().Result;


            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }



        [Test]
        public void GetCoffee_WhenCalledFifthTime_Returns503Result()
        {
            _counterService.Setup(cns => cns.IsFifth).Returns(true);

            _controller = new CoffeeController(_coffeeService.Object, _counterService.Object);


            var result = _controller.GetCoffee().Result;


            Assert.That(result, Is.TypeOf<ObjectResult>());
        }


    }
}
