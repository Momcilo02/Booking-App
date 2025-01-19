using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModel.Guide;
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

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for EndedToursView.xaml
    /// </summary>
    public partial class EndedToursView : Window
    {

        public EndedToursViewModel EndedToursViewModel { get; set; }
        public EndedToursView(User user)
        {
            InitializeComponent();
            EndedToursViewModel = new EndedToursViewModel(user);
            DataContext = EndedToursViewModel;
        }
    }
}
