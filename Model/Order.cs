using System;
using System.Collections.Generic;

namespace demoPP.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public int? UserId { get; set; }

    public DateTime OrderDeliveryDate { get; set; }

    public string OrderPickupPoint { get; set; } = null!;

    public virtual ICollection<Orderproduct> Orderproducts { get; set; } = new List<Orderproduct>();

    public virtual User? User { get; set; }
}
