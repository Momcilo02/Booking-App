using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ForumCommentDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string comment;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if(comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if(username != value)
                {
                    username = value; OnPropertyChanged(nameof(Username));
                }
            }
        }
        private Enumeration.UserType userType;
        public Enumeration.UserType UserType
        {
            get { return userType; }
            set
            {
                if( userType != value)
                {
                    userType = value; OnPropertyChanged(nameof(UserType));
                }
            }
        }
        private bool visited;
        public bool Visited 
        {
            get { return visited; }


            set { 
                if(  visited != value )
                {
                    visited = value; OnPropertyChanged(nameof(Visited));
                }
            } }

        private bool isSameUser;
        public bool IsSameUser
        {
            get { return isSameUser; }
            set
            {
                if( isSameUser != value )
                {
                    isSameUser = value;
                    OnPropertyChanged(nameof(IsSameUser));
                }
            }
        }
        public ForumCommentDTO()
        {
            
        }
        public ForumCommentDTO(ForumComment forumComment)
        {
            Id = forumComment.Id;
            Comment = forumComment.Comment;
            Username = forumComment.Creator.Username;
            UserType = forumComment.Creator.Type;
            Visited = false;
            IsSameUser = false;
        }
        
    }
}
