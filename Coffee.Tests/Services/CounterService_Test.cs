using Coffee.Services;
using Coffee.Services.Interfaces;

namespace Coffee.Tests.Services
{

    [TestFixture]
    internal class CounterService_Test
    {

        private ICounterService _counterService;


        [SetUp] 
        public void SetUp() 
        {
            _counterService = new CounterService();
        }




        [Test]
        public void IsFifth_WhenCalled_ReturnsFalse()
        {
            var result = _counterService.IsFifth;

            Assert.That(result, Is.False);
        }



        [Test]
        public void IsFifth_WhenCalledFifthTime_ReturnsTrue()
        {
            _ = _counterService.IsFifth;
            _ = _counterService.IsFifth;
            _ = _counterService.IsFifth;
            _ = _counterService.IsFifth;
            var result = _counterService.IsFifth;

            Assert.That(result, Is.True);
        }
    }
}
