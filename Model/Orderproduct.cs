using System;
using System.Collections.Generic;

namespace demoPP.Model;

public partial class Orderproduct
{
    public int OrderId { get; set; }

    public string ProductArticleNumber { get; set; } = null!;

    public int? OrderProductAmount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product ProductArticleNumberNavigation { get; set; } = null!;
}
