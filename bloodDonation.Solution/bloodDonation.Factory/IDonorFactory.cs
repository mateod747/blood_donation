using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public interface IDonorFactory
    {
        Task<DonorModel> GetDonor(int id);
        Task<bool> PostDonor(DonorModel model);
        Task<bool> EditDonor(DonorModel model);
        Task<bool> DeleteDonor(int id);
    }
}
