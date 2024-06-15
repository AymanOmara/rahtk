using Microsoft.AspNetCore.Identity;
using Rahtk.Domain.Features.Products;
using Rahtk.Domain.Features.Reminder;

namespace Rahtk.Domain.Features.User
{
	public class RahtkUser : IdentityUser
    {
        public string RefreshToken { get; set; } = string.Empty;

        public string FcmToken { get; set; } = string.Empty;

        public string VerificationToken { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public ICollection<FavoriteProductUser>? FavoriteProductUsers { get; set; }

        public ICollection<AddressEntity>? Addresses { get; set; }

        public ICollection<PaymentOptionEntity>? PaymentOptions { get; set; }

        public ICollection<ReminderEntity>? PeriodicallyOrders { get; set; }

        public ICollection<NotificationEntity>? Notifications { get; set; }
    }
}