using Business.ServiceResult.Interfaces;

namespace Coffee.Services.Interfaces
{
    public interface ICoffeeService
    {
        Task<IServiceResult<string>> GetCoffee();
    }
}
