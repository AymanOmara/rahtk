using System.ComponentModel.DataAnnotations.Schema;
using Rahtk.Domain.Features.Pharmacy;

namespace Rahtk.Domain.Features.Order
{
    public class OrderDrugItemEntity
    {
        public int Id { get; set; }

        public DrugEntity? Drug { get; set; }

        [ForeignKey("DrugId")]
        public int? DrugId { get; set; }

        public int DrugCounter { get; set; }
    }
}