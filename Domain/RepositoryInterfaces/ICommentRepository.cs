using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        List<Comment> GetAll();
        Comment Save(Comment comment);
        void Delete(Comment comment);
        Comment Update(Comment comment);
        List<Comment> GetByUser(User user);
    }
}
