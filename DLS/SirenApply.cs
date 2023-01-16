namespace DLS
{
    using System.Drawing;
    using DLS.Utils;
    using Rage;
    internal static class SirenApply
    {
        public static void ApplySirenSettingsToEmergencyLighting(this SirenSetting setting, EmergencyLighting els, bool clearExcessSirens = true)
        {
            els.TimeMultiplier = setting.TimeMultiplier;
            els.LightFalloffMax = setting.LightFalloffMax;
            els.LightFalloffExponent = setting.LightFalloffExponent;
            els.LightInnerConeAngle = setting.LightInnerConeAngle;
            els.LightOuterConeAngle = setting.LightOuterConeAngle;
            els.LightOffset = setting.LightOffset;
            els.TextureHash = setting.TextureHash;
            els.SequencerBpm = setting.SequencerBPM;
            els.UseRealLights = setting.UseRealLights;
            els.LeftHeadLightSequenceRaw = setting.LeftHeadLightSequencer;
            els.LeftHeadLightMultiples = setting.LeftHeadLightMultiples;
            els.RightHeadLightSequenceRaw = setting.RightHeadLightSequencer;
            els.RightHeadLightMultiples = setting.RightHeadLightMultiples;
            els.LeftTailLightSequenceRaw = setting.LeftTailLightSequencer;
            els.LeftTailLightMultiples = setting.LeftTailLightMultiples;
            els.RightTailLightSequenceRaw = setting.RightTailLightSequencer;
            els.RightTailLightMultiples = setting.RightTailLightMultiples;

            for (int i = 0; i < els.Lights.Length; i++)
            {
                EmergencyLight light = els.Lights[i];

                if (i < setting.Sirens.Length)
                {
                    SirenEntry entry = setting.Sirens[i];

                    // Main light settings
                    light.Color = entry.LightColor;
                    light.Intensity = entry.Intensity;
                    light.LightGroup = entry.LightGroup;
                    light.Rotate = entry.Rotate;
                    light.Scale = entry.Scale;
                    light.ScaleFactor = entry.ScaleFactor;
                    light.Flash = entry.Flash;
                    light.SpotLight = entry.SpotLight;
                    light.CastShadows = entry.CastShadows;
                    light.Light = entry.Light;

                    // Corona settings
                    light.CoronaIntensity = entry.Corona.CoronaIntensity;
                    light.CoronaSize = entry.Corona.CoronaSize;
                    light.CoronaPull = entry.Corona.CoronaPull;
                    light.CoronaFaceCamera = entry.Corona.CoronaFaceCamera;

                    // Rotation settings
                    light.RotationDelta = entry.Rotation.DeltaDeg;
                    light.RotationStart = entry.Rotation.StartDeg;
                    light.RotationSpeed = entry.Rotation.Speed;
                    light.RotationSequenceRaw = entry.Rotation.Sequence;
                    light.RotationMultiples = entry.Rotation.Multiples;
                    light.RotationDirection = entry.Rotation.Direction;
                    light.RotationSynchronizeToBpm = entry.Rotation.SyncToBPM;

                    // Flash settings
                    light.FlashinessDelta = entry.Flashiness.DeltaDeg;
                    light.FlashinessStart = entry.Flashiness.StartDeg;
                    light.FlashinessSpeed = entry.Flashiness.Speed;
                    light.FlashinessSequenceRaw = entry.Flashiness.Sequence;
                    light.FlashinessMultiples = entry.Flashiness.Multiples;
                    light.FlashinessDirection = entry.Flashiness.Direction;
                    light.FlashinessSynchronizeToBpm = entry.Flashiness.SyncToBPM;
                }
                else if (clearExcessSirens)
                {
                    // Main light settings
                    light.Color = Color.Black;
                    light.Intensity = 0;
                    light.LightGroup = 0;
                    light.Rotate = false;
                    light.Scale = false;
                    light.ScaleFactor = 0;
                    light.Flash = false;
                    light.SpotLight = false;
                    light.CastShadows = false;
                    light.Light = false;

                    // Corona settings
                    light.CoronaIntensity = 0;
                    light.CoronaSize = 0;
                    light.CoronaPull = 0;
                    light.CoronaFaceCamera = false;

                    // Rotation settings
                    light.RotationDelta = 0;
                    light.RotationStart = 0;
                    light.RotationSpeed = 0;
                    light.RotationSequenceRaw = 0;
                    light.RotationMultiples = 0;
                    light.RotationDirection = false;
                    light.RotationSynchronizeToBpm = false;

                    // Flash settings
                    light.FlashinessDelta = 0;
                    light.FlashinessStart = 0;
                    light.FlashinessSpeed = 0;
                    light.FlashinessSequenceRaw = 0;
                    light.FlashinessMultiples = 0;
                    light.FlashinessDirection = false;
                    light.FlashinessSynchronizeToBpm = false;
                }
            }
        }
    }
}