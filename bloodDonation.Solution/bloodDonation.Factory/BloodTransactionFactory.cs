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

        public BloodTransactionFactory(IBloodTransactionDAL bloodTransactionDAL)
        {
            _bloodTransactionDAL = bloodTransactionDAL;
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
