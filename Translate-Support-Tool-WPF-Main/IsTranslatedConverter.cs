﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Translate_Support_Tool_WPF_Main
{
    public class IsTranslatedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}