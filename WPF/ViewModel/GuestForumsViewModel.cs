using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel
{
    public class GuestForumsViewModel: BindableBase
    {
        public User User { get; set; }
        private ForumService forumService;
        private LocationService locationService;

        private LocationDTO selectedLocation;
        public LocationDTO SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                if (selectedLocation != value)
                {
                    selectedLocation = value;
                    SortForums();
                    OnPropertyChanged(nameof(SelectedLocation));
                }
            }
        }
        private ObservableCollection<LocationDTO> locations;
        public ObservableCollection<LocationDTO> Locations
        {
            get { return locations; }
            set
            {
                if(locations != value)
                {
                    locations = value;
                    OnPropertyChanged(nameof(Locations));
                }
            }
        }
        private ObservableCollection<ForumDTO> forums;
        public ObservableCollection<ForumDTO> Forums
        {
            get { return forums; }
            set
            {
                if(forums != value)
                {
                    forums = value;
                    OnPropertyChanged(nameof(Forums));
                }
            }
        }
        private ForumDTO selectedForum;
        public ForumDTO SelectedForum
        {
            get { return selectedForum; }
            set
            {
                if(selectedForum != value)
                {
                    selectedForum = value;
                    OnPropertyChanged(nameof(SelectedForum));
                }
            }
        }
        private void SortForums()
        {
            Forums.Clear();
            if(SelectedLocation.Id == -1)
            {
                InitializeForums();
            }
            else
            {
                foreach (Forum forum in forumService.GetAll())
                {
                    if (forum.Location.Id == SelectedLocation.Id)
                    {
                        Forums.Add(new ForumDTO(forum));
                    }
                }
            }
            
        }
        public ICommand AddForumClick { get; }
        public ICommand OpenForumClick {  get; }
        private ForumCommentService commentService;

        public GuestForumsViewModel()
        {
            User = new User();
            forumService = new ForumService();
            locationService = new LocationService();
            commentService = new ForumCommentService();
            Locations = new ObservableCollection<LocationDTO>();
            Forums = new ObservableCollection<ForumDTO>();
            AddForumClick = new RelayCommand(param => AddForum(param));
            OpenForumClick = new RelayCommand(param =>  OpenForum(param));
            InitializeForums();
            InitializeLocation();
        }
        public void Initiate(User user)
        {
            User = user;
            InitializeLocation();
            InitializeForums();
        }

        private void InitializeForums() { 
            Forums.Clear();
            foreach(Forum forum in forumService.GetAll())
            {
                ForumDTO forumDTO = new ForumDTO(forum);
                forumDTO.FullLocation = forum.Location.City + ", " + forum.Location.Country;     
                forumDTO.CommentNumber = commentService.GetByForum(forum.Id).Count();
                forumDTO.CreatorUsername = forum.Creator.Username;
                Forums.Add(forumDTO);
            }
        }
        private void OpenForum(object parameter)
        {
            SelectedForumView selectedForumView = new SelectedForumView();
            SelectedForumViewModel viewModel = new SelectedForumViewModel();
            Forum forum = forumService.GetById(SelectedForum.Id);
            viewModel.Initiate(User, forum);
            selectedForumView.DataContext = viewModel;
            selectedForumView.ShowDialog();
        }
        public void InitializeLocation()
        {
            Location empty = new Location(-1, "", "");
            Locations.Clear();
            Locations.Add(new LocationDTO(empty));
            foreach (Location location in locationService.GetAll())
            {
                Locations.Add(new LocationDTO(location));
            }
            SelectedLocation = new LocationDTO(empty);

        }
        private void AddForum(object parameter)
        {
            StartNewForum startNewForum = new StartNewForum();
            StartNewForumViewModel startNewForumViewModel = new StartNewForumViewModel();
            startNewForumViewModel.Initiate(User);
            startNewForum.DataContext = startNewForumViewModel;
            startNewForum.ShowDialog();
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
