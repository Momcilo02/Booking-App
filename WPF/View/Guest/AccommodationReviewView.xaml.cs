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
using System.Windows.Shapes;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModel;

namespace BookingApp.View.Guest
{
    /// <summary>
    /// Interaction logic for AccommodationReviewView.xaml
    /// </summary>
    public partial class AccommodationReviewView : Window
    {
        public AccommodationReviewView(User guest, Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = new AccommodationReviewViewModel(guest, accommodation, this, CorecntnessComboBox, CleannessComboBox);
        }
    }
}
