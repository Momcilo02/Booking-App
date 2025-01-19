using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for AddAccommodationView.xaml
    /// </summary>
    public partial class AddAccommodationView : Window
    {
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ImageRepository _imageRepository;
        public AccommodationDTO AccommodationDTO { get; set; }
        public LocationDTO LocationDTO { get; set; }
        public ObservableCollection<string> ImageUrls { get; set; }
        public ImageDTO ImageDTO { get; set; }
        public string CurrentUrl { get; set; }

        public AddAccommodationView(User user)
        {
            
            InitializeComponent();
            DataContext = this;
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _imageRepository = new ImageRepository();
            AccommodationDTO = new AccommodationDTO();
            LocationDTO = new LocationDTO();
            ImageUrls = new ObservableCollection<string>();
            CurrentUrl = string.Empty;
            ImageDTO = new ImageDTO();
            AccommodationDTO.Owner = user;
            
        }
        private void Register(object sender, RoutedEventArgs e)
        {
            AccommodationDTO.LocationId = _locationRepository.Save(LocationDTO.ToLocation()).Id;
            AccommodationDTO.MergedLocation = LocationDTO.FullLocation;
            ImageDTO.EntityId = _accommodationRepository.Save(AccommodationDTO.ToAccommodation()).Id;
            ImageDTO.Type = 0;
            foreach(string url in ImageUrls) 
            {

                ImageDTO.Path = url;
                _imageRepository.Save(ImageDTO.ToImage());

            }
            MessageBox.Show("You have successfully registered! Welcome!", "Successful Registration", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void ChangeSelection(object sender, SelectionChangedEventArgs e)
        {
            switch (TakeCboTypesValue())
            {
                case "Apartment":
                    AccommodationDTO.Type = Enumeration.AccommodationType.Apartment;
                    break;
                case "Cottage":
                    AccommodationDTO.Type = Enumeration.AccommodationType.Cottage;
                    break;
                default:
                    AccommodationDTO.Type = Enumeration.AccommodationType.House ;
                    break;
            }
            
        }
        private String TakeCboTypesValue()
        {
            if (cboTypes.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cboTypes.SelectedItem;
                return selectedItem.Content.ToString();
            }
            return " ";
        }

        private void BrowseImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string CurrentUrl = openFileDialog.FileName;
                tbxImageUrls.Text = CurrentUrl;
                ImageUrls.Add(CurrentUrl);
            }
        }
        
    }
    
}
