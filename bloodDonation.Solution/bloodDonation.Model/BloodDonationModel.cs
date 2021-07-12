using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Model
{
    public class BloodDonationModel
    {
        public int BloodID { get; set; }
        public int DonorID { get; set; }
        public DateTime DateDonated { get; set; }
        public int Quantity { get; set; }
    }
}
