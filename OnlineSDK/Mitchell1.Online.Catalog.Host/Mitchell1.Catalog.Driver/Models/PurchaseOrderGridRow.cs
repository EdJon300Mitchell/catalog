using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Catalog.Driver.Models
{
    public class PurchaseOrderGridRow : PurchaseOrder
    {
        public bool WasRetrieved { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
    }
}
