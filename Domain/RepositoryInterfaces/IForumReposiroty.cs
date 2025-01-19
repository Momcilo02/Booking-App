using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumReposiroty
    {
        List<Forum> GetAll();
        Forum GetById(int id);
        Forum Save(Forum forum);
        void CloseForum(Forum forum);
    }
}
