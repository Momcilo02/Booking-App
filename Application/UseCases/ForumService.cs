using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class ForumService
    {
        private readonly IForumReposiroty forumReposiroty;
        public ForumService()
        {
            forumReposiroty = Injector.Injector.CreateInstance<IForumReposiroty>();
        }
        public List<Forum> GetAll()
        {
            return forumReposiroty.GetAll();
        }
        public Forum GetById(int id)
        {
            return forumReposiroty.GetById(id);
        }
        public Forum Save(Forum forum)
        {
            return forumReposiroty.Save(forum);
        }
        public void CloseForum(Forum forum)
        {
            forumReposiroty.CloseForum(forum);
        }

    }
}
