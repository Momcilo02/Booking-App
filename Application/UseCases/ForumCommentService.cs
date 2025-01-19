using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class ForumCommentService
    {
        private readonly IForumCommentRepository forumCommentRepository;
        public ForumCommentService()
        {
            forumCommentRepository = Injector.Injector.CreateInstance<IForumCommentRepository>();
        }
        public List<ForumComment> GetAll()
        {
            return forumCommentRepository.GetAll();
        }
        public List<ForumComment> GetByForum( int id)
        {
            return forumCommentRepository.GetByForum(id);
        }
        public ForumComment Save(ForumComment forumComment)
        {
            return forumCommentRepository.Save(forumComment);
        }
    }
}
