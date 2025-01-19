using BookingApp.DTO;
using BookingApp.View.Tourist;
using System;
using System.ComponentModel;
using System.Windows.Input;
using BookingApp.Commands;
using System.Windows.Controls;
using BookingApp.Domain.Models;
using BookingApp.WPF.View.Tourist;
using BookingApp.View;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TouristMainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public User User { get; set; }

        public ICommand SearchClick { get; }
        public Frame frame;
        public ICommand VoucherClick { get; }
        public ICommand NotificationClick { get; }
        public ICommand ActiveTourClick { get; }
        public ICommand RequestClick { get; }
        public ICommand LogOutClick { get; }
        public ICommand TutorialClick { get; }
        private Window _currentWindow;
        public Frame frame2;

        public TouristMainViewModel(User user, Frame fr, Frame fr2, Window currentWindow)
        {
            User = user;
            frame = fr;
            frame2 = fr2;
            _currentWindow = currentWindow;
            frame.Navigate(new TouristHomeView(User, frame, frame2));
            SearchClick = new RelayCommand(param => Search(param));
            VoucherClick = new RelayCommand(param => ShowVouchers(param));
            NotificationClick = new RelayCommand(param => ShowNotifications(param));
            ActiveTourClick = new RelayCommand(param => ShowActiveTour(param));
            RequestClick = new RelayCommand(param => Request(param));
            LogOutClick = new RelayCommand(param => LogOut(param));
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Search(object parameter)
        {
            frame2.Navigate(new SearchTourWindow(User, frame, frame2));
        }

        private void LogOut(object parameter)
        {
            User.Username = "";
            User.Password = "";
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            _currentWindow.Close();
        }

        private void Request(object parameter)
        {
            frame.Navigate(new TourRequestView(User, frame, frame2));
        }

        public void ShowActiveTour(object parameter)
        {
            frame.Navigate(new ReservedToursView(User, frame, frame2));
        }

        private void ShowVouchers(object parameter)
        {
            frame.Navigate(new VoucherView(User, frame, frame2));
        }

        private void ShowNotifications(object parameter)
        {
            frame.Navigate(new TouristNotificationView(User, frame, frame2));
        }
    }
}
