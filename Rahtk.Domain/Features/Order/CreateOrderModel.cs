namespace Rahtk.Domain.Features.Order
{
    public class CreateOrderModel
    {
        public int? PaymentId { get; set; }

        public int? AddressId { get; set; }

        public string? PaymentMethod { get; set; }

        public ICollection<OrderItemModel>? OrderItemModel { get; set; }

        public ICollection<OrderItemModel>? DrugItemModel { get; set; }

    }
    public class OrderItemModel
    {
        public int ProductId { get; set; }

        public int ProductCount { get; set; }
    }
}