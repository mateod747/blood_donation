using System;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public interface IPasswordHash
    {
        Task<bool> HashPassword(string username, string password, Guid donorId, bool admin);
        Task<(string, Guid, bool)> ValidatePassword(string username, string password);
    }
}