using Models;

namespace Helpers
{
    public interface ITokenHelper
    {
        string CreateToken(User user);
    }
}