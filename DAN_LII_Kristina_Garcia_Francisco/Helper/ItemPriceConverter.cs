using DAN_LII_Kristina_Garcia_Francisco.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DAN_LII_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Convertes the id of the item to the price
    /// </summary>
    class ItemPriceConverter : IValueConverter
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
            Service service = new Service();

            double orderPrice = 0;
            for (int i = 0; i < service.GetAllShoppingCarts().Count; i++)
            {
                if (service.GetAllShoppingCarts()[i].ItemID == (int)value && service.GetAllShoppingCarts()[i].UserID == LoggedUser.CurrentUser.UserID)
                {
                    int index = service.GetAllItems().FindIndex(f => f.ItemID == service.GetAllShoppingCarts()[i].ItemID);
                    double price = double.Parse(service.GetAllItems()[index].Price);
                    orderPrice = orderPrice + (double)service.GetAllShoppingCarts()[i].Amount * price;
                    return orderPrice;
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
            return value;
        }
    }
}
