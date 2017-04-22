using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization; // tarvii valueconverterijuttuu varte --> cultureinfo ja jutut
using Battleship.Model;

namespace Battleship.View
{
    [ValueConversion(typeof(SquareType), typeof(Brush))]
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SquareType type = (SquareType)value;

            switch (type)
            {
                case SquareType.Unknown:
                    return new SolidColorBrush(Colors.LightBlue);
                case SquareType.Water:
                    return new SolidColorBrush(Colors.LightBlue);
                case SquareType.Undamaged:
                    return new SolidColorBrush(Colors.Black);
                case SquareType.Damaged:
                    return new SolidColorBrush(Colors.Orange);
                case SquareType.Sunk:
                    return new SolidColorBrush(Colors.Red);
            }

            throw new Exception("something went horribly wrong");
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
