using LoLTeamSearch.Model;
using LoLTeamSearch.Service;
using LoLTeamSearch.Service.Response;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LoLTeamSearch.ViewModel
{
    public class MultiSearchViewModel : BindableBase
    {
        LOLApiService lolApiService = new LOLApiService();
        JsonFileManager jsonFileManager = new JsonFileManager();
        private string _summorNames;
        public string SummorNames
        {
            get=>_summorNames;
            set
            {
                SetProperty(ref _summorNames, value);
                if (value != "" && value != null && value != string.Empty)
                {
                    SetAllData();
                }

            }
        }

        public Summor Summor { get; set; } = new Summor();

        private Match Match { get; set; } = new Match();

        private List<MatchDetail> MatchDetails { get; set; } = new List<MatchDetail>();

        private League League { get; set; } = new League();

        private ObservableCollection<MultiSearch> _multiSearchData = new ObservableCollection<MultiSearch>();
        public ObservableCollection<MultiSearch> MultiSearchData
        {
            get => _multiSearchData;
            set => SetProperty(ref _multiSearchData, value);
        }

        public MultiSearchViewModel()
        {
        }

        private string[] ExtractSummorName()
        {
            return SummorNames.Replace("님이 방에 참가했습니다.", "").Split("\r");
        }




        //각 유저들의 데이터 받음
        private async void SetAllData()
        {
            string[] names = ExtractSummorName();
            
            foreach(string name in names)
            {
                Summor = await lolApiService.GetSummorData(name);
                await GetLeagueData();
                await GetMatchData();
                await CombineDatas();
            }

        }

        private async Task GetLeagueData()
        {
            var resp = (await lolApiService.GetLeagueData(Summor.Id));
            League = resp.Where(x=>x.QueueType=="RANKED_SOLO_5x5").ToList()[0];
        }

        private async Task GetMatchData()
        {
            Match = (await lolApiService.GetMatchs(Summor.AccountId, 13, 0, 30));
        }

        private async Task CombineDatas()
        {
            MultiSearch multiSearchData = new MultiSearch();

            multiSearchData.Name = League.SummonerName;
            multiSearchData.Point = League.LeaguePoints;
            multiSearchData.Tier = League.Tier;
            multiSearchData.Win = League.Wins;
            multiSearchData.Lose = League.Losses;
            multiSearchData.Step = League.Rank;
            multiSearchData.WinRate = (double)League.Wins / (League.Wins + League.Losses) * 100;
            foreach (MatchReferenceDto matchReference in Match.Matches)
            {
                MatchDetails.Add(await lolApiService.GetMatchDetailData(matchReference.GameId));
            }
            multiSearchData = await AnalyzeMatches(multiSearchData, Summor.Name);
            MultiSearchData.Add(multiSearchData);
            MatchDetails.Clear();
        }
        
        private async Task<MultiSearch> AnalyzeMatches(MultiSearch multiSearchData, string summorName)
        {
            Dictionary<int, MatchData> ChampionAndCount = new Dictionary<int, MatchData>();
            List<ParticipantIdentityDto> Playerdata = null;
            List<ParticipantDto> MatchData = null;
            int count = 0;
            foreach (MatchDetail matchDetail in MatchDetails)
            {
                Playerdata = matchDetail.ParticipantIdentities.Where(x => x.Player.SummonerName == summorName).ToList();

                if (Playerdata.Count == 0) break;
                
                MatchData = matchDetail.Participants.Where(x => x.ParticipantId == Playerdata[0].ParticipantId).ToList();

                if (!ChampionAndCount.ContainsKey(MatchData[0].ChampionId))
                {
                    MatchData championData = new MatchData();

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

                if (count < 5)
                {
                    multiSearchData.LastestGames.Add(new LastestGame
                    {
                        Champion = (await FindChampionName(MatchData[0].ChampionId)).EnglishName,
                        Kill = MatchData[0].Stats.Kills,
                        Death = MatchData[0].Stats.Deaths,
                        Assist = MatchData[0].Stats.Assists,
                        AverageKDA = (double)(MatchData[0].Stats.Kills+ MatchData[0].Stats.Assists)/ MatchData[0].Stats.Deaths
                    });
                }
                count++;
            }
            ChampionAndCount = ChampionAndCount.OrderByDescending(x => x.Value.Count).ToDictionary(key=>key.Key, value=>value.Value);
            
            for(int i = 0; i < 5; i++)
            {
                MatchData data = ChampionAndCount.FirstOrDefault().Value;
                
                multiSearchData.MostChampions.Add(new MostChampion
                {
                    Count = data.Count,
                    WinRate = (double)data.Win / (data.Win + data.Lose) * 100,
                    AverageKDA = (double)(data.Kill+data.Assist)/data.Death,
                    Champion =(await FindChampionName(ChampionAndCount.FirstOrDefault().Key)).EnglishName
                });
                ChampionAndCount.Remove(ChampionAndCount.FirstOrDefault().Key);
            }

            return multiSearchData;
        }

        private async Task<ChampionInfo> FindChampionName(int champId)
        {
            List<ChampionInfo> championDatas = null;

            await Task.Run(() =>
            {
                if (JsonFileManager.JsonData != null)
                {
                    championDatas = jsonFileManager.FindData<ChampionInfo>(champId);

                }
                else
                {
                    jsonFileManager.ReadJsonFile(FilePaths.jsonPath.Path + "\\champion.json");
                    championDatas = jsonFileManager.FindData<ChampionInfo>(champId);
                }

            });
            
            return championDatas.Where(x => x.Key == champId).ToList()[0];
        }
    }

    public class ChampionInfo
    {
        [JsonProperty("version")]
        public string Version
        {
            get;
            set;
        }

        [JsonProperty("id")]
        public string EnglishName
        {
            get;
            set;
        }

        [JsonProperty("key")]
        public int Key
        {
            get;
            set;
        }


        [JsonProperty("name")]
        public string KoreanName
        {
            get;
            set;
        }


        [JsonProperty("title")]
        public string Title
        {
            get;
            set;
        }

        [JsonProperty("blurb")]
        public string Story
        {
            get;
            set;
        }

        [JsonProperty("info")]
        public Info Info
        {
            get;
            set;
        }

        [JsonProperty("image")]
        public ImageInfo ImageInfo
        {
            get;
            set;
        }

        [JsonProperty("tags")]
        public List<string> Role
        {
            get;
            set;
        }

        [JsonProperty("partype")]
        public string Cost
        {
            get;
            set;
        }

        [JsonProperty("stats")]
        public Stats Stats
        {
            get;
            set;
        }
    }

    public class Stats
    {
        [JsonProperty("hp")]
        public int Hp
        {
            get;
            set;
        }

        [JsonProperty("hpperlevel")]
        public int HpPerLevel
        {
            get;
            set;
        }

        [JsonProperty("mp")]
        public int Mp
        {
            get;
            set;
        }


        [JsonProperty("mpperlevel")]
        public int MpPerLevel
        {
            get;
            set;
        }


        [JsonProperty("movespeed")]
        public int MoveSpeed
        {
            get;
            set;
        }

        [JsonProperty("armor")]
        public double Armor
        {
            get;
            set;
        }

        [JsonProperty("armorperlevel")]
        public double ArmorPerLevel
        {
            get;
            set;
        }

        [JsonProperty("spellblock")]
        public int SpellBlock
        {
            get;
            set;
        }

        [JsonProperty("spellblockperlevel")]
        public double SpellblockPerLevel
        {
            get;
            set;
        }

        [JsonProperty("attackrange")]
        public int AttackRange
        {
            get;
            set;
        }

        [JsonProperty("hpregen")]
        public double Hpregen
        {
            get;
            set;
        }

        [JsonProperty("hpregenperlevel")]
        public double HpregenPerLevel
        {
            get;
            set;
        }

        [JsonProperty("mpregen")]
        public double Mpregen
        {
            get;
            set;
        }

        [JsonProperty("mpregenperlevel")]
        public double MpregenPerLevel
        {
            get;
            set;
        }

        [JsonProperty("crit")]
        public int Crit
        {
            get;
            set;
        }

        [JsonProperty("critperlevel")]
        public int CritPerLevel
        {
            get;
            set;
        }

        [JsonProperty("attackdamage")]
        public double AttackDamage
        {
            get;
            set;
        }

        [JsonProperty("attackdamageperlevel")]
        public double AttackDamagePerLevel
        {
            get;
            set;
        }

        [JsonProperty("attackspeedperlevel")]
        public double AttackSpeedPerLevel
        {
            get;
            set;
        }

        [JsonProperty("attackspeed")]
        public double AttackSpeed
        {
            get;
            set;
        }
    }

    public class Info
    {
        [JsonProperty("attack")]
        public int Attack
        {
            get;
            set;
        }

        [JsonProperty("defense")]
        public int Defense
        {
            get;
            set;
        }

        [JsonProperty("magic")]
        public int Magic
        {
            get;
            set;
        }

        [JsonProperty("diffculty")]
        public int Difficulty
        {
            get;
            set;
        }
    }
    public class ImageInfo
    {
        [JsonProperty("full")]
        public string FullImage
        {
            get;
            set;
        }

        [JsonProperty("sprite")]
        public string SpriteImage
        {
            get;
            set;
        }

        [JsonProperty("group")]
        public string Group
        {
            get;
            set;
        }

        [JsonProperty("x")]
        public int X
        {
            get;
            set;
        }

        [JsonProperty("y")]
        public int Y
        {
            get;
            set;
        }

        [JsonProperty("w")]
        public int W
        {
            get;
            set;
        }

        [JsonProperty("h")]
        public int H
        {
            get;
            set;
        }

        [JsonProperty("resource")]
        public string Resource
        {
            get;
            set;
        }
    }
    public class MatchData
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
