using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }
        public User Update(User user)
        {
            _users = _serializer.FromCSV(FilePath);
            User current = _users.Find(ar => ar.Id == user.Id);
            int index = _users.IndexOf(current);
            _users[index] = user;
            _serializer.ToCSV(FilePath, _users);
            return user;
        }
        public User Save(User user)
        {
            
            _users = _serializer.FromCSV(FilePath);
            _users.Add(user);
            _serializer.ToCSV(FilePath, _users);
            return user;
        }
        public User Get(int id)
        {
            User accommodation = _users.Find(a => a.Id == id);
            return accommodation;
        }

        public User GetById(int id)
        {
            return _users.Find(x => x.Id == id);
        }
    }
}
