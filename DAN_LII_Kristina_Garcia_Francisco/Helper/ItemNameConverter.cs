using System;
using System.Globalization;
using System.Windows.Data;

namespace DAN_LII_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Convertes the id of the item to the name
    /// </summary>
    class ItemNameConverter : IValueConverter
    {
        /// <summary>
        /// Converts the parameter value into the item name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Service service = new Service();
            for (int i = 0; i < service.GetAllItems().Count; i++)
            {
                if (service.GetAllItems()[i].ItemID == (int)value)
                {
                    return service.GetAllItems()[i].ItemName;
                }
            }

            return value;
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
            throw new NotImplementedException();
        }
    }
}
