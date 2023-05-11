using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.GeneratedModels;

namespace Service.UsersService
{
    public interface IUserData
    {
        Task<int> isExsitsUser(string  tz);

        Task<string> createUser(User user);
        Task<string> validate(User user);
    }
}
