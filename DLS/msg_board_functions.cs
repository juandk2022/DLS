namespace DLS
{
    using DLS.Utils;
    using Rage;
    using Rage.Attributes;
    using Rage.Native;
    using RAGENativeUI;
    using RAGENativeUI.Elements;
    using RAGENativeUI.PauseMenu;
    using System.Windows.Forms;
    using System.Linq;

    public class msg_board_functions
    {
        
        public System.Media.SoundPlayer panelup2 = new System.Media.SoundPlayer("plugins/DLS/audio/panel.wav");
        public System.Media.SoundPlayer paneldow2 = new System.Media.SoundPlayer("plugins/DLS/audio/paneld.wav");

        public void paneltexto()
        {
            int indez = 5;
            int speed = 1;
            float rot1 = 90;
            Vehicle coche2 = Game.LocalPlayer.Character.CurrentVehicle;
            NativeFunction.CallByName<bool>("SET_VEHICLE_DOOR_CONTROL", coche2, indez, speed, rot1);

        }

        public void paneltextobajar()
        {
            int indez2 = 5;
            int speed2 = 1;
            float rota2 = -90;
            Vehicle coche3 = Game.LocalPlayer.Character.CurrentVehicle;
            NativeFunction.CallByName<bool>("SET_VEHICLE_DOOR_CONTROL", coche3, indez2, speed2, rota2);

        }


        public void alto()
        {

            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            coche.GetDLS();
            DLSModel vehDLS = coche.GetDLS();
            int left;

            left = vehDLS.msgBoard.SR1.ToInt32();

            coche.EmergencyLightingOverride.Lights[left - 1].FlashinessSequence = "11111111111100001111111111110000";
            coche.EmergencyLightingOverride.Lights[left - 1].Flash = true;



            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {

                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 3, false);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 7, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 8, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 9, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 10, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 11, true);

            }
        }



        public void mipanelUp()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;

            DLSModel dlsModel = coche.GetDLS();


            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {

                NativeFunction.CallByName<bool>("SET_VEHICLE_SIREN", coche, true);

                int indez = 5;
                int speed = 1;
                float rot1 = 0f;
                Vehicle coche2 = Game.LocalPlayer.Character.CurrentVehicle;

                //var angle = NativeFunction.Natives.GetVehicleDoorAngleRatio(coche2, 5);

                //Sleep(1000); = 1 segundo

                panelup2.Play();

                for (float i = 0f; i <= 1f; i += 0.01f)
                {
                    NativeFunction.Natives.SetVehicleDoorControl(coche2, indez, speed, rot1 + i);
                    GameFiber.Sleep(35);
                }
            }
        }

        public void mipanelDown()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;

            DLSModel dlsModel = coche.GetDLS();


            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {

                NativeFunction.CallByName<bool>("SET_VEHICLE_SIREN", coche, false);

                int indez2 = 5;
                int speed2 = 1;
                float rota2 = 1f;
                Vehicle coche3 = Game.LocalPlayer.Character.CurrentVehicle;
                //0.029999999f



                paneldow2.Play();

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
        }

        public void sigame()
        {

            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            coche.GetDLS();
            DLSModel vehDLS = coche.GetDLS();
            int left;

            left = vehDLS.msgBoard.SR2.ToInt32();

            coche.EmergencyLightingOverride.Lights[left - 1].FlashinessSequence = "11111111111100001111111111110000";
            coche.EmergencyLightingOverride.Lights[left - 1].Flash = true;



            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {

                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 3, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 7, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 8, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 9, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 10, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 11, false);

            }
        }

        public void control()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            coche.GetDLS();
            DLSModel vehDLS = coche.GetDLS();
            int left;

            left = vehDLS.msgBoard.SR3.ToInt32();

            coche.EmergencyLightingOverride.Lights[left - 1].FlashinessSequence = "11111111111100001111111111110000";
            coche.EmergencyLightingOverride.Lights[left - 1].Flash = true;



            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {

                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 3, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 7, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 8, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 9, false);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 10, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 11, true);

            }
        }

        public void gcivil()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;

            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {
                //paneltexto();

                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 3, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 7, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 8, false);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 9, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 10, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 11, true);
                Ped playerPed = Game.LocalPlayer.Character;
                //playerPed.CurrentVehicle.ShouldVehiclesYieldToThisVehicle = false;
            }
        }

        public void desvio()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            coche.GetDLS();
            DLSModel vehDLS = coche.GetDLS();
            int left;

            left = vehDLS.msgBoard.SR4.ToInt32();

            coche.EmergencyLightingOverride.Lights[left - 1].FlashinessSequence = "11111111111100001111111111110000";
            coche.EmergencyLightingOverride.Lights[left - 1].Flash = true;



            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {

                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 3, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 7, false);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 8, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 9, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 10, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 11, true);

            }
        }

        public void precaucion()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            coche.GetDLS();
            DLSModel vehDLS = coche.GetDLS();
            int left;

            left = vehDLS.msgBoard.SR5.ToInt32();

            coche.EmergencyLightingOverride.Lights[left - 1].FlashinessSequence = "11111111111100001111111111110000";
            coche.EmergencyLightingOverride.Lights[left - 1].Flash = true;



            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {

                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 3, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 7, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 8, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 9, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 10, false);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 11, true);

            }
        }

        public void ambar()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            coche.GetDLS();
            DLSModel vehDLS = coche.GetDLS();
            bool enable = true;

            enable = vehDLS.SpecialModes.ambar.enable.ToBoolean();
            if (enable)
            {
                int right;
                int left;

                left = vehDLS.SpecialModes.ambar.AL.ToInt32();
                right = vehDLS.SpecialModes.ambar.AR.ToInt32();

                coche.EmergencyLightingOverride.Lights[left - 1].FlashinessSequence = "11111111000000001111111100000000";
                coche.EmergencyLightingOverride.Lights[right - 1].FlashinessSequence = "00000000111111110000000011111111";
                coche.EmergencyLightingOverride.Lights[left - 1].Flash = true;
                coche.EmergencyLightingOverride.Lights[right - 1].Flash = true;



                if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
                {
                    Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
                }
                else
                {

                    NativeFunction.CallByName<bool>("SET_VEHICLE_SIREN", coche, true);
                    NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 12, false);
                    Ped playerPed = Game.LocalPlayer.Character;
                    //playerPed.CurrentVehicle.ShouldVehiclesYieldToThisVehicle = false;

                }
            }
            else
            {
                Game.DisplayNotification("~b~ AMBAR NO HABILIADO");
            }


        }

        public void mipaneloff()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;

            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 3, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 7, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 8, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 9, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 10, true);
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 11, true);
            }
        }

        public void miambaroff()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;
            coche.GetDLS();
            DLSModel vehDLS = coche.GetDLS();
            bool enable = true;
            enable = vehDLS.SpecialModes.ambar.enable.ToBoolean();
            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {
                int right;
                int left;
                left = vehDLS.SpecialModes.ambar.AL.ToInt32();
                right = vehDLS.SpecialModes.ambar.AR.ToInt32();

                coche.EmergencyLightingOverride.Lights[left - 1].FlashinessSequence = "00000000000000000000000000000000";
                coche.EmergencyLightingOverride.Lights[right - 1].FlashinessSequence = "00000000000000000000000000000000";
                coche.EmergencyLightingOverride.Lights[left - 1].Flash = true;
                coche.EmergencyLightingOverride.Lights[right - 1].Flash = true;
                NativeFunction.CallByName<bool>("SET_VEHICLE_EXTRA", coche, 12, true);
            }
        }

        public void quemecedas()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;

            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {
                Ped playerPed = Game.LocalPlayer.Character;
                playerPed.CurrentVehicle.ShouldVehiclesYieldToThisVehicle = true;
                Game.DisplayNotification("~b~ LOS COCHES AHORA CEDERAN A TUS SIRENAS");
            }
        }
        public void quenocedas()
        {
            Vehicle coche = Game.LocalPlayer.Character.CurrentVehicle;

            if (!Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                Game.DisplayNotification("~b~ NO ESTAS EN UN VEHICULO");
            }
            else
            {
                Ped playerPed = Game.LocalPlayer.Character;
                playerPed.CurrentVehicle.ShouldVehiclesYieldToThisVehicle = false;
                Game.DisplayNotification("~b~ NADIE CEDERA A TUS SIRENAS");
            }
        }
    }
}