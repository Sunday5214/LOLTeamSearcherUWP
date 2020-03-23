using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLTeamSearch.Model
{
    public class MatchHistory
    {

        public string Champion
        {
            get;
            set;
        }

        public string Spell1
        {
            get;
            set;
        }

        public string Spell2
        {
            get;
            set;
        }

        public string MainRune
        {
            get;
            set;
        }

        public string SubRune
        {
            get;
            set;
        }

        public string ChampionName
        {
            get;
            set;
        }

        public int Kill
        {
            get;
            set;
        }

        public int Death
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

        public int Level
        {
            get;
            set;
        }

        public int CS
        {
            get;
            set;
        }

        private DateTime _time;
        public string Time
        {
            get=>_time.ToString("mm분 ss초");
            set=>_time = DateTime.Parse(value);
        }

        public int Item1
        {
            get;
            set;
        }

        public int Item2
        {
            get;
            set;
        }

        public int Item3
        {
            get;
            set;
        }

        public int Item4
        {
            get;
            set;
        }

        public int Item5
        {
            get;
            set;
        }

        public int Item6
        {
            get;
            set;
        }


        public int Item7
        {
            get;
            set;
        }

        public List<GameMemberData> GameMemberDatas
        {
            get;
            set;
        } = new List<GameMemberData>();
    }

    public class GameMemberData
    {
        public string GameMemberChampion
        {
            get;
            set;
        }

        public string GameMemberName
        {
            get;
            set;
        }
    }
}
