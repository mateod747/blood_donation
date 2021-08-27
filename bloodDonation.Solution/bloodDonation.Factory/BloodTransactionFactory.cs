using bloodDonation.DAL;
using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public class BloodTransactionFactory : IBloodTransactionFactory
    {
        public readonly IBloodTransactionDAL _bloodTransactionDAL;
        public readonly IDonorDAL _donorDAL;
        public readonly IMedicalPersonnelDAL _medicalPersonnelDAL;
        public readonly IBloodDonationDAL _bloodDonationDAL;

        public BloodTransactionFactory(IBloodTransactionDAL bloodTransactionDAL, IDonorDAL donorDAL, IMedicalPersonnelDAL medicalPersonnelDAL, IBloodDonationDAL bloodDonationDAL)
        {
            _bloodTransactionDAL = bloodTransactionDAL;
            _donorDAL = donorDAL;
            _medicalPersonnelDAL = medicalPersonnelDAL;
            _bloodDonationDAL = bloodDonationDAL;
        }

        public async Task<(Guid, string)> GetDonorIdAndCheckIfPersonnelExists(string username, Guid personnelId)
        {
            var message = String.Empty;
            var donorId = await _donorDAL.GetDonorIdByUsername(username);
            
            if(donorId == null || donorId == Guid.Empty)
            {
                message = "Donor ne postoji!";
                return (Guid.Empty, message);
            }

            var personnelCheck = await _medicalPersonnelDAL.GetMedicalPersonnel(personnelId);

            if(personnelCheck == null || personnelCheck.EmpID == Guid.Empty)
            {
                message = "Osoblje ne postoji";
                return (Guid.Empty, message);
            }

            return (donorId, message); 
        }

        public async Task<string> GetExistingTransactionDonorId(int year, int month, int day)
        {
            var result = await _bloodDonationDAL.GetBloodDonation(year, month, day);

            if (result == null || result.BloodID == Guid.Empty) return "";

            return result.BloodID.ToString();
        }

        public async Task<BloodTransactionModel> GetBloodTransaction(Guid id)
        {
            return await _bloodTransactionDAL.GetBloodTransaction(id);
        }

        public async Task<bool> PostBloodTransaction(BloodTransactionModel model)
        {
            model.TransactID = Guid.NewGuid();
            return await _bloodTransactionDAL.PostBloodTransaction(model);
        }

        public async Task<bool> EditBloodTransaction(BloodTransactionModel model)
        {
            return await _bloodTransactionDAL.EditBloodTransaction(model);
        }

        public async Task<bool> DeleteBloodTransaction(Guid id)
        {
            return await _bloodTransactionDAL.DeleteBloodTransaction(id);
        }
    }
}
