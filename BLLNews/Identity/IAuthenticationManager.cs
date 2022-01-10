using News.BLL.DataTransferObjects.UserDto;
using System.Threading.Tasks;
namespace News.BLL.Identity
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);

        Task<string> CreateToken();
    }
}
