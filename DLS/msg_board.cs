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

    public class msg_board
    {
        public static MenuPool pool;
        public static UIMenu mainMenu;

        public static readonly Keys KeyBinding = Keys.F7;
        public static UIMenuItem msgExtra3;
        public static UIMenuItem msgExtra7;
        public static UIMenuItem msgExtra8;
        public static UIMenuItem msgExtra9;
        public static UIMenuItem msgExtra10;
        public static UIMenuItem msgExtra11;
        public static UIMenuItem msgExtra12;
        public static UIMenuItem ambaroff;

        public static UIMenuItem paneloff;
        public static UIMenuItem paneldown;
        public static UIMenuItem panelup;
  
        public static void Main()
        {
            
            // create the pool that handles drawing and processing the menus
            pool = new MenuPool();

            // create the main menu
            mainMenu = new UIMenu("RAGENativeUI", "EXAMPLE");

            // create the menu items
            {

                mainMenu.MouseControlsEnabled = false;
                mainMenu.AllowCameraMovement = true;

                msgExtra12 = new UIMenuItem("SIGNAL MASTER ~r~AMBAR TRASERO");
                //mainMenu.AddItem(msgExtra12);

                panelup = new UIMenuItem("MENSAJES ~g~ABRIR PANEL");
                //mainMenu.AddItem(panelup);
                paneldown = new UIMenuItem("MENSAJES ~r~BAJAR PANEL");
                //mainMenu.AddItem(paneldown);

                paneloff = new UIMenuItem("MENSAJES ~r~QUITAR TEXTO");
                //mainMenu.AddItem(paneloff);
                //paneldown = new UIMenuItem("~r~BAJAR PANEL");
                //mainMenu.AddItem(paneldown);

                ambaroff = new UIMenuItem("SIGNAL MASTER ~r~OFF");
                //mainMenu.AddItem(ambaroff);
                msgExtra3 = new UIMenuItem("DELANTE ~b~G. CIVIL TRÁFICO ALTO");
                msgExtra11 = new UIMenuItem("TRASERA ~b~SÍGAME");
                msgExtra9 = new UIMenuItem("AMBOS LADOS ~b~CONTROL");
                msgExtra7 = new UIMenuItem("ATRAS ~b~DESVIO");
                msgExtra10 = new UIMenuItem("AMBOS LADOS ~b~PRECAUCION");

                //NO TOCAR
                mainMenu.RefreshIndex();

                mainMenu.OnItemSelect += OnItemSelect;


                /*var cb = new UIMenuCheckboxItem("Checkbox", false, "A checkbox menu item.");
                // show a message when toggling the checkbox
                cb.CheckboxEvent += (item, isChecked) => Game.DisplaySubtitle($"The checkbox is {(isChecked ? "" : "~r~not~s~ ")}checked");

                var spawnCar = new UIMenuItem("Spawn car", "Spawns a random car after 5 seconds.");
                spawnCar.Activated += (menu, item) =>
                {
                    // disable the item so the user cannot activate it again until the car has spawned
                    spawnCar.Enabled = false;

                    GameFiber.StartNew(() =>
                    {
                        // 5 second countdown
                        for (int sec = 5; sec > 0; sec--)
                        {
                            // show the countdown in the menu description
                            mainMenu.DescriptionOverride = $"The car will spawn in ~b~{sec}~s~ second(s).";

                            GameFiber.Sleep(1000); // sleep for 1 second
                        }

                        // remove the countdown from the description
                        mainMenu.DescriptionOverride = null;

                        // spawn the random car
                        new Vehicle(m => m.IsCar, Game.LocalPlayer.Character.GetOffsetPositionFront(5.0f)).Dismiss();

                        // wait a bit and re-enable the item
                        GameFiber.Sleep(500);
                        spawnCar.Enabled = true;
                    });
                };

                // a numeric scroller from -50 to 50 in intervals of 5
                var numbers = new UIMenuNumericScrollerItem<int>("Numbers", "A numeric scroller menu item.", -50, 50, 5);
                numbers.IndexChanged += (item, oldIndex, newIndex) => Game.DisplaySubtitle($"{oldIndex} -> {newIndex}| Selected number = {numbers.Value}");

                // a list scroller with strings
                var strings = new UIMenuListScrollerItem<string>("Strings", "A list scroller menu item with strings.", new[] { "Hello", "World", "Foo", "Bar" });
                strings.IndexChanged += (item, oldIndex, newIndex) => Game.DisplaySubtitle($"{oldIndex} -> {newIndex}| Selected string = {strings.SelectedItem}");

                
                // add the items to the menu
                mainMenu.AddItems(cb, spawnCar, numbers, strings);*/
            }

            
            // add all the menus to the pool
            pool.Add(mainMenu);

            // start the fiber which will handle drawing and processing the menus
            GameFiber.StartNew(ProcessMenus);

            // continue with the plugin...
            //Game.Console.Print($"  Press {KeyBinding} to open the menu.");
            //Game.DisplayHelp($"Press ~{KeyBinding.GetInstructionalId()}~ to open the menu.");
        }

        public static void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            msg_board_functions msg_Board_Functions = new msg_board_functions();
            if (sender == mainMenu)
            {
                if (selectedItem == msgExtra3)   // We check which item has been selected and do different things for each.
                {
                    
                    msg_Board_Functions.alto();
                    Game.DisplayNotification("MENSAJE PANEL - ALTO");
                    mainMenu.Visible = !mainMenu.Visible;
                }
                if (selectedItem == msgExtra11)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.sigame();
                    Game.DisplayNotification("MENSAJE PANEL - SIGAME");
                    mainMenu.Visible = !mainMenu.Visible;
                }
                if (selectedItem == msgExtra9)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.control();
                    Game.DisplayNotification("MENSAJE PANEL - CONTROL");
                    mainMenu.Visible = !mainMenu.Visible;
                }
                if (selectedItem == msgExtra8)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.gcivil();
                    Game.DisplayNotification("MENSAJE PANEL - G.CIVIL");
                    mainMenu.Visible = !mainMenu.Visible;
                }
                if (selectedItem == msgExtra7)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.desvio();
                    Game.DisplayNotification("MENSAJE PANEL - DESVIO");
                    mainMenu.Visible = !mainMenu.Visible;
                }
                if (selectedItem == msgExtra10)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.precaucion();
                    Game.DisplayNotification("MENSAJE PANEL - PRECAUCION");
                    mainMenu.Visible = !mainMenu.Visible;
                }
                if (selectedItem == msgExtra12)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.ambar();
                    Game.DisplayNotification("LUCES EMERGENCIA - AMBAR TRASERO");
                    mainMenu.Visible = !mainMenu.Visible;
                }
                if (selectedItem == ambaroff)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.miambaroff();
                    Game.DisplayNotification("LUCES EMERGENCIA - AMBAR OFF");
                    mainMenu.Visible = !mainMenu.Visible;
                }
                if (selectedItem == paneloff)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.mipaneloff();
                    Game.DisplayNotification("PANEL - APAGADO");
                    mainMenu.Visible = !mainMenu.Visible;
                }
              
                if (selectedItem == paneldown)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.mipanelDown();
                }
                if (selectedItem == panelup)   // We check which item has been selected and do different things for each.
                {

                    msg_Board_Functions.mipanelUp();
                }

            }
        }

        public static void ProcessMenus()
        {
            // draw the menu banners (only needed if UIMenu.SetBannerType(Rage.Texture) is used)
            // Game.RawFrameRender += (s, e) => pool.DrawBanners(e.Graphics);

            while (true)
            {
                GameFiber.Yield();

                pool.ProcessMenus();

            }
        }

        [ConsoleCommand]
        public static void RunMenuExample() => GameFiber.StartNew(Main);
    }
}