using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace UltraKillYourself
{ 
    [BepInPlugin(UKYInfo.PLUGIN_GUID, UKYInfo.PLUGIN_NAME, UKYInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static new ManualLogSource Logger;

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {UKYInfo.PLUGIN_GUID} is loaded!");
            Logger.LogInfo("Time to kick ass.");

            UKYConfig.initConfig(Config);

            Harmony harmony = new Harmony(UKYInfo.PLUGIN_GUID);
            harmony.PatchAll(); //kickstart my shart
        }

        private void Update()
        {
            //silly test code, HP will reset to 50 each tick, but this also fucks up respawning
            //NewMovement nmi = MonoSingleton<NewMovement>.Instance;
            //if (nmi != null) nmi.hp = 50;

            //the meat and bones of the whole system
            UKYDirector.updateDirector();
            //for the rest, see the patches and the UKYDirector calls used by those
        }
    }
}