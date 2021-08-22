using bloodDonation.DAL;
using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public class RecipientFactory : IRecipientFactory
    {
        public readonly IRecipientDAL _recipientDAL;

        public RecipientFactory(IRecipientDAL recipientDAL)
        {
            _recipientDAL = recipientDAL;
        }

        public async Task<RecipientModel> GetRecipient(int id)
        {
            return await _recipientDAL.GetRecipient(id);
        }

        public async Task<bool> PostRecipient(RecipientModel model)
        {
            return await _recipientDAL.PostRecipient(model);
        }

        public async Task<bool> EditRecipient(RecipientModel model)
        {
            return await _recipientDAL.EditRecipient(model);
        }

        public async Task<bool> DeleteRecipient(int id)
        {
            return await _recipientDAL.DeleteRecipient(id);
        }
    }
}
