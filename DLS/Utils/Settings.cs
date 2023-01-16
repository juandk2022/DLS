﻿using Rage;
using System.Windows.Forms;

namespace DLS.Utils
{
    class Settings
    {
        internal static InitializationFile INI = new InitializationFile(@"Plugins\DLS.ini");

        // General
        public static Keys GEN_MODIFIER { get; } = INI.ReadEnum("Keyboard", "Modifier", Keys.Shift);
        public static Keys GEN_LOCKALL { get; } = INI.ReadEnum("Keyboard", "LockAll", Keys.Scroll);
        public static string GEN_DISABLEDS { get; } = INI.ReadString("Keyboard", "Disabled", "80,19,85,86,27,100,164,165");

        // Sirens
        public static Keys SIREN_TOGGLE { get; } = INI.ReadEnum("Keyboard", "SirenToggle", Keys.G);
        public static Keys SIREN_TONE1 { get; } = INI.ReadEnum("Keyboard", "Tone1", Keys.D1);
        public static Keys SIREN_TONE2 { get; } = INI.ReadEnum("Keyboard", "Tone2", Keys.D2);
        public static Keys SIREN_TONE3 { get; } = INI.ReadEnum("Keyboard", "Tone3", Keys.D3);
        public static Keys SIREN_TONE4 { get; } = INI.ReadEnum("Keyboard", "Tone4", Keys.D4);
        //public static Keys SIREN_SCAN { get; } = INI.ReadEnum("Keyboard", "Scan", Keys.D5);
        public static Keys SIREN_AUX { get; } = INI.ReadEnum("Keyboard", "AuxToggle", Keys.D6);
        public static Keys SIREN_HORN { get; } = INI.ReadEnum("Keyboard", "Horn", Keys.Y);
        public static Keys SIREN_MAN { get; } = INI.ReadEnum("Keyboard", "Manual", Keys.T);

        // Lights
        public static Keys LIGHT_TOGGLE { get; } = INI.ReadEnum("Keyboard", "LightStage", Keys.J);
        public static Keys LIGHT_TADVISOR { get; } = INI.ReadEnum("Keyboard", "TAdvisor", Keys.K);
        public static Keys LIGHT_SBURN { get; } = INI.ReadEnum("Keyboard", "SteadyBurn", Keys.OemOpenBrackets);
        public static Keys LIGHT_INTLT { get; } = INI.ReadEnum("Keyboard", "InteriorLT", Keys.OemCloseBrackets);
        public static Keys LIGHT_INDL { get; } = INI.ReadEnum("Keyboard", "IndL", Keys.OemMinus);
        public static Keys LIGHT_INDR { get; } = INI.ReadEnum("Keyboard", "IndR", Keys.Oemplus);
        public static Keys LIGHT_HAZRD { get; } = INI.ReadEnum("Keyboard", "Hazard", Keys.Back);

        // Settings
        public static bool SET_SCNDLS { get; } = INI.ReadBoolean("Settings", "SirenControlNonDLS", true);
        public static bool SET_AILC { get; } = INI.ReadBoolean("Settings", "AILightsControl", true);
        public static bool SET_INDENABLED { get; } = INI.ReadBoolean("Settings", "IndEnabled", true);
        public static bool SET_BRAKELIGHT { get; } = INI.ReadBoolean("Settings", "BrakeLightsEnabled", true);
        public static bool SET_UIENABLED { get; } = INI.ReadBoolean("Settings", "UIEnabled", true);
        public static bool SET_SIRENKILL { get; } = INI.ReadBoolean("Settings", "SirenKill", true);
        public static bool SET_LOGTOCONSOLE { get; } = INI.ReadBoolean("Settings", "LogToConsole", false);
        public static bool SET_PATCHEXTRAS { get; } = INI.ReadBoolean("Settings", "PatchExtras", true);
        public static string SET_AUDIONAME { get; } = INI.ReadString("Settings", "AudioName", "TOGGLE_ON");
        public static string SET_AUDIOREF { get; } = INI.ReadString("Settings", "AudioRef", "HUD_FRONTEND_DEFAULT_SOUNDSET");

        // UI
        public static Keys UI_TOGGLE { get; } = INI.ReadEnum("Keyboard", "UIKey", Keys.E);
        public static int UI_WIDTH { get; } = INI.ReadInt32("UI", "Width", 550);
        public static int UI_HEIGHT { get; } = INI.ReadInt32("UI", "Height", 220);
        public static int UI_OFFSETX { get; } = INI.ReadInt32("UI", "OffsetX", 1920);
        public static int UI_OFFSETY { get; } = INI.ReadInt32("UI", "OffsetY", 1080);


        /* 
         *  V1.4.1.1 EUROPEAN
         *  */
        public static Keys LP { get; } = INI.ReadEnum("Keyboard", "RedLights", Keys.Back);
        public static Keys BOOT { get; } = INI.ReadEnum("Keyboard", "boot", Keys.Back);
        public static Keys DELANTE { get; } = INI.ReadEnum("Keyboard", "DELANTE", Keys.NumPad8);
        public static Keys DETRAS { get; } = INI.ReadEnum("Keyboard", "DETRAS", Keys.NumPad5);
        public static Keys whiteLaterales { get; } = INI.ReadEnum("Keyboard", "whiteLaterales", Keys.NumPad3);
        public static Keys DESTELLO { get; } = INI.ReadEnum("Keyboard", "WhiteLights", Keys.Back);
        /* 
         *  V1.4.1.1 END
         *  */



        internal static void IniCheck()
        {
            if (INI.Exists())
            {
                "Loaded: DLS.ini".ToLog();
                return;
            }
            "ERROR: DLS.ini was not found!".ToLog();
        }
    }
}