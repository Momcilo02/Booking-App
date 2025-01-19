using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.View.Guest;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel
{
    public class GuestMainWindowViewModel : BindableBase
    {
        private MyReservationsViewModel myReservationsViewModel;
        private NewReservationsViewModel newReservationsViewModel;
        private GuestForumsViewModel guestForumsViewModel;
        private GuestProfileViewModel guestProfileViewModel;
        private BindableBase currentViewModel;
        private bool isNewChecked;
        public bool IsNewChecked
        {
            get { return isNewChecked; }
            set
            {
                if (isNewChecked != value) { 
                    isNewChecked = value;
                    OnPropertyChanged(nameof(IsNewChecked));
                }
            }
        }
        private bool isMyChecked;
        public bool IsMyChecked
        {
            get { return isMyChecked; }
            set
            {
                if (isMyChecked != value)
                {
                    isMyChecked = value;
                    OnPropertyChanged(nameof(IsMyChecked));
                }
            }
        }
        private bool isForumChecked;
        public bool IsForumChecked
        {
            get {
                return isForumChecked;
            }
            set
            {
                if (isForumChecked != value) { 
                    isForumChecked = value;
                    OnPropertyChanged(nameof(IsForumChecked));
                }
            }
        }
        private bool isProfileChecked;
        public bool IsProfileChecked
        {
            get { return isProfileChecked; }
            set
            {
                if (isProfileChecked != value) { 
                    isProfileChecked = value;
                    OnPropertyChanged(nameof(IsProfileChecked));
                }
            }   
        }
        public User User { get; set; }
        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }
        public MyICommand<string> NavCommand { get; private set; }
        public ICommand LogOutClick { get; }
        private SuperGuestService superGuestService;

        public GuestMainWindowViewModel(User user)
        {
            NavCommand = new MyICommand<string>(OnNav);
            User = user;
            newReservationsViewModel = new NewReservationsViewModel();
            newReservationsViewModel.Initiate(User);
            myReservationsViewModel = new MyReservationsViewModel();
            myReservationsViewModel.Initiate(User);
            guestProfileViewModel = new GuestProfileViewModel(User);
            guestForumsViewModel = new GuestForumsViewModel();
            guestForumsViewModel.Initiate(User);
            superGuestService = new SuperGuestService();
            CurrentViewModel = newReservationsViewModel;
            LogOutClick = new RelayCommand(param => LogOut(param));
            IsNewChecked = true;
            CheckSuperGuest();
        }

        private void CheckSuperGuest()
        {
            switch(superGuestService.IsGuestEligible(User))
            {
                case 0:
                    MessageBox.Show("You became a Super Guest\n You now have 10 discount points");
                    break;
                case 1:
                    MessageBox.Show("You are no longer a super guest");
                    break;
            }
        }
        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "newReservations":
                    CurrentViewModel = newReservationsViewModel;
                    IsNewChecked = true;
                    break;
                case "myReservations":
                    CurrentViewModel = myReservationsViewModel;
                    IsMyChecked = true;
                    break;
                case "profile":
                    CurrentViewModel = guestProfileViewModel;
                    IsProfileChecked = true;
                    break;
                case "forum":
                    CurrentViewModel = guestForumsViewModel;
                    IsForumChecked = true;
                    break;
            }
        }
        private void LogOut(object parameter)
        {
            Window window = (Window)parameter;
            window.Close();
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
