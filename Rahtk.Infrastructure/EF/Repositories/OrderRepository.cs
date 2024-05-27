using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Features.Order;
using Rahtk.Domain.Features.Order;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RahtkContext _context;
        private readonly UserManager<RahtkUser> _userManager;
        public OrderRepository(RahtkContext context, UserManager<RahtkUser> userManager)
        {
            _context = context;

            _userManager = userManager;
        }

        public async Task<OrderEntity> CreateOrder(CreateOrderModel order, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var allProducts = await _context.Products.ToListAsync();
            List<OrderItemEntity> selectedProduct = new();

            foreach (var element in order.OrderItemModel)
            {
                var isFound = allProducts.FirstOrDefault(pro => pro.Id == element.ProductId);
                if (isFound != null)
                {
                    isFound.PurchasementCount += 1;
                    _context.Entry(isFound).State = EntityState.Modified;
                    selectedProduct.Add(new OrderItemEntity { ProductId = element.ProductId, ProductCounter = element.ProductCount });
                }
            }
            var orderEntity = new OrderEntity { Items = selectedProduct, PaymentMethod = order.PaymentMethod, PaymentOptionId = order.PaymentId, AddressId = order.AddressId, UserId = user.Id };
            await _context.Orders.AddAsync(orderEntity);
            await _context.SaveChangesAsync();
            return orderEntity;
        }

        public async Task<ICollection<OrderEntity>> GetOrders(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _context.Orders.Include(or=>or.Items).ThenInclude(pr=>pr.Product).Include(add=> add.Address).Include(pay=> pay.Payment).Where(or => or.UserId == user.Id).ToListAsync();
            return result;
        }
    }
}

