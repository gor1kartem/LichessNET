using LichessNET.Entities;
using LichessNET.Entities.Enumerations;

namespace LichessNET.API.Converter
{
    internal class UserConverters
    {
        public static Dictionary<Gamemode, GamemodeStats> PerfsConverter(dynamic obj)
        {
            var gm_bullet = obj.bullet.ToObject<GamemodeStats>();
            var gm_blitz = obj.blitz.ToObject<GamemodeStats>();
            var gm_rapid = obj.rapid.ToObject<GamemodeStats>();
            var gm_classical = obj.classical.ToObject<GamemodeStats>();
            var gm_chess960 = obj.chess960.ToObject<GamemodeStats>();
            var gm_kingOfTheHill = obj.kingOfTheHill.ToObject<GamemodeStats>();
            var gm_threeCheck = obj.threeCheck.ToObject<GamemodeStats>();
            var gm_antichess = obj.antichess.ToObject<GamemodeStats>();
            var gm_atomic = obj.atomic.ToObject<GamemodeStats>();
            var gm_horde = obj.horde.ToObject<GamemodeStats>();
            var gm_racingKings = obj.racingKings.ToObject<GamemodeStats>();
            var gm_crazyhouse = obj.crazyhouse.ToObject<GamemodeStats>();
            var gm_storm = obj.storm.ToObject<GamemodeStats>();
            var gm_racer = obj.racer.ToObject<GamemodeStats>();
            var gm_streak = obj.streak.ToObject<GamemodeStats>();

            var ratings = new Dictionary<Gamemode, GamemodeStats>();

            if (gm_bullet != null)
            {
                ratings.Add(Gamemode.Bullet, gm_bullet);
            }

            if (gm_blitz != null)
            {
                ratings.Add(Gamemode.Blitz, gm_blitz);
            }

            if (gm_rapid != null)
            {
                ratings.Add(Gamemode.Rapid, gm_rapid);
            }

            if (gm_classical != null)
            {
                ratings.Add(Gamemode.Classical, gm_classical);
            }

            if (gm_chess960 != null)
            {
                ratings.Add(Gamemode.Chess960, gm_chess960);
            }

            if (gm_kingOfTheHill != null)
            {
                ratings.Add(Gamemode.KingOfTheHill, gm_kingOfTheHill);
            }

            if (gm_threeCheck != null)
            {
                ratings.Add(Gamemode.ThreeCheck, gm_threeCheck);
            }

            if (gm_antichess != null)
            {
                ratings.Add(Gamemode.Antichess, gm_antichess);
            }

            if (gm_atomic != null)
            {
                ratings.Add(Gamemode.Atomic, gm_atomic);
            }

            if (gm_horde != null)
            {
                ratings.Add(Gamemode.Horde, gm_horde);
            }

            if (gm_racingKings != null)
            {
                ratings.Add(Gamemode.RacingKings, gm_racingKings);
            }

            if (gm_crazyhouse != null)
            {
                ratings.Add(Gamemode.Crazyhouse, gm_crazyhouse);
            }

            if (gm_storm != null)
            {
                ratings.Add(Gamemode.Storm, gm_storm);
            }

            if (gm_racer != null)
            {
                ratings.Add(Gamemode.Racer, gm_racer);
            }

            if (gm_streak != null)
            {
                ratings.Add(Gamemode.Streak, gm_streak);
            }

            return ratings;
        }
    }
}
