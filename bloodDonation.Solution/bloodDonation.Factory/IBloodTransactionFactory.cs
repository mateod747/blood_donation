using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public interface IBloodTransactionFactory
    {
        Task<(Guid, string)> GetDonorIdAndCheckIfPersonnelExists(string username, Guid personnelId);
        Task<string> GetExistingTransactionDonorId(int year, int month, int day);
        Task<BloodTransactionModel> GetBloodTransaction(Guid id);
        Task<bool> PostBloodTransaction(BloodTransactionModel model);
        Task<bool> EditBloodTransaction(BloodTransactionModel model);
        Task<bool> DeleteBloodTransaction(Guid id);
    }
}
