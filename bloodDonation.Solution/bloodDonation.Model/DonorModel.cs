using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Model
{
    public class DonorModel
    {
        public Guid DonorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BloodType { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    }
}
