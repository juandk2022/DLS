using Rage;
using Rage.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLS.Utils
{
    class Lights
    {
        public static void Update(ActiveVehicle activeVeh)
        {
            switch (activeVeh.LightStage)
            {
                case LightStage.Off:
                    activeVeh.Vehicle.IsSirenOn = false;
                    activeVeh.SirenStage = SirenStage.Off;
                    if (activeVeh.AuxOn)
                    {
                        Sound.ClearTempSoundID(activeVeh.AuxID);
                        activeVeh.AuxOn = false;
                    }                    
                    activeVeh.TAStage = TAStage.Off;
                    activeVeh.SBOn = false;
                    //activeVeh.IsScanOn = false;
                    activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                    Sirens.Update(activeVeh);
                    UpdateTA(false, activeVeh);
                    UpdateSB(activeVeh);
                    UpdateExtras(activeVeh);
                    break;
                case LightStage.One:
                    activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                    activeVeh.Vehicle.IsSirenOn = true;
                    activeVeh.Vehicle.IsSirenSilent = true;
                    activeVeh.Vehicle.ShouldVehiclesYieldToThisVehicle = activeVeh.Vehicle.GetDLS().SpecialModes.LSAIYield.Stage1Yield.ToBoolean();
                    UpdateTA(false, activeVeh);
                    UpdateSB(activeVeh);
                    UpdateExtras(activeVeh);
                    break;
                case LightStage.Two:
                    activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                    activeVeh.Vehicle.IsSirenOn = true;
                    activeVeh.Vehicle.IsSirenSilent = true;
                    activeVeh.Vehicle.ShouldVehiclesYieldToThisVehicle = activeVeh.Vehicle.GetDLS().SpecialModes.LSAIYield.Stage2Yield.ToBoolean();
                    UpdateTA(false, activeVeh);
                    UpdateSB(activeVeh);
                    UpdateExtras(activeVeh);
                    break;
                case LightStage.Three:
                    activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                    activeVeh.Vehicle.IsSirenOn = true;
                    activeVeh.Vehicle.IsSirenSilent = true;
                    activeVeh.Vehicle.ShouldVehiclesYieldToThisVehicle = activeVeh.Vehicle.GetDLS().SpecialModes.LSAIYield.Stage3Yield.ToBoolean();
                    UpdateTA(false, activeVeh);
                    UpdateSB(activeVeh);
                    UpdateExtras(activeVeh);
                    break;
                case LightStage.CustomOne:
                    activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                    activeVeh.Vehicle.IsSirenOn = true;
                    activeVeh.Vehicle.IsSirenSilent = true;
                    activeVeh.Vehicle.ShouldVehiclesYieldToThisVehicle = activeVeh.Vehicle.GetDLS().SpecialModes.LSAIYield.Custom1Yield.ToBoolean();
                    UpdateTA(false, activeVeh);
                    UpdateSB(activeVeh);
                    UpdateExtras(activeVeh);
                    break;
                case LightStage.CustomTwo:
                    activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                    activeVeh.Vehicle.IsSirenOn = true;
                    activeVeh.Vehicle.IsSirenSilent = true;
                    activeVeh.Vehicle.ShouldVehiclesYieldToThisVehicle = activeVeh.Vehicle.GetDLS().SpecialModes.LSAIYield.Custom2Yield.ToBoolean();
                    UpdateTA(false, activeVeh);
                    UpdateSB(activeVeh);
                    UpdateExtras(activeVeh);
                    break;
                /* 
                 *  V1.4.1.1 EUROPEAN
                 *  */
                case LightStage.CustomThree:
                    activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                    activeVeh.Vehicle.IsSirenOn = true;
                    activeVeh.Vehicle.IsSirenSilent = true;
                    activeVeh.Vehicle.ShouldVehiclesYieldToThisVehicle = activeVeh.Vehicle.GetDLS().SpecialModes.LSAIYield.Custom2Yield.ToBoolean();
                    UpdateTA(false, activeVeh);
                    UpdateSB(activeVeh);
                    UpdateExtras(activeVeh);
                    break;
                /* 
                 *  V1.4.1.1 END
                 *  */

                default:
                    break;
            }
        }

        public static void UpdateTA(bool taCalled, ActiveVehicle activeVeh)
        {
            DLSModel dlsModel = activeVeh.Vehicle.GetDLS();
            if (dlsModel.TrafficAdvisory.Type != "off")
            {
                if (!taCalled)
                {
                    List<LightStage> enableStages = new List<LightStage>();
                    List<LightStage> disableStages = new List<LightStage>();
                    if (dlsModel.TrafficAdvisory.AutoEnableStages != "")
                    {
                        foreach (int i in dlsModel.TrafficAdvisory.AutoEnableStages.Split(',').Select(n => int.Parse(n)).ToList())
                            enableStages.Add((LightStage)i);
                    }
                    if (dlsModel.TrafficAdvisory.AutoDisableStages != "")
                    {
                        foreach (int i in dlsModel.TrafficAdvisory.AutoDisableStages.Split(',').Select(n => int.Parse(n)).ToList())
                            disableStages.Add((LightStage)i);
                    }
                    if (enableStages.Contains(activeVeh.LightStage))
                    {
                        if (activeVeh.TAStage == TAStage.Off)
                        {
                            switch (dlsModel.TrafficAdvisory.DefaultEnabledDirection.ToLower())
                            {
                                case "left":
                                    activeVeh.TAStage = TAStage.Left;
                                    break;
                                case "diverge":
                                    activeVeh.TAStage = TAStage.Diverge;
                                    break;
                                case "right":
                                    activeVeh.TAStage = TAStage.Right;
                                    break;
                                case "warn":
                                    activeVeh.TAStage = TAStage.Warn;
                                    break;
                            }
                        }
                    }
                    if (disableStages.Contains(activeVeh.LightStage))
                        activeVeh.TAStage = TAStage.Off;
                }
                if (dlsModel.TrafficAdvisory.DivergeOnly.ToBoolean())
                {
                    if (activeVeh.TAStage != TAStage.Off)
                    {
                        switch (dlsModel.TrafficAdvisory.Type)
                        {
                            case "three":
                                foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00110000110000110000110000110000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "11000011000011000011000011000011";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00110000110000110000110000110000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                break;
                            case "four":
                                foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00110000110000110000110000110000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "11000011000011000011000011000000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;

                                }
                                foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "11000011000011000011000011000000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00110000110000110000110000110000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                break;
                            case "five":
                                foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00001100000011000000110000001100";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00110000001100000011000000110000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "11000000110000001100000011000000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00110000001100000011000000110000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00001100000011000000110000001100";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                break;
                            case "six":
                                foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00001100000011000000110000001100";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00110000001100000011000000110000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "11000000110000001100000011000000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "11000000110000001100000011000000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00110000001100000011000000110000";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                {
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00001100000011000000110000001100";
                                    activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (taCalled)
                        {
                            switch (dlsModel.TrafficAdvisory.Type)
                            {
                                case "three":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "four":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;

                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "five":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "six":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                            }
                        }
                        else
                            return;
                    }
                }
                else
                {
                    switch (activeVeh.TAStage)
                    {
                        case TAStage.Left:
                            switch (dlsModel.TrafficAdvisory.Type)
                            {
                                case "three":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Left.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Left.C;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Left.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "four":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Left.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Left.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Left.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Left.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "five":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Left.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Left.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Left.C;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Left.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Left.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "six":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Left.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;

                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Left.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;

                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Left.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;

                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Left.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;

                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Left.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;

                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Left.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;

                                    }
                                    break;
                                /* 
                                 *  V1.4.1.1 EUROPEAN
                                 *  */
                                case "seven":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Left.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Left.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Left.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Left.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Left.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Left.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Left.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                case "eight":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Left.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Left.RR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Left.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Left.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Left.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Left.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Left.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Left.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                case "nine":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Left.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Left.RR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cc.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Left.CC;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Left.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Left.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Left.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Left.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Left.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Left.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                    /* 
                                     *  V1.4.1.1 END
                                     *  */

                            }
                            break;
                        case TAStage.Diverge:
                            switch (dlsModel.TrafficAdvisory.Type)
                            {
                                case "three":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Diverge.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Diverge.C;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Diverge.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "four":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Diverge.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Diverge.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Diverge.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Diverge.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "five":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Diverge.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Diverge.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Diverge.C;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Diverge.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Diverge.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "six":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Diverge.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Diverge.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Diverge.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Diverge.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Diverge.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Diverge.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                /* 
                                 *  V1.4.1.1 EUROPEAN
                                 *  */
                                case "seven":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Diverge.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Diverge.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Diverge.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Diverge.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Diverge.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Diverge.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Diverge.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                case "eight":
                                    foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Diverge.RR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Diverge.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Diverge.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Diverge.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Diverge.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Diverge.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Diverge.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Diverge.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                case "nine":
                                    foreach (int i in dlsModel.TrafficAdvisory.cc.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Diverge.CC;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Diverge.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Diverge.RR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Diverge.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Diverge.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Diverge.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Diverge.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Diverge.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Diverge.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                    /* 
                                     *  V1.4.1.1 END
                                     *  */

                            }
                            break;
                        case TAStage.Right:
                            switch (dlsModel.TrafficAdvisory.Type)
                            {
                                case "three":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Right.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Right.C;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Right.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "four":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Right.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Right.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Right.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Right.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "five":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Right.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Right.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Right.C;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Right.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Right.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "six":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Right.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Right.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Right.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Right.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Right.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Right.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                /* 
                                 *  V1.4.1.1 EUROPEAN
                                 *  */
                                case "seven":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Right.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Right.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Right.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Right.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Right.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Right.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Right.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                case "eight":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Right.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Right.RR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Right.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Right.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Right.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Right.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Right.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Right.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                case "nine":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Right.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Right.RR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cc.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Right.CC;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Right.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Right.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Right.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Right.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Right.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Right.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                    /* 
                                     *  V1.4.1.1 END
                                     *  */

                            }
                            break;
                        case TAStage.Warn:
                            switch (dlsModel.TrafficAdvisory.Type)
                            {
                                case "three":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Warn.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Warn.C;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Three.Warn.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "four":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Warn.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Warn.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Warn.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Four.Warn.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "five":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Warn.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Warn.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Warn.C;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Warn.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Five.Warn.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                case "six":
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Warn.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Warn.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Warn.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Warn.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Warn.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].Six.Warn.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                    }
                                    break;
                                /* 
                                 *  V1.4.1.1 EUROPEAN
                                 *  */
                                case "seven":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Warn.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Warn.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Warn.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Warn.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Warn.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Warn.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].seven.Warn.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                case "eight":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Warn.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Warn.RR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Warn.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Warn.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Warn.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Warn.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Warn.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].eight.Warn.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                case "nine":
                                    foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Warn.LL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Warn.RR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cc.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Warn.CC;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Warn.L;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Warn.EL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Warn.CL;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Warn.CR;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Warn.ER;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                    {
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = activeVeh.TAgroup.TaPatterns[activeVeh.TApatternCurrentIndex].nine.Warn.R;
                                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                    }
                                    break;
                                    /* 
                                     *  V1.4.1.1 END
                                     *  */

                            }
                            break;
                        case TAStage.Off:
                            if (taCalled)
                            {
                                switch (dlsModel.TrafficAdvisory.Type)
                                {
                                    case "three":
                                        foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        break;
                                    case "four":
                                        foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;

                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        break;
                                    case "five":
                                        foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.c.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        break;
                                    case "six":
                                        foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                            if (dlsModel.TrafficAdvisory?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.TrafficAdvisory.Color.ColorValue;
                                        }
                                        break;
                                    /* 
                                     *  V1.4.1.1 EUROPEAN
                                     *  */
                                    case "seven":
                                        foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        break;
                                    case "eight":
                                        foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        break;
                                    case "nine":
                                        foreach (int i in dlsModel.TrafficAdvisory.ll.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.rr.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cc.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.l.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.el.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cl.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.cr.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.er.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        foreach (int i in dlsModel.TrafficAdvisory.r.Split(',').Select(n => int.Parse(n)).ToList())
                                        {
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = "00000000000000000000000000000000";
                                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                                        }
                                        break;
                                        /* 
                                         *  V1.4.1.1 END
                                         *  */

                                }
                            }
                            else
                                return;
                            if (!activeVeh.SBOn)
                                activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                            else
                            {
                                activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                                UpdateSB(activeVeh);
                            }
                            break;
                    }
                }
            }

            if (taCalled)
            {
                UpdateExtras(activeVeh);
            }
        }

        /* 
         *  V1.4.1.1 EUROPEAN
         *  */
        public static void UpdateLP(ActiveVehicle activeVeh)
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;

            coche.GetDLS();
            DLSModel vehDLS;
            vehDLS = coche.GetDLS();

            string codigo = "";
            codigo = vehDLS.SpecialModes.code.ToString();

            if (codigo == null || codigo == "")
            {
                codigo = "";
            }
            switch (activeVeh.LPStatus)
            {
                case LPStatus.Off:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 5, true);
                    break;
                case LPStatus.On:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 5, false);
                    break;
                default:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 5, true);
                    break;
            }
        }

        public static void UpdateDELANTE(ActiveVehicle activeVeh)
        {
            DLSModel dlsModel = activeVeh.Vehicle.GetDLS();
            int front = dlsModel.light_site.L_FRONT.ToInt32();

            switch (activeVeh.Delante)
            {
                case Delante.Off:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, front, true);
                    break;
                case Delante.On:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, front, false);
                    break;
                default:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, front, true);
                    break;
            }
        }

        public static void UpdateDETRAS(ActiveVehicle activeVeh)
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            DLSModel dlsModel = activeVeh.Vehicle.GetDLS();
            int rear = dlsModel.light_site.L_REAR.ToInt32();
            switch (activeVeh.Detras)
            {
                case Detras.Off:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, rear, true);
                    break;
                case Detras.On:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, rear, false);
                    break;
                default:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, rear, true);
                    break;
            }
        }

        public static void updateboot(ActiveVehicle activeVeh)
        {
            DLSModel dlsModel = activeVeh.Vehicle.GetDLS();

            if (dlsModel.SpecialModes.EnablePanelMsgBoard.ToBoolean())
            {
                switch (activeVeh.boot)
                {
                    case boot.open:
                        Vehicle coche2 = Game.LocalPlayer.Character.CurrentVehicle;
                        //Sleep(1000); = 1 segundo
                        System.Media.SoundPlayer panelup = new System.Media.SoundPlayer("plugins/DLS/audio/panel.wav");
                        System.Media.SoundPlayer panelupCamuflado = new System.Media.SoundPlayer("plugins/DLS/audio/panelc.wav");
                        string codigo = "";
                        codigo = dlsModel.SpecialModes.code.ToString();
                        if (codigo == "CD12")
                        {
                            int indez = 5;
                            int speed = 100;
                            float rot1 = 0f;

                            panelupCamuflado.Play();
                            for (float i = 0f; i <= 1f; i += 0.035f)
                            {
                                NativeFunction.Natives.SetVehicleDoorControl(coche2, indez, speed, rot1 + i);
                                GameFiber.Sleep(35);
                            }
                        }
                        else
                        {
                            int indez = 5;
                            int speed = 1;
                            float rot1 = 0f;

                            panelup.Play();
                            panelupCamuflado.Play();
                            for (float i = 0f; i <= 1f; i += 0.01f)
                            {
                                NativeFunction.Natives.SetVehicleDoorControl(coche2, indez, speed, rot1 + i);
                                GameFiber.Sleep(35);
                            }
                        }

                        break;
                    case boot.close:
                        System.Media.SoundPlayer paneldown = new System.Media.SoundPlayer("plugins/DLS/audio/paneld.wav");
                        System.Media.SoundPlayer paneldowCamuflado = new System.Media.SoundPlayer("plugins/DLS/audio/paneldc.wav");
                        Vehicle coche3 = Game.LocalPlayer.Character.CurrentVehicle;
                        //0.029999999f


                        string codigo2 = "";
                        codigo2 = dlsModel.SpecialModes.code.ToString();
                        if (codigo2 == "CD12")
                        {
                            int indez2 = 5;
                            int speed2 = 100;
                            float rota2 = 1f;
                            paneldowCamuflado.Play();

                            //float i = 1f; i >= 0f; i -= 0.035f
                            for (float i = 0f; i >= -1f; i -= 0.028f)
                            {
                                NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 + i);
                                GameFiber.Sleep(35);
                            }
                        }
                        else
                        {
                            int indez2 = 5;
                            int speed2 = 1;
                            float rota2 = 1f;
                            paneldown.Play();
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.01f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.02f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.03f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.04f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.05f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.06f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.07f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.08f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.09f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.1f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.11f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.12f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.13f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.14f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.15f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.16f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.17f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.18f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.19f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.2f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.21f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.22f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.23f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.24f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.25f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.26f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.27f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.28f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.29f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.3f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.31f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.32f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.33f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.34f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.35f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.36f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.37f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.38f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.39f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.4f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.41f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.42f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.43f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.44f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.45f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.46f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.47f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.48f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.49f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.5f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.51f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.52f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.53f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.54f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.55f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.56f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.57f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.58f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.59f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.6f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.61f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.62f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.63f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.64f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.65f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.66f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.67f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.68f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.69f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.7f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.71f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.72f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.73f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.74f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.75f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.76f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.77f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.78f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.79f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.8f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.81f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.82f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.83f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.84f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.85f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.86f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.87f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.88f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.89f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.9f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.91f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.92f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.93f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.94f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.95f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.96f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.97f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.98f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 0.99f); GameFiber.Sleep(35);
                            NativeFunction.Natives.SetVehicleDoorControl(coche3, indez2, speed2, rota2 - 1f); GameFiber.Sleep(35);
                        }
                        break;
                    default:
                        int indez3 = 5;
                        int speed3 = 1;
                        float rota3 = 1f;
                        Vehicle coche4 = Game.LocalPlayer.Character.CurrentVehicle;
                        NativeFunction.Natives.SetVehicleDoorControl(coche4, indez3, speed3, rota3 - 1f);

                        break;
                }

            }
            else
            {
                return;
            }

        }

        public static void updateNaranja(ActiveVehicle activeVeh)
        {
            DLSModel dlsModel = activeVeh.Vehicle.GetDLS();
            int right = dlsModel.SpecialModes.ambar.AL.ToInt32();
            int left = dlsModel.SpecialModes.ambar.AR.ToInt32();

            if (dlsModel.SpecialModes.ambar.enable.ToBoolean())
            {
                switch (activeVeh.elAmbar)
                {
                    case elAmbar.On:
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[left - 1].FlashinessSequence = "11111111000000001111111100000000";
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[right - 1].FlashinessSequence = "00000000111111110000000011111111";
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[left - 1].Flash = true;
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[right - 1].Flash = true;
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 12, false);
                        break;
                    case elAmbar.Off:
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[left - 1].FlashinessSequence = "00000000000000000000000000000000";
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[right - 1].FlashinessSequence = "00000000000000000000000000000000";
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[left - 1].Flash = true;
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[right - 1].Flash = true;
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 12, true);
                        break;
                }

            }
            else
            {
                return;
            }

        }

        public static void UpdateWhiteLaterales(ActiveVehicle activeVeh)
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            coche.GetDLS();
            DLSModel vehDLS;
            vehDLS = coche.GetDLS();
            string DLScode = "";


            //HABILITAR CODIGO
            bool CodeEnabler = false;
            CodeEnabler = vehDLS.SpecialModes.codeEnabler.ToBoolean();

            //RECOGE CODIGO
            string codigo = "";
            codigo = vehDLS.SpecialModes.code.ToString();

            //EXTRAS HABILITADOS?
            bool ext2E = vehDLS.special_lights.tkdExEn.ToBoolean();
            bool tkd6E = vehDLS.special_lights.tkd_ltExEn.ToBoolean();

            //GET EXTRAS



            switch (activeVeh.whiteLaterales)
            {
                case whiteLaterales.off:
                    if (tkd6E)
                    {
                        if (codigo == "CD1") {
                            coche.EmergencyLightingOverride.Lights[12 - 1].FlashinessSequence = "00000000000000000000000000000000";
                            coche.EmergencyLightingOverride.Lights[13 - 1].FlashinessSequence = "00000000000000000000000000000000";
                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[12 - 1].Flash = false;
                            activeVeh.Vehicle.EmergencyLightingOverride.Lights[13 - 1].Flash = false;
                        }
                       

                        int extra6 = vehDLS.special_lights.tkdLatExtra.ToInt32();
                        int extra4 = vehDLS.special_lights.tkdLatExtra2.ToInt32();
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra6, true);
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra4, true);
                    }
                    break;

                case whiteLaterales.izq:
                    if (codigo == "CD1") {
                        coche.EmergencyLightingOverride.Lights[12 - 1].FlashinessSequence = "11111111111111111111111111111111";
                        coche.EmergencyLightingOverride.Lights[13 - 1].FlashinessSequence = "00000000000000000000000000000000";
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[12 - 1].Flash = true;
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[13 - 1].Flash = false;
                    }
                    if (tkd6E)
                    {
                        int extra6 = vehDLS.special_lights.tkdLatExtra.ToInt32();
                        int extra4 = vehDLS.special_lights.tkdLatExtra2.ToInt32();
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra6, false);
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra4, true);
                    }
                    break;

                case whiteLaterales.der:
                    if (codigo == "CD1")
                    {
                        coche.EmergencyLightingOverride.Lights[12 - 1].FlashinessSequence = "00000000000000000000000000000000";
                        coche.EmergencyLightingOverride.Lights[13 - 1].FlashinessSequence = "11111111111111111111111111111111";
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[12 - 1].Flash = false;
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[13 - 1].Flash = true;
                    }
                    if (tkd6E)
                    {
                        int extra6 = vehDLS.special_lights.tkdLatExtra.ToInt32();
                        int extra4 = vehDLS.special_lights.tkdLatExtra2.ToInt32();
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra6, true);
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra4, false);
                    }
                    break;

                case whiteLaterales.both:
                    if (codigo == "CD1")
                    {
                        coche.EmergencyLightingOverride.Lights[12 - 1].FlashinessSequence = "11111111111111111111111111111111"; 
                        coche.EmergencyLightingOverride.Lights[13 - 1].FlashinessSequence = "11111111111111111111111111111111";
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[12 - 1].Flash = true;
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[13 - 1].Flash = true;
                    }
                    if (tkd6E)
                    {
                        int extra6 = vehDLS.special_lights.tkdLatExtra.ToInt32();
                        int extra4 = vehDLS.special_lights.tkdLatExtra2.ToInt32();
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra6, false);
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra4, false);
                    }
                    break;
                default:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 6, true);
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 2, true);
                    break;
            }
        }

        public static void UpdateWhites(ActiveVehicle activeVeh)
        {

            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            coche.GetDLS();
            DLSModel vehDLS;
            vehDLS = coche.GetDLS();
            string DLScode = "";
            bool destello1 = false;
            destello1 = vehDLS.SpecialModes.codeEnabler.ToBoolean();

            string codigo = "";
            codigo = vehDLS.SpecialModes.code.ToString();
            switch (activeVeh.WhiteStatus)
            {
                case WhiteStatus.Off:
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 2, true);
                    if (destello1)
                    {
                        if (codigo == "CD11")
                        {
                            coche.EmergencyLightingOverride.Lights[9 - 1].FlashinessSequence = "00000000000000000000000000000000"; coche.EmergencyLightingOverride.Lights[9 - 1].Flash = true;
                        }
                        else if (codigo == "CD1")
                        {
                            coche.EmergencyLightingOverride.Lights[11 - 1].FlashinessSequence = "00000000000000000000000000000000"; coche.EmergencyLightingOverride.Lights[11 - 1].Flash = true;
                        }
                    }
                    break;
                case WhiteStatus.Static:
                    if (destello1)
                    {
                        if (codigo == "CD0" || codigo == "CD2" || codigo == "CD3" || codigo == "CD5" || codigo == "CD8" || codigo == "CD9" || codigo == "CD11")
                        {
                            coche.EmergencyLightingOverride.Lights[9 - 1].FlashinessSequence = "11111111111111111111111111111111"; coche.EmergencyLightingOverride.Lights[9 - 1].Flash = true;
                        } else if (codigo == "CD1") {
                            coche.EmergencyLightingOverride.Lights[11 - 1].FlashinessSequence = "11111111111111111111111111111111"; coche.EmergencyLightingOverride.Lights[11 - 1].Flash = true;
                        }

                    }
                    //NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 6, true);
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 2, false);

                    break;
                case WhiteStatus.On:

                    if (destello1)
                    {

                        if (codigo == "CD10")
                        {
                            coche.EmergencyLightingOverride.Lights[5 - 1].FlashinessSequence = "11111111000000001111111100000000"; coche.EmergencyLightingOverride.Lights[5 - 1].Flash = true;
                            coche.EmergencyLightingOverride.Lights[6 - 1].FlashinessSequence = "00000000111111110000000011111111"; coche.EmergencyLightingOverride.Lights[6 - 1].Flash = true;
                        }
                        else if (codigo == "CD0" || codigo == "CD2" || codigo == "CD3" || codigo == "CD5" || codigo == "CD8" || codigo == "CD9" || codigo == "CD11")
                        {
                            coche.EmergencyLightingOverride.Lights[9 - 1].FlashinessSequence = "11111111000000001111111100000000"; coche.EmergencyLightingOverride.Lights[9 - 1].Flash = true;
                        }
                        else if (codigo == "CD4") {
                            DLSModel dlsModel = activeVeh.Vehicle.GetDLS();

                            int extra_tkdl = vehDLS.special_lights.tkdExtra.ToInt32();
                            GameFiber.StartNew(delegate {
                                while (DLS.Threads.PlayerController.whiteisOn)
                                {

                                    Game.LogTrivial("DECLARANDO DATO");
                                    string dato = dlsModel.SpecialModes.WhitePT1;
                                    string[] subs = dato.Split(',');


                                    Game.LogTrivial("DATO DECLARADO: " + subs);
                                    foreach (string sub in subs)
                                    {

                                        if (sub.Contains('1'))
                                        {

                                            NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra_tkdl, false);

                                         

                                            GameFiber.Sleep(200);
                                        }
                                        else
                                        {
                                            NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, extra_tkdl, true);
                                            GameFiber.Sleep(200);
                                        }

                                    }
                                }
                            });
                            
                        } else if (codigo == "CD1") {
                            coche.EmergencyLightingOverride.Lights[11 - 1].FlashinessSequence = "00000000111111110000000011111111"; coche.EmergencyLightingOverride.Lights[11 - 1].Flash = true;
                        }
                    }

                    //NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 6, true);
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 2, true);
                    break;
                default:
                    //NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 6, true);
                    NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 2, true);
                    break;
            }
        }
        /* 
         *  V1.4.1.1 END
         *  */

        public static void UpdateSB(ActiveVehicle activeVeh)
        {
            DLSModel dlsModel = activeVeh.Vehicle.GetDLS();
            if (dlsModel.SpecialModes.SteadyBurn.SteadyBurnEnabled.ToBoolean())
            {
                List<int> ssb = dlsModel.SpecialModes.SteadyBurn.Sirens.Replace(" ", "").Split(',').Select(n => int.Parse(n)).ToList();
                if (activeVeh.SBOn)
                {
                    foreach (int i in ssb)
                    {
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].FlashinessSequence = dlsModel.SpecialModes.SteadyBurn.Pattern;
                        activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Flash = true;
                        if (dlsModel.SpecialModes.SteadyBurn?.Color?.ColorValue != null) activeVeh.Vehicle.EmergencyLightingOverride.Lights[i - 1].Color = dlsModel.SpecialModes.SteadyBurn.Color.ColorValue;
                    }
                    bool extra1crucero = dlsModel.SpecialModes.SteadyBurn.Extra1.ToBoolean();
                    if (extra1crucero) {
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 1, false);
                    }
                    
                }
                else
                {
                    activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                    if (activeVeh.TAStage != TAStage.Off)
                    {
                        UpdateTA(true, activeVeh);
                    }
                    bool extra1crucero = dlsModel.SpecialModes.SteadyBurn.Extra1.ToBoolean();
                    if (extra1crucero)
                    {
                        NativeFunction.Natives.SET_VEHICLE_EXTRA(activeVeh.Vehicle, 1, true);
                    }
                }
            }
        }

        public static void ResetExtras(ActiveVehicle activeVeh)
        {
            DLSModel dlsModel = activeVeh.Vehicle.GetDLS();
            if (dlsModel.StageExtras != null && dlsModel.StageExtras.OffExtras != null)
            {
                foreach (var extra in dlsModel.StageExtras.OffExtras)
                {
                    if (activeVeh.Vehicle.HasExtra(extra.ID))
                    {
                        activeVeh.Vehicle.SetExtra(extra.ID, extra.Enabled);
                    }
                }
            }
        }

        public static void UpdateExtras(ActiveVehicle activeVeh)
        {
            DLSModel dlsModel = activeVeh.Vehicle.GetDLS();
            if (dlsModel.StageExtras != null)
            {
                List<ExtraState> extras = new List<ExtraState>();
                switch (activeVeh.LightStage)
                {
                    case LightStage.One:
                        extras = dlsModel.StageExtras.Stage1Extras.ToList();
                        break;
                    case LightStage.Two:
                        extras = dlsModel.StageExtras.Stage2Extras.ToList();
                        break;
                    case LightStage.Three:
                        extras = dlsModel.StageExtras.Stage3Extras.ToList();
                        break;
                    case LightStage.CustomOne:
                        extras = dlsModel.StageExtras.CustomStage1Extras.ToList();
                        break;
                    case LightStage.CustomTwo:
                        extras = dlsModel.StageExtras.CustomStage2Extras.ToList();
                        break;
                    case LightStage.Off:
                    default:
                        extras = dlsModel.StageExtras.OffExtras.ToList();
                        break;
                }

                if (activeVeh.TAStage != TAStage.Off)
                {
                    extras.AddRange(dlsModel.StageExtras.TAExtras);
                }
                if (activeVeh.SBOn)
                {
                    extras.AddRange(dlsModel.StageExtras.SBExtras);
                }

                if (extras.Count > 0)
                {
                    foreach (var extra in extras)
                    {
                        if (activeVeh.Vehicle.HasExtra(extra.ID))
                        {
                            activeVeh.Vehicle.SetExtra(extra.ID, extra.Enabled);
                        }
                    }
                }
            }
        }

        public static void MoveUpStage(ActiveVehicle activeVeh)
        {
            NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, Settings.SET_AUDIONAME, Settings.SET_AUDIOREF, true);
            activeVeh.LightStage = activeVeh.Vehicle.GetDLS().AvailableLightStages.Next(activeVeh.LightStage);
            Update(activeVeh);
        }

        public static void MoveDownStage(ActiveVehicle activeVeh)
        {
            NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, Settings.SET_AUDIONAME, Settings.SET_AUDIOREF, true);
            activeVeh.LightStage = activeVeh.Vehicle.GetDLS().AvailableLightStages.Previous(activeVeh.LightStage);
            Update(activeVeh);
        }

        public static void MoveUpStageTA(ActiveVehicle activeVeh)
        {
            if (!activeVeh.Vehicle.GetDLS().TrafficAdvisory.DivergeOnly.ToBoolean())
            {
                switch (activeVeh.TAStage)
                {
                    case TAStage.Off:
                        NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                        activeVeh.TAStage = TAStage.Left;
                        if (activeVeh.LightStage == LightStage.Off || activeVeh.LightStage == LightStage.Empty)
                            activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                        UpdateTA(true, activeVeh);
                        break;
                    case TAStage.Left:
                        NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                        activeVeh.TAStage = TAStage.Diverge;
                        if (activeVeh.LightStage == LightStage.Off || activeVeh.LightStage == LightStage.Empty)
                            activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                        UpdateTA(true, activeVeh);
                        break;
                    case TAStage.Diverge:
                        NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                        activeVeh.TAStage = TAStage.Right;
                        if (activeVeh.LightStage == LightStage.Off || activeVeh.LightStage == LightStage.Empty)
                            activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                        UpdateTA(true, activeVeh);
                        break;
                    case TAStage.Right:
                        NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                        activeVeh.TAStage = TAStage.Warn;
                        if (activeVeh.LightStage == LightStage.Off || activeVeh.LightStage == LightStage.Empty)
                            activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                        UpdateTA(true, activeVeh);
                        break;
                    case TAStage.Warn:
                        NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                        activeVeh.TAStage = TAStage.Off;
                        if (activeVeh.LightStage == LightStage.Off || activeVeh.LightStage == LightStage.Empty)
                            activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                        UpdateTA(true, activeVeh);
                        break;
                }
            }
            else
            {
                switch (activeVeh.TAStage)
                {
                    case TAStage.Off:
                        NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                        activeVeh.TAStage = TAStage.Diverge;
                        if (activeVeh.LightStage == LightStage.Off || activeVeh.LightStage == LightStage.Empty)
                            activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                        UpdateTA(true, activeVeh);
                        break;
                    case TAStage.Diverge:
                        NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                        activeVeh.TAStage = TAStage.Off;
                        if (activeVeh.LightStage == LightStage.Off || activeVeh.LightStage == LightStage.Empty)
                            activeVeh.Vehicle.EmergencyLightingOverride = Vehicles.GetEL(activeVeh.Vehicle);
                        UpdateTA(true, activeVeh);
                        break;
                }
            }
        }

        public static void ToggleIntLight(ActiveVehicle activeVeh)
        {
            Vehicle veh = activeVeh.Vehicle;
            veh.IsInteriorLightOn = !veh.GetActiveVehicle().IntLightOn;
            veh.GetActiveVehicle().IntLightOn = !veh.GetActiveVehicle().IntLightOn;
        }

        public static void UpdateIndicator(ActiveVehicle activeVeh)
        {
            switch (activeVeh.IndStatus)
            {
                case IndStatus.Off:
                    NativeFunction.Natives.SET_VEHICLE_INDICATOR_LIGHTS(activeVeh.Vehicle, 0, false);
                    NativeFunction.Natives.SET_VEHICLE_INDICATOR_LIGHTS(activeVeh.Vehicle, 1, false);
                    break;
                case IndStatus.Left:
                    NativeFunction.Natives.SET_VEHICLE_INDICATOR_LIGHTS(activeVeh.Vehicle, 0, false);
                    NativeFunction.Natives.SET_VEHICLE_INDICATOR_LIGHTS(activeVeh.Vehicle, 1, true);
                    break;
                case IndStatus.Right:
                    NativeFunction.Natives.SET_VEHICLE_INDICATOR_LIGHTS(activeVeh.Vehicle, 0, true);
                    NativeFunction.Natives.SET_VEHICLE_INDICATOR_LIGHTS(activeVeh.Vehicle, 1, false);
                    break;
                case IndStatus.Both:
                    NativeFunction.Natives.SET_VEHICLE_INDICATOR_LIGHTS(activeVeh.Vehicle, 0, true);
                    NativeFunction.Natives.SET_VEHICLE_INDICATOR_LIGHTS(activeVeh.Vehicle, 1, true);
                    break;
            }
        }

        public static SirenStatus GetSirenStatus(ActiveVehicle activeVeh, int sirenID, bool includeBroken = true)
        {
            /*Vehicle v = activeVeh.Vehicle;
            string bone = "siren" + sirenID;
            if (v.HasBone(bone) && (includeBroken || v.GetBonePosition(bone).DistanceTo(Vector3.Zero) > 1))
            {
                float length = v.GetBoneOrientation(bone).LengthSquared();
                bool on = Math.Round(length, 2) != Math.Round(activeVeh.InitialLengths[sirenID - 1], 2);
                if (on)
                    return SirenStatus.On;
                else
                    return SirenStatus.Off;
            }
            else
                return SirenStatus.None;*/
            return SirenStatus.None;
        }
    
    
        
    
    }
}
