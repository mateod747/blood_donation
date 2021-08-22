using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public interface IRecipientFactory
    {
        Task<RecipientModel> GetRecipient(int id);
        Task<bool> PostRecipient(RecipientModel model);
        Task<bool> EditRecipient(RecipientModel model);
        Task<bool> DeleteRecipient(int id);
    }
}
