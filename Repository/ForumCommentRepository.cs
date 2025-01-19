using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ForumCommentRepository : IForumCommentRepository
    {
        private const string FilePath = "../../../Resources/Data/forum-comments.csv";
        private readonly Serializer<ForumComment> _serializer;
        private List<ForumComment> _comments;
        private ForumRepository _forumRepository;
        private List<Forum> _forums;
        private UserRepository _userRepository;
        public ForumCommentRepository()
        {
            _serializer = new Serializer<ForumComment>();
            _comments = _serializer.FromCSV(FilePath);
            _forumRepository = new ForumRepository();
            _userRepository = new UserRepository();
            _forums = _forumRepository.GetAll();
            Initiate();
        }
        private void Initiate()
        {
            _comments.Clear();
            _comments = _serializer.FromCSV(FilePath);
            foreach (ForumComment comment in _comments)
            {
                comment.Creator = _userRepository.Get(comment.Creator.Id);
            }
        }
        public int NextId()
        {
            _comments = _serializer.FromCSV(FilePath);
            if (_comments.Count < 1)
            {
                return 1;
            }
            return _comments.Max(x => x.Id) + 1;
        }
        public void Delete(ForumComment forumComment)
        {
            _comments = _serializer.FromCSV(FilePath);
            ForumComment founded = _comments.Find(c => c.Id == forumComment.Id);
            if(founded != null)
            {
                _comments.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _comments);
        }

        public List<ForumComment> GetAll()
        {
            Initiate();
            return _comments;
        }

        public List<ForumComment> GetByForum(int id)
        {
            Initiate();
            return _comments.FindAll(c => c.ForumId == id);
        }

        public ForumComment GetById(int id)
        {
            ForumComment comment = _comments.Find(c => c.Id == id);
            return comment;
        }

        public ForumComment Save(ForumComment forumComment)
        {
            forumComment.Id = NextId();
            _comments = _serializer.FromCSV(FilePath);
            _comments.Add(forumComment);
            _serializer.ToCSV(FilePath, _comments);
            return forumComment;
        }
    }
}
