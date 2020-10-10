using System.Threading.Tasks;
using Models;

namespace Helpers
{
    public interface IApiHelper
    {
        #region TestMethods
        Task<Value> GetValueById(string id);
        Task<Value> AddValue(Value value);
        #endregion

        #region UserMethods
        Task<bool> AddUser(User user, string password);
        Task<User> Login(string email, string password);
        #endregion

    }
}