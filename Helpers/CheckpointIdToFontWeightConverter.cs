using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BookingApp.Helpers
{
    public class CheckpointIdToFontWeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is int rowId && values[1] is int currentCheckpointId)
            {
                return rowId == currentCheckpointId ? FontWeights.Bold : FontWeights.Normal;
            }
            return FontWeights.Normal;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
