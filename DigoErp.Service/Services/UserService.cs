using DigoErp.Resources.App_Resources;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.Service.Services
{
    public class UserService : BaseService
    {
        public DataTableResponse<User> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var currentUser = HttpContext.Current.Session["UserDetail"] as User ?? new User();

                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Repository.Edmx.Tbl_User, bool>> filter =
                        b =>
                        b.FullName.ToString().Contains(searchModel.SearchTerm)
                        || (b.Email.Contains(searchModel.SearchTerm) && b.Email != currentUser.Email && b.Tbl_Role.Name != "Super Admin");

                    var tableResponse = UnitOfWork.UserRepository.GetPagination<Repository.Edmx.Tbl_User>(take, skip, filter, c => c.OrderBy(o => o.FullName));

                    var response = new DataTableResponse<User>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {
                    var tableResponse = UnitOfWork.UserRepository.GetPagination<Repository.Edmx.Tbl_User>(take, skip,x=>x.Email != currentUser.Email && x.Tbl_Role.Name != "Super Admin", c => c.OrderBy(o => o.FullName));

                    var response = new DataTableResponse<User>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new DataTableResponse<User>();
            }
        }

        public List<Role> GetRoles()
        {
            try
            {
                var roles = UnitOfWork.RoleRepository.Get(r=>r.Name != "Super Admin");
                return roles.Select(r => new Role
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList();
            }
            catch (Exception)
            {
                return new List<Role>();
            }
        }

        public User Login(string email, string password)
        {
            try
            {
                var user = UnitOfWork.UserRepository.Get(x => x.Email == email).FirstOrDefault()?.MapFrom();

                if (!password.Equals(user?.Password))
                {
                    throw new Exception(UserRes.WrongPassword);
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(UserRes.WrongPassword);
            }
        }

        public void AddOrUpdate(User user)
        {
            if (user.Id > 0)
            {
                var dbUser = UnitOfWork.UserRepository.GetByIdAsNoTracking(user.Id);
                user.Created_At = dbUser.Created_At;
                UnitOfWork.UserRepository.Update(user.MapFrom());
            }
            else
            {
                UnitOfWork.UserRepository.Insert(user.MapFrom());
            }
            UnitOfWork.Save();
        }

        public User GetById(long userId)
        {
            try
            {
                return UnitOfWork.UserRepository.GetByID(userId)?.MapFrom();
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public void ResetPasswordCode(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int batchId)
        {
            UnitOfWork.UserRepository.Delete(batchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public User GetByEmail(string email)
        {
            try
            {
                var user = UnitOfWork.UserRepository.Get(x => x.Email == email)?.FirstOrDefault();
                return user?.MapFrom();
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public User GetByResetPasswordCode(string resetCode)
        {
            try
            {
                //var user = UnitOfWork.UserRepository.Get(u => u.ResetPasswordCode == resetPasswordCode).FirstOrDefault();
                //return user?.MapFrom();\
                return new User();
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public void UpdateBySqlQuery(string sqlQuery)
        {
            try
            {
                UnitOfWork.UserRepository.RunSqlQuery(sqlQuery);
            }
            catch (Exception)
            {
            }
        }
    }
}
