using System;
namespace UltraKillYourself
{
    /** Handles all the ticking and keeping track of UKY's stats */
    public static class UKYDirector
    {
        /** Timestamp of the last time an enemy has taken damage, in milliseconds */
        public static long lastEnemyDamage;
        /** Timestamp of the last time the director received a tick, used to determine ukyLevel increase in subsequent tick */
        public static long lastDirectorTick;
        /** 'Seconds' of the UKY counter */
        public static int ukyLevel = 0;
        public static bool isParrying = false;

        public static DateTime epochStart = new DateTime(1970, 1, 1);
        public static long getEpoch()
        {
            TimeSpan t = DateTime.UtcNow - epochStart;
            return (long) t.TotalMilliseconds;
        }

        public static void updateDirector()
        {
            long epoch = getEpoch();
            long timeSinceLastTick = epoch - lastDirectorTick;

            if(timeSinceLastTick >= 1000 && doesDirectorTick())
            {
                ukyLevel++;
                lastDirectorTick = epoch;
                normalizeLevel();
            }
        }

        public static void onPlayerDamage(int damage)
        {
            ukyLevel -= UKYConfig.ukyDecreaseOnDamageFlat.Value;
            ukyLevel -= (int) (UKYConfig.ukyDecreaseOnDamageMult.Value * damage);
            normalizeLevel();
        }

        public static void onPlayerDeath()
        {
            ukyLevel -= UKYConfig.ukyDecreaseOnDeath.Value;
            normalizeLevel();
        }

        public static int modifyPlayerDamage(int damage)
        {
            double scale = (double)ukyLevel / (double)UKYConfig.ukyMaxValue.Value;
            double min = UKYConfig.ukyPlayerDamageScaleBegin.Value;
            double max = UKYConfig.ukyPlayerDamageScaleEnd.Value;
            double multiplier = min + (max - min) * scale;
            return (int)(damage * scale);
        }

        public static int modifyPlayerParryHeal()
        {
            double scale = (double)ukyLevel / (double)UKYConfig.ukyMaxValue.Value;
            int min = UKYConfig.ukyParryHealBegin.Value;
            int max = UKYConfig.ukyParryHealEnd.Value;
            int heal = (int) (min + (max - min) * scale);
            return heal;
        }

        public static bool doesDirectorTick()
        {
            int timeUntilPause = UKYConfig.ukyDirectorPause.Value * 1000; //from seconds to milliseconds
            long timeSinceLastDamage = getEpoch() - lastEnemyDamage;
            return timeUntilPause > timeSinceLastDamage;
        }

        public static void normalizeLevel()
        {
            if (ukyLevel < 0) ukyLevel = 0;
            if (ukyLevel > UKYConfig.ukyMaxValue.Value) ukyLevel = UKYConfig.ukyMaxValue.Value;
        }
    }
}
