using DAN_LII_Kristina_Garcia_Francisco.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DAN_LII_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Convertes the id of the item to the amount
    /// </summary>
    class ShoppingCartAmountConverter : IValueConverter
    {
        /// <summary>
        /// Converts the parameter value into the shopping cart amount
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Service service = new Service();
            for (int i = 0; i < service.GetAllShoppingCarts().Count; i++)
            {
                if (service.GetAllShoppingCarts()[i].ItemID == (int)value && service.GetAllShoppingCarts()[i].UserID == LoggedUser.CurrentUser.UserID)
                {
                    return service.GetAllShoppingCarts()[i].Amount;
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
