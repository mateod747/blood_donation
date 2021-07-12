using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public interface IBloodTransactionDAL
    {
        Task<BloodTransactionModel> GetBloodTransaction(int id);
        Task<bool> PostBloodTransaction(BloodTransactionModel model);
        Task<bool> EditBloodTransaction(BloodTransactionModel model);
        Task<bool> DeleteBloodTransaction(int id);
    }
}
