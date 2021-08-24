using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public interface IDonorDAL
    {
        Task<DonorModel> GetDonor(Guid id);
        Task<bool> PostDonor(DonorModel model);
        Task<bool> EditDonor(DonorModel model);
        Task<bool> DeleteDonor(Guid id);
    }
}
