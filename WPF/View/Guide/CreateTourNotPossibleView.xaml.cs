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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for CreateTourNotPossibleView.xaml
    /// </summary>
    public partial class CreateTourNotPossibleView : UserControl
    {
        public Frame MainFrame { get; set; }
        public User User{ get; set; }
        public CreateTourNotPossibleView(Frame mainFrame, User user)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            User = user;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var guideHomePage = new GuideHomePage();
            MainFrame.Navigate(guideHomePage);
            guideHomePage.DataContext = new GuideMainViewModel(User);
        }
    }
}
