using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bloodDonation.Factory.DonationListFactory;

namespace bloodDonation.Factory
{
    public interface IDonationListFactory
    {
        Task<DonationListDto> GetDonationListAsync(int page, int pageSize, int id);
    }
}
