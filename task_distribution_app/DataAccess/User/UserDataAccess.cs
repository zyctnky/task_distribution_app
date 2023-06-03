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
        TaskDistributionEntities _context;
        public UserVM Login(string username, string password)
        {
            using (_context = new TaskDistributionEntities())
            {
                _userRepo = new GenericRepository<TUSER>(_context);
                TUSER user = _userRepo.FindByLambda(u => u.USER_USERNAME == username && u.USER_PASSWORD == password);
                if (user == null)
                    return null;

                return Map_UserToUserVM(user);
            }
        }

        public List<UserVM> GetList(int role_id = 0)
        {
            using (_context = new TaskDistributionEntities())
            {
                _userRepo = new GenericRepository<TUSER>(_context);
                List<TUSER> userlist = _userRepo.Select().ToList();

                List<UserVM> userVmList = new List<UserVM>();
                foreach (TUSER user in userlist)
                {
                    userVmList.Add(Map_UserToUserVM(user));
                }
                return userVmList;
            }
        }

        private UserVM Map_UserToUserVM(TUSER user)
        {
            return new UserVM()
            {
                id = user.USER_ID,
                username = user.USER_USERNAME,
                fullname = user.USER_FULLNAME,
                roleId = user.USER_ROLE_ID,
                roleName = user.TROLE.ROLE_NAME
            };
        }
    }
}