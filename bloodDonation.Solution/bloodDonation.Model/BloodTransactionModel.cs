using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Model
{
    public class BloodTransactionModel
    {
        public Guid TransactID { get; set; }
        public Guid BloodID { get; set; }
        public Guid RecipientID { get; set; }
        public Guid EmpID { get; set; }
        public DateTime DateOut { get; set; }
        public int Quantity { get; set; }
        public int Hemoglobin { get; set; }
        public string BloodPressure { get; set; }
        public string Notes { get; set; }
        public bool Success { get; set; }
    }
}
