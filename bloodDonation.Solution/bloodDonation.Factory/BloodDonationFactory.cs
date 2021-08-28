using bloodDonation.DAL;
using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public class BloodDonationFactory : IBloodDonationFactory
    {
        public readonly IBloodDonationDAL _bloodDonationDAL;

        public BloodDonationFactory(IBloodDonationDAL bloodDonationDAL)
        {
            _bloodDonationDAL = bloodDonationDAL;
        }

        public async Task<BloodDonationModel> GetBloodDonation(Guid id)
        {
            return await _bloodDonationDAL.GetBloodDonation(1, 1, 1);
        }

        public async Task<bool> PostBloodDonation(BloodDonationModel model)
        {
            return await _bloodDonationDAL.PostBloodDonation(model);
        }

        public async Task<bool> EditBloodDonation(BloodDonationModel model)
        {
            return await _bloodDonationDAL.EditBloodDonation(model);
        }

        public async Task<bool> DeleteBloodDonation(Guid id)
        {
            return await _bloodDonationDAL.DeleteBloodDonation(id);
        }
    }
}
