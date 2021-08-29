using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public interface IBloodDonationDAL
    {
        Task<BloodDonationModel> GetBloodDonation(int year, int month, int day, Guid donorId);
        Task<List<BloodDonationModel>> GetBloodDonationsAsync(Guid id);
        Task<bool> PostBloodDonation(BloodDonationModel model);
        Task<bool> EditBloodDonation(BloodDonationModel model);
        Task<bool> DeleteBloodDonation(Guid id);
    }
}
