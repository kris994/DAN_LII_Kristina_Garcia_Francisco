using System;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DAN_LII_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Convertes price of the item
    /// </summary>
    class ItemPriceCalculatorConverter : IValueConverter
    {
        /// <summary>
        /// Converts the parameter value into the item price
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) * 120 /100;
        }

        /// <summary>
        /// Converts back
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
