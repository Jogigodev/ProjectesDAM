using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Trivial.Model;

namespace Trivial.Infrastructure
{
    public class TemaToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Tema tema = (Tema)value;
            SolidColorBrush resultat;
            switch (tema)
            {
                case Tema.Art:
                    resultat = Brushes.LightYellow;
                    break;
                case Tema.Ciència:
                    resultat = Brushes.LightGreen;
                    break;
                case Tema.Entreteniment:
                    resultat = Brushes.LightBlue;
                    break;
                case Tema.Esport:
                    resultat = Brushes.Orange;
                    break;
                case Tema.Geografia:
                    resultat = Brushes.SeaShell;
                    break;
                default:
                    resultat = Brushes.DarkMagenta;
                    break;
            }
            return resultat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
