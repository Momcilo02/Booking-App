using BookingApp.DTO;
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
using BookingApp.WPF.ViewModel;

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for RenovationFinal.xaml
    /// </summary>
    public partial class RenovationFinal : Window
    {
        public RenovationFinal(AccommodationRenovationDTO renovation, DateTime start, DateTime end, int duration)
        {
            InitializeComponent();
            this.DataContext = new ReservationFinalViewModel(renovation, start, end, duration);
        }
    }
}
