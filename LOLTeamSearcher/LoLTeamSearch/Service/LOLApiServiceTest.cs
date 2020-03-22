using LoLTeamSearch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLTeamSearch.Service
{
    public class LOLApiServiceTest
    {
        public LOLApiServiceTest()
        {
            ServerApiTest();
        }

        public async void ServerApiTest()
        {
            LOLApiService lOLApiService = new LOLApiService();
            var resp = await lOLApiService.GetSummorData("XAML");

            var resp2 = await lOLApiService.GetMatchs(resp.AccountId, 13, 0, 5);

            var resp3 = await lOLApiService.GetMatchDetailData(resp2.Matches[0].GameId);
        }
    }
}
