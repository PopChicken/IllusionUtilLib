using System;
using System.Collections.Generic;
using System.Text;
using IUL.Utilities;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Harmony;
using BepInEx.Logging;

using KeyboardShortcut = BepInEx.Configuration.KeyboardShortcut;

namespace IUL {
    public partial class VolumeManager : BaseUnityPlugin {
        public const string DisplayName = "Volume Manager";
        public const string GUID = "com.polarstar.iul.volumemanager";
        public const string Version = "1.0";
        public static readonly string SystemLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.Name;

        private static HookManager HookManager;
        private static LanguageManager LangManager;
        internal static new ManualLogSource Logger;

        public static ConfigEntry<float> VolumeReductionRate;
        public static ConfigEntry<KeyboardShortcut> MuteKey;

        void Awake() {
            HookManager = new HookManager();
            LangManager = new LanguageManager(SystemLanguage);
            LanguageManager lm = LangManager;
            Logger = base.Logger;
            Logger.LogInfo($"[{DisplayName}] {lm.Text["Init"]}");

            Config.Bind(lm.Text["Section"], lm.Text["ReduceRate"], 0.20);
            Config.Bind(lm.Text["Section"], lm.Text["MuteKey"], new KeyboardShortcut(KeyCode.BackQuote));
        }
    }
}
