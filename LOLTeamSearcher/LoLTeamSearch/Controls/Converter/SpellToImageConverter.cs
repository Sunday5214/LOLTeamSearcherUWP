using LoLTeamSearch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace LoLTeamSearch.Controls.Converter
{
    public class SpellToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string path = FilePaths.SpellPath.Path + "\\" + "summorFlash";
            if (value != null)
            {
                path = FilePaths.SpellPath.Path + "\\" + value.ToString();
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
