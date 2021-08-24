using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Model
{
    public class BloodDonationModel
    {
        public Guid BloodID { get; set; }
        public Guid DonorID { get; set; }
        public DateTime DateDonated { get; set; }
    }
}
