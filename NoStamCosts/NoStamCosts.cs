using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;

namespace ValheimNoStam
{

    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class NoStamCosts : BaseUnityPlugin
    {
        #region[Declarations]

        public const string
            MODNAME = "NoStamCosts",
            AUTHOR = "Basil",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "0.1.2";

        internal readonly ManualLogSource log;
        internal readonly Harmony harmony;
        internal readonly Assembly assembly;
        public readonly string modFolder;

        #endregion

        #region Config

        public static ConfigEntry<int> StaminaCostSetting;

        #endregion

        #region Initialize Config
        
        public void InitConfig()
        {
            StaminaCostSetting = Config.Bind(
                "Stamina Cost Setting",
                "StaminaCostSetting",
                1,
                "0 = Off, 1 = No stamina costs only when using the hammer, hoe or cultivator, 2 = No stamina costs in god mode, 3 = No stamina costs even when not in god mode"
                );
        }

        #endregion

        public NoStamCosts()
        {
            log = Logger;
            harmony = new Harmony(GUID);
            assembly = Assembly.GetExecutingAssembly();
            modFolder = Path.GetDirectoryName(assembly.Location);
        }

        public void Awake()
        {
            InitConfig();
            harmony.PatchAll(assembly);
        }

    }
}
