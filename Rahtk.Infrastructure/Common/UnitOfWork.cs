using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features;
using Rahtk.Contracts.Features.Address;
using Rahtk.Contracts.Features.Order;
using Rahtk.Contracts.Features.Payment;
using Rahtk.Contracts.Features.Product.Product;
using Rahtk.Contracts.Features.Reminder;
using Rahtk.Contracts.Features.User;
using Rahtk.Contracts.Features.Product.Category;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.Common
{
    public class UnitOfWork(
        RahtkContext context,
        IUserRepository users,
        ICategoryRepository category,
        IProductRepository product,
        IAddressRepository address,
        IPaymentRepository payment,
        IOrderRepository order,
        IReminderRepository reminder
    ) : IUnitOfWork
    {
        public IUserRepository Users { get; } = users;
        public ICategoryRepository Category { get; } = category;
        public IProductRepository Product { get; } = product;
        public IAddressRepository Address { get; } = address;
        public IPaymentRepository Payment { get; } = payment;
        public IOrderRepository Order { get; } = order;
        public IReminderRepository Reminder { get; } = reminder;

        private readonly List<Func<Task>> _postCommitActions = new();

        public void RegisterPostCommitAction(Func<Task> action)
        {
            _postCommitActions.Add(action);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await context.SaveChangesAsync(cancellationToken);
            foreach (var action in _postCommitActions)
            {
                await action();
            }
            _postCommitActions.Clear();
            return result;
        }
    }
}