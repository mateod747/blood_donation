using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Model
{
    public class RecipientModel
    {
        public Guid RecipientID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BloodType { get; set; }
        public Anon Anon { get; set; }
    }

    public enum Anon
    {
        Anonymous = 0,
        Known = 1,
        Science = 2
    }
}
