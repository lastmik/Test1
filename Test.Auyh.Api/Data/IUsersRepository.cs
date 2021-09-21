using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Auyth.Api.Models;

namespace Test.Auyth.Api.Data
{
    public interface IUsersRepository
    {
        IEnumerable<User> Get();
        User Create(User user);
        User GetByLogin(string login);
        User GetById(int id);

    }
}
