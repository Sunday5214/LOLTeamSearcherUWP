
using LoLTeamSearch.Service.Response;
using LOLTeamSearcherNetWork;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoLTeamSearch.Service
{
    public class LOLApiService
    {
        const string API_KEY = "RGAPI-bdb3dde5-2112-44c0-a1e8-532b9f86e9d1";
        const string GET_SUMMONER = "/summoner/v4/summoners/by-name/";
        const string GET_MATCHS = "/match/v4/matchlists/by-account/";
        const string GET_MATCH_DETAIL = "/match/v4/matches/";
        const string GET_LEAGUE = "/league/v4/entries/by-summoner/";

        RestManager restManager = new RestManager();

        public async Task<Summor> GetSummorData(string name)
        {
            QueryParam[] queryParams = new QueryParam[1];
            queryParams[0] = new QueryParam("api_key", API_KEY);
            return await restManager.GetResponse<Summor>(GET_SUMMONER+name, Method.GET, null, queryParams);
        }

        public async Task<Match> GetMatchs(string accountId, int season, int startIdx, int endIdx)
        {
            QueryParam[] queryParams = new QueryParam[5];
            queryParams[0] = new QueryParam("queue", 420);
            queryParams[1] = new QueryParam("season", season);
            queryParams[2] = new QueryParam("endIndex", endIdx);
            queryParams[3] = new QueryParam("beginIndex", startIdx);
            queryParams[4] = new QueryParam("api_key", API_KEY);
            return await restManager.GetResponse<Match>(GET_MATCHS + accountId, Method.GET, null, queryParams);
        }

        public async Task<MatchDetail> GetMatchDetailData(long matchId)
        {
            QueryParam[] queryParams = new QueryParam[1];
            queryParams[0] = new QueryParam("api_key", API_KEY);
            return await restManager.GetResponse<MatchDetail>(GET_MATCH_DETAIL + matchId, Method.GET, null, queryParams);
        }

        public async Task<List<League>> GetLeagueData(string summonerId)
        {
            QueryParam[] queryParams = new QueryParam[1];
            queryParams[0] = new QueryParam("api_key", API_KEY);
            return await restManager.GetResponse<List<League>>(GET_LEAGUE + summonerId, Method.GET, null, queryParams);
        }
    }
}
