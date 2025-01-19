using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Image = BookingApp.Domain.Models.Image;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        private readonly TourRepository _tourRepository;
        private readonly CheckPointRepository _checkPointRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly TourTimeRepository _tourTimeRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ImageRepository _imageRepository;
        public TourDTO tourDTO { get; set; }
        public CheckPointDTO checkPointDTO { get; set; }
        public LanguageDTO languageDTO { get; set; }
        public LocationDTO locationDTO { get; set; }
        public ImageDTO imageDTO { get; set; }
        
        public TourTimeDTO timeDTO { get; set; }
        public ObservableCollection<string> ImageUrls { get; set; }
        public string CurrentUrl { get; set; }



        public CreateTour(User user)
        {
            InitializeComponent();
            tourDTO = new TourDTO();
            locationDTO = new LocationDTO();
            imageDTO = new ImageDTO();
            ImageUrls = new ObservableCollection<string>();
            CurrentUrl = string.Empty;
            _imageRepository = new ImageRepository();
            _tourRepository = new TourRepository();
            languageDTO = new LanguageDTO();
            _locationRepository = new LocationRepository();
            _checkPointRepository = new CheckPointRepository();
            _languageRepository = new LanguageRepository();
            _tourTimeRepository = new TourTimeRepository();
            checkPointDTO = new CheckPointDTO();
            timeDTO = new TourTimeDTO();
            DataContext = this;
        }
        private void addToList(object sender, RoutedEventArgs e)
        {
            ImageUrls.Add(CurrentUrl);
            tbxImageUrls.Text = string.Empty;
        }
        private void Create(object sender, RoutedEventArgs e)
        {
            var location = locationDTO.ToLocation();
            tourDTO.Location = _locationRepository.Save(location);

            tourDTO.Id = _tourRepository.NextId();
            imageDTO.Type = (Enumeration.EntityType)1;
            foreach (string url in ImageUrls)
            {

                imageDTO.Path = url;
                _imageRepository.Save(imageDTO.ToImage());

            }
            tourDTO.CheckPoint = _checkPointRepository.Save(new CheckPoint(1, checkPointDTO.Name, tourDTO.Id));
            tourDTO.TourTime = _tourTimeRepository.Save(new TourTime(timeDTO.Time.Value, tourDTO.MaxGuests, tourDTO.Id, false));
            tourDTO.Language = _languageRepository.Save(languageDTO.ToLanguage());
            _tourRepository.Save(tourDTO.ToTour());
            MessageBox.Show("You have successfully created! Welcome!", "Successful Created", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
        private void ChangeSelection(object sender, RoutedEventArgs e)
        {
            if (cboTypes.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cboTypes.SelectedItem;
                string selectedContent = selectedItem.Content.ToString();

                if (selectedContent == "English")
                {
                    languageDTO.Name = Enumeration.LanguageType.English.ToString();
                }
                else if (selectedContent == "French")
                {
                    languageDTO.Name = Enumeration.LanguageType.French.ToString();
                }
                else
                {
                    languageDTO.Name = Enumeration.LanguageType.Serbian.ToString();
                }
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
