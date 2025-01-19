using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using BookingApp.DTO;
using OxyPlot.Axes;
using System.Windows.Input;
using BookingApp.Commands;
using Microsoft.Win32;
using System.Windows.Controls;
using BookingApp.WPF.View.Tourist;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestStatisticsViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public TourRequestStatisticsDTO TourRequestStatistics { get; set; }
        public User User { get; set; }
        public LanguageService LanguageService;
        public LocationService LocationService;
        public Frame frame { get; set; }
        public ICommand BackClick { get; }
        public PlotModel BarChartModel { get; private set; }
        public PlotModel BarChartModel1 { get; private set; }
        public TourRequestService TourRequestService { get; set; }
        private string _selectedYear;
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                    UpdateStatistics();
                }
            }
        }
        private double peopleYear;
        public double PeopleYear
        {
            get { return peopleYear; }
            set
            {
                double roundedValue = Math.Round(value, 2);
                if (roundedValue != peopleYear)
                {
                    peopleYear = roundedValue;
                    OnPropertyChanged(nameof(PeopleYear));
                }
            }
        }

        private double peopleEver;
        public double PeopleEver
        {
            get { return peopleEver; }
            set
            {
                double roundedValue = Math.Round(value, 2);
                if (roundedValue != peopleEver)
                {
                    peopleEver = roundedValue;
                    OnPropertyChanged(nameof(PeopleEver));
                }
            }
        }

        public List<string> Years { get; } = new List<string> { "2024", "2023", "2022" };
        public PlotModel PieModel2 { get; private set; }
        public PlotModel PieModel1 { get; private set; }
        public Frame frame2;
        public TourRequestStatisticsViewModel(User user, Frame fr, Frame fr2)
        {
            User = user;
            frame = fr;
            frame2 = fr2;
            TourRequestService = new TourRequestService();
            LanguageService = new LanguageService();
            LocationService = new LocationService();
            TourRequestStatistics = new TourRequestStatisticsDTO();
            SelectedYear = "2024";
            BackClick = new RelayCommand(param => Back(param));
            UpdateStatistics();
            UpdateLanguageGraph();
            UpdateLocationGraph();
        }
        private void Back(object parameter)
        {
            frame.Navigate(new TourRequestView(User, frame, frame2));
        }
        private void GetData()
        {
            int year = Convert.ToInt32(SelectedYear.ToString());
            PeopleEver = TourRequestService.GetPeopleGuestProcentage();
            PeopleYear = TourRequestService.GetPeopleGuestProcentageByYear(year);
            TourRequestStatistics.AceptedTourByYearProcentage = TourRequestService.GetAceptedTourProcentageByYear(year);
            TourRequestStatistics.DeclinedTourByYearProcentage = TourRequestService.GetRejectedTourProcentageByYear(year);
            TourRequestStatistics.AceptedTourProcentage = TourRequestService.GetAceptedTourProcentage();
            TourRequestStatistics.DeclinedTourProcentage = TourRequestService.GetRejectedTourProcentage();
        }
        private void CreatePieModel2()
        {
            PieModel2 = new PlotModel { Title = "Tour Requests" };
            PieSeries series = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 0,
                Diameter = 0.7,
                ExplodedDistance = 0.1
            };
            series.Slices.Add(new PieSlice("Accepted", TourRequestStatistics.AceptedTourByYearProcentage));
            series.Slices.Add(new PieSlice("Declined", TourRequestStatistics.DeclinedTourByYearProcentage));
            PieModel2.Series.Add(series);
            OnPropertyChanged(nameof(PieModel2));
        }
        private void CreatePieModel1()
        {
            PieModel1 = new PlotModel { Title = "Tour Requests" };
            PieSeries series1 = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 0,
                Diameter = 0.7,
                ExplodedDistance = 0.1
            };
            series1.Slices.Add(new PieSlice("Accepted", TourRequestStatistics.AceptedTourProcentage));
            series1.Slices.Add(new PieSlice("Declined", TourRequestStatistics.DeclinedTourProcentage));
            PieModel1.Series.Add(series1);
            OnPropertyChanged(nameof(PieModel1));
        }
        private void UpdateStatistics()
        {
            GetData();
            CreatePieModel2();
            CreatePieModel1();
        }
        private void UpdateLanguageGraph()
        {
            var languageCounts = TourRequestService.GetAll()
                .GroupBy(tr => tr.Language.Id)
                .ToDictionary(g => g.Key, g => g.Count());
            var barSeries = new BarSeries
            {
                Title = "Language Requests",
                StrokeColor = OxyColors.Black,
                FillColor = OxyColors.BlueViolet,
                ItemsSource = languageCounts.Select(kvp => new BarItem { Value = kvp.Value })
            };
            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Languages"
            };
            foreach (var languageId in languageCounts.Keys)
            {
                var language = LanguageService.Get(languageId);
                categoryAxis.Labels.Add(language.Name);
            }
            BarChartModel = new PlotModel
            {
                Title = "Language Requests",
                Series = { barSeries },
                Axes =
        {
            categoryAxis,
            new LinearAxis { Position = AxisPosition.Bottom, Title = "Number of Requests" }
        }
            };
        }
        private void UpdateLocationGraph()
        {
            var locationCounts = TourRequestService.GetAll()
                .GroupBy(tr => tr.Location.Id)
                .ToDictionary(g => g.Key, g => g.Count());

            var barSeries = new BarSeries
            {
                Title = "Location Requests",
                StrokeColor = OxyColors.Black,
                FillColor = OxyColors.BlueViolet,
                ItemsSource = locationCounts.Select(kvp => new BarItem { Value = kvp.Value })
            };

            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Location"
            };

            foreach (var locationId in locationCounts.Keys)
            {
                var location = LocationService.Get(locationId);
                categoryAxis.Labels.Add(location.City);
            }

            BarChartModel1 = new PlotModel
            {
                Title = "Location Requests",
                Series = { barSeries },
                Axes =
        {
            categoryAxis,
            new LinearAxis { Position = AxisPosition.Bottom, Title = "Number of Requests" }
        }
            };
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
