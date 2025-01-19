using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumCommentRepository
    {
        List<ForumComment> GetAll();
        ForumComment GetById(int id);
        ForumComment Save(ForumComment forumComment);
        void Delete(ForumComment forumComment);
        List<ForumComment> GetByForum(int id);
    }
}
