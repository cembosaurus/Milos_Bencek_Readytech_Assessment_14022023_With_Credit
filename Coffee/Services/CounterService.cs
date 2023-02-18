using Coffee.Services.Interfaces;

namespace Coffee.Services
{
    public class CounterService : ICounterService
    {

        private int _requestCount;


		public bool IsFifth
		{
			get 
			{
                _requestCount = ++_requestCount == 5 ? 0 : _requestCount;

				return _requestCount == 0 ? true: false; 
			}
		}

	}
}
