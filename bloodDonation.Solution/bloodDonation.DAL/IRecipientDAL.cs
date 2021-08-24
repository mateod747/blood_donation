using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public interface IRecipientDAL
    {
        Task<RecipientModel> GetRecipient(Guid id);
        Task<bool> PostRecipient(RecipientModel model);
        Task<bool> EditRecipient(RecipientModel model);
        Task<bool> DeleteRecipient(Guid id);
    }
}
