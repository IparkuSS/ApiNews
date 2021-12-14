using Entities.DataTransferObjects.UserDto;
using System.Threading.Tasks;
namespace Contract.Identity
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
