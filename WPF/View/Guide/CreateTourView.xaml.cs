using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Guide;
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
using Image = BookingApp.Domain.Models.Image;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTourView : Page
    {
        public CreateTourViewModel CreateTourViewModel { get; set; }
        public CreateTourView(User user, Frame mainFrame, CreateTourRecommendedInfo? createTourRecommendedInfo)
        {
            InitializeComponent();
         
            CreateTourViewModel = new CreateTourViewModel(user,mainFrame, createTourRecommendedInfo);
            DataContext = CreateTourViewModel;
        }
    }
}
