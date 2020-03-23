using LoLTeamSearch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace LoLTeamSearch.Controls.Converter
{
    public class SummorTierToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string path = FilePaths.tierPath.Path + "\\" + "provisional.png";
            if (value != null)
            {
                path = FilePaths.tierPath.Path + "\\" +value.ToString() + ".png";
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
