using BookingApp.Domain.Models;
using BookingApp.DTO;
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

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for AcceptTourRequestView.xaml
    /// </summary>
    public partial class AcceptTourRequestView : UserControl
    {
        public AcceptTourRequestView(TourRequestDTO tourRequestDTO, Frame mainFrame,User user,bool forComplex)
        {
            InitializeComponent();
            DataContextChanged += AcceptTourRequestView_DataContextChanged;
            DataContext = new AcceptTourRequestViewModel(tourRequestDTO,mainFrame, user,forComplex);

        }

        private void AcceptTourRequestView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is AcceptTourRequestViewModel viewModel)
            {
                SetBlackoutDates(viewModel);
            }
        }

        private void SetBlackoutDates(AcceptTourRequestViewModel viewModel)
        {
            datePicker.BlackoutDates.Clear();

            DateTime startDate = viewModel.TourRequestDTO.StartDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDate = viewModel.TourRequestDTO.EndDate.ToDateTime(TimeOnly.MinValue);

           
            datePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, startDate.AddDays(-1)));
           
            datePicker.BlackoutDates.Add(new CalendarDateRange(endDate.AddDays(1), DateTime.MaxValue));
            
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (!viewModel.AvailableDates.Contains(date))
                {
                    datePicker.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }
        }

    }
}
