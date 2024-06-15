using System.ComponentModel.DataAnnotations.Schema;
using Rahtk.Domain.Features.User;

namespace Rahtk.Domain.Features.Order
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public AddressEntity? Address { get; set; }

        [ForeignKey("AddressId")]
        public int? AddressId { get; set; }

        public RahtkUser? User { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<OrderItemEntity>? Items { get; set; }

        public PaymentOptionEntity? Payment { get; set; }

        [ForeignKey("PaymentOptionId")]
        public int? PaymentOptionId { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;
    }

}

