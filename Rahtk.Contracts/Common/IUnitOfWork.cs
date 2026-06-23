using Rahtk.Contracts.Features;
using Rahtk.Contracts.Features.Address;
using Rahtk.Contracts.Features.Order;
using Rahtk.Contracts.Features.Payment;
using Rahtk.Contracts.Features.Product.Product;
using Rahtk.Contracts.Features.Reminder;
using Rahtk.Contracts.Features.User;
using Rahtk.Contracts.Features.Product.Category;

namespace Rahtk.Contracts.Common
{
	public interface IUnitOfWork
	{
        public IUserRepository Users
        {
            get;
        }

        public ICategoryRepository Category
        {
            get;
        }

        public IProductRepository Product
        {
            get;
        }

        public IAddressRepository Address
        {
            get;
        }
        public IPaymentRepository Payment
        {
            get;
        }
        public IOrderRepository Order
        {
            get;
        }
        public IReminderRepository Reminder
        {
            get;
        }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void RegisterPostCommitAction(Func<Task> action);
    }
}