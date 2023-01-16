using DLS.Threads;
using DLS.Utils;
using Rage;
using System;
using System.IO;
using System.Xml.Serialization;

namespace DLS.UI
{
    class Importer
    {
        public static void GetCustomSprites()
        {
            string path = @"Plugins\DLS\UI\default";
            CustomUI customUI = new CustomUI();
            customUI.Width = UIManager.sizeX;
            customUI.Height = UIManager.sizeY;
            customUI.OffsetX = UIManager.offsetX;
            customUI.OffsetY = UIManager.offsetY;
            if (File.Exists(path + @"\custom.xml"))
            {
                try
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(CustomUI));
                    StreamReader streamReader = new StreamReader(path + @"\custom.xml");
                    customUI = (CustomUI)mySerializer.Deserialize(streamReader);
                    streamReader.Close();
                    customUI.Height = customUI.SHeight.ToInt32();
                    customUI.Width = customUI.SWidth.ToInt32();
                    customUI.OffsetX = customUI.SOffsetX.ToInt32();
                    customUI.OffsetY = customUI.SOffsetY.ToInt32();
                }
                catch (Exception e)
                {
                    ("CUSTOM.xml ERROR: " + e.Message).ToLog();
                    Game.LogTrivial("CUSTOM.xml ERROR: " + e.Message);
                }
            }
            
