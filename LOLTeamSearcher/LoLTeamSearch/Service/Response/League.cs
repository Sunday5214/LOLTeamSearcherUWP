using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLTeamSearch.Service.Response
{
    //public class LeagueResponse
    //{
    //    List
    //}
    public class League
    {
        [JsonProperty("queueType")]
        public string QueueType
        {
            get;
            set;
        }

        [JsonProperty("summonerName")]
        public string SummonerName
        {
            get;
            set;
        }

        [JsonProperty("hotStreak")]
        public bool HotStreak
        {
            get;
            set;
        }

        [JsonProperty("miniSeries")]
        public MiniSeriesDTO MiniSeries
        {
            get;
            set;
        }

        [JsonProperty("wins")]
        public int Wins
        {
            get;
            set;
        }

        [JsonProperty("veteran")]
        public bool Veteran
        {
            get;
            set;
        }

        [JsonProperty("losses")]
        public int Losses
        {
            get;
            set;
        }

        [JsonProperty("rank")]
        public string Rank
        {
            get;
            set;
        }

        [JsonProperty("leagueId")]
        public string LeagueId
        {
            get;
            set;
        }

        [JsonProperty("inactive")]
        public bool Inactive
        {
            get;
            set;
        }

        [JsonProperty("freshBlood")]
        public bool FreshBlood
        {
            get;
            set;
        }

        [JsonProperty("tier")]
        public string Tier
        {
            get;
            set;
        }

        [JsonProperty("summonerId")]
        public string SummonerId
        {
            get;
            set;
        }

        [JsonProperty("leaguePoints")]
        public int LeaguePoints
        {
            get;
            set;
        }
    }

    public class MiniSeriesDTO
    {
        [JsonProperty("progress")]
        public string Progress
        {
            get;
            set;
        }

        [JsonProperty("losses")]
        public int Losses
        {
            get;
            set;
        }

        [JsonProperty("wins")]
        public int Wins
        {
            get;
            set;
        }

        [JsonProperty("target")]
        public int Target
        {
            get;
            set;
        }
    }
}
