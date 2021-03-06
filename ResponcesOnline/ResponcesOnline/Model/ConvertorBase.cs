using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace ResponcesOnline.Model
{
    public abstract class ConvertorBase<T> : MarkupExtension, IValueConverter
where T : class, new()
    {
        /// <summary>
        /// Must be implemented in inheritor.
        /// </summary>
        public abstract object Convert(object value, Type targetType, object parameter,
        CultureInfo culture);
        /// <summary>
        /// Override if needed.
        /// </summary>
        public virtual object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #region MarkupExtension members
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
                _converter = new T();
            return _converter;
        }
        private static T _converter = null;
        #endregion
        ////https://habrahabr.ru/post/140876/
    }
}
