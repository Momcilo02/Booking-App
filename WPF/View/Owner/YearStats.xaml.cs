using BookingApp.Application.UseCases;
using BookingApp.DTO;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for YearStats.xaml
    /// </summary>
    public partial class YearStats : Window
    {
        

        public YearStats(AccommodationDTO acc)
        {
            
            InitializeComponent();
            
            DataContext = new AccommodationStatsViewModel(acc);
            
           
            
        }
       
    }
}
