using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class QuitConfirmationViewModel : INotifyPropertyChanged
    {
        public User User { get; set; }

        private TourRequestService _tourRequestService;
        public ICommand QuitCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        private Frame MainNavigationFrame;

        public QuitConfirmationViewModel(User user, Frame mainNavigationFrame)
        {
            User = user;
            MainNavigationFrame = mainNavigationFrame;
            _tourRequestService = new TourRequestService();
            QuitCommand = new RelayCommand(ExecuteQuit);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private void ExecuteQuit(object parameter)
        {
            _tourRequestService.CancelToursAndIssueVouchers(User.Id);
            var guideHomePage = new GuideHomePage();
            MainNavigationFrame.Navigate(guideHomePage);
            guideHomePage.DataContext = new GuideMainViewModel(User);
        }

        private void ExecuteCancel(object parameter)
        {
            var guideHomePage = new GuideHomePage();
            MainNavigationFrame.Navigate(guideHomePage);
            guideHomePage.DataContext = new GuideMainViewModel(User);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}