using Dinein_UserApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dinein_UserApp.Models
{
    public class UserModel
    {
        private readonly DataBase _dataBase;

        public UserModel()
        {
            _dataBase = new DataBase();
        }

        public async Task<bool> RegisterUser(string email, string name, string password)
        {
            return await _dataBase.Register(email, name, password);
        }
    }
}