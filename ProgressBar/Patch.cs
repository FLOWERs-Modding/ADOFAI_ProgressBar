using BlendModes;
using FlowerLib.Function;
using FlowerLib.Gui;
using FlowerLib.Patch;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using RenderMode = UnityEngine.RenderMode;

namespace ProgressBar
{
    public static class Patch
    {

        public static EasyCanvas easyCanvas = new EasyCanvas();
        public static Image bar;
        public static bool first = true;
        
        [AdofaiPatch(
            "Patch.ChangeLanguage",
            "RDString",
            "ChangeLanguage")]
        public static class ChangeLanguage
        {
            public static void Prefix(SystemLanguage language)
            {
                Main.language = Main.languages.ContainsKey(language.ToString()) ? Main.languages[language.ToString()] : Main.languages["English"];
            }
        }
        
        [AdofaiPatch(
            "Patch.Awake",
            "scrController",
            "Awake")]
        internal static class Awake
        {
            public static void Prefix()
            {
                if (first)
                {
                    first = false;
                    Main.language = Main.languages.ContainsKey(RDString.language.ToString()) ? Main.languages[RDString.language.ToString()] : Main.languages["English"];
                }
            }
        }

        [AdofaiPatch(
            "Patch.WipeToBlack",
            "scrUIController",
            "WipeToBlack")]
        public static class WipeToBlack
        {
            public static void Prefix()
            {
                if (!Main.IsEnabled) return;
                if (easyCanvas.ui==null) return;
                easyCanvas.DestroyAll();
            }
        }
        
        [AdofaiPatch(
            "Patch.ResetScene",
            "scnEditor",
            "ResetScene")]
        public static class ResetScene
        {
            public static void Prefix()
            {
                if (!Main.IsEnabled) return;
                if (easyCanvas.ui==null) return;
                easyCanvas.DestroyAll();
            }
        }
        
        [AdofaiPatch(
            "Patch.StartLoadingScene",
            "scrController",
            "StartLoadingScene")]
        public static class StartLoadingScene
        {
            public static void Prefix()
            {
                if (!Main.IsEnabled) return;
                if (easyCanvas.ui==null) return;
                easyCanvas.DestroyAll();
            }
        }
        

        [AdofaiPatch(
            "Patch.Play",
            "CustomLevel",
            "Play")]
        public static class Play
        {
            public static void Prefix()
            {
                if (!Main.IsEnabled) return;
                if (!scrController.instance.isLevelEditor) return;
                if (easyCanvas.ui!=null) easyCanvas.DestroyAll();
                Create();
            }
        }
        
       
        [AdofaiPatch(
            "Patch.Play2",
            "scrCalibrationPlanet",
            "Start")]
        public static class Play2
        {
            public static void Prefix()
            {
                if (!Main.IsEnabled) return;
                if (easyCanvas.ui!=null) easyCanvas.DestroyAll();
            }
        }

        [AdofaiPatch(
            "Patch.ShowText",
            "scrPressToStart",
            "ShowText")]
        internal static class BossLevelStart
        {
            private static void Prefix(scrPressToStart __instance)
            {
                if (!Main.IsEnabled) return;
                if (scrController.instance.isLevelEditor||scrController.instance.customLevel!=null) return;
                if (easyCanvas.ui!=null) easyCanvas.DestroyAll();
                Create();
            }
        }

        [AdofaiPatch(
            "Patch.MoveToNextFloor",
            "scrPlanet",
            "MoveToNextFloor")]
        public static class MoveToNextFloor
        {
            public static void Postfix()
            {
                if (!Main.IsEnabled) return;
                if (bar == null||easyCanvas.ui==null) return;
                if (scnEditor.instance == null)
                {
                    easyCanvas.DestroyAll();
                    return;
                }
                bar.fillAmount = scrController.instance.percentComplete;
            }
        }

        public static void Create()
        {
            if (easyCanvas.ui==null)
            {
                easyCanvas.Create();
                bar = easyCanvas.Add<Image>("bar");
                bar.rectTransform.sizeDelta = new Vector2((int)(Screen.width*Main.setting.size/100),Main.setting.height);
                Sprite sprite = Misc.tex2spr(Misc.makeTex((int)(Screen.width*Main.setting.size/100),Main.setting.height,new Color32((byte)Main.setting.R,(byte)Main.setting.G,(byte)Main.setting.B,255)));
                bar.sprite = sprite;
                bar.raycastTarget = true;
                bar.fillMethod = Image.FillMethod.Horizontal;
                bar.type = Image.Type.Filled;
                
                easyCanvas.SetPosition(Main.setting.x/100,Main.setting.y/100);

            }
            bar.fillAmount = 0;
        }
    }
}