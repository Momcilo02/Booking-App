using BookingApp.DTO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;
using System.ComponentModel;
using System.Windows.Media;
using BookingApp.WPF.View.Tourist;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TouristReviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private ImageService _imageService;
        private TourReservationService tourReservationService;
        public TourReservationDTO TourReservation { get; set; }
        public TouristReviewDTO TouristReview { get; set; }
        public User User { get; set; }
        private TextBox tbxImageUrls;
        private StackPanel GuideKnowledgeStackPanel;
        private StackPanel GuideLanguageStackPanel;
        private StackPanel TourInterestStackPanel;
        public ICommand BrowseImage { get; }
        public ICommand ReviewClick { get; }
        public ICommand MenuClick { get; }
        public ICommand TutorialClick { get; }
        public ImageDTO ImageDTO { get; set; }
        public System.Windows.Controls.Frame frame { get; set; }
        private string _currentUrl;
        public string CurrentUrl
        {
            get { return _currentUrl; }
            set
            {
                _currentUrl = value;
                OnPropertyChanged(nameof(CurrentUrl));
            }
        }
        private string _imageUrls;
        public string ImageUrls
        {
            get { return _imageUrls; }
            set
            {
                _imageUrls = value;
                OnPropertyChanged(nameof(ImageUrls));
            }
        }
        private string _finish;
        public string Finish
        {
            get { return _finish; }
            set
            {
                _finish = value;
                OnPropertyChanged(nameof(Finish));
            }
        }
        public ICommand RadioButtonCheckedCommand { get; }
        private Button review;
        public TouristReviewViewModel(TourReservationDTO tourReservation, User user, System.Windows.Controls.Frame fr, Button _review, TextBox tbxImageUrls, StackPanel guideKnowledgeStackPanel, StackPanel guideLanguageStackPanel, StackPanel tourInterestStackPanel)
        {
            TouristReview = new TouristReviewDTO();
            TourReservation = tourReservation;
            frame = fr;
            review = _review;
            _imageService = new ImageService();
            ImageDTO = new ImageDTO();
            tourReservationService = new TourReservationService();
            BrowseImage = new RelayCommand(param => Browse(param));
            RadioButtonCheckedCommand = new RelayCommand(param => RadioButtonChecked(param));
            ReviewClick = new RelayCommand(param => Review(param));
            MenuClick = new RelayCommand(param => Menu(param));
            User = user;
            this.tbxImageUrls = tbxImageUrls;
            GuideKnowledgeStackPanel = guideKnowledgeStackPanel;
            GuideLanguageStackPanel = guideLanguageStackPanel;
            TourInterestStackPanel = tourInterestStackPanel;
            GetTouristReviewRatings();

        }
        
        private void Menu(object parameter)
        {
            frame.Content = null;
        }
        private void RadioButtonChecked(object parameter)
        {
            GetTouristReviewRatings();
        }
        private void Browse(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string currentPath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(currentPath);

                CurrentUrl = "/Resources/Images/" + fileName;
                ImageUrls = CurrentUrl;
                OnPropertyChanged(nameof(ImageUrls));
            }
        }

        private int GetSelectedRating(StackPanel stackPanel)
        {
            foreach (var child in stackPanel.Children)
            {
                if (child is RadioButton radioButton && radioButton.IsChecked == true)
                {
                    return Convert.ToInt32(radioButton.Content);
                }
            }

            return 0;
        }
        private void SaveTouristReview()
        {
            TouristReview review = TouristReview.ToTouristReview();
            review.TourReservationId = TourReservation.Id;
            review.UserId = User.Id;
            TouristReviewService touristReviewService = new TouristReviewService();
            touristReviewService.Save(review);
        }
        private void DeleteTourReservation()
        {
            TourReservation reservation = TourReservation.ToTourReservation();
            tourReservationService.Delete(reservation);
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            GetTouristReviewRatings();
        }

        private void GetTouristReviewRatings()
        {
            TouristReview.GuideKnowledge = GetSelectedRating(GuideKnowledgeStackPanel);
            TouristReview.GuideLanguage = GetSelectedRating(GuideLanguageStackPanel);
            TouristReview.TourInterest = GetSelectedRating(TourInterestStackPanel);
            if (TouristReview.GuideKnowledge == 0 || TouristReview.GuideLanguage == 0 || TouristReview.TourInterest == 0)
            {
                review.IsEnabled = false;
                review.Background = Brushes.LightGray;

            }
            else
            {
                review.IsEnabled = true;
                review.Background = Brushes.White;
            }
        }
        private async void Review(object parameter)
        {
            GetTouristReviewRatings();

            SaveTouristReview();
            DeleteTourReservation();
            Finish = "Review saved successfully! Closing...";

            await Task.Delay(2000);

            frame.Content = null;
        }


    }
}
