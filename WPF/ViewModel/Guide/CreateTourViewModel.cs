using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.DTO.TourDTOs;
using BookingApp.View.Guide;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class CreateTourViewModel : INotifyPropertyChanged
    {


        #region Properties
        public ObservableCollection<LanguageDTO> Languages { get; private set; }

        private CreateTourDTO _createTourDto;
        public CreateTourDTO CreateTourDto
        {
            get { return _createTourDto; }
            set
            {
                if (_createTourDto != value)
                {
                    _createTourDto = value;
                    OnPropertyChanged(nameof(CreateTourDto));
                }
            }
        }

        private CheckPointDTO _checkPointDto;
        public CheckPointDTO CheckPointDto
        {
            get { return _checkPointDto; }
            set
            {
                if (_checkPointDto != value)
                {
                    _checkPointDto = value;
                    OnPropertyChanged(nameof(CheckPointDto));
                }
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        private LanguageDTO _selectedLanguage;
        public LanguageDTO SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        private LocationDTO _selectedLocation;
        public LocationDTO SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                if (_selectedLocation != value)
                {
                    _selectedLocation = value;
                    OnPropertyChanged(nameof(SelectedLocation));
                }
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }

        private TourTimeDTO _tourTimeDto;
        public TourTimeDTO TourTimeDto
        {
            get => _tourTimeDto;
            set
            {
                if (_tourTimeDto != value)
                {
                    _tourTimeDto = value;
                    OnPropertyChanged(nameof(TourTimeDto));
                }
            }
        }

        private string _checkpointsDisplay;
        public string CheckpointsDisplay
        {
            get { return _checkpointsDisplay; }
            set
            {
                if (_checkpointsDisplay != value)
                {
                    _checkpointsDisplay = value;
                    OnPropertyChanged(nameof(CheckpointsDisplay));
                }
            }
        }

        private string _imagesDisplay;
        public string ImagesDisplay
        {
            get { return _imagesDisplay; }
            set
            {
                if (_imagesDisplay != value)
                {
                    _imagesDisplay = value;
                    OnPropertyChanged(nameof(ImagesDisplay));
                }
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        private string _tourDatesDisplay;
        public string TourDatesDisplay
        {
            get { return _tourDatesDisplay; }
            set
            {
                if (_tourDatesDisplay != value)
                {
                    _tourDatesDisplay = value;
                    OnPropertyChanged(nameof(TourDatesDisplay));
                }
            }
        }

        private User _user;

        #endregion Properties


        private TourService _tourService { get; set; }
        private CheckPointService _checkPointService { get; set; }
        private LanguageService _languageService { get; set; }
        private LocationService _locationService { get; set; }
        private TourTimeService _tourTimeService { get; set; }
        public Frame MainFrame{ get; set; }

        public ICommand AddTourCommand { get; private set; }
        public ICommand AddCheckPointCommand { get; private set; }
        public ICommand AddTourTimeCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand OpenImageCommand { get; private set; }

        public event Action RequestClose;

        
        public CreateTourRecommendedInfo? RecommendedInfo { get; set; }
        public CreateTourViewModel(User user, Frame mainFrame, CreateTourRecommendedInfo? recommendedInfo)
        {
            RecommendedInfo = recommendedInfo;
            _tourService = new TourService();
            _languageService = new LanguageService();
            _checkPointService = new CheckPointService();
            _locationService = new LocationService();
            _tourTimeService = new TourTimeService();

            CreateTourDto = new CreateTourDTO();
            CheckPointDto = new CheckPointDTO();
            TourTimeDto = new TourTimeDTO();
            _user = user;
            AddTourCommand = new RelayCommand(OnAddTour, CanAddTour);
            AddCheckPointCommand = new RelayCommand(OnAddCheckPoint, CanAddCheckPoint);
            AddTourTimeCommand = new RelayCommand(OnAddTourTime, CanAddTourTime);
            OpenImageCommand = new RelayCommand(OpenImageDialog);
            CancelCommand = new RelayCommand(CancelCommandExecute);

            MainFrame = mainFrame;
            var languages = _languageService.GetAll();
            var languagesDto = languages.Select(x => new LanguageDTO
            {
                Id = x.Id,
                Name = x.Name

            }).ToList();
            Languages = new ObservableCollection<LanguageDTO>(languagesDto);
            ExtractRecommendedInfo();

        }


        private void ExtractRecommendedInfo() 
        {
            if(RecommendedInfo?.SelectedLanguage != null) 
            {
                var  selectedLanguage = Languages.FirstOrDefault(x=>x.Id ==RecommendedInfo.SelectedLanguage.Id);
                if(selectedLanguage != null) 
                {
                    SelectedLanguage = selectedLanguage;
                }
            }
            if(RecommendedInfo?.SelectedLocation != null) 
            {
                City = RecommendedInfo.SelectedLocation.City;
                Country = RecommendedInfo.SelectedLocation.Country;
            }
        }
        private string GetRelativePath(string fullPath)
        {
            string root = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string relativePath = Path.GetRelativePath(root, fullPath);
            return relativePath;
        }

        private void OpenImageDialog(object obj)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                var relativePath = GetRelativePath(dlg.FileName);
                ImagePath = relativePath;
                ImagesDisplay += string.IsNullOrEmpty(ImagesDisplay) ? ImagePath : $", {ImagePath}";

                CreateTourDto.ImagePaths.Add(ImagePath);
            }
        }
        private bool CanAddTour(object obj)
        {
            return CreateTourDto.CheckPointDtos.Count > 1;
        }

        private bool CanAddCheckPoint(object obj)
        {
            return CheckPointDto.Name != string.Empty;
        }

        private void CancelCommandExecute(object obj)
        {
            var guideHomePage = new GuideHomePage();
            MainFrame.Navigate(guideHomePage);
            guideHomePage.DataContext = new GuideMainViewModel(_user);
        }
        private void OnAddCheckPoint(object obj)
        {
            CreateTourDto.CheckPointDtos.Add(CheckPointDto);
            CheckpointsDisplay += string.IsNullOrEmpty(CheckpointsDisplay) ? CheckPointDto.Name : $", {CheckPointDto.Name}";
            CheckPointDto = new CheckPointDTO();
        }

        private void OnAddTourTime(object obj)
        {
            TourTimeDto.MaxGuests = CreateTourDto.MaxTourists;
            CreateTourDto.TourTimeDTOs.Add(TourTimeDto);
            TourDatesDisplay += string.IsNullOrEmpty(TourDatesDisplay) ? TourTimeDto.Time.ToString() : $", {TourTimeDto.Time:dd-MM-yyyy HH:mm}";

            TourTimeDto = new TourTimeDTO();

        }

        private bool CanAddTourTime(object obj)
        {
            return TourTimeDto.Time != null;
        }
        private void OnAddTour(object obj)
        {
            CreateTourDto.Location.Country = Country;
            CreateTourDto.Location.City = City;

            var location = _locationService.Save(CreateTourDto.Location.ToLocationV2());
            CreateTourDto.Location.Id = location.Id;

            var tour = CreateTourDto.ToTour();
            tour.GuideId = _user.Id;
            tour = _tourService.Save(tour);


            foreach (var checkPointDto in CreateTourDto.CheckPointDtos)
            {
                checkPointDto.TourId = tour.Id;
                _checkPointService.Save(checkPointDto.ToCheckPoint());
            }

            foreach (var tourTimeDto in CreateTourDto.TourTimeDTOs)
            {
                tourTimeDto.TourId = tour.Id;
                var tourTime = tourTimeDto.ToTourTimeV2();
                if (tourTime == null) continue;

                _tourTimeService.Save(tourTime);
            }
            var guideHomePage = new GuideHomePage();
            MainFrame.Navigate(guideHomePage);
            guideHomePage.DataContext = new GuideMainViewModel(_user);
        }
        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke();
        }




        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
