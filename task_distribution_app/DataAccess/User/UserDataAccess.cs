using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using task_distribution_app.Models.DTO;
using task_distribution_app.Models.ViewModels;
using task_distribution_app.Repository;

namespace task_distribution_app.DataAccess.User
{
    public class UserDataAccess : IUserDataAccess
    {
        IGenericRepository<TUSER> _userRepo;
        IGenericRepository<TROLE> _roleRepo;
        TaskDistributionEntities _context;
        public USER Login(string username, string password)
        {
            using (_context = new TaskDistributionEntities())
            {
                _userRepo = new GenericRepository<TUSER>(_context);
                _roleRepo = new GenericRepository<TROLE>(_context);
                TUSER user = _userRepo.FindByLambda(u => u.USER_USERNAME == username && u.USER_PASSWORD == password);
                if (user == null)
                    return null;

                TROLE role = _roleRepo.FindById(user.USER_ROL_ID);
                return new USER()
                {
                    id = user.USER_ID,
                    username = user.USER_USERNAME,
                    fullname = user.USER_FULLNAME,
                    rol_id = user.USER_ROL_ID,
                    rol_name = role != null ? role.ROLE_NAME : ""
                };
            }
        }
    }
}