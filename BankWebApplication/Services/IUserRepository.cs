using BankWebbApp.Data;
using BankWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Services
{
    interface IUserRepository
    {
        IQueryable<User> GetAllUsers();
        List<User> GetList(int skip, int antal);
        public void AddTransaction(IUserRepository  dbUser);
        public void DeleteUser(User DeleteTransaction);
        public void Save();

    }
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        private static List<User> Users;
        //public List<User> GetList(int skip, int antal)
        //{
        //    return Users.Skip(skip).Take(antal).ToList();
        //}

        public void AddUser(User dbUser)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(User DeleteUser)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<User> GetAllUsers()
        {
            return (IQueryable<User>)_dbContext.Users;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        List<User> IUserRepository.GetList(int skip, int antal)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.AddTransaction(IUserRepository dbUser)
        {
            throw new NotImplementedException();
        }
    }
}
