using HarmonyLib;
using UnityEngine;

namespace UltraKillYourself
{
    /* This sorta works but we don't use it anymore */
    [HarmonyPatch(typeof(StyleHUD), "Update")]
    public static class UKYStyleMeterPatch
    {
        static void Postfix(GameObject ___styleHud, TMPro.TMP_Text ___styleInfo)
        {
            //___styleHud.SetActive(true); //force style HUD to be constantly visible
            //___styleInfo.text = "Balls";
        }
    }
}
