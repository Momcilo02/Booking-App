using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Application.UseCases;
using BookingApp.WPF.ViewModel.Tourist;
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

namespace BookingApp.View.Tourist
{
    /// <summary>
    /// Interaction logic for VoucherView.xaml
    /// </summary>
    public partial class VoucherView : Page
    {

        public VoucherView(User user, Frame frame, Frame frame2)
        {

            InitializeComponent();
            DataContext = new VoucherViewModel(user, frame, frame2);
        }

    }
}
