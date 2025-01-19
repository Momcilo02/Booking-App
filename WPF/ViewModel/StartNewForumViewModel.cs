using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.View.Guest;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel
{
    public class StartNewForumViewModel : BindableBase
    {
        public User User { get; set; }
        private LocationService locationService;
        private ForumService forumService;
        private string search;
        public string Search
        {
            get { return search; } set
            {
                if(search != value)
                {
                    search = value;
                    OnPropertyChanged(nameof(Search));
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
        private LocationDTO selectedLocation;
        public LocationDTO SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                if(selectedLocation != value)
                {
                    selectedLocation = value;
                    OnPropertyChanged(nameof(SelectedLocation));
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
        private string locationError;
        public string LocationError
        {
            get { return locationError; }
            set
            {
                if(locationError != value)
                {
                    locationError = value;
                    OnPropertyChanged(nameof(LocationError));
                }
            }
        }
        private string commentError;
        public string CommentError
        {
            get { return commentError; }
             set   {
                if(commentError != value)
                {
                    commentError = value;
                    OnPropertyChanged(nameof(CommentError));
                }
            }
        }
        public ICommand CancelClick { get;}
        public ICommand ConfirmClick { get;}
        public ICommand SearchClick { get;}
        public ICommand ClearSearchClick {  get;}
        public StartNewForumViewModel()
        {
            User = new User();
            Locations = new ObservableCollection<LocationDTO>();
            locationService = new LocationService();
            forumService = new ForumService();
            CancelClick = new RelayCommand(param => Cancel(param));
            ConfirmClick = new RelayCommand(param => Confirm(param));   
            SearchClick = new RelayCommand(param => SearchLocation(param));
            ClearSearchClick = new RelayCommand(param => ClearSearch(param));
            Search = "";
            CommentError = "";
            LocationError = "";
            InitializeLocation();
        }

        public void Initiate(User user)
        {
            User = user;
        }

        public void InitializeLocation()
        {
            
            Locations.Clear();
            foreach (Location location in locationService.GetAll())
            {
                Locations.Add(new LocationDTO(location));
            }
        }
        private void Confirm(object parameter)
        {
            if (CheckInput())
            {
                Window win = (Window)parameter;
                SelectedForumView selectedForumView = new SelectedForumView();
                Forum forum = new Forum(locationService.Get(SelectedLocation.Id), Comment, User);
                forumService.Save(forum);
                SelectedForumViewModel selectedForumViewModel = new SelectedForumViewModel();
                selectedForumViewModel.Initiate(User, forum);
                selectedForumView.DataContext = selectedForumViewModel;
                selectedForumView.ShowDialog();
                win.Close();
            }
            
        }
        private bool CheckInput()
        {
            bool ret = true;
            if (SelectedLocation == null)
            {
                LocationError = "Select One Location";
                ret = false;
            }
            else
            {
                LocationError = "";
            }
            if (string.IsNullOrEmpty(Comment)) {
                CommentError = "Comment is Required";
                ret |= false;
            }
            else
            {
                CommentError = "";
            }
            return ret;
        }
        private void Cancel(object window)
        {
            Window win = (Window)window;
            win.Close();
        }
        private void SearchLocation(object parametar)
        {
            if (Search == "")
            {
                InitializeLocation();
            }
            else
            {
                getSearchedLocations();
            }
        }
        private void getSearchedLocations()
        {
            Locations.Clear();
            foreach(Location location in locationService.GetAll()) {
                LocationDTO loc = new LocationDTO(location);
                if (loc.FullLocation.Contains(Search))
                {
                    Locations.Add(loc);
                }
            }
            
        }
        private void ClearSearch(object parametar)
        {
            Search = "";
            InitializeLocation();
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
