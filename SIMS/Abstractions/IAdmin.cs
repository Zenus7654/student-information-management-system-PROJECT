using SIMS.Models;
namespace SIMS.Abstractions
{
    public interface IAdmin
    {
        Administrator GetAdminByUsername(string username);
        bool ValidateAdmin(string username, string password);
        void AddAdmin(Administrator admin);
    }
}
