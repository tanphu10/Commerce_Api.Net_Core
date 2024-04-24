using Commerce.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Domain.Content
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid UserId { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public OrderStatus Status { set; get; }

        public List<OrderDetail> OrderDetails { get; set; }

        public AppUser AppUser { get; set; }
        [Required]
        [MaxLength(250)]
        public string Note { get; set; }
    }
    public enum OrderStatus
    {
        InProgress = 0,
        Confirmed = 1,
        Shipping = 2,
        Success = 3,
        Canceled = 4
    }
}
