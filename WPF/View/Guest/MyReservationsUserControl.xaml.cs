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
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModel;

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for MyReservationsUserControl.xaml
    /// </summary>
    public partial class MyReservationsUserControl : UserControl
    {
        public MyReservationsUserControl()
        {
            InitializeComponent();
            var curr = (App)System.Windows.Application.Current;
            User user = curr.User;
            MyReservationsViewModel viewModel = new MyReservationsViewModel();
            viewModel.Initiate(user);
            DataContext = viewModel;

        }
    }
}
