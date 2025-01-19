using BookingApp.Domain.Models;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for WelcomeTouristWindow.xaml
    /// </summary>
    public partial class WelcomeTouristWindow : Window
    {
        public WelcomeTouristWindow(User user)
        {
            InitializeComponent();
            DataContext = new WelcomeTouristViewModel(user,this);
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
