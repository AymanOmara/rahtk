using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Order;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Order
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LanguageService _languageService;
        public OrderService(IUnitOfWork unitOfWork, LanguageService languageService)
        {
            _unitOfWork = unitOfWork;

            _languageService = languageService;
        }

        public async Task<BaseResponse<OrderEntity>> CreateOrder(CreateOrderModel model, string userEmail)
        {
            var result = await _unitOfWork.Order.CreateOrder(model, userEmail);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<OrderEntity> { Data = result, StatusCode = 200, Success = true };
        }

        public async Task<BaseResponse<ICollection<OrderEntity>>> GetOrders(string userEmail)
        {
            var result = await _unitOfWork.Order.GetOrders(userEmail);
            return new BaseResponse<ICollection<OrderEntity>> { Data = result, StatusCode = 200, Success = true };
        }
    }
}

