using bloodDonation.DAL;
using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public class DonorFactory : IDonorFactory
    {
        private readonly IDonorDAL _donorDal;

        public DonorFactory(IDonorDAL donorDal)
        {
            _donorDal = donorDal;
        }       

        public async Task<DonorModel> GetDonor(Guid id)
        {
            var donor = await _donorDal.GetDonor(id);
            return donor;
        }

        public async Task<bool> PostDonor(DonorModel model)
        {
            model.DonorID = Guid.NewGuid();
            var success = await _donorDal.PostDonor(model);
            return success;
        }

        public async Task<bool> EditDonor(DonorModel model)
        {
            var success = await _donorDal.EditDonor(model);
            return success;
        }

        public async Task<bool> DeleteDonor(Guid id)
        {
            var success = await _donorDal.DeleteDonor(id);
            return success;
        }
    }
}
