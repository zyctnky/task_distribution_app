﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_distribution_app.Models.ViewModels;

namespace task_distribution_app.DataAccess.User
{
    public interface IUserDataAccess
    {
        UserVM Login(string username, string password);
        List<UserVM> GetList(int role_id);
        List<DeveloperVM> GetDeveloperList();
        DeveloperVM GetDeveloperById(int developerId);
    }
}
