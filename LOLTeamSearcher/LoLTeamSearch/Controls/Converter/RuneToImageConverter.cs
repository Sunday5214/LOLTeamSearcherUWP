using LoLTeamSearch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace LoLTeamSearch.Controls.Converter
{
    public class RuneToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string path = null;
            if(value != null)
            {
                path = FilePaths.PerkPath.Path +"\\" +(value.ToString().Replace("/", "\\"));
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
