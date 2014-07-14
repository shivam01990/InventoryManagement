using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class TransactionPurchaseEntity
    {
        public int ID { get; set; }
        public string DelarName { get; set; }
        public string ProductName { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public string PaymentType { get; set; }
        public string Remarks { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
