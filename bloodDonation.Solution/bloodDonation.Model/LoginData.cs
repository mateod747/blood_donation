using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Model
{
    public class LoginData
    {
        public Guid DonorID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool Admin { get; set; }
    }
}
