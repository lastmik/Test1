using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Auyth.Api.Models;

namespace Test.Auyth.Api.Data
{
    public class UserRepository:IUsersRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> Get()
        {
            return _context.Users;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();
            return user;
        }
        public User GetByLogin(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }
        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
