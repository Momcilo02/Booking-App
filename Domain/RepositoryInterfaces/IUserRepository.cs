using BookingApp.Domain.Models;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        User GetById(int id);
    }
}
