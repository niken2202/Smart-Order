using System;

namespace Common.ViewModels
{
    public class BillViewModel
    {
        public int ID { set; get; }
        public string Voucher { get; set; }

        public string CustomerName { get; set; }

        public string Content { get; set; }
        public int TableID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TableName { get; set; }
        public double Total { get; set; }
        public string CreatedBy { set; get; }
        public int? Discount { set; get; }
        public bool Status { get; set; }
    }
}