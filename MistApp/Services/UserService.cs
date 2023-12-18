using MistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistApp.Services
{
    public class UserService
    {
        private static UserService _instance;
        private User _currentUser;

        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserService();
                }
                return _instance;
            }
        }

        public User CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }
    }

}
