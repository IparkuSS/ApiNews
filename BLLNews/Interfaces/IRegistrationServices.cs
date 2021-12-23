using BLLNews.DataTransferObjects.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLLNews.Interfaces
{
    public interface IRegistrationServices
    {
        Task<bool> RegisterUser(UserForRegistrationDto userForRegistration);
    }
}
