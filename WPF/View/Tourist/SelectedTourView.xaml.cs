using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.WPF.ViewModel.Tourist;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for SelectedTourView.xaml
    /// </summary>
    public partial class SelectedTourView : Page
    {
        public SelectedTourView(User user, Frame frame, Frame frame2, TouristNotificationDTO tourNotification)
        {
            InitializeComponent();
            DataContext = new SelectedTourViewModel(user, frame, frame2, tourNotification);
        }
    }
}
