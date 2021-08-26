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
        Task<DonorModel> GetDonorByUsername(string username);
        Task<bool> PostDonor(string username, string password, DonorModel model, bool admin);
        Task<bool> EditDonor(DonorModel model);
        Task<bool> DeleteDonor(Guid id);
    }
}
