using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public interface IBloodDonationFactory
    {
        Task<BloodDonationModel> GetBloodDonationByDateIn(int year, int month, int day, Guid donorId);
        Task<bool> PostBloodDonation(BloodDonationModel model);
        Task<bool> EditBloodDonation(BloodDonationModel model);
        Task<bool> DeleteBloodDonation(Guid id);
    }
}
