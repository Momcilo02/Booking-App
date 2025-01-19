using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.View.Owner;
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

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationMainView.xaml
    /// </summary>
    public partial class AccommodationMainView : Window
    {
        public User User { get; set; }
        private AccommodationRepository accommodationRepository { get; set; }
        public ObservableCollection<AccommodationDTO> accommodations{ get; set; }
        public AccommodationDTO selectedReservation { get; set; }
        public AccommodationMainView( User user)
        {
            User = user;
            accommodationRepository = new  AccommodationRepository();
            accommodations = new ObservableCollection<AccommodationDTO>();
            selectedReservation = new AccommodationDTO();
            DataContext = this;
            InitializeComponent();
            Update();
        }
        private void Update() 
        {
            accommodations.Clear();
            foreach (var acc in accommodationRepository.GetAccommodationsByOwnerId(User.Id)) 
            {
                accommodations.Add(new AccommodationDTO(acc));
            }

        }
        private void OpenAddAccommodationView_Click(object sender, RoutedEventArgs e)
        {
            AddAccommodationView addAccommodationView = new AddAccommodationView(User);
            addAccommodationView.ShowDialog();
        }
        private void RenovationButton_Click(object sender, RoutedEventArgs e)
        {
            RenovationSetUp setUp = new RenovationSetUp(User,selectedReservation);
            setUp.ShowDialog();
        }
        
            private void Stats_Clilck(object sender, RoutedEventArgs e)
        {
            YearStats stat = new YearStats(selectedReservation);
            stat.ShowDialog();
        }

    }
}
