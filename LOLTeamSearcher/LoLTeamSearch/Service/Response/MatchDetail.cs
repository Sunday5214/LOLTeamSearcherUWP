using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLTeamSearch.Service.Response
{
    //https://kr.api.riotgames.com/lol/match/v4/matches/4200054377?api_key=RGAPI-406429c5-086e-4ce7-909c-6863b8dc76c5
    public class MatchDetail
    {
        [JsonProperty("seasonId")]
        public int SeasonId
        {
            get;
            set;
        }

        [JsonProperty("queueId")]
        public int QueueId
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

        [JsonProperty("participantIdentities")]
        public List<ParticipantIdentityDto> ParticipantIdentities
        {
            get;
            set;
        }

        [JsonProperty("gameVersion")]
        public string GameVersion
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

        [JsonProperty("gameMode")]
        public string GameMode
        {
            get;
            set;
        }

        [JsonProperty("mapId")]
        public int MapId
        {
            get;
            set;
        }

        [JsonProperty("gameType")]
        public string GameType
        {
            get;
            set;
        }

        [JsonProperty("teams")]
        public List<TeamStatsDto> Teams
        {
            get;
            set;
        }

        [JsonProperty("participants")]
        public List<ParticipantDto> Participants
        {
            get;
            set;
        }

        [JsonProperty("gameDuration")]
        public long GameDuration
        {
            get;
            set;
        }

        [JsonProperty("gameCreation")]
        public long GameCreation
        {
            get;
            set;
        }
    }

    public class ParticipantDto
    {
        [JsonProperty("stats")]
        public ParticipantStatsDto Stats
        {
            get;
            set;
        }

        [JsonProperty("participantId")]
        public int ParticipantId
        {
            get;
            set;
        }

        [JsonProperty("runes")]
        public List<RuneDto> Runes
        {
            get;
            set;
        }

        [JsonProperty("timeline")]
        public ParticipantTimeLineDto TimeLine
        {
            get;
            set;
        }

        [JsonProperty("teamId")]
        public int Teamid
        {
            get;
            set;
        }

        [JsonProperty("spell2Id")]
        public int Spell2Id
        {
            get;
            set;
        }

        [JsonProperty("masteries")]
        public List<MasteryDto> Masteries
        {
            get;
            set;
        }

        [JsonProperty("highestAchievedSeasonTier")]
        public string HighestAchievedSeasonTier
        {
            get;
            set;
        }

        [JsonProperty("spell1Id")]
        public int Spell1Id
        {
            get;
            set;
        }

        [JsonProperty("championId")]
        public int ChampionId
        {
            get;
            set;
        }
    }

    public class MasteryDto
    {
        [JsonProperty("masteryId")]
        public int MasteryId
        {
            get;
            set;
        }

        [JsonProperty("rank")]
        public int Rank
        {
            get;
            set;
        }
    }

    public class ParticipantTimeLineDto
    {
        [JsonProperty("lane")]
        public string Lane
        {
            get;
            set;
        }

        [JsonProperty("participantId")]
        public int ParticipantId
        {
            get;
            set;
        }

        [JsonProperty("csDiffPerMinDeltas")]
        public Dictionary<string, double> CsDiffPerMinDeltas
        {
            get;
            set;
        }


        [JsonProperty("goldPerMinDeltas")]
        public Dictionary<string, double> GoldPerMinDeltas
        {
            get;
            set;
        }

        [JsonProperty("xpDiffPerMinDeltas")]
        public Dictionary<string, double> XpDiffPerMinDeltas
        {
            get;
            set;
        }

        [JsonProperty("creepsPerMinDeltas")]
        public Dictionary<string, double> CreepsPerMinDeltas
        {
            get;
            set;
        }

        [JsonProperty("xpPerMinDeltas")]
        public Dictionary<string, double> XpPerMinDeltas
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

        [JsonProperty("damageTakenDiffPerMinDeltas")]
        public Dictionary<string, double> DamageTakenDiffPerMinDeltas
        {
            get;
            set;
        }

        [JsonProperty("damageTakenPerMinDeltas")]
        public Dictionary<string, double> DamageTakenPerMinDeltas
        {
            get;
            set;
        }
    }

    public class RuneDto
    {
        [JsonProperty("runeId")]
        public int RuneId
        {
            get;
            set;
        }

        [JsonProperty("rank")]
        public int Rank
        {
            get;
            set;
        }
    }

    public class ParticipantStatsDto
    {
        [JsonProperty("firstBloodAssist")]
        public bool FirstBloodAssist
        {
            get;
            set;
        }

        [JsonProperty("visionScore")]
        public long VisionScore
        {
            get;
            set;
        }

        [JsonProperty("magicDamageDealtToChampions")]
        public long MagicDamageDealtToChampions
        {
            get;
            set;
        }

        [JsonProperty("damageDealtToObjectives")]
        public long damageDealtToObjectives
        {
            get;
            set;
        }

        [JsonProperty("totalTimeCrowdControlDealt")]
        public int TotalTimeCrowdControlDealt
        {
            get;
            set;
        }

        [JsonProperty("longestTimeSpentLiving")]
        public int LongestTimeSpentLiving
        {
            get;
            set;
        }

        [JsonProperty("perk1Var1")]
        public int Perk1Var1
        {
            get;
            set;
        }

        [JsonProperty("perk1Var3")]
        public int Perk1Var3
        {
            get;
            set;
        }

        [JsonProperty("perk1Var2")]
        public int Perk1Var2
        {
            get;
            set;
        }

        [JsonProperty("tripleKills")]
        public int tripleKills
        {
            get;
            set;
        }

        [JsonProperty("perk3Var3")]
        public int Perk3Var3
        {
            get;
            set;
        }

        [JsonProperty("nodeNeutralizeAssist")]
        public int NodeNeutralizeAssist
        {
            get;
            set;
        }

        [JsonProperty("perk3Var2")]
        public int Perk3Var2
        {
            get;
            set;
        }

        [JsonProperty("playerScore9")]
        public int PlayerScore9
        {
            get;
            set;
        }

        [JsonProperty("playerScore8")]
        public int PlayerScore8
        {
            get;
            set;
        }

        [JsonProperty("kills")]
        public int Kills
        {
            get;
            set;
        }

        [JsonProperty("playerScore1")]
        public int PlayerScore1
        {
            get;
            set;
        }

        [JsonProperty("playerScore0")]
        public int PlayerScore0
        {
            get;
            set;
        }

        [JsonProperty("playerScore3")]
        public int PlayerScore3
        {
            get;
            set;
        }

        [JsonProperty("playerScore2")]
        public int PlayerScore2
        {
            get;
            set;
        }

        [JsonProperty("playerScore5")]
        public int PlayerScore5
        {
            get;
            set;
        }

        [JsonProperty("playerScore4")]
        public int PlayerScore4
        {
            get;
            set;
        }

        [JsonProperty("playerScore7")]
        public int PlayerScore7
        {
            get;
            set;
        }

        [JsonProperty("playerScore6")]
        public int PlayerScore6
        {
            get;
            set;
        }

        [JsonProperty("perk5Var1")]
        public int Perk5Var1
        {
            get;
            set;
        }

        [JsonProperty("perk5Var3")]
        public int Perk5Var3
        {
            get;
            set;
        }

        [JsonProperty("perk5Var2")]
        public int Perk5Var2
        {
            get;
            set;
        }

        [JsonProperty("totalScoreRank")]
        public int TotalScoreRank
        {
            get;
            set;
        }

        [JsonProperty("neutralMinionsKilled")]
        public int NeutralMinionsKilled
        {
            get;
            set;
        }

        [JsonProperty("damageDealtToTurrets	")]
        public long DamageDealtToTurrets
        {
            get;
            set;
        }

        [JsonProperty("physicalDamageDealtToChampions")]
        public long PhysicalDamageDealtToChampions
        {
            get;
            set;
        }

        [JsonProperty("nodeCapture")]
        public int NodeCapture
        {
            get;
            set;
        }

        [JsonProperty("largestMultiKill")]
        public int LargestMultiKill
        {
            get;
            set;
        }

        [JsonProperty("perk2Var2")]
        public int Perk2Var2
        {
            get;
            set;
        }

        [JsonProperty("perk2Var3")]
        public int Perk2Var3
        {
            get;
            set;
        }

        [JsonProperty("totalUnitsHealed")]
        public int TotalUnitsHealed
        {
            get;
            set;
        }

        [JsonProperty("perk2Var1")]
        public int Perk2Var1
        {
            get;
            set;
        }

        [JsonProperty("perk4Var1")]
        public int Perk4Var1
        {
            get;
            set;
        }

        [JsonProperty("perk4Var2")]
        public int Perk4Var2
        {
            get;
            set;
        }

        [JsonProperty("perk4Var3")]
        public int Perk4Var3
        {
            get;
            set;
        }

        [JsonProperty("wardsKilled")]
        public int WardsKilled
        {
            get;
            set;
        }

        [JsonProperty("largestCriticalStrike")]
        public int LargestCriticalStrike
        {
            get;
            set;
        }

        [JsonProperty("largestKillingSpree")]
        public int LargestKillingSpree
        {
            get;
            set;
        }

        [JsonProperty("quadraKills")]
        public int QuadraKills
        {
            get;
            set;
        }

        [JsonProperty("teamObjective")]
        public int TeamObjective
        {
            get;
            set;
        }

        [JsonProperty("magicDamageDealt")]
        public long MagicDamageDealt
        {
            get;
            set;
        }

        [JsonProperty("item2")]
        public int Item2
        {
            get;
            set;
        }

        [JsonProperty("item3")]
        public int Item3
        {
            get;
            set;
        }

        [JsonProperty("item0")]
        public int Item0
        {
            get;
            set;
        }

        [JsonProperty("neutralMinionsKilledTeamJungle")]
        public int NeutralMinionsKilledTeamJungle
        {
            get;
            set;
        }

        [JsonProperty("item6")]
        public int Item6
        {
            get;
            set;
        }

        [JsonProperty("item4")]
        public int Item4
        {
            get;
            set;
        }

        [JsonProperty("item5")]
        public int Item5
        {
            get;
            set;
        }

        [JsonProperty("perk1")]
        public int Perk1
        {
            get;
            set;
        }

        [JsonProperty("perk0")]
        public int Perk0
        {
            get;
            set;
        }

        [JsonProperty("perk3")]
        public int Perk3
        {
            get;
            set;
        }

        [JsonProperty("perk2")]
        public int Perk2
        {
            get;
            set;
        }

        [JsonProperty("perk5")]
        public int Perk5
        {
            get;
            set;
        }

        [JsonProperty("perk4")]
        public int Perk4
        {
            get;
            set;
        }

        [JsonProperty("perk3Var1")]
        public int Perk3Var1
        {
            get;
            set;
        }

        [JsonProperty("damageSelfMitigated")]
        public long DamageSelfMitigated
        {
            get;
            set;
        }

        [JsonProperty("magicalDamageTaken")]
        public long MagicalDamageTaken
        {
            get;
            set;
        }

        [JsonProperty("firstInhibitorKill")]
        public bool FirstInhibitorKill
        {
            get;
            set;
        }

        [JsonProperty("trueDamageTaken")]
        public long TrueDamageTaken
        {
            get;
            set;
        }

        [JsonProperty("nodeNeutralize")]
        public int NodeNeutralize
        {
            get;
            set;
        }

        [JsonProperty("assists")]
        public int Assists
        {
            get;
            set;
        }

        [JsonProperty("combatPlayerScore")]
        public int CombatPlayerScore
        {
            get;
            set;
        }

        [JsonProperty("perkPrimaryStyle")]
        public int PerkPrimaryStyle
        {
            get;
            set;
        }

        [JsonProperty("goldSpent")]
        public int GoldSpent
        {
            get;
            set;
        }

        [JsonProperty("trueDamageDealt")]
        public long TrueDamageDealt
        {
            get;
            set;
        }

        [JsonProperty("participantId")]
        public int ParticipantId
        {
            get;
            set;
        }

        [JsonProperty("totalDamageTaken")]
        public long TotalDamageTaken
        {
            get;
            set;
        }

        [JsonProperty("physicalDamageDealt")]
        public long PhysicalDamageDealt
        {
            get;
            set;
        }

        [JsonProperty("sightWardsBoughtInGame")]
        public int SightWardsBoughtInGame
        {
            get;
            set;
        }

        [JsonProperty("totalDamageDealtToChampions")]
        public long TotalDamageDealtToChampions
        {
            get;
            set;
        }

        [JsonProperty("physicalDamageTaken")]
        public long PhysicalDamageTaken
        {
            get;
            set;
        }

        [JsonProperty("totalPlayerScore")]
        public int TotalPlayerScore
        {
            get;
            set;
        }

        [JsonProperty("win")]
        public bool Win
        {
            get;
            set;
        }

        [JsonProperty("objectivePlayerScore")]
        public int ObjectivePlayerScore
        {
            get;
            set;
        }

        [JsonProperty("totalDamageDealt")]
        public long TotalDamageDealt
        {
            get;
            set;
        }

        [JsonProperty("item1")]
        public int Item1
        {
            get;
            set;
        }

        [JsonProperty("neutralMinionsKilledEnemyJungle")]
        public int NeutralMinionsKilledEnemyJungle
        {
            get;
            set;
        }

        [JsonProperty("deaths")]
        public int Deaths
        {
            get;
            set;
        }

        [JsonProperty("wardsPlaced")]
        public int WardsPlaced
        {
            get;
            set;
        }

        [JsonProperty("perkSubStyle")]
        public int PerkSubStyle
        {
            get;
            set;
        }

        [JsonProperty("turretKills")]
        public int TurretKills
        {
            get;
            set;
        }

        [JsonProperty("firstBloodKill")]
        public bool FirstBloodKill
        {
            get;
            set;
        }

        [JsonProperty("trueDamageDealtToChampions")]
        public long TrueDamageDealtToChampions
        {
            get;
            set;
        }

        [JsonProperty("goldEarned")]
        public int GoldEarned
        {
            get;
            set;
        }

        [JsonProperty("killingSprees")]
        public int KillingSprees
        {
            get;
            set;
        }

        [JsonProperty("unrealKills")]
        public int UnrealKills
        {
            get;
            set;
        }

        [JsonProperty("altarsCaptured")]
        public int AltarsCaptured
        {
            get;
            set;
        }

        [JsonProperty("firstTowerAssist")]
        public bool FirstTowerAssist
        {
            get;
            set;
        }

        [JsonProperty("firstTowerKill")]
        public bool FirstTowerKill
        {
            get;
            set;
        }

        [JsonProperty("champLevel")]
        public int ChampLevel
        {
            get;
            set;
        }

        [JsonProperty("doubleKills")]
        public int DoubleKills
        {
            get;
            set;
        }

        [JsonProperty("nodeCaptureAssist")]
        public int NodeCaptureAssist
        {
            get;
            set;
        }

        [JsonProperty("inhibitorKills")]
        public int InhibitorKills
        {
            get;
            set;
        }

        [JsonProperty("firstInhibitorAssist")]
        public bool FirstInhibitorAssist
        {
            get;
            set;
        }

        [JsonProperty("perk0Var1")]
        public int Perk0Var1
        {
            get;
            set;
        }

        [JsonProperty("perk0Var2")]
        public int Perk0Var2
        {
            get;
            set;
        }

        [JsonProperty("perk0Var3")]
        public int Perk0Var3
        {
            get;
            set;
        }

        [JsonProperty("visionWardsBoughtInGame")]
        public int VisionWardsBoughtInGame
        {
            get;
            set;
        }

        [JsonProperty("altarsNeutralized")]
        public int AltarsNeutralized
        {
            get;
            set;
        }

        [JsonProperty("pentaKills")]
        public int PentaKills
        {
            get;
            set;
        }

        [JsonProperty("totalHeal")]
        public long TotalHeal
        {
            get;
            set;
        }

        [JsonProperty("totalMinionsKilled")]
        public int TotalMinionsKilled
        {
            get;
            set;
        }

        [JsonProperty("timeCCingOthers")]
        public long TimeCCingOthers
        {
            get;
            set;
        }
    }

    public class TeamStatsDto
    {
        [JsonProperty("firstDragon")]
        public bool FirstDragon
        {
            get;
            set;
        }

        [JsonProperty("firstInhibitor")]
        public bool FirstInhibitor
        {
            get;
            set;
        }

        [JsonProperty("bans")]
        public List<TeamBansDto> Bans
        {
            get;
            set;
        }

        [JsonProperty("baronKills")]
        public int BaronKills
        {
            get;
            set;
        }

        [JsonProperty("firstRiftHerald")]
        public bool FirstRiftHerald
        {
            get;
            set;
        }

        [JsonProperty("firstBaron")]
        public bool FirstBaron
        {
            get;
            set;
        }

        [JsonProperty("riftHeraldKills")]
        public int RiftHeraldKills
        {
            get;
            set;
        }

        [JsonProperty("firstBlood")]
        public bool FirstBlood
        {
            get;
            set;
        }

        [JsonProperty("teamId")]
        public int TeamId
        {
            get;
            set;
        }

        [JsonProperty("firstTower")]
        public bool firstTower
        {
            get;
            set;
        }

        [JsonProperty("vilemawKills")]
        public int VilemawKills
        {
            get;
            set;
        }

        [JsonProperty("inhibitorKills")]
        public int InhibitorKills
        {
            get;
            set;
        }

        [JsonProperty("towerKills")]
        public int TowerKills
        {
            get;
            set;
        }

        [JsonProperty("dominionVictoryScore")]
        public int DominionVictoryScore
        {
            get;
            set;
        }

        [JsonProperty("win")]
        public string Win
        {
            get;
            set;
        }

        [JsonProperty("dragonKills")]
        public int DragonKills
        {
            get;
            set;
        }
    }

    public class TeamBansDto
    {
        [JsonProperty("pickTurn")]
        public int PickTurn
        {
            get;
            set;
        }

        [JsonProperty("championId")]
        public int ChampionId
        {
            get;
            set;
        }
    }

    public class ParticipantIdentityDto
    {
        [JsonProperty("player")]
        public PlayerDto Player
        {
            get;
            set;
        }

        [JsonProperty("participantId")]
        public int ParticipantId
        {
            get;
            set;
        }
    }

    public class PlayerDto
    {
        [JsonProperty("currentPlatformId")]
        public string CurrentPlatformId
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

        [JsonProperty("matchHistoryUri")]
        public string MatchHistoryUri
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

        [JsonProperty("currentAccountId")]
        public string CurrentAccountId
        {
            get;
            set;
        }

        [JsonProperty("profileIcon")]
        public int ProfileIcon
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

        [JsonProperty("accountId")]
        public string AccountId
        {
            get;
            set;
        }
    }
}
