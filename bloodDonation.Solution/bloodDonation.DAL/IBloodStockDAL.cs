using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public interface IBloodStockDAL
    {
        Task<BloodStockModel> GetBloodStockAsync();
        Task<bool> EditBloodStockAsync(BloodStockModel model);
    }
}
