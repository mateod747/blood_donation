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
        Task<(DonorModel, int)> GetDonor(Guid id);
        Task<bool> PostDonor(DonorModel model);
        Task<bool> EditDonor(DonorModel model);
        Task<bool> DeleteDonor(Guid id);
    }
}
