using Avalonia.Data.Converters;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace Cash.Converters
{
    public class ImageConverter : IValueConverter
    {
        private const double MaxImageHeight = 50;
        private const double MaxImageWidth = 100;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                using (var memoryStream = new MemoryStream())
                {
                    var bitmap1 = new System.Drawing.Bitmap(value as Image);
                    //(value as Image)
                        bitmap1.Save(memoryStream, ImageFormat.Jpeg);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var bitmap = new Avalonia.Media.Imaging.Bitmap(memoryStream);
                    return bitmap;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
