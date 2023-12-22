using MenuAPI;

namespace vMenuClient.menus
{
    public class About
    {
        // Variables
        private Menu menu;

        private void CreateMenu()
        {
            // Create the menu.
            menu = new Menu("vMenu", "About TK-vMenu");

            // Create menu items.
            var version = new MenuItem("TK-vMenu Version", $"This server is using TK-vMenu ~b~~h~{MainMenu.Version}~h~~s~.")
            {
                Label = $"~h~{MainMenu.Version}~h~"
            };
            var Github = new MenuItem("~r~~h~TK-vMenu Github Download~h~~r~", "\n\nDownload TK-vMenu: ~p~~h~github.com/takeoverzzz/TK-vMenu~h~~s~");
			
			var TKLABSDX = new MenuItem("~r~~h~TK-vMenu Discord Link *FOR SUPPORT*~h~~r~", "\n\n~r~~h~https://discord.gg/EchnCe4gBb~h~~r~");

            var serverInfoMessage = vMenuShared.ConfigManager.GetSettingsString(vMenuShared.ConfigManager.Setting.vmenu_server_info_message);
            if (!string.IsNullOrEmpty(serverInfoMessage))
            {
                var serverInfo = new MenuItem("Server Info", serverInfoMessage);
                var siteUrl = vMenuShared.ConfigManager.GetSettingsString(vMenuShared.ConfigManager.Setting.vmenu_server_info_website_url);
                if (!string.IsNullOrEmpty(siteUrl))
                {
                    serverInfo.Label = $"{siteUrl}";
                }
                menu.AddMenuItem(serverInfo);
            }
            menu.AddMenuItem(version);
			menu.AddMenuItem(Github);
			menu.AddMenuItem(TKLABSDX);
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
