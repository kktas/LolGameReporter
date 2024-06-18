using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Cache.Models
{
    public record ActiveGame(
        long GameId,
        int MapId,
        string GameMode,
        string GameType,
        List<Participant> Participants,
        int GameConfigId,
        long GameStartTime,
        int GameLength
    );

    public record Participant(
       string Puuid,
       int TeamId,
       int Spell1Id,
       int Spell2Id,
       int ChampionId,
       int ProfileIconId,
       string RiotId,
       bool Bot,
       string SummonerId,
       Perks Perks
    );

    public record Perks(int[] PerksIds, int PerkStyle, int PerkSubStyle);
}
