using System;
using System.Globalization;
using System.Windows.Data;

namespace Translate_Support_Tool_WPF_Main
{
    public class IsTranslatedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string Data;

            switch ((string)parameter)
            {
                case "reverse":
                    Data = "test";
                    break;
                default: 
                    Data = "야스야스";
                    break;
            }

            return Data;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}