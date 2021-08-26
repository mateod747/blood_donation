using System;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public interface ILoginFactory
    {
        Task<(string, Guid, bool)> GetLoginData(string username, string password);
    }
}