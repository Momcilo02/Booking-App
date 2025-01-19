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
using static BookingApp.WPF.ViewModel.Tourist.PeopleReportViewModel;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for CreateTourRequestView.xaml
    /// </summary>
    public partial class CreateTourRequestView : Page
    {
        public CreateTourRequestView(User user, TourRequestViewModel tourRequestViewModel, Frame frame)
        {
            InitializeComponent();
            DataContext = new CreateTourRequestViewModel(CreateRequest, user, frame, ComboBoxLocation, ComboBoxLanguage, TextBoxGuests, tourRequestViewModel, CallerWindow.CreateTourRequestView,
                ErrorLanguage, ErrorLocation, ErrorTourNumber);
        }
    }
}
