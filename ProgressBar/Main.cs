using System.Collections.Generic;
using FlowerLib.Function;
using FlowerLib.Patch;
using UnityEngine;
using UnityModManagerNet;

namespace ProgressBar
{
    public static class Main
    {
        public static bool IsEnabled { get; private set; }
        public static Setting setting;
        public static Dictionary<string, Language> languages = new Dictionary<string, Language>()
        {
            {"Korean", new Korean()},
            {"English", new English()}
        };

        public static Language language = new English();
        
        internal static void Setup(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
            setting = new Setting();
            setting = UnityModManager.ModSettings.Load<Setting>(modEntry);
        }
        
        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            IsEnabled = value;
            
            if (value)
            {
                modEntry.OnGUI = OnGUI;
                modEntry.OnSaveGUI = OnSaveGUI;
                Start(modEntry);
            }
            else
            { 
                Patch.easyCanvas.DestroyAll();
                Stop(modEntry);
            }
            return true;
        }

        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            setting.Save(modEntry);
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUIStyle guiStyle = new GUIStyle(GUI.skin.box);
            guiStyle.normal.background = Misc.makeTex(1, 1, new Color32((byte)setting.R,(byte)setting.G,(byte)setting.B,255));
            GUILayout.Box("",guiStyle);
            
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Red ({setting.R}) - ");
            setting.R = (int)GUILayout.HorizontalSlider(setting.R, 0f, 255f,GUILayout.Width(400));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Green ({setting.G}) - ");
            setting.G = (int)GUILayout.HorizontalSlider(setting.G, 0f, 255f,GUILayout.Width(400));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Blue ({setting.B}) - ");
            setting.B = (int)GUILayout.HorizontalSlider(setting.B, 0f, 255f,GUILayout.Width(400));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.Label(" ");
            
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{language.x} ({setting.x/100}) - ");
            setting.x = (int)GUILayout.HorizontalSlider(setting.x, -20f, 120f,GUILayout.Width(400));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{language.y} ({setting.y/100}) - ");
            setting.y = (int)GUILayout.HorizontalSlider(setting.y, -20f, 120f,GUILayout.Width(400));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{language.width} ({setting.size/100}) - ");
            setting.size = (int)GUILayout.HorizontalSlider(setting.size, 1f, 100f,GUILayout.Width(400));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{language.height} ({setting.height}) - ");
            setting.height = (int)GUILayout.HorizontalSlider(setting.height, 1f, 50f,GUILayout.Width(400));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            /*
            if (newB != setting.B || newR != setting.R || newG != setting.G)
            {
                setting.R = newR;
                setting.B = newB;
                setting.G = newG;
            }*/

           
        }
        
        private static void Start(UnityModManager.ModEntry modEntry)
        {
            AdofaiPatch.PatchAll(modEntry);
        }

        private static void Stop(UnityModManager.ModEntry modEntry)
        {
            AdofaiPatch.UnpatchAll(modEntry);
        }
    }
}