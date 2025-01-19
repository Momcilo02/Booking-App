using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ForumDTO: INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string fullLocation;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public string FullLocation
        {
            get { return fullLocation; }
            set {
                if(fullLocation != value)
                {
                    fullLocation = value;
                    OnPropertyChanged(nameof(FullLocation));
                }
            }
        }
        private string creatorUsername;
        public string CreatorUsername
        {
            get { return creatorUsername; }
            set
            {
                if(creatorUsername != value)
                {
                    creatorUsername = value;
                    OnPropertyChanged(nameof(CreatorUsername));
                }
            }
        }
        private int commentNumber;
        public int CommentNumber
        {
            get { return commentNumber; }
            set
            {
                if (commentNumber != value)
                {
                    commentNumber = value;
                    OnPropertyChanged(nameof(CommentNumber));
                }
            }
        }
        private bool isUsefull;
        public bool IsUsefull
        {
            get { return isUsefull; }
            set
            {
                if(isUsefull !=  value)
                {
                    isUsefull = value;
                    OnPropertyChanged(nameof(IsUsefull));
                }
            }
        }
        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }
        private bool isClosed;
        public bool IsClosed
        {
            get { return isClosed; }
            set
            {
                if(isClosed != value)
                {
                    isClosed = value;
                    OnPropertyChanged(nameof(IsClosed));
                }
            }
        }
        public ForumDTO()
        {
            
        }
        public ForumDTO(Forum forum)
        {
            Id = forum.Id;
            FullLocation = "";
            Comment = forum.FirstComment;
            IsUsefull = false;
            CommentNumber = 0;
            CreatorUsername = "";
            IsClosed = forum.IsClosed;
        }

    }
}
