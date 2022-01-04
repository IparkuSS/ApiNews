using News.BLL.DataTransferObjects.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace News.BLL.Interfaces
{
    public interface IRegistrationServices
    {
        Task<bool> RegisterUser(UserForRegistrationDto userForRegistration);
    }
}
