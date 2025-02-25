using iFood.Models;
using iFood.Models.Momo;

namespace iFood.Interfaces;

public interface IMomoService 
{
    Task<MomoCreatePaymentResponseModel> CreatePaymentMomoAsync(OrderInfo model);
    Task<MomoExecuteResponseModel> PaymentExecuteAsync(IQueryCollection collection);
}
