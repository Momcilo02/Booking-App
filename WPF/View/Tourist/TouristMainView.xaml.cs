using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Application.UseCases;
using BookingApp.WPF.ViewModel.Tourist;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.WPF.View.Tourist;

namespace BookingApp.View.Tourist
{
    public partial class TouristMainView : Window
    {
        public TouristMainView(User user)
        {
            InitializeComponent();
            DataContext = new TouristMainViewModel(user, frame, frame2, this);
        }


    }
}
