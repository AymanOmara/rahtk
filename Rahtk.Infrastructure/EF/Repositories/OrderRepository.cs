using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Features.Order;
using Rahtk.Domain.Features.Order;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class OrderRepository(RahtkContext context, UserManager<RahtkUser> userManager) : IOrderRepository
    {
        public async Task<OrderEntity> CreateOrder(CreateOrderModel order, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var productIds = order?.OrderItemModel?.Select(e => e.ProductId).ToList() ?? new List<int>();
            var selectedProducts = await context.Products.Where(pro => productIds.Contains(pro.Id)).ToListAsync();
            List<OrderItemEntity> selectedProduct = new();

            if (order?.OrderItemModel != null)
            {
                foreach (var element in order.OrderItemModel)
                {
                    var isFound = selectedProducts.FirstOrDefault(pro => pro.Id == element.ProductId);
                    if (isFound != null)
                    {
                        isFound.PurchasementCount += 1;
                        context.Entry(isFound).State = EntityState.Modified;
                        selectedProduct.Add(new OrderItemEntity { ProductId = element.ProductId, ProductCounter = element.ProductCount });
                    }
                }
            }

            var orderEntity = new OrderEntity { Items = selectedProduct, PaymentMethod = order?.PaymentMethod, PaymentOptionId = order?.PaymentId, AddressId = order?.AddressId, UserId = user.Id };
            await context.Orders.AddAsync(orderEntity);
            return orderEntity;
        }

        public async Task<ICollection<OrderEntity>> GetOrders(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var result = await context.Orders.AsNoTracking().Include(or => or.Items).ThenInclude(pr => pr.Product).Include(add => add.Address).Include(pay => pay.Payment).Where(or => or.UserId == user.Id).ToListAsync();
            return result;
        }
    }
}