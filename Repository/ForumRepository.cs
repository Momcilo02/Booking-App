using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookingApp.Repository
{
    public class ForumRepository : IForumReposiroty
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";
        private readonly Serializer<Forum> _serializer;
        private List<Forum> _forums;
        private List<Location> _locations;
        private LocationRepository _locationRepository;
        private List<User> _users;
        private UserRepository _userRepository;

        public ForumRepository()
        {
            _serializer = new Serializer<Forum>();
            _forums = _serializer.FromCSV(FilePath);
            _locationRepository = new LocationRepository();
            _userRepository = new UserRepository();
            _locations = _locationRepository.GetAll();
            Initiate();
        }
        private void Initiate()
        {
            foreach(Forum forum in  _forums)
            {
                forum.Location = _locationRepository.Get(forum.Location.Id);
                forum.Creator = _userRepository.Get(forum.Creator.Id);
            }
        }
        public int NextId()
        {
            _forums = _serializer.FromCSV(FilePath);
            if (_forums.Count < 1)
            {
                return 1;
            }
            return _forums.Max(x => x.Id) + 1;
        }
        public void CloseForum(Forum forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forum current = _forums.Find(f => f.Id == forum.Id);
            int index = _forums.IndexOf(current);
            forum.IsClosed = true;
            _forums[index] = forum;
            _serializer.ToCSV(FilePath, _forums);
        }

        public List<Forum> GetAll()
        {
            _forums = _serializer.FromCSV(FilePath);
            Initiate();
            return _forums;
        }

        public Forum GetById(int id)
        {
            Forum forum = _forums.Find(f => f.Id == id);
            return forum;
        }

        public Forum Save(Forum forum)
        {
            forum.Id = NextId();
            _forums = _serializer.FromCSV(FilePath);
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }
    }
}
