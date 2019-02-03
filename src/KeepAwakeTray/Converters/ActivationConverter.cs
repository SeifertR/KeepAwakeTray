using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace KeepAwakeTray.Converters
{
    public class ActivationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imagePath;

            if (value != null && (bool)value)
                imagePath = @"pack://application:,,,/KeepAwakeTray;component/Images/coffee-cup-green.ico";
            else
                imagePath = @"pack://application:,,,/KeepAwakeTray;component/Images/coffee-cup-red.ico";

            var uri = new Uri(imagePath, UriKind.Absolute);
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = uri;
            image.EndInit();
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //
            // Not used
            //
            throw new NotImplementedException();
        }
    }
}
