using LoLTeamSearch.Model;
using LoLTeamSearch.Service;
using LoLTeamSearch.Service.Response;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;

namespace LoLTeamSearch.ViewModel
{
    public class MatchHistorySearchViewModel : BindableBase
    {
        int startIdx = 0;
        int endIdx = 7;
        int total = -1;

        LOLApiService lolApiService = new LOLApiService();
        JsonFileManager jsonFileManager = new JsonFileManager();
        private ObservableCollection<MatchHistory> _matchHistories = new ObservableCollection<MatchHistory>();
        public ObservableCollection<MatchHistory> MatchHistories
        {
            get => _matchHistories;
            set => SetProperty(ref _matchHistories, value);
        }

        //private List<MatchDetail> MatchDetails { get; set; } = new List<MatchDetail>();

        public Summor Summor { get; set; } = new Summor();

        private Match Match { get; set; } = new Match();

        private string _summorName;
        public string SummorName
        {
            get => _summorName;
            set
            {
                SetProperty(ref _summorName, value);
            }
        }

        public ICommand SearchCommand
        {
            get;
            set;
        }

        public MatchHistorySearchViewModel()
        {
            SearchCommand = new DelegateCommand(GetSummorData);
        }

        private async void GetSummorData()
        {
            Summor = await lolApiService.GetSummorData(SummorName);
            await GetMatchData();
        }

        private async Task GetMatchData()
        {
            if (MatchHistories.Count == total) return;
            
            Match = await lolApiService.GetMatchs(Summor.AccountId, 13, startIdx, endIdx);
            await CombineData();


            if (total == -1) total = Match.TotalGames;
        }

        private async Task CombineData()
        {
            MatchDetail resp = null;
            List<ParticipantIdentityDto> Playerdata = null;
            List<ParticipantDto> MatchData = null;
            foreach (MatchReferenceDto matchReference in Match.Matches)
            {
                resp = await lolApiService.GetMatchDetailData(matchReference.GameId);
                MatchHistory matchHistory = new MatchHistory();

                foreach(var item in resp.Participants)
                {
                    ChampionInfo champion = await FindChampionName(item.ChampionId);
                    matchHistory.GameMemberDatas.Add(new GameMemberData
                    {
                        GameMemberChampion = champion.EnglishName,
                        GameMemberName = champion.KoreanName
                    });
                }

                Playerdata = resp.ParticipantIdentities.Where(x => x.Player.SummonerName == SummorName).ToList();

                if (Playerdata.Count == 0) break;

                MatchData = resp.Participants.Where(x => x.ParticipantId == Playerdata[0].ParticipantId).ToList();

                matchHistory.Kill = MatchData[0].Stats.Kills;
                matchHistory.Death = MatchData[0].Stats.Deaths;
                matchHistory.Assist = MatchData[0].Stats.Assists;

                matchHistory.AverageKDA = (double)(MatchData[0].Stats.Kills + MatchData[0].Stats.Assists) / MatchData[0].Stats.Deaths;

                ChampionInfo championInfo = await FindChampionName(MatchData[0].ChampionId);
                matchHistory.Champion = championInfo.EnglishName;
                matchHistory.ChampionName = championInfo.KoreanName;

                matchHistory.Item1 = MatchData[0].Stats.Item0;
                matchHistory.Item2 = MatchData[0].Stats.Item1;
                matchHistory.Item3 = MatchData[0].Stats.Item2;
                matchHistory.Item4 = MatchData[0].Stats.Item3;
                matchHistory.Item5 = MatchData[0].Stats.Item4;
                matchHistory.Item6 = MatchData[0].Stats.Item5;
                matchHistory.Item7 = MatchData[0].Stats.Item6;

                matchHistory.Spell1 = (await FindSpellPath(MatchData[0].Spell1Id)).ImageInfo.FullImage;
                matchHistory.Spell2 = (await FindSpellPath(MatchData[0].Spell2Id)).ImageInfo.FullImage;

                matchHistory.Level = MatchData[0].Stats.ChampLevel;

                List<RuneInfo> runeInfos = await GetRuneObject();


                matchHistory.MainRune = await GetRunePath(runeInfos, MatchData[0].Stats.PerkPrimaryStyle);
                matchHistory.SubRune = await GetRunePath(runeInfos, MatchData[0].Stats.PerkSubStyle);

                matchHistory.CS = MatchData[0].Stats.TotalMinionsKilled;
                TimeSpan time = TimeSpan.FromMilliseconds(matchReference.Timestamp);
                matchHistory.Time = (new DateTime(1970, 1, 1)+time).ToString();

                MatchHistories.Add(matchHistory);
            }
        }

