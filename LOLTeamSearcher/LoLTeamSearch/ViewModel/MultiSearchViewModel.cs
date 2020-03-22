using LoLTeamSearch.Model;
using LoLTeamSearch.Service;
using LoLTeamSearch.Service.Response;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLTeamSearch.ViewModel
{
    public class MultiSearchViewModel : BindableBase
    {
        LOLApiService lolApiService = new LOLApiService();

        private string _summorNames;
        public string SummorNames
        {
            get=>_summorNames;
            set
            {
                SetProperty(ref _summorNames, value);
                if (value != "" && value != null && value != string.Empty)
                {
                    SummorSearch();
                }

            }
        }

        public List<Summor> Summors { get; set; } = new List<Summor>();

        private List<Match> Matches { get; set; } = new List<Match>();

        private List<MatchDetail> MatchDetails { get; set; } = new List<MatchDetail>();

        private List<League> Leagues { get; set; } = new List<League>();

        private ObservableCollection<MultiSearch> _multiSearchData = new ObservableCollection<MultiSearch>();
        public ObservableCollection<MultiSearch> MultiSearchData
        {
            get => _multiSearchData;
            set => SetProperty(ref _multiSearchData, value);
        }

        public MultiSearchViewModel()
        {
            FindChampionName();
        }

        private string[] ExtractSummorName()
        {
            return SummorNames.Replace("님이 방에 참가했습니다.", "").Split("\n");
        }




        //각 유저들의 데이터 받음
        private async void SummorSearch()
        {
            string[] names = ExtractSummorName();
            foreach(string summorName in names)
            {
                Summors.Add(await lolApiService.GetSummorData(summorName));
            }
            await GetLeagueData();
            await GetMatchData();
            await CombineDatas();
        }

        private async Task GetLeagueData()
        {
            foreach(Summor summor in Summors)
            {
                Leagues.Add((await lolApiService.GetLeagueData(summor.Id))[0]);
            }
        }

        private async Task GetMatchData()
        {
            foreach (Summor summor in Summors)
            {
                Matches.Add(await lolApiService.GetMatchs(summor.AccountId, 13, 0, 50));
            }
        }

        private async Task CombineDatas()
        {
            foreach(Summor summor in Summors)
            {
                MultiSearch multiSearchData = new MultiSearch();
                
                foreach (League league in Leagues)
                {
                    multiSearchData.Name = league.SummonerName;
                    multiSearchData.Point = league.LeaguePoints;
                    multiSearchData.Tier = league.Tier;
                    multiSearchData.Win = league.Wins;
                    multiSearchData.Lose = league.Losses;
                    multiSearchData.WinRate = league.Wins / (league.Wins + league.Losses) * 100;
                }
                foreach(Match match in Matches)
                {
                    LastestGame lastestGame = new LastestGame();
                    foreach(MatchReferenceDto matchReference in match.Matches)
                    {
                        MatchDetails.Add(await lolApiService.GetMatchDetailData(matchReference.GameId));
                    }
                }
                AnalyzeMatches(ref multiSearchData, summor.Name);
            }

        }
        
        private void AnalyzeMatches(ref MultiSearch multiSearchData, string summorName)
        {
            Dictionary<int, ChampionData> ChampionAndCount = new Dictionary<int, ChampionData>();
            List<ParticipantIdentityDto> Playerdata = null;
            List<ParticipantDto> MatchData = null;

            foreach (MatchDetail matchDetail in MatchDetails)
            {
                Playerdata = matchDetail.ParticipantIdentities.Where(x => x.Player.SummonerName == summorName).ToList();
                MatchData = matchDetail.Participants.Where(x => x.ParticipantId == Playerdata[0].ParticipantId).ToList();

                if (!ChampionAndCount.ContainsKey(MatchData[0].ChampionId))
                {
                    ChampionData championData = new ChampionData();

                    championData.Count++;

                    if (MatchData[0].Stats.Win == true)
                    {
                        championData.Win++;
                    }
                    else
                    {
                        championData.Lose++;
                    }

                    championData.Kill = MatchData[0].Stats.Kills;
                    championData.Death = MatchData[0].Stats.Deaths;
                    championData.Assist = MatchData[0].Stats.Assists;

                    ChampionAndCount.Add(MatchData[0].ChampionId, championData);
                }
                else
                {
                    ChampionAndCount[MatchData[0].ChampionId].Kill+= MatchData[0].Stats.Kills;
                    ChampionAndCount[MatchData[0].ChampionId].Death += MatchData[0].Stats.Deaths;
                    ChampionAndCount[MatchData[0].ChampionId].Assist += MatchData[0].Stats.Assists;

                    ChampionAndCount[MatchData[0].ChampionId].Count++;

                    if (MatchData[0].Stats.Win == true)
                    {
                        ChampionAndCount[MatchData[0].ChampionId].Win++;
                    }
                    else
                    {
                        ChampionAndCount[MatchData[0].ChampionId].Lose++;
                    }
                }
            }
            ChampionAndCount = ChampionAndCount.OrderByDescending(x => x.Value.Count).ToDictionary(key=>key.Key, value=>value.Value);
            
            for(int i = 0; i < 5; i++)
            {
                ChampionData data = ChampionAndCount.FirstOrDefault().Value;
                
                multiSearchData.MostChampions.Add(new MostChampion
                {
                    Count = data.Count,
                    WinRate = data.Win / (data.Win + data.Lose) * 100,
                    AverageKDA = (data.Kill+data.Assist)/data.Death,
                    //Champion = FindChampionName(ChampionAndCount.FirstOrDefault().Key)
                });
                ChampionAndCount.Remove(ChampionAndCount.FirstOrDefault().Key);
            }

        }

        private async void FindChampionName()
        {
            var jsonFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\json");

            JsonFileManager jsonFileManager = new JsonFileManager(jsonFolder.Path);
            jsonFileManager.ReadJsonFile();
        }
    }

   

    public class ChampionData
    {
        public int Count
        {
            get;
            set;
        }

        public int Win
        {
            get;
            set;
        }

        public int Lose
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
    }
}
