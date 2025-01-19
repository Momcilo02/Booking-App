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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for NewReservationsUserControl.xaml
    /// </summary>
    public partial class NewReservationsUserControl : UserControl
    {
        public NewReservationsUserControl()
        {
            InitializeComponent();
            NewReservationsViewModel viewModel = new NewReservationsViewModel();
            var curr = (App)System.Windows.Application.Current;
            User user = curr.User;
            viewModel.Initiate(user);
            DataContext = viewModel;
        }
    }
}
