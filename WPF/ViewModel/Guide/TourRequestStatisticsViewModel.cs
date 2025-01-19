using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.View.Guide;
using BookingApp.WPF.View.Guide;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TourRequestStatisticsViewModel : INotifyPropertyChanged
    {
        public Dictionary<string, int> Statistics { get; set; }
        public Frame MainFrame { get; set; }

        public User User { get; set; }

        private TourRequestService _tourRequestService;

        private LocationDTO? _selectedLocation;
        public LocationDTO? SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
            }
        }


        private Language? _selectedLanguage { get; set; }
        public Language? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Values { get; set; }





        public ICommand CreateTourCommand { get; set; }

        

        public TourRequestStatisticsViewModel(Frame mainFrame,Dictionary<string, int> statistics,LocationDTO? selectedLocation, Language? selectedLanguage, User user)
        {
            User = user;
            MainFrame = mainFrame;
            CreateTourCommand = new RelayCommand(CreateTourExecute);
            _tourRequestService = new TourRequestService();
            Statistics = statistics;
            SeriesCollection = new SeriesCollection();
            SelectedLocation = selectedLocation;
            SelectedLanguage = selectedLanguage;
            PopulateBars();
        }


        private void CreateTourExecute(object obj) 
        {
            Language? mostRequestedLanguage = _tourRequestService.GetMostRequestedLanguage();

            Location? mostRequestedLocation = _tourRequestService.GetMostRequestedLocation();

            if(mostRequestedLocation == null && mostRequestedLanguage == null) 
            {
                MainFrame.Navigate(new CreateTourNotPossibleView(MainFrame,User));
                return;
            }
            var createTourRecommendedInfo = new CreateTourRecommendedInfo();
            if (SelectedLanguage != null)
            {
                createTourRecommendedInfo.SelectedLanguage = mostRequestedLanguage;
            }
            if(SelectedLocation != null) 
            {
                createTourRecommendedInfo.SelectedLocation = mostRequestedLocation == null ? null : new LocationDTO(mostRequestedLocation);
            }

            MainFrame.Navigate(new CreateTourView(User,MainFrame,createTourRecommendedInfo));
        }

        public void PopulateBars()
        {
            foreach (var key in Statistics.Keys)
            {
                var value = Statistics[key];
                SeriesCollection.Add(new ColumnSeries 
                {
                    Title = key,
                    Values = new ChartValues<int> {value}
                });

            }
            Labels = Statistics.Keys.ToArray();
            Values = value => value.ToString("N");

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
