using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModel;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for ReservationRequests.xaml
    /// </summary>
    public partial class ReservationRequests : Window
    {
        public ReservationRequests()
        {
            InitializeComponent();
            DataContext = new ReservationRequestViewModel(this);
        }
    }
}
