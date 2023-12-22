using System;
using System.Collections.Generic;
using System.Linq;
using MenuAPI;
using Newtonsoft.Json;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using static vMenuClient.CommonFunctions;
using static vMenuShared.PermissionsManager;

namespace vMenuClient
{
    public class TeleportOptions
    {
        // Variables
        private Menu menu;

        // keybind states
        public bool KbTpToWaypoint { get; private set; } = UserDefaults.KbTpToWaypoint;
        public int KbTpToWaypointKey { get; } = vMenuShared.ConfigManager.GetSettingsInt(vMenuShared.ConfigManager.Setting.vmenu_teleport_to_wp_keybind_key) != -1
            ? vMenuShared.ConfigManager.GetSettingsInt(vMenuShared.ConfigManager.Setting.vmenu_teleport_to_wp_keybind_key)
            : 168; // 168 (F7 by default)

        internal static List<vMenuShared.ConfigManager.TeleportLocation> TpLocations = new List<vMenuShared.ConfigManager.TeleportLocation>();
        /// <summary>
        /// Creates the menu.
        /// </summary>
        private void CreateMenu()
        {

            menu = new Menu("Teleport Options", "Teleport Related Options");
            // menu items
            var teleportMenu = new Menu("Teleport Locations", "Teleport Locations");
            var teleportMenuBtn = new MenuItem("Teleport Locations", "Teleport to pre-configured locations, added by the server owner.");
            MenuController.AddSubmenu(menu, teleportMenu);
            MenuController.BindMenuItem(menu, teleportMenu, teleportMenuBtn);

            // Keybind settings menu items
            var kbTpToWaypoint = new MenuCheckboxItem("Teleport To Waypoint", "Teleport to your waypoint when pressing the keybind. By default, this keybind is set to ~r~F7~s~, server owners are able to change this however so ask them if you don't know what it is.", KbTpToWaypoint);
            var backBtn = new MenuItem("Back");

            // Teleportation options
            if (IsAllowed(Permission.TPTeleportToWp) || IsAllowed(Permission.TPTeleportLocations) || IsAllowed(Permission.TPTeleportToCoord))
            {
                var tptowp = new MenuItem("Teleport To Waypoint", "Teleport to the waypoint on your map.");
                var tpToCoord = new MenuItem("Teleport To Coords", "Enter the X, Y, Z coordinates and you will be teleported to that location.");
                var saveLocationBtn = new MenuItem("Save Teleport Location", "Adds your current location to the teleport locations menu and saves it on the server ~r~~h~(script restart required after adding new location(s)).");
                menu.OnItemSelect += async (sender, item, index) =>
                {
                    // Teleport to waypoint.
                    if (item == tptowp)
                    {
                        TeleportToWp();
                    }
                    else if (item == tpToCoord)
                    {
                        var x = await GetUserInput("Enter X coordinate.");
                        if (string.IsNullOrEmpty(x))
                        {
                            Notify.Error(CommonErrors.InvalidInput);
                            return;
                        }
                        var y = await GetUserInput("Enter Y coordinate.");
                        if (string.IsNullOrEmpty(y))
                        {
                            Notify.Error(CommonErrors.InvalidInput);
                            return;
                        }
                        var z = await GetUserInput("Enter Z coordinate.");
                        if (string.IsNullOrEmpty(z))
                        {
                            Notify.Error(CommonErrors.InvalidInput);
                            return;
                        }

                        if (!float.TryParse(x, out var posX))
                        {
                            if (int.TryParse(x, out var intX))
                            {
                                posX = intX;
                            }
                            else
                            {
                                Notify.Error("You did not enter a valid X coordinate.");
                                return;
                            }
                        }
                        if (!float.TryParse(y, out var posY))
                        {
                            if (int.TryParse(y, out var intY))
                            {
                                posY = intY;
                            }
                            else
                            {
                                Notify.Error("You did not enter a valid Y coordinate.");
                                return;
                            }
                        }
                        if (!float.TryParse(z, out var posZ))
                        {
                            if (int.TryParse(z, out var intZ))
                            {
                                posZ = intZ;
                            }
                            else
                            {
                                Notify.Error("You did not enter a valid Z coordinate.");
                                return;
                            }
                        }

                        await TeleportToCoords(new Vector3(posX, posY, posZ), true);
                    }
                    else if (item == saveLocationBtn)
                    {
                        SavePlayerLocationToLocationsFile();
                    }
                };

                if (IsAllowed(Permission.TPTeleportToWp))
                {
                    menu.AddMenuItem(tptowp);
                }
                if (IsAllowed(Permission.TPTeleportToCoord))
                {
                    menu.AddMenuItem(tpToCoord);
                }
                if (IsAllowed(Permission.TPTeleportLocations))
                {
                    menu.AddMenuItem(teleportMenuBtn);

                    MenuController.AddSubmenu(menu, teleportMenu);
                    MenuController.BindMenuItem(menu, teleportMenu, teleportMenuBtn);
                    teleportMenuBtn.Label = "→→→";

                    teleportMenu.OnMenuOpen += (sender) =>
                    {
                        var jsonFile2 = LoadResourceFile(GetCurrentResourceName(), "config/TeleportCategories.json");
                        var data2 = JsonConvert.DeserializeObject<vMenuShared.ConfigManager.LocationsSubMenu>(jsonFile2);

                        if (teleportMenu.Size != data2.teleports.Count())
                        {
                            teleportMenu.ClearMenuItems();
                            foreach (var location in data2.teleports)
                            {
                                Debug.WriteLine(location.JsonName);

                                var jsonFile = LoadResourceFile(GetCurrentResourceName(), "config/locations/" + location.JsonName);
                                var data = JsonConvert.DeserializeObject<vMenuShared.ConfigManager.Locationsteleport>(jsonFile);
                                Menu teleportSubMenu = new Menu(location.name, location.name);
                                MenuItem teleportSubMenuBtn = new MenuItem(location.name, $"Teleport to ~b~{location.name}~w~, added by the server owner.") { Label = "→→→" };
                                teleportMenu.AddMenuItem(teleportSubMenuBtn);

                                
                                foreach (var tplocations in data.teleports)
                                {
                                    var x = Math.Round(tplocations.coordinates.X, 2);
                                    var y = Math.Round(tplocations.coordinates.Y, 2);
                                    var z = Math.Round(tplocations.coordinates.Z, 2);
                                    var heading = Math.Round(tplocations.heading, 2);
                                    var tpBtn = new MenuItem(tplocations.name, $"Teleport to ~y~{tplocations.name}~n~~s~x: ~y~{x}~n~~s~y: ~y~{y}~n~~s~z: ~y~{z}~n~~s~heading: ~y~{heading}") { ItemData = tplocations };
                                    teleportSubMenu.AddMenuItem(tpBtn);
                                }

                                if (teleportSubMenu.Size > 0)
                                {
                                    MenuController.AddSubmenu(teleportMenu, teleportSubMenu);
                                    MenuController.BindMenuItem(teleportMenu, teleportSubMenu, teleportSubMenuBtn);
                                }
                                teleportSubMenu.OnItemSelect += async (sender, item, index) =>
                                {
                                    if (item.ItemData is vMenuShared.ConfigManager.TeleportLocation tl)
                                    {
                                        await TeleportToCoords(tl.coordinates, true);
                                        SetEntityHeading(Game.PlayerPed.Handle, tl.heading);
                                        SetGameplayCamRelativeHeading(0f);
                                    }
                                };
                            }

                        }
                    };



                    if (IsAllowed(Permission.TPTeleportSaveLocation))
                    {
                        menu.AddMenuItem(saveLocationBtn);
                    };
                }
            }
        }

        /// <summary>
        /// Create the menu if it doesn't exist, and then returns it.
        /// </summary>
        /// <returns>The Menu</returns>
        public Menu GetMenu()
        {
            if (menu == null)
            {
                CreateMenu();
            }
            return menu;
        }
    }
}