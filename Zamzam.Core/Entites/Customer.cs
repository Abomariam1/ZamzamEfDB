﻿using Zamzam.Domain.Common;
using Zamzam.Domain.Entites;

namespace Zamzam.Domain
{
    public class Customer : BaseAuditableEntity
    {
        public string Name { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public bool IsProplem { get; set; } = false;
        public bool IsBlackList { get; set; } = false;
        public int AreaId { get; set; }
        public virtual Area? Area { get; set; }
        public virtual ICollection<SaleOrder>? SaleOrders { get; set; }
        public virtual ICollection<InstallmentedSaleOrder>? InstallmentedSaleOrders { get; set; }
        public virtual ICollection<Installment>? Installments { get; set; }
    }
}
