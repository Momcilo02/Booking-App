using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;


namespace BookingApp.WPF.ViewModel
{
    public class SelectedForumViewModel : BindableBase
    {
        public User User { get; set; }
        private string newComment;
        public string NewComment { get { return newComment; }
            set { 
                if(newComment != value)
                {
                    newComment = value;
                    OnPropertyChanged(nameof(NewComment));
                }
            }
        }
        private string creatorUsername;
        public string CreatorUsername
        {
            get { return creatorUsername;}
            set
            {
                if(creatorUsername != value)
                {
                    creatorUsername = value;
                    OnPropertyChanged(nameof(CreatorUsername));
                }
            }
        }
        private LocationDTO location;
        public LocationDTO Location
        {
            get { 
                return location;
            }
            set
            {
                if(location != value)
                {
                    location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }
        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if( comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }

        }
        public Forum Forum { get; set; }
        public bool IsOwner { get; set; }
        private ObservableCollection<ForumCommentDTO> comments;
        public ObservableCollection<ForumCommentDTO> Comments
        {
            get { return comments; }
            set
            {
                if (comments != value)
                {
                    comments = value;
                    OnPropertyChanged(nameof(Comments));
                }
            }
        }
        private ForumCommentService commentService;
        private AccommodationReservationService reservationService;
        private ForumService forumService;
        

        public ICommand CloseClick { get; }
        public ICommand AddCommentClick { get; }

        public SelectedForumViewModel()
        {
            User = new User();
            Forum = new Forum();
            IsOwner = true;
            Location = new LocationDTO();
            Comments = new ObservableCollection<ForumCommentDTO>();
            NewComment = "";
            commentService = new ForumCommentService();
            reservationService = new AccommodationReservationService();
            forumService = new ForumService();
            CloseClick = new RelayCommand(param => CloseForum(param));
            AddCommentClick = new RelayCommand(param => AddComment(param));
        }
        public void Initiate(User user, Forum forum)
        {
            User = user;
            Forum = forum;
            Location = new LocationDTO(Forum.Location);
            CreatorUsername = user.Username;
            Comment = forum.FirstComment;
            IsOwner = User.Id == Forum.Creator.Id;
            InitializeComments();
        }

        private void InitializeComments()
        {
            Comments.Clear();
            foreach(ForumComment comment in commentService.GetByForum(Forum.Id))
            {
                ForumCommentDTO forumCommentDTO = new ForumCommentDTO(comment);
                forumCommentDTO.Visited = DidVisit(comment, Forum.Location);
                forumCommentDTO.IsSameUser = CheckIfSameUser(comment);
                if (forumCommentDTO.IsSameUser) {
                    forumCommentDTO.Username = "You";
                    System.Windows.Forms.MessageBox.Show(forumCommentDTO.Username);
                }
                Comments.Add(forumCommentDTO);
            }
        }
        private bool DidVisit(ForumComment comment, Location location)
        {
            foreach(AccommodationReservationDTO reservation in reservationService.GetPastReservations(comment.Creator.Id))
            {
                if(reservation.Accommodation.Location.Id == Location.Id)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckIfSameUser(ForumComment comment)
        {
            return comment.Creator.Id == User.Id;
        }
        private void AddComment(object parametar)
        {
            ForumComment comment = new ForumComment(NewComment, User, Forum.Id, 0);
            commentService.Save(comment);
            InitializeComments();
        }
        private void CloseForum(object parametar)
        {
            // result = MessageBox.Show("Are you sure you want to close this forum", "Close Forum", MessageBoxButton.YesNo);
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure you want to close this forum", "Close Forum", (MessageBoxButtons)MessageBoxButton.YesNo);
            if(result == DialogResult.Yes)
            {
                forumService.CloseForum(Forum);
            }
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
