using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Model
{
    public class BloodTransactionModel
    {
        public int TransactID { get; set; }
        public int BloodID { get; set; }
        public int RecipientID { get; set; }
        public int EmpID { get; set; }
        public DateTime DateOut { get; set; }
        public int Quantity { get; set; }
    }
}