            if (Directory.Exists(path))
            {
                bool euromode = true;
                if (euromode)
                {
                    foreach (string file in Directory.EnumerateFiles(path, "*.png"))
                    {
                        switch (Path.GetFileNameWithoutExtension(file))
                        {
                            case "red_off":
                                UIManager.red_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "red_on":
                                UIManager.red_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "white_off":
                                UIManager.white_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "white_on":
                                UIManager.white_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "background":
                                UIManager.Background = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "hazard_off":
                                UIManager.Hazard_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "hazard_on":
                                UIManager.Hazard_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "horn_off":
                                UIManager.Horn_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "horn_on":
                                UIManager.Horn_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext1_off":
                                UIManager.Ext1_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext1_on":
                                UIManager.Ext1_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "intlt_off":
                                UIManager.Intlt_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "intlt_on":
                                UIManager.Intlt_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "lind_off":
                                UIManager.Lind_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "lind_on":
                                UIManager.Lind_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "manual_off":
                                UIManager.Manual_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "manual_on":
                                UIManager.Manual_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext2_off":
                                UIManager.Ext2_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext2_on":
                                UIManager.Ext2_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "rind_off":
                                UIManager.Rind_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "rind_on":
                                UIManager.Rind_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s1_off":
                                UIManager.S1_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s1_on":
                                UIManager.S1_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s2_off":
                                UIManager.S2_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s2_on":
                                UIManager.S2_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s3_off":
                                UIManager.S3_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s3_on":
                                UIManager.S3_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "sb_off":
                                UIManager.SB_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "sb_on":
                                UIManager.SB_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "blkt_off":
                                UIManager.Blkt_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "blkt_on":
                                UIManager.Blkt_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tadiv_off":
                                UIManager.Tadiv_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tadiv_on":
                                UIManager.Tadiv_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taleft_off":
                                UIManager.Taleft_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taleft_on":
                                UIManager.Taleft_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taright_off":
                                UIManager.Taright_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taright_on":
                                UIManager.Taright_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tawarn_off":
                                UIManager.Tawarn_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tawarn_on":
                                UIManager.Tawarn_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "wail_off":
                                UIManager.Wail_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "wail_on":
                                UIManager.Wail_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "yelp_off":
                                UIManager.Yelp_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "yelp_on":
                                UIManager.Yelp_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                        }
                    }
                }
                else {
                    foreach (string file in Directory.EnumerateFiles(path, "*.png"))
                    {
                        switch (Path.GetFileNameWithoutExtension(file))
                        {

                            /* 
                             *  V1.4.1.1 EUROPEAN
                             *  */
                            case "red_off":
                                UIManager.red_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "red_on":
                                UIManager.red_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;

                            case "white_off":
                                UIManager.white_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "white_on":
                                UIManager.white_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;

                            case "ambaroff":
                                UIManager.ambaroff = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ambaron":
                                UIManager.ambaron = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_l_off":
                                UIManager._7_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_l_on":
                                UIManager._7_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_el_off":
                                UIManager._7_el_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_el_on":
                                UIManager._7_el_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_cl_off":
                                UIManager._7_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_cl_on":
                                UIManager._7_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_cr_off":
                                UIManager._7_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_cr_on":
                                UIManager._7_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_er_off":
                                UIManager._7_er_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_er_on":
                                UIManager._7_er_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_r_off":
                                UIManager._7_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_r_on":
                                UIManager._7_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_ll_off":
                                UIManager._7_ll_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_ll_on":
                                UIManager._7_ll_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;


                            case "8_l_off":
                                UIManager._8_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_l_on":
                                UIManager._8_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_el_off":
                                UIManager._8_el_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_el_on":
                                UIManager._8_el_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_cl_off":
                                UIManager._8_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_cl_on":
                                UIManager._8_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_cr_off":
                                UIManager._8_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_cr_on":
                                UIManager._8_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_er_off":
                                UIManager._8_er_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_er_on":
                                UIManager._8_er_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_r_off":
                                UIManager._8_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_r_on":
                                UIManager._8_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_ll_off":
                                UIManager._8_ll_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_ll_on":
                                UIManager._8_ll_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_rr_off":
                                UIManager._8_rr_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_rr_on":
                                UIManager._8_rr_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;


                            case "9_l_off":
                                UIManager._9_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_l_on":
                                UIManager._9_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_el_off":
                                UIManager._9_el_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_el_on":
                                UIManager._9_el_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cl_off":
                                UIManager._9_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cl_on":
                                UIManager._9_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cr_off":
                                UIManager._9_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cr_on":
                                UIManager._9_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_er_off":
                                UIManager._9_er_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_er_on":
                                UIManager._9_er_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_r_off":
                                UIManager._9_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_r_on":
                                UIManager._9_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_ll_off":
                                UIManager._9_ll_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_ll_on":
                                UIManager._9_ll_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_rr_off":
                                UIManager._9_rr_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_rr_on":
                                UIManager._9_rr_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cc_off":
                                UIManager._9_cc_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cc_on":
                                UIManager._9_cc_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            /* 
                             *  V1.4.1.1 END
                             *  */

                            case "background":
                                UIManager.Background = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "hazard_off":
                                UIManager.Hazard_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "hazard_on":
                                UIManager.Hazard_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "horn_off":
                                UIManager.Horn_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "horn_on":
                                UIManager.Horn_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext1_off":
                                UIManager.Ext1_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext1_on":
                                UIManager.Ext1_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "intlt_off":
                                UIManager.Intlt_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "intlt_on":
                                UIManager.Intlt_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "lind_off":
                                UIManager.Lind_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "lind_on":
                                UIManager.Lind_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "manual_off":
                                UIManager.Manual_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "manual_on":
                                UIManager.Manual_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext2_off":
                                UIManager.Ext2_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext2_on":
                                UIManager.Ext2_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "rind_off":
                                UIManager.Rind_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "rind_on":
                                UIManager.Rind_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s1_off":
                                UIManager.S1_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s1_on":
                                UIManager.S1_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s2_off":
                                UIManager.S2_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s2_on":
                                UIManager.S2_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s3_off":
                                UIManager.S3_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s3_on":
                                UIManager.S3_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "sb_off":
                                UIManager.SB_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "sb_on":
                                UIManager.SB_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "blkt_off":
                                UIManager.Blkt_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "blkt_on":
                                UIManager.Blkt_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tadiv_off":
                                UIManager.Tadiv_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tadiv_on":
                                UIManager.Tadiv_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taleft_off":
                                UIManager.Taleft_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taleft_on":
                                UIManager.Taleft_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taright_off":
                                UIManager.Taright_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taright_on":
                                UIManager.Taright_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tawarn_off":
                                UIManager.Tawarn_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tawarn_on":
                                UIManager.Tawarn_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "wail_off":
                                UIManager.Wail_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "wail_on":
                                UIManager.Wail_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "yelp_off":
                                UIManager.Yelp_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "yelp_on":
                                UIManager.Yelp_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_l_off":
                                UIManager._3_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_l_on":
                                UIManager._3_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_c_off":
                                UIManager._3_c_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_c_on":
                                UIManager._3_c_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_r_off":
                                UIManager._3_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_r_on":
                                UIManager._3_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_l_off":
                                UIManager._4_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_l_on":
                                UIManager._4_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_cl_off":
                                UIManager._4_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_cl_on":
                                UIManager._4_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_cr_off":
                                UIManager._4_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_cr_on":
                                UIManager._4_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_r_off":
                                UIManager._4_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_r_on":
                                UIManager._4_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_l_off":
                                UIManager._5_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_l_on":
                                UIManager._5_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_cl_off":
                                UIManager._5_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_cl_on":
                                UIManager._5_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_c_off":
                                UIManager._5_c_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_c_on":
                                UIManager._5_c_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_cr_off":
                                UIManager._5_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_cr_on":
                                UIManager._5_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_r_off":
                                UIManager._5_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_r_on":
                                UIManager._5_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_l_off":
                                UIManager._6_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_l_on":
                                UIManager._6_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_el_off":
                                UIManager._6_el_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_el_on":
                                UIManager._6_el_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_cl_off":
                                UIManager._6_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_cl_on":
                                UIManager._6_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_cr_off":
                                UIManager._6_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_cr_on":
                                UIManager._6_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_er_off":
                                UIManager._6_er_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_er_on":
                                UIManager._6_er_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_r_off":
                                UIManager._6_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_r_on":
                                UIManager._6_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                        }
                    }
                }


                
            }
        }

        public static void GetCustomSprites(string customFolder)
        {
            //Custom folder = Isae2
            string path = @"plugins\DLS\UI\" + customFolder;
            Game.LogTrivial("Ruta --> " + path);
            if (Directory.Exists(path))
            {
                CustomUI customUI = new CustomUI();
                customUI.Width = UIManager.sizeX;
                customUI.Height = UIManager.sizeY;
                if (File.Exists(path + @"\" + customFolder + ".xml"))
                {
                    try
                    {
                        XmlSerializer mySerializer = new XmlSerializer(typeof(CustomUI));
                        StreamReader streamReader = new StreamReader(path + @"\" + customFolder + ".xml");
                        Game.LogTrivial("Ruta 2 -->" + path + @"\" + customFolder + ".xml");
                        customUI = (CustomUI)mySerializer.Deserialize(streamReader);
                        streamReader.Close();
                        customUI.Height = customUI.SHeight.ToInt32();
                        customUI.Width = customUI.SWidth.ToInt32();
                        customUI.OffsetX = customUI.SOffsetX.ToInt32();
                        customUI.OffsetY = customUI.SOffsetY.ToInt32();
                    }
                    catch (Exception e)
                    {
                        ("ERROR (" + customFolder + "): " + e.Message).ToLog();
                        Game.LogTrivial("ERROR (" + customFolder + "): " + e.Message);
                    }
                }
                bool euromode = true;
                if (euromode) {
                    foreach (string file in Directory.EnumerateFiles(path, "*.png"))
                    {
                        switch (Path.GetFileNameWithoutExtension(file))
                        {
                            case "red_off":
                                UIManager.red_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "red_on":
                                UIManager.red_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ambaroff":
                                UIManager.ambaroff = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ambaron":
                                UIManager.ambaron = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "white_off":
                                UIManager.white_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "white_on":
                                UIManager.white_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "background":
                                UIManager.Background = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "hazard_off":
                                UIManager.Hazard_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "hazard_on":
                                UIManager.Hazard_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "horn_off":
                                UIManager.Horn_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "horn_on":
                                UIManager.Horn_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext1_off":
                                UIManager.Ext1_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext1_on":
                                UIManager.Ext1_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "intlt_off":
                                UIManager.Intlt_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "intlt_on":
                                UIManager.Intlt_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "lind_off":
                                UIManager.Lind_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "lind_on":
                                UIManager.Lind_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "manual_off":
                                UIManager.Manual_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "manual_on":
                                UIManager.Manual_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext2_off":
                                UIManager.Ext2_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext2_on":
                                UIManager.Ext2_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "rind_off":
                                UIManager.Rind_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "rind_on":
                                UIManager.Rind_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s1_off":
                                UIManager.S1_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s1_on":
                                UIManager.S1_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s2_off":
                                UIManager.S2_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s2_on":
                                UIManager.S2_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s3_off":
                                UIManager.S3_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s3_on":
                                UIManager.S3_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "sb_off":
                                UIManager.SB_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "sb_on":
                                UIManager.SB_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "blkt_off":
                                UIManager.Blkt_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "blkt_on":
                                UIManager.Blkt_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tadiv_off":
                                UIManager.Tadiv_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tadiv_on":
                                UIManager.Tadiv_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taleft_off":
                                UIManager.Taleft_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taleft_on":
                                UIManager.Taleft_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taright_off":
                                UIManager.Taright_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taright_on":
                                UIManager.Taright_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tawarn_off":
                                UIManager.Tawarn_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tawarn_on":
                                UIManager.Tawarn_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "wail_off":
                                UIManager.Wail_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "wail_on":
                                UIManager.Wail_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "yelp_off":
                                UIManager.Yelp_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "yelp_on":
                                UIManager.Yelp_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                        }
                    }
                } else
                {
                    foreach (string file in Directory.EnumerateFiles(path, "*.png"))
                    {

                        switch (Path.GetFileNameWithoutExtension(file))
                        {

                            /* 
                             *  V1.4.1.1 EUROPEAN
                             *  */
                            case "red_off":
                                UIManager.red_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "red_on":
                                UIManager.red_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ambaroff":
                                UIManager.ambaroff = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ambaron":
                                UIManager.ambaron = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "white_off":
                                UIManager.white_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "white_on":
                                UIManager.white_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_ll_off":
                                UIManager._7_ll_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_ll_on":
                                UIManager._7_ll_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_l_off":
                                UIManager._7_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_l_on":
                                UIManager._7_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_el_off":
                                UIManager._7_el_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_el_on":
                                UIManager._7_el_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_cl_off":
                                UIManager._7_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_cl_on":
                                UIManager._7_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_cr_off":
                                UIManager._7_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_cr_on":
                                UIManager._7_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_er_off":
                                UIManager._7_er_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_er_on":
                                UIManager._7_er_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_r_off":
                                UIManager._7_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "7_r_on":
                                UIManager._7_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;


                            case "8_ll_off":
                                UIManager._8_ll_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_ll_on":
                                UIManager._8_ll_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_l_off":
                                UIManager._8_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_l_on":
                                UIManager._8_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_el_off":
                                UIManager._8_el_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_el_on":
                                UIManager._8_el_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_cl_off":
                                UIManager._8_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_cl_on":
                                UIManager._8_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_cr_off":
                                UIManager._8_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_cr_on":
                                UIManager._8_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_er_off":
                                UIManager._8_er_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_er_on":
                                UIManager._8_er_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_r_off":
                                UIManager._8_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_r_on":
                                UIManager._8_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_rr_off":
                                UIManager._8_ll_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "8_rr_on":
                                UIManager._8_ll_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_ll_off":
                                UIManager._9_ll_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_ll_on":
                                UIManager._9_ll_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_l_off":
                                UIManager._9_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_l_on":
                                UIManager._9_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_el_off":
                                UIManager._9_el_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_el_on":
                                UIManager._9_el_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cl_off":
                                UIManager._9_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cl_on":
                                UIManager._9_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cc_off":
                                UIManager._9_cc_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cc_on":
                                UIManager._9_cc_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cr_off":
                                UIManager._9_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_cr_on":
                                UIManager._9_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_er_off":
                                UIManager._9_er_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_er_on":
                                UIManager._9_er_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_r_off":
                                UIManager._9_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_r_on":
                                UIManager._9_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_rr_off":
                                UIManager._9_rr_off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "9_rr_on":
                                UIManager._9_rr_on = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(1920 - customUI.Width, 1080 - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            /* 
                             *  V1.4.1.1 END
                             *  */

                            case "background":
                                UIManager.Background = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "hazard_off":
                                UIManager.Hazard_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "hazard_on":
                                UIManager.Hazard_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "horn_off":
                                UIManager.Horn_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "horn_on":
                                UIManager.Horn_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext1_off":
                                UIManager.Ext1_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext1_on":
                                UIManager.Ext1_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "intlt_off":
                                UIManager.Intlt_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "intlt_on":
                                UIManager.Intlt_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "lind_off":
                                UIManager.Lind_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "lind_on":
                                UIManager.Lind_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "manual_off":
                                UIManager.Manual_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "manual_on":
                                UIManager.Manual_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext2_off":
                                UIManager.Ext2_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "ext2_on":
                                UIManager.Ext2_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "rind_off":
                                UIManager.Rind_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "rind_on":
                                UIManager.Rind_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s1_off":
                                UIManager.S1_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s1_on":
                                UIManager.S1_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s2_off":
                                UIManager.S2_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s2_on":
                                UIManager.S2_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s3_off":
                                UIManager.S3_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "s3_on":
                                UIManager.S3_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "sb_off":
                                UIManager.SB_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "sb_on":
                                UIManager.SB_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "blkt_off":
                                UIManager.Blkt_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "blkt_on":
                                UIManager.Blkt_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tadiv_off":
                                UIManager.Tadiv_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tadiv_on":
                                UIManager.Tadiv_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taleft_off":
                                UIManager.Taleft_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taleft_on":
                                UIManager.Taleft_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taright_off":
                                UIManager.Taright_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "taright_on":
                                UIManager.Taright_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tawarn_off":
                                UIManager.Tawarn_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "tawarn_on":
                                UIManager.Tawarn_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "wail_off":
                                UIManager.Wail_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "wail_on":
                                UIManager.Wail_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "yelp_off":
                                UIManager.Yelp_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "yelp_on":
                                UIManager.Yelp_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_l_off":
                                UIManager._3_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_l_on":
                                UIManager._3_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_c_off":
                                UIManager._3_c_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_c_on":
                                UIManager._3_c_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_r_off":
                                UIManager._3_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "3_r_on":
                                UIManager._3_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_l_off":
                                UIManager._4_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_l_on":
                                UIManager._4_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_cl_off":
                                UIManager._4_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_cl_on":
                                UIManager._4_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_cr_off":
                                UIManager._4_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_cr_on":
                                UIManager._4_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_r_off":
                                UIManager._4_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "4_r_on":
                                UIManager._4_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_l_off":
                                UIManager._5_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_l_on":
                                UIManager._5_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_cl_off":
                                UIManager._5_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_cl_on":
                                UIManager._5_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_c_off":
                                UIManager._5_c_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_c_on":
                                UIManager._5_c_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_cr_off":
                                UIManager._5_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_cr_on":
                                UIManager._5_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_r_off":
                                UIManager._5_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "5_r_on":
                                UIManager._5_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_l_off":
                                UIManager._6_l_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_l_on":
                                UIManager._6_l_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_el_off":
                                UIManager._6_el_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_el_on":
                                UIManager._6_el_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_cl_off":
                                UIManager._6_cl_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_cl_on":
                                UIManager._6_cl_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_cr_off":
                                UIManager._6_cr_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_cr_on":
                                UIManager._6_cr_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_er_off":
                                UIManager._6_er_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_er_on":
                                UIManager._6_er_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_r_off":
                                UIManager._6_r_Off = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                            case "6_r_on":
                                UIManager._6_r_On = new Sprite(Game.CreateTextureFromFile(file), new System.Drawing.Point(customUI.OffsetX - customUI.Width, customUI.OffsetY - customUI.Height), new System.Drawing.Size(customUI.Width, customUI.Height));
                                break;
                        }
                    }
                }
                
            }
            else {
                Game.LogTrivial("Ruta No existe --> " + path);
            }
        }

        public static void ResetSprites()
        {

            bool euromode = true;
            if (euromode)
            {
                
                UIManager.red_On = new Sprite(Properties.Resources.red_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.red_Off = new Sprite(Properties.Resources.red_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                UIManager.ambaron = new Sprite(Properties.Resources.ambaron, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.ambaroff = new Sprite(Properties.Resources.ambaroff, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                UIManager.white_On = new Sprite(Properties.Resources.white_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.white_Off = new Sprite(Properties.Resources.white_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                /* 
                 *  V1.4.1.1 END
                 *  */
                UIManager.Background = new Sprite(Properties.Resources.background, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Horn_On = new Sprite(Properties.Resources.horn_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Horn_Off = new Sprite(Properties.Resources.horn_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Manual_On = new Sprite(Properties.Resources.manual_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Manual_Off = new Sprite(Properties.Resources.manual_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Wail_On = new Sprite(Properties.Resources.wail_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Wail_Off = new Sprite(Properties.Resources.wail_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Yelp_On = new Sprite(Properties.Resources.yelp_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Yelp_Off = new Sprite(Properties.Resources.yelp_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Ext1_On = new Sprite(Properties.Resources.ext1_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Ext1_Off = new Sprite(Properties.Resources.ext1_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Ext2_On = new Sprite(Properties.Resources.ext2_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Ext2_Off = new Sprite(Properties.Resources.ext2_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Hazard_On = new Sprite(Properties.Resources.hazard_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Hazard_Off = new Sprite(Properties.Resources.hazard_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Lind_On = new Sprite(Properties.Resources.lind_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Lind_Off = new Sprite(Properties.Resources.lind_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Rind_On = new Sprite(Properties.Resources.rind_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Rind_Off = new Sprite(Properties.Resources.rind_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S1_On = new Sprite(Properties.Resources.s1_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S1_Off = new Sprite(Properties.Resources.s1_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S2_On = new Sprite(Properties.Resources.s2_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S2_Off = new Sprite(Properties.Resources.s2_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S3_On = new Sprite(Properties.Resources.s3_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S3_Off = new Sprite(Properties.Resources.s3_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Taleft_On = new Sprite(Properties.Resources.taleft_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Taleft_Off = new Sprite(Properties.Resources.taleft_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Taright_On = new Sprite(Properties.Resources.taright_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Taright_Off = new Sprite(Properties.Resources.taright_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Tadiv_On = new Sprite(Properties.Resources.tadiv_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Tadiv_Off = new Sprite(Properties.Resources.tadiv_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Tawarn_On = new Sprite(Properties.Resources.tawarn_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Tawarn_Off = new Sprite(Properties.Resources.tawarn_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Intlt_On = new Sprite(Properties.Resources.intlt_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Intlt_Off = new Sprite(Properties.Resources.intlt_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.SB_On = new Sprite(Properties.Resources.sb_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.SB_Off = new Sprite(Properties.Resources.sb_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Blkt_On = new Sprite(Properties.Resources.blkt_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Blkt_Off = new Sprite(Properties.Resources.blkt_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
            }
            else {
                UIManager._7_ll_on = new Sprite(Properties.Resources._7_ll_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_ll_off = new Sprite(Properties.Resources._7_ll_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_l_On = new Sprite(Properties.Resources._7_l_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_l_Off = new Sprite(Properties.Resources._7_l_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_el_On = new Sprite(Properties.Resources._7_el_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_el_Off = new Sprite(Properties.Resources._7_el_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_cl_On = new Sprite(Properties.Resources._7_cl_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_cl_Off = new Sprite(Properties.Resources._7_cl_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_cr_On = new Sprite(Properties.Resources._7_cr_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_cr_Off = new Sprite(Properties.Resources._7_cr_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_er_On = new Sprite(Properties.Resources._7_er_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_er_Off = new Sprite(Properties.Resources._7_er_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_r_On = new Sprite(Properties.Resources._7_r_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._7_r_Off = new Sprite(Properties.Resources._7_r_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                UIManager._8_ll_on = new Sprite(Properties.Resources._8_ll_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_ll_off = new Sprite(Properties.Resources._8_ll_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_l_On = new Sprite(Properties.Resources._8_l_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_l_Off = new Sprite(Properties.Resources._8_l_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_el_On = new Sprite(Properties.Resources._8_el_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_el_Off = new Sprite(Properties.Resources._8_el_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_cl_On = new Sprite(Properties.Resources._8_cl_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_cl_Off = new Sprite(Properties.Resources._8_cl_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_cr_On = new Sprite(Properties.Resources._8_cr_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_cr_Off = new Sprite(Properties.Resources._8_cr_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_er_On = new Sprite(Properties.Resources._8_er_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_er_Off = new Sprite(Properties.Resources._8_er_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_r_On = new Sprite(Properties.Resources._8_r_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_r_Off = new Sprite(Properties.Resources._8_r_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_rr_on = new Sprite(Properties.Resources._8_rr_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._8_rr_off = new Sprite(Properties.Resources._8_rr_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                UIManager._9_ll_on = new Sprite(Properties.Resources._9_ll_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_ll_off = new Sprite(Properties.Resources._9_ll_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_l_On = new Sprite(Properties.Resources._9_l_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_l_Off = new Sprite(Properties.Resources._9_l_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_el_On = new Sprite(Properties.Resources._9_el_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_el_Off = new Sprite(Properties.Resources._9_el_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_cl_On = new Sprite(Properties.Resources._9_cl_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_cl_Off = new Sprite(Properties.Resources._9_cl_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                UIManager._9_cc_on = new Sprite(Properties.Resources._9_cc_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_cc_off = new Sprite(Properties.Resources._9_cc_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                UIManager._9_cr_On = new Sprite(Properties.Resources._9_cr_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_cr_Off = new Sprite(Properties.Resources._9_cr_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_er_On = new Sprite(Properties.Resources._9_er_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_er_Off = new Sprite(Properties.Resources._9_er_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_r_On = new Sprite(Properties.Resources._9_r_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_r_Off = new Sprite(Properties.Resources._9_r_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_rr_on = new Sprite(Properties.Resources._9_rr_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._9_rr_off = new Sprite(Properties.Resources._9_rr_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                UIManager.red_On = new Sprite(Properties.Resources.red_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.red_Off = new Sprite(Properties.Resources.red_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                UIManager.ambaron = new Sprite(Properties.Resources.ambaron, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.ambaroff = new Sprite(Properties.Resources.ambaroff, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                UIManager.white_On = new Sprite(Properties.Resources.white_on, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.white_Off = new Sprite(Properties.Resources.white_off, new System.Drawing.Point(1920 - UIManager.sizeX, 1080 - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));

                /* 
                 *  V1.4.1.1 END
                 *  */

                UIManager.Background = new Sprite(Properties.Resources.background, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Horn_On = new Sprite(Properties.Resources.horn_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Horn_Off = new Sprite(Properties.Resources.horn_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Manual_On = new Sprite(Properties.Resources.manual_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Manual_Off = new Sprite(Properties.Resources.manual_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Wail_On = new Sprite(Properties.Resources.wail_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Wail_Off = new Sprite(Properties.Resources.wail_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Yelp_On = new Sprite(Properties.Resources.yelp_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Yelp_Off = new Sprite(Properties.Resources.yelp_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Ext1_On = new Sprite(Properties.Resources.ext1_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Ext1_Off = new Sprite(Properties.Resources.ext1_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Ext2_On = new Sprite(Properties.Resources.ext2_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Ext2_Off = new Sprite(Properties.Resources.ext2_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Hazard_On = new Sprite(Properties.Resources.hazard_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Hazard_Off = new Sprite(Properties.Resources.hazard_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Lind_On = new Sprite(Properties.Resources.lind_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Lind_Off = new Sprite(Properties.Resources.lind_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Rind_On = new Sprite(Properties.Resources.rind_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Rind_Off = new Sprite(Properties.Resources.rind_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S1_On = new Sprite(Properties.Resources.s1_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S1_Off = new Sprite(Properties.Resources.s1_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S2_On = new Sprite(Properties.Resources.s2_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S2_Off = new Sprite(Properties.Resources.s2_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S3_On = new Sprite(Properties.Resources.s3_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.S3_Off = new Sprite(Properties.Resources.s3_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Taleft_On = new Sprite(Properties.Resources.taleft_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Taleft_Off = new Sprite(Properties.Resources.taleft_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Taright_On = new Sprite(Properties.Resources.taright_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Taright_Off = new Sprite(Properties.Resources.taright_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Tadiv_On = new Sprite(Properties.Resources.tadiv_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Tadiv_Off = new Sprite(Properties.Resources.tadiv_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Tawarn_On = new Sprite(Properties.Resources.tawarn_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Tawarn_Off = new Sprite(Properties.Resources.tawarn_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Intlt_On = new Sprite(Properties.Resources.intlt_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Intlt_Off = new Sprite(Properties.Resources.intlt_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.SB_On = new Sprite(Properties.Resources.sb_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.SB_Off = new Sprite(Properties.Resources.sb_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Blkt_On = new Sprite(Properties.Resources.blkt_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager.Blkt_Off = new Sprite(Properties.Resources.blkt_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._3_l_On = new Sprite(Properties.Resources._3_l_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._3_l_Off = new Sprite(Properties.Resources._3_l_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._3_c_On = new Sprite(Properties.Resources._3_c_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._3_c_Off = new Sprite(Properties.Resources._3_c_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._3_r_On = new Sprite(Properties.Resources._3_r_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._3_r_Off = new Sprite(Properties.Resources._3_r_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._4_l_On = new Sprite(Properties.Resources._4_l_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._4_l_Off = new Sprite(Properties.Resources._4_l_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._4_cl_On = new Sprite(Properties.Resources._4_cl_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._4_cl_Off = new Sprite(Properties.Resources._4_cl_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._4_cr_On = new Sprite(Properties.Resources._4_cr_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._4_cr_Off = new Sprite(Properties.Resources._4_cr_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._4_r_On = new Sprite(Properties.Resources._4_r_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._4_r_Off = new Sprite(Properties.Resources._4_r_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_l_On = new Sprite(Properties.Resources._5_l_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_l_Off = new Sprite(Properties.Resources._5_l_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_cl_On = new Sprite(Properties.Resources._5_cl_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_cl_Off = new Sprite(Properties.Resources._5_cl_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_c_On = new Sprite(Properties.Resources._5_c_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_c_Off = new Sprite(Properties.Resources._5_c_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_cr_On = new Sprite(Properties.Resources._5_cr_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_cr_Off = new Sprite(Properties.Resources._5_cr_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_r_On = new Sprite(Properties.Resources._5_r_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._5_r_Off = new Sprite(Properties.Resources._5_r_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_l_On = new Sprite(Properties.Resources._6_l_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_l_Off = new Sprite(Properties.Resources._6_l_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_el_On = new Sprite(Properties.Resources._6_el_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_el_Off = new Sprite(Properties.Resources._6_el_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_cl_On = new Sprite(Properties.Resources._6_cl_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_cl_Off = new Sprite(Properties.Resources._6_cl_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_cr_On = new Sprite(Properties.Resources._6_cr_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_cr_Off = new Sprite(Properties.Resources._6_cr_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_er_On = new Sprite(Properties.Resources._6_er_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_er_Off = new Sprite(Properties.Resources._6_er_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_r_On = new Sprite(Properties.Resources._6_r_on, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
                UIManager._6_r_Off = new Sprite(Properties.Resources._6_r_off, new System.Drawing.Point(UIManager.offsetX - UIManager.sizeX, UIManager.offsetY - UIManager.sizeY), new System.Drawing.Size(UIManager.sizeX, UIManager.sizeY));
            }
            /* 
             *  V1.4.1.1 EUROPEAN
             *  */
            
        }
    }
}
