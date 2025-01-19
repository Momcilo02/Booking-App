using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.DTO.TourDTOs;
using BookingApp.Helpers;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TourStatisticsViewModel : INotifyPropertyChanged
    {
        private readonly StatisticsService _statisticsService;
        private TourStatisticsDto _tourStatistics;
        private ObservableCollection<TourListItem> _tourListItems;
        private ObservableCollection<string> _years;
        private TourListItem _selectedTourListItem;
        private string _selectedYear;
        public TourGuideHelper _tourGuideHelper;
        private TourTimeService _tourTimeService;
        private LocationService _locationService;
        private TourService _tourService;

        public SeriesCollection PieSeries { get; set; }


        public ObservableCollection<TourListItem> TourListItems
        {
            get => _tourListItems;
            set
            {
                _tourListItems = value;
                OnPropertyChanged(nameof(TourListItems));
            }
        }

        public ObservableCollection<string> Years
        {
            get => _years;
            set
            {
                _years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        public TourStatisticsDto TourStatistics
        {
            get => _tourStatistics;
            set
            {
                _tourStatistics = value;
                OnPropertyChanged(nameof(TourStatistics));
            }
        }

        public TourListItem SelectedTourListItem
        {
            get => _selectedTourListItem;
            set
            {
                if (_selectedTourListItem != value)
                {
                    _selectedTourListItem = value;
                    OnPropertyChanged(nameof(SelectedTourListItem));
                    LoadTourStatistics();
                }
            }
        }

        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                    LoadMostVisitedTourStatistics();
                }
            }
        }

        public TourStatisticsViewModel(User user)
        {
            _statisticsService = new StatisticsService();
            TourListItems = new ObservableCollection<TourListItem>();

            _locationService = new LocationService();
            _tourTimeService = new TourTimeService();
            _tourService = new TourService();

            _tourGuideHelper = new TourGuideHelper(_tourService, _tourTimeService, _locationService);
            _tourGuideHelper.GetToursForStatistics(TourListItems, user);



            Years = new ObservableCollection<string> { "Ever" };
            for (int i = 0; i < 5; i++)
            {
                Years.Add(Convert.ToString(DateTime.Now.Year - i));
            }

        }

        private void PopulatePieChart()
        {
            if (SelectedTourListItem != null)
            {
                PieSeries = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Under 18",
                    Values = new ChartValues<int> { TourStatistics.Under18Num },
                    Fill = Brushes.LightBlue
                },
                new PieSeries
                {
                    Title = "18 to 50",
                    Values = new ChartValues<int> { TourStatistics.From18To50 },
                    Fill = Brushes.LightGreen
                },
                new PieSeries
                {
                    Title = "Above 50",
                    Values = new ChartValues<int> { TourStatistics.Above50 },
                    Fill = Brushes.Salmon
                }
            };
                OnPropertyChanged(nameof(PieSeries));
            }
        }


        private void LoadTourStatistics()
        {
            if (SelectedTourListItem == null) return;

            TourStatistics = _statisticsService.GetStatisticsForMostVisitedTour(SelectedTourListItem.Id, null);

            PopulatePieChart();
        }

        private void LoadMostVisitedTourStatistics()
        {
            if (SelectedYear != null)
            {
                if (SelectedYear == "Ever")
                {
                    TourStatistics = _statisticsService.GetStatisticsForMostVisitedTour(null, null);
                }
                else
                {
                    int year = Convert.ToInt32(SelectedYear);
                    TourStatistics = _statisticsService.GetStatisticsForMostVisitedTour(null, year);
                }
            }
            PopulatePieChart();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
