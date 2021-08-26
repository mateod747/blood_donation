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
        private readonly IBloodStockDAL _bloodStockDAL;
        private readonly IPasswordHash _passwordHash;

        public DonorFactory(IDonorDAL donorDal, IBloodStockDAL bloodStockDAL, IPasswordHash passwordHash)
        {
            _donorDal = donorDal;
            _bloodStockDAL = bloodStockDAL;
            _passwordHash = passwordHash;
        }

        public async Task<(DonorModel, int)> GetDonor(Guid id)
        {
            var donor = await _donorDal.GetDonor(id);
            var bloodStock = await _bloodStockDAL.GetBloodStockAsync();

            var stock = 0;

            switch (donor.BloodType)
            {
                case "0-":
                    stock = bloodStock.ZeroMinus;
                    break;
                case "0+":
                    stock = bloodStock.ZeroPlus;
                    break;
                case "A-":
                    stock = bloodStock.AMinus;
                    break;
                case "A+":
                    stock = bloodStock.APlus;
                    break;
                case "B-":
                    stock = bloodStock.BMinus;
                    break;
                case "B+":
                    stock = bloodStock.BPlus;
                    break;
                case "AB-":
                    stock = bloodStock.ABMinus;
                    break;
                case "AB+":
                    stock = bloodStock.ABPlus;
                    break;
            }
            return (donor, stock);
        }

        public async Task<bool> PostDonor(string username, string password, DonorModel model, bool admin)
        {
            model.DonorID = Guid.NewGuid();

            var success = await _passwordHash.HashPassword(username, password, model.DonorID, admin);

            if (!success) return false;

            success = await _donorDal.PostDonor(model);

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