        private async Task<string> GetRunePath(List<RuneInfo> runes, int id)
        {
            string path = null;

            await Task.Run(() =>
            {
                foreach (RuneInfo rune in runes)
                {
                    if (rune.Id == id)
                    {
                        path = rune.Icon;
                    }
                    else
                    {
                        foreach (var slot in rune.Slots)
                        {
                            foreach (var detailrune in slot.Rune)
                            {
                                if (detailrune.Id == id)
                                {
                                    path = detailrune.Icon;
                                }
                            }
                        }
                    }
                }
            });
           

            return path;
        }

        private async Task<List<RuneInfo>> GetRuneObject()
        {
            List<RuneInfo> SpellDatas = null;
            await Task.Run(() =>
            {
                SpellDatas = jsonFileManager.ConvertJsonToObject<List<RuneInfo>>(FilePaths.jsonPath.Path + "\\" + "runesReforged.json");

            });

            return SpellDatas;
        }

        private async Task<SpellInfo> FindSpellPath(int Key)
        {
            List<SpellInfo> SpellDatas = null;
            await Task.Run(() =>
            {
                jsonFileManager.ReadJsonFile(FilePaths.jsonPath.Path + "\\" + "summoner.json");
                SpellDatas = jsonFileManager.FindData<SpellInfo>(Key);

            });

            return SpellDatas.Where(x => x.Key == Key).ToList()[0];
        }
        private async Task<ChampionInfo> FindChampionName(int champId)
        {
            List<ChampionInfo> championDatas = null;
            await Task.Run(() =>
            {
                jsonFileManager.ReadJsonFile(FilePaths.jsonPath.Path + "\\champion.json");
                championDatas = jsonFileManager.FindData<ChampionInfo>(champId);

            });

            return championDatas.Where(x => x.Key == champId).ToList()[0];
        }
    }


    public class RuneInfo
    {
        [JsonProperty("id")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("key")]
        public string Key
        {
            get;
            set;
        }

        [JsonProperty("icon")]
        public string Icon
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

        [JsonProperty("slots")]
        public List<Runes> Slots
        {
            get;
            set;
        }

    }
    public class Runes
    {
        [JsonProperty("runes")]
        public List<RuneInfoDetail> Rune
        {
            get;
            set;
        }
    }

    public class RuneInfoDetail
    {
        [JsonProperty("id")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("key")]
        public string Key
        {
            get;
            set;
        }

        [JsonProperty("icon")]
        public string Icon
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


        [JsonProperty("shortDesc")]
        public string ShortDesc
        {
            get;
            set;
        }

        [JsonProperty("longDesc")]
        public string LongDesc
        {
            get;
            set;
        }
    }

    public class SpellInfo
    {
        [JsonProperty("id")]
        public string Id
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

        [JsonProperty("description")]
        public string Description
        {
            get;
            set;
        }

        [JsonProperty("tooltip")]
        public string Tooltip
        {
            get;
            set;
        }

        [JsonProperty("maxrank")]
        public int Maxrank
        {
            get;
            set;
        }

        [JsonProperty("cooldown")]
        public List<int> CoolDown
        {
            get;
            set;
        }

        [JsonProperty("cooldownBurn")]
        public int CooldownBurn
        {
            get;
            set;
        }

        [JsonProperty("cost")]
        public List<int> Cost
        {
            get;
            set;
        }

        [JsonProperty("costBurn")]
        public int CostBurn
        {
            get;
            set;
        }

        [JsonProperty("datavalues")]
        public DataValues DataValues
        {
            get;
            set;
        }

        [JsonProperty("effect")]
        public List<List<string>> Effect
        {
            get;
            set;
        }

        [JsonProperty("effectBurn")]
        public List<string> EffectBurn
        {
            get;
            set;
        }

        [JsonProperty("vars")]
        public List<Key> Vars
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

        [JsonProperty("summonerLevel")]
        public int SummonerLevel
        {
            get;
            set;
        }

        [JsonProperty("modes")]
        public List<string> Modes
        {
            get;
            set;
        }

        [JsonProperty("costType")]
        public string CostType
        {
            get;
            set;
        }

        [JsonProperty("maxammo")]
        public int Maxammo
        {
            get;
            set;
        }

        [JsonProperty("range")]
        public List<int> Range
        {
            get;
            set;
        }

        [JsonProperty("rangeBurn")]
        public int RangeBurn
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
    }

    public class Key
    {
    }

    public class DataValues
    {
    }
}
