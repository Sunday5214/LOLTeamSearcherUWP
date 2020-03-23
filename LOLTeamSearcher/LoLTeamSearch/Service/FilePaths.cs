using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace LoLTeamSearch.Service
{
    public class FilePaths
    {
        public static StorageFolder jsonPath = null;
        public static StorageFolder itemPath = null;
        public static StorageFolder profilePath = null;
        public static StorageFolder tierPath = null;
        public static StorageFolder PerkPath = null;
        public static StorageFolder ChampionPath = null;
        public static StorageFolder SpellPath = null;
        public FilePaths()
        {
            GetAllPath();
        }

        private async void GetAllPath()
        {
            jsonPath = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets\json");
            itemPath = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets\item");
            profilePath = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets\profileIcon");
            tierPath = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets\tier");
            PerkPath = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            ChampionPath = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets\champion");
            SpellPath = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets\spell");
        }
    }
}
