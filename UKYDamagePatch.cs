using System;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace UltraKillYourself
{
    /* for enemy damage */
    [HarmonyPatch(typeof(EnemyIdentifier), "DeliverDamage")]
    public static class UKYDeliverDamagePatch
    {
        static void Postfix(GameObject sourceWeapon)
        {
            if(sourceWeapon != null) UKYDirector.lastEnemyDamage = UKYDirector.getEpoch();
        }
    }

    /* for player damage */
    [HarmonyPatch(typeof(NewMovement), "GetHurt")]
    public static class UKYGetHurtPatch
    {
        static void Prefix(ref int damage)
        {
            /*
             * initially the counter reset was performed before the damage modification,
             * effectively using the original damage. this resulted in the player taking no damage, but the counter
             * taking a massive hit. really, "hypothetical damage the player could have received" is less
             * of a useful metric than "the effective ass-whooping the player got"            
             */
            damage = UKYDirector.modifyPlayerDamage(damage);
            UKYDirector.onPlayerDamage(damage);
        }

        static void Postfix(NewMovement __instance)
        {
            if (__instance.hp <= 0) UKYDirector.onPlayerDeath();
        }
    }

    /* for parrying */
    [HarmonyPatch(typeof(NewMovement), "Parry")]
    public static class UKYParryPatch
    {
        static void Prefix()
        {
            UKYDirector.isParrying = true;
        }

        static void Postfix()
        {
            UKYDirector.isParrying = false;
        }
    }

    /* for healing */
    [HarmonyPatch(typeof(NewMovement), "GetHealth")]
    public static class UKYHealPatch
    {
        static void Prefix(ref int health)
        {
            health = UKYDirector.modifyPlayerParryHeal();
        }
    }

    /* hijacks the cheats display to show UKY debug info */
    [HarmonyPatch(typeof(CheatsController), "Update")]
    public static class UKYRenderInfo
    {
        static void Postfix(bool ___cheatsEnabled, GameObject ___cheatsInfoPanel, GameObject ___cheatsEnabledPanel)
        {
            //if (___cheatsEnabled) return;
            MonoSingleton<CheatsController>.Instance.cheatsInfo.text = "UKY ACTIVE\nLevel: " + UKYDirector.ukyLevel + " / " + UKYConfig.ukyMaxValue.Value;
            //___cheatsEnabledPanel.SetActive(value: true);
            //___cheatsInfoPanel.SetActive(value: true);
        }
    }
}
