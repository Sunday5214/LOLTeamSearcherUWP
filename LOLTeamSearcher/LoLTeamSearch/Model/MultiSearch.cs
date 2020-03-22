using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLTeamSearch.Model
{
    public class MultiSearch
    {
        public string Name //league
        {
            get;
            set;
        }

        public string Tier//league
        {
            get;
            set;
        }

        public int Point//league
        {
            get;
            set;
        }

        public int Lose//league
        {
            get;
            set;
        }

        public int Win//league
        {
            get;
            set;
        }

        public double WinRate//league
        {
            get;
            set;
        }

        public string MainLane
        {
            get;
            set;
        }

        public List<LastestGame> LastestGames
        {
            get;
            set;
        } = new List<LastestGame>();

        public List<MostChampion> MostChampions
        {
            get;
            set;
        } = new List<MostChampion>();
    }
    /// <summary>
    /// 가장 마지막 5게임의 챔프정보
    /// </summary>
    public class LastestGame
    {
        public string Champion
        {
            get;
            set;
        }

        public int Death
        {
            get;
            set;
        }

        public int Kill
        {
            get;
            set;
        }

        public int Assist
        {
            get;
            set;
        }

        public double AverageKDA
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 최근 50판의 모스트챔프 정보
    /// </summary>
    public class MostChampion
    {
        public string Champion
        {
            get;
            set;
        }

        public int Count
        {  
            get;
            set;
        }

        public double WinRate
        {
            get;
            set;
        }

        public double AverageKDA
        {
            get;
            set;
        }
    }
}
