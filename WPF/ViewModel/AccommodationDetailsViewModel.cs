using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;
using System.Windows;
using System.IO;
using System.Net.Security;
using System.Windows.Input;
using BookingApp.WPF.View.Guest;
using BookingApp.Commands;
using BookingApp.View.Guest;
using BookingApp.WPF.View.Owner;

namespace BookingApp.WPF.ViewModel
{
    public class AccommodationDetailsViewModel : BindableBase
    {
        AccommodationDTO Accommodation { get; set; }
        public User User { get; set; }
        private List<string> imagePaths;
        private bool canGoPreviousImage;
        public bool CanGoPreviousImage { get { return canGoPreviousImage; } set {
                if (canGoPreviousImage != value) { 
                    canGoPreviousImage = value;
                    OnPropertyChanged(nameof(CanGoPreviousImage));
                }
            }
        }
        private bool canGoNextImage;
        public bool CanGoNextImage { get { 
                return canGoNextImage;
            } set {
                if (canGoNextImage != value) { }
                canGoNextImage = value;
                OnPropertyChanged(nameof(CanGoNextImage));
            }
        }
        
        private int imageCounter;
        public List<string> ImagePaths
        {
            get { return imagePaths; }
            set
            {
                if (imagePaths != value)
                {
                    imagePaths = value;
                    OnPropertyChanged(nameof(ImagePaths));
                }
            }
        }
        public string? CurrentImage { get; set; }
        public int CurrentImageCounter { get; set; }
        public bool IsReservation {  get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set  {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Location {  get; set; }
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                if (type != value) { 
                    type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }
        private string maxGuests;
        public string MaxGuests
        {
            get { return maxGuests; }
            set
            {
                if (maxGuests != value)
                {
                    maxGuests = value;
                    OnPropertyChanged(nameof(MaxGuests));
                }
            }
        }
        private string minResDays;
        public string MinResDays
        {
            get { return minResDays; }
            set
            {
                if(minResDays != value)
                {
                    minResDays = value;
                    OnPropertyChanged(nameof(MinResDays));
                }
            }
        }
        public string CancelPeriod
        {
            get;set;
        }
        private ImageService imageService;
        private AccommodationService accommodationService;
        public ICommand NextImageClick { get; }
        public ICommand PreviousImageClick { get; }
        public ICommand ReserveClick { get; }
        public AccommodationDetailsViewModel(AccommodationDTO selected, User user, bool isReservation)
        {
            User = user;
            imageService = new ImageService();
            accommodationService = new AccommodationService();
            NextImageClick = new RelayCommand(param=> GoNextImage(param));
            PreviousImageClick = new RelayCommand(param => GoPreviousImage(param));
            ReserveClick = new RelayCommand(param => Reserve(param));
            Accommodation = selected;
            IsReservation = isReservation;
            ImagePaths = new List<string>();
            MaxGuests = selected.MaxGuests.ToString();
            Name = selected.Name;
            Type = selected.Type.ToString();
            Location = selected.MergedLocation;
            MinResDays = "Your stay must be at least " + selected.MinReservationDays.ToString() +" days long";
            CancelPeriod = "You can't cancel " + selected.UncancellablePeriod.ToString() + " days before your reservation";
            InitializeImages();
        }   
        private void InitializeImages()
        {
            ImagePaths.Clear();
            imageCounter = 0;
            foreach (Image image in imageService.GetByAccommodationId(Accommodation.Id))
            {
                ImagePaths.Add(image.Path);
            }
            if (ImagePaths.Count > 0)
            {
                CurrentImage = ImagePaths[0];
                imageCounter = 0;
            }
            else
            {
                imageCounter = 0;
                CurrentImage = "\\Resources\\Images\\AccommodationImagePlaceholder.png";
                CanGoNextImage = false;
                CanGoPreviousImage = false;
            }
            CheckMoveImage();
        }
        private void GoNextImage(object parameter)
        {
            imageCounter++;
            CurrentImage = ImagePaths[imageCounter];
            CheckMoveImage();
        }
        private void GoPreviousImage(object parameter)
        {
            imageCounter--;
            CurrentImage = ImagePaths[imageCounter];
            CheckMoveImage();
        }
        private void CheckNextMoveImage()
        {
            if (imageCounter < ImagePaths.Count - 1)
            {
                CanGoNextImage = true;
            }
            else
            {
                CanGoNextImage = false;
            }
        }
        private void CheckPreviousMoveImage()
        {
            if (imageCounter == 0)
            {
                CanGoPreviousImage = false;
            }
            else
            {
                CanGoPreviousImage = true;
            }
        }
        private void CheckMoveImage()
        {
            CheckPreviousMoveImage();
            CheckNextMoveImage();
        }
        private void Reserve(object parameter)
        {
            Accommodation accommodation = accommodationService.Get(Accommodation.Id);
            ReserveAccommodationView reserveAccommodationView = new ReserveAccommodationView(accommodation, User);
            reserveAccommodationView.ShowDialog();
            Window win = (Window)parameter;
            win.Close();
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
