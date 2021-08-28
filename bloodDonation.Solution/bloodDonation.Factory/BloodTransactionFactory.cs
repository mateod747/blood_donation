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
        public readonly IRecipientDAL _recipientDAL;

        public BloodTransactionFactory(IBloodTransactionDAL bloodTransactionDAL, IDonorDAL donorDAL, IMedicalPersonnelDAL medicalPersonnelDAL, IBloodDonationDAL bloodDonationDAL, IRecipientDAL recipientDAL)
        {
            _bloodTransactionDAL = bloodTransactionDAL;
            _donorDAL = donorDAL;
            _medicalPersonnelDAL = medicalPersonnelDAL;
            _bloodDonationDAL = bloodDonationDAL;
            _recipientDAL = recipientDAL;
        }

        public async Task<(Guid, string)> GetDonorIdAndCheckIfPersonnelExists(string username, Guid personnelId, string recipientId)
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
                message = "Osoblje ne postoji!";
                return (Guid.Empty, message);
            }

            var recipientGuid = Guid.Empty;

            if (recipientId == "") recipientId = "AA4DE667-8D99-461A-BBA1-73815EBB0EFE";

            if (!Guid.TryParse(recipientId, out recipientGuid))
            {
                message = "Primalac ne postoji!";
                return (Guid.Empty, message);
            }

            var recipientCheck = await _recipientDAL.GetRecipient(recipientGuid);

            if (recipientCheck == null || recipientCheck.RecipientID == Guid.Empty)
            {
                message = "Primalac ne postoji!";
                return (Guid.Empty, message);
            }

            return (donorId, message); 
        }

        public async Task<(Guid, BloodTransactionModel)> GetDonorIdAndBloodTransactionByUsernameAndDate(string username, int year, int month, int day)
        {
            var bloodTransaction = new BloodTransactionModel();
            
            var donorId = await _donorDAL.GetDonorIdByUsername(username);

            if (donorId == null || donorId == Guid.Empty)
            {
                bloodTransaction.Notes = "Donor ne postoji!";
                return (Guid.Empty, bloodTransaction);
            }

            var result = await _bloodDonationDAL.GetBloodDonation(year, month, day);

            if (result.BloodID == null || result.BloodID == Guid.Empty)
            {
                bloodTransaction.Notes = "Nepostojeća donacija na taj dan!";
                return (Guid.Empty, bloodTransaction);
            }

            bloodTransaction = await _bloodTransactionDAL.GetBloodTransaction(result.BloodID);

            if(bloodTransaction.TransactID == null || bloodTransaction.TransactID == Guid.Empty)
            {
                bloodTransaction.Notes = "Greška u sustavu!";
                return (Guid.Empty, bloodTransaction);
            }

            return (donorId, bloodTransaction);
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
