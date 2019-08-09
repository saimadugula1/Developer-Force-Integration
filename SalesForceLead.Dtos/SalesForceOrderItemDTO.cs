using System;
using System.Collections.Generic;
using System.Text;

namespace SalesForceLeadDtos
{
    public class SalesForceOrderItemDTO
    {
        public const string SObjectTypeName = "OrderItem";

        public string AccountId { get; set; }

        public int OrderId { get; set; }

        public string OrderItemNumber { get; set; }

        public string OriginalOrderItemId { get; set; }

        public double AvailableQuantity { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public double ListPrice { get; set; }

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public string QuoteLineItemId { get; set; }

        public DateTime ServiceDate { get; set; }
    }
}
