using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLTeamSearch.Service.Response
{
    //https://kr.api.riotgames.com/lol/summoner/v4/summoners/by-name/xaml?api_key=RGAPI-406429c5-086e-4ce7-909c-6863b8dc76c5
    public class Summor
    {
        [JsonProperty("profileIconId")]
        public int ProfileIconId
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        //player universally unique identifiers 플레이어 식별 고유 아이디
        [JsonProperty("puuid")]
        public string Puuid
        {
            get;
            set;
        }


        [JsonProperty("summonerLevel")]
        public long SummonerLevel
        {
            get;
            set;
        }


        [JsonProperty("revisionDate")]
        public long RevisionDate
        {
            get;
            set;
        }


        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }


        [JsonProperty("accountId")]
        public string AccountId
        {
            get;
            set;
        }
    }
}
