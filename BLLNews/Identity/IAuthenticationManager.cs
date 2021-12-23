using BLLNews.DataTransferObjects.UserDto;
using System.Threading.Tasks;
namespace DALNews.Identity
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
