using System;

/// <summary>
/// required Fields for this Object are 
/// Account Name
/// </summary>
namespace SalesForceLeadDtos
{
    public class SalesForceOrderDTO
    {
        public const string SObjectTypeName = "Order";

        public string AccountId { get; set; }

        public int OrderId { get; set; }

        public string OrderItemNumber { get; set; }

        public string ShippingPostalCode { get; set; }

        public string ShippingState { get; set; }

        public string ShippingStateCode { get; set; }

        public string ShippingStreet { get; set; }

        public int ShipToContactId { get; set; }

        public double ShippingLongitude { get; set; }

        public double ShippingLatitude { get; set; }

        public string ShippingCountryCode { get; set; }

        public string ShippingCountry { get; set; }

        public string ShippingCity { get; set; }

        public int RecordTypeId { get; set; }

        public int QuoteId { get; set; }

        public int OriginalOrderId { get; set; }

        public string Name { get; set; }

        public int OpportunityId { get; set; }

        public int OrderNumber { get; set; }

        public int OrderReferenceNumber { get; set; }

        public string Status { get; set; }

        public string StatusCode { get; set; }

        public double TotalAmount { get; set; }

        public string Type { get; set; }

        public string BillingPostalCode { get; set; }

        public string BillingState { get; set; }

        public double BillingStateCode { get; set; }

        public string BillingStreet { get; set; }

        public string BillingCountry { get; set; }

        public string BillingCountryCode { get; set; }

        public double BillingLatitude { get; set; }

        public double BillingLongitude { get; set; }

        public string ActivatedById { get; set; }

        public DateTime ActivatedDate { get; set; }

        public string BillingCity { get; set; }

        public int BillToContactId { get; set; }

        public int ContractId { get; set; }

        public DateTime CustomerAuthorizedDate { get; set; }

        public string Description { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool IsReductionOrder { get; set; }

        public DateTime LastReferencedDate { get; set; }

        public DateTime LastViewedDate { get; set; }

        public SalesForceOrderItemDTO OrderItem { get; set; }
    }
}
