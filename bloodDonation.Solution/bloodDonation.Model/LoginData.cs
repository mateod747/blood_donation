using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Model
{
    public class LoginData
    {
        public int donorID { get; set; }
        public string username { get; set; }
        public string passwordHash { get; set; }
    }
}
