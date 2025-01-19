using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BookingApp.WPF.ViewModel
{
    public class AccommodationReviewViewModel : INotifyPropertyChanged
    {
        public User Guest { get; set; }
        public Accommodation Accommodation { get; set; }
        public bool IsSuggestionChecked { get; set; }
        public string Cleanness { get; set; }
        public string Correctness { get; set; }
        public string Urgency { get; set; }
        public string Comment { get; set; }
        private Window _window;

        private ImageService _imageService;
        private AccommodationReviewService _reviewService;
        private AccommodationRenovationSuggestionService _renovationSuggestionService;

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                if (imageUrl != value)
                {
                    imageUrl = value;
                    OnPropertyChanged(nameof(ImageUrl));
                }
            }
        }
        private string suggestion;
        public string Suggestion
        {
            get { return suggestion; }
            set
            {
                if (suggestion != value)
                {
                    suggestion = value;
                    OnPropertyChanged(nameof(Suggestion));
                }
            }
        }

        private List<string> imagePaths;

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
        private bool canGoPreviousImage;
        public bool CanGoPreviousImage
        {
            get { return canGoPreviousImage; }
            set
            {
                if (canGoPreviousImage != value)
                {
                    canGoPreviousImage = value;
                    OnPropertyChanged(nameof(CanGoPreviousImage));
                }
            }
        }
        private bool canGoNextImage;
        public bool CanGoNextImage
        {
            get
            {
                return canGoNextImage;
            }
            set
            {
                if (canGoNextImage != value) { }
                canGoNextImage = value;
                OnPropertyChanged(nameof(CanGoNextImage));
            }
        }

        private int imageCounter;
        private string? currentImage;
        public string? CurrentImage { get { return currentImage; } set {
                if (currentImage != value)
                {
                    currentImage = value;
                    OnPropertyChanged(nameof(CurrentImage));
                }
            } }
        public int CurrentImageCounter { get; set; }
        public ComboBox CorrectnessComboBox {  get; set; }
        public ComboBox CleannessComboBox { get; set; }
        public ICommand CancelClick { get; set; }
        public ICommand ConfirmClick { get; set; }
        public ICommand AddClick { get; set; }
        public ICommand NextImageClick { get; }
        public ICommand PreviousImageClick { get; }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public AccommodationReviewViewModel()
        {

        }
        public AccommodationReviewViewModel(User guest, Accommodation accommodation, Window window, ComboBox correctnessComboBox, ComboBox cleannessComboBox)
        {
            Guest = guest;
            Accommodation = accommodation;
            ImagePaths = new List<string>();
            _window = window;
            CancelClick = new RelayCommand(param => Cancel(param));
            ConfirmClick = new RelayCommand(param => Confirm(param));
            AddClick = new RelayCommand(param => AddImage(param));
            NextImageClick = new RelayCommand(param => GoNextImage(param));
            PreviousImageClick = new RelayCommand(param => GoPreviousImage(param));
            _imageService = new ImageService();
            _reviewService = new AccommodationReviewService();
            _renovationSuggestionService = new AccommodationRenovationSuggestionService();
            CorrectnessComboBox = correctnessComboBox;
            CleannessComboBox = cleannessComboBox;
            Suggestion = "";
            CurrentImage = "\\Resources\\Images\\AccommodationImagePlaceholder.png";
            imageCounter = -1;
            CheckMoveImage();
        }
        private void Cancel(object parameter)
        {
            _window.Close();
        }
        private void Confirm(object parameter)
        {
            
            int cleanness = Convert.ToInt32(CleannessComboBox.Text[0].ToString());
            int corectness = Convert.ToInt32(CorrectnessComboBox.Text[0].ToString());
            MakeSuggestion();
            AccommodationReview review = _reviewService.Save(new AccommodationReview(Guest, Accommodation, cleanness, corectness, Comment));
            MakeImages(review.Id);
            _window.Close();
        }
        private void MakeSuggestion()
        {
            if (IsSuggestionChecked)
            {
                string urgency = Urgency;
                int urgency_value =  Convert.ToInt32(urgency[0].ToString());
                _renovationSuggestionService.Save(new AccommodationRenovationSuggestion(Accommodation, urgency_value, Suggestion));
            }
        }

        private void MakeImages(int reviewId)
        {
            foreach (string image in ImagePaths)
            {
                Domain.Models.Image img = new Domain.Models.Image(image, reviewId, Enumeration.EntityType.AccommodationReview);
                _imageService.Save(img);
            }
        }
        private void AddImage(object parameter)
        {
            imageCounter++;
            ImagePaths.Add(ImageUrl);
            
            CurrentImage = ImagePaths[imageCounter];
            MessageBox.Show(ImageUrl + ", " + CurrentImage);
            ImageUrl = string.Empty;
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
            if (imageCounter <= 0)
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
    }
}
