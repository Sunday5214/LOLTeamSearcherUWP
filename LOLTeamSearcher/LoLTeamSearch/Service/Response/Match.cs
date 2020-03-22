using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLTeamSearch.Service.Response
{
    //https://kr.api.riotgames.com/lol/match/v4/matchlists/by-account/qjh8_bSpysDhId2lbUgJMyxZlhZxiHn9G-VY-VYbIULzFjc?queue=420&season=13&api_key=RGAPI-406429c5-086e-4ce7-909c-6863b8dc76c5
    
    public class Match
    {
        [JsonProperty("matches")]
        public List<MatchReferenceDto> Matches
        {
            get;
            set;
        }

        [JsonProperty("totalGames")]
        public int TotalGames
        {
            get;
            set;
        }

        [JsonProperty("startIndex")]
        public int StartIdx
        {
            get;
            set;
        }

        [JsonProperty("endIndex")]
        public int EndIdx
        {
            get;
            set;
        }
    }

    //
    public class MatchReferenceDto
    {
        [JsonProperty("lane")]
        public string Lane
        {
            get;
            set;
        }

        [JsonProperty("gameId")]
        public long GameId
        {
            get;
            set;
        }

        [JsonProperty("champion")]
        public int Champion
        {
            get;
            set;
        }

        [JsonProperty("platformId")]
        public string PlatformId
        {
            get;
            set;
        }

        [JsonProperty("season")]
        public int Season
        {
            get;
            set;
        }

        [JsonProperty("queue")]
        public int Queue
        {
            get;
            set;
        }

        [JsonProperty("role")]
        public string Role
        {
            get;
            set;
        }

        [JsonProperty("timestamp")]
        public long Timestamp
        {
            get;
            set;
        }
    }
}
