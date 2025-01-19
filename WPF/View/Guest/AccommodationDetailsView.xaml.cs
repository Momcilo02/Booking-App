using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.View.Guest
{
    /// <summary>
    /// Interaction logic for AccommodationDetailsView.xaml
    /// </summary>
    public partial class AccommodationDetailsView : Window
    {
        //AccommodationDTO accommodation {  get; set; }
        //public User Guest { get; set; }
        //private List<string> imagePaths;
        //public bool CanGoPreviousImage { get; set; }
        //public bool CanGoNextImage { get; set; }
        //private int imageCounter;

        //public event PropertyChangedEventHandler? PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        //public List<string> ImagePaths
        //{
        //    get { return imagePaths; }
        //    set
        //    {
        //        if(imagePaths != value)
        //        {
        //            imagePaths = value;
        //            OnPropertyChanged(nameof(ImagePaths));
        //        }
        //    }
        //}
        //public string? CurrentImage {  get; set; }

        public AccommodationDetailsView(AccommodationDTO selected, User user, bool isResrvation)
        {
            InitializeComponent();
            AccommodationDetailsViewModel viewModel = new AccommodationDetailsViewModel(selected, user, isResrvation);
            DataContext = viewModel;
        }
        //private void InitializeImages()
        //{
        //    ImagePaths.Clear();
        //    imageCounter = 0;
        //    foreach(Image image in accommodation.Images)
        //    {
        //        ImagePaths.Add(image.Path);
        //    }
        //    if(ImagePaths.Count > 0)
        //    {
        //        CurrentImage = ImagePaths[0];
        //    }
        //    else
        //    {
        //        CurrentImage= "\\Resources\\Images\\AccommodationImagePlaceholder.png";
        //        CanGoNextImage = false;
        //        CanGoPreviousImage = false;
        //    }
        //    CheckMoveImage();
        //}
        //private void CheckNextMoveImage()
        //{
        //    if(imageCounter < ImagePaths.Count - 1)
        //    {
        //        CanGoNextImage = true;
        //    }
        //    else
        //    {
        //        CanGoNextImage = false;
        //    }
        //}
        //private void CheckPreviousMoveImage()
        //{
        //    if (imageCounter ==0)
        //    {
        //        CanGoPreviousImage = false;
        //    }
        //    else
        //    {
        //        CanGoPreviousImage = true;
        //    }
        //}
        //private void CheckMoveImage()
        //{
        //    CheckPreviousMoveImage();
        //    CheckNextMoveImage();
        //}
        //public void Initialize()
        //{
            
        //    NameLabel.Content = accommodation.Name;
        //    LocationLabel.Content = accommodation.MergedLocation;
        //    TypeLabel.Content = accommodation.Type.ToString();
        //    MaxGuestsLabel.Content = accommodation.MaxGuests.ToString();
        //    MinReservationLabel.Content = accommodation.MinReservationDays.ToString();
        //}

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

     
    }
}
