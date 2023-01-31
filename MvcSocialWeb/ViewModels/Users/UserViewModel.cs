﻿using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.ViewModels.Users
{
    public class UserViewModel
    {
        public User User { get; set; }

        public UserViewModel(User user)
        {
            User = user;
        }
    }
}
