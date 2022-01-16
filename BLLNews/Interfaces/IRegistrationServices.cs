using News.BLL.DataTransferObjects.UserDto;
using System.Threading.Tasks;
namespace News.BLL.Interfaces
{
    public interface IRegistrationServices
    {
        Task<bool> RegisterUser(UserForRegistrationDto userForRegistration);
    }
}
