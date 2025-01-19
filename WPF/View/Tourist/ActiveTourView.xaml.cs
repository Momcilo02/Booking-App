using BookingApp.DTO;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;
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

namespace BookingApp.View.Tourist
{
    /// <summary>
    /// Interaction logic for ActiveTourView.xaml
    /// </summary>
    public partial class ActiveTourView : Page
    {

        public ActiveTourView(User user, Frame frame, Frame frame2)
        {
            InitializeComponent();
            DataContext = new ActiveTourViewModel(user, frame, frame2);
        }



    }
}
