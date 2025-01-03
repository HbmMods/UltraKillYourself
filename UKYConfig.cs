using System;
using BepInEx.Configuration;

namespace UltraKillYourself
{
    /** Config, self-explanatory */
    public static class UKYConfig
    {
        public static ConfigEntry<bool>     configTest;
        public static ConfigEntry<int>      ukyMaxValue;
        public static ConfigEntry<int>      ukyDirectorPause;
        public static ConfigEntry<int>      ukyDecreaseOnDamageFlat;
        public static ConfigEntry<double>   ukyDecreaseOnDamageMult;
        public static ConfigEntry<int>      ukyDecreaseOnDeath;
        public static ConfigEntry<double>   ukyPlayerDamageScaleBegin;
        public static ConfigEntry<double>   ukyPlayerDamageScaleEnd;
        public static ConfigEntry<bool>     ukyOverrideParryHeal;
        public static ConfigEntry<int>      ukyParryHealBegin;
        public static ConfigEntry<int>      ukyParryHealEnd;

        public static void initConfig(ConfigFile config)
        {
            configTest =                config.Bind("General.Toggles",  "TestValue",                true,       "Does absolutely fucking nothing");
            ukyMaxValue =               config.Bind("UKY.Counter",      "CounterMaxValue",          7 * 60,     "How much the UKY director's counter may tick up until it maxes out, measured in seconds");
            ukyDirectorPause =          config.Bind("UKY.Counter",      "CounterPause",             10,         "How many seconds of inactivity (no enemy being damaged) need to pass until the UKY director halts ticking up the counter");
            ukyDecreaseOnDamageFlat =   config.Bind("UKY.Motion",       "DecreaseOnDamageFlat",     0,           "How many seconds of the director's counter are subtracted every time the player takes damage");
            ukyDecreaseOnDamageMult =   config.Bind("UKY.Motion",       "DecreaseOnDamageMult",     0.25,       "Multiplied with the damage taken to figure out how many seconds should be subtracted from the counter, stacks with the previous value");
            ukyDecreaseOnDeath =        config.Bind("UKY.Motion",       "DecreaseOnDeath",          60,         "How many seconds of the director's counter are subtracted every time the player dies");
            ukyPlayerDamageScaleBegin = config.Bind("UKY.Scale",        "PlayerDamageScaleBegin",   0.5D,       "Damage multiplier if the director's counter is at 0");
            ukyPlayerDamageScaleEnd =   config.Bind("UKY.Scale",        "PlayerDamageScaleEnd",     2.5D,       "Damage multiplier if the director's counter is maxed out");
            ukyOverrideParryHeal =      config.Bind("UKY.Misc",         "OverrideParryHeal",        true,       "Replaces the default 'heal 999 from any parry' with custom scaled behavior");
            ukyParryHealBegin =         config.Bind("UKY.Misc",         "ParryHealBegin",           50,         "Parry heal if the director's counter is at 0");
            ukyParryHealEnd =           config.Bind("UKY.Misc",         "ParryHealEnd",             -50,        "Parry heal if the director's counter is maxed out (cannot go below 0, negative values mean 0 is reached before counter maximum)");
        }
    }
}
