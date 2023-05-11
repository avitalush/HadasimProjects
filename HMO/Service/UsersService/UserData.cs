using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.UsersService
{
    public class UserData : IUserData
    {
        private readonly MyDBContext _context;

        public UserData(MyDBContext context)
        {
            _context = context;
        }
        public Task<int> isExsitsUser(string tz)
        {
            var checkID = _context.Users.Where(u => u.UserTz == tz).FirstOrDefault();
            if (checkID != null)
            {
                return Task.FromResult(checkID.UserId);
            }
            return Task.FromResult(-1);
        }
        public async Task<string> createUser(User user)
        {
            var result = await validate(user);
            if ( result!= "true")
            {
                return result;
            }

            var isExsistsMember = await isExsitsUser(user.UserTz);
            var isOk = false;

            if (isExsistsMember == -1)
            {
                await _context.AddAsync(user);
                isOk = await _context.SaveChangesAsync() >= 0;
                if (isOk) return "Added successfully";
            }
            return "User exist";
        }
        public static bool ValidateId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            
            if (id.Length != 9)
                return false;

          
            if (!id.All(char.IsDigit))
                return false;

            int[] factors = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = int.Parse(id[i].ToString());
                int product = digit * factors[i];
                if (product > 9)
                    product -= 9;
                sum += product;
            }
            return sum % 10 == 0;
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            if (!phoneNumber.All(char.IsDigit))
                return false;

         
            if (phoneNumber.Length < 10 || phoneNumber.Length > 15)
                return false;

            
            if (!(phoneNumber.StartsWith("0") || phoneNumber.StartsWith("+")))
                return false;

            return true;
        }
        public static bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // וודא שהשם מכיל רק אותיות
            if (!name.All(char.IsLetter))
                return false;

            // וודא שהשם מכיל לפחות שתי מילים
            if (name.Split(' ').Length < 2)
                return false;

            return true;
        }

        public async Task<string> validate(User user) {
            if (!ValidateId(user.UserTz)) 
                return "Invalid ID number";
            if (!ValidatePhoneNumber(user.Phon))
                return "Invalid phon number";
            if (ValidateName(user.FullName))
                return "Invalid fullName";
            return "true";
        }


    }
}
