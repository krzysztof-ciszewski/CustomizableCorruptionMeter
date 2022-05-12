using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R2API.Utils;

namespace CustomizableCorruptionMeter
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [R2APISubmoduleDependency(nameof(CommandHelper))]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync)]
    public class CustomizableCorruptionMeter : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "InvisibleMan";
        public const string PluginName = "CustomizableCorruptionMeter";
        public const string PluginVersion = "1.0.2";
        public static ConfigEntry<Vector3> Position { get; set; }
        public static ConfigEntry<bool> DisableCorruptionText { get; set; }
        public static ConfigEntry<float> MeterOpacity { get; set; }

        private static ConfigFile conf = null;

        private GameObject overlayPrefab;
        private GameObject fillRoot;

        public void Awake()
        {
            CommandHelper.AddToConsoleWhenReady();
            conf = Config;
            initializeConfigEntries();
            addHooks();
        }

        private void initializeConfigEntries()
        {
            Position = Config.Bind<Vector3>("Options", "Position", new Vector3(147.3702f, -59.2215f, 1f), "The global position, default value is the center of the screen");
            DisableCorruptionText = Config.Bind<bool>("Options", "DisableCorruptionText", true, "Disables the text with corruption percentage");
            MeterOpacity = Config.Bind<float>("Options", "MeterOpacity", 0.3f, "Transparency value of the meter: 0 is transparent, 1 is full opacity");

            Position.SettingChanged += (sender, args) =>
            {
                setOverlayPosition();
            };

            DisableCorruptionText.SettingChanged += (sender, args) =>
            {
                hideCorruptionText();
            };

            MeterOpacity.SettingChanged += (sender, args) =>
            {
                setMeterOpacity();
            };
        }

        private void addHooks()
        {
            On.RoR2.VoidSurvivorController.OnEnable += (orig, self) =>
            {
                overlayPrefab = self.overlayPrefab;
                setOverlayPosition();
                orig(self);
            };

            On.RoR2.VoidSurvivorController.OnOverlayInstanceAdded += (orig, self, controller, instance) =>
            {
                fillRoot = instance.transform.GetChild(0).gameObject;
                hideCorruptionText();
                setMeterOpacity();
                orig(self, controller, instance);
            };
        }

        private void setOverlayPosition()
        {
            overlayPrefab.transform.position = Position.Value;
        }

        private void hideCorruptionText()
        {
            fillRoot.GetComponentInChildren<TextMeshProUGUI>().transform.parent.gameObject.SetActive(!DisableCorruptionText.Value);
        }

        private void setMeterOpacity()
        {
            var images = fillRoot.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Clamp(MeterOpacity.Value, 0f, 1f));
            }
        }

        //TODO: Fix this
        [RoR2.ConCommand(commandName = "ccm_config_reload", flags = RoR2.ConVarFlags.None, helpText = "Reload the config file of CustomizableCorruptionMeter.")]
        static void CCReloadConfig(RoR2.ConCommandArgs args)
        {
            if (conf == null) { return; }
            conf.Reload();
        }
    }
}
