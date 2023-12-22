﻿using System;
using System.Collections.Generic;

using CitizenFX.Core;

using Newtonsoft.Json;


using static CitizenFX.Core.Native.API;

namespace vMenuShared
{
    public static class ConfigManager
    {
        public enum Setting
        {
            // Id for RegisterKeyMapping
            vmenu_individual_server_id,

            // General settings
            vmenu_use_permissions,
            vmenu_menu_staff_only,
            vmenu_menu_toggle_key,
            vmenu_noclip_toggle_key,
            vmenu_keep_spawned_vehicles_persistent,
            vmenu_use_els_compatibility_mode,
            vmenu_handle_invisibility,
            vmenu_quit_session_in_rockstar_editor,
            vmenu_server_info_message,
            vmenu_server_info_website_url,
            vmenu_teleport_to_wp_keybind_key,
            vmenu_pvp_mode,
            keep_player_head_props,
            vmenu_disable_server_info_convars,
            vmenu_player_names_distance,
            vmenu_disable_entity_outlines_tool,
            vmenu_disable_player_stats_setup,
            pfvmenu_moshnotify_setting,

            // Vehicle Chameleon Colours
            vmenu_using_chameleon_colours,

            // Kick & ban settings
            vmenu_default_ban_message_information,
            vmenu_auto_ban_cheaters,
            vmenu_auto_ban_cheaters_ban_message,
            vmenu_log_ban_actions,
            vmenu_log_kick_actions,

            // Weather settings
            vmenu_enable_weather_sync,
            vmenu_enable_dynamic_weather,
            vmenu_dynamic_weather_timer,
            vmenu_current_weather,
            vmenu_blackout_enabled,
            vmenu_weather_change_duration,
            vmenu_enable_snow,

            // Time settings
            vmenu_enable_time_sync,
            vmenu_freeze_time,
            vmenu_ingame_minute_duration,
            vmenu_current_hour,
            vmenu_current_minute,
            vmenu_sync_to_machine_time,

            // Discord Rich Presence
            vmenu_discord_appid,
            vmenu_disable_richpresence,
            vmenu_discord_link_two,
            vmenu_discord_link_two_text,
            vmenu_discord_link_one_text,
            vmenu_discord_link_one,
            vmenu_discord_text,
            vmenu_discord_large_image,
            vmenu_discord_small_image,
            vmenu_discord_small_image_text,
            vmenu_discord_large_image_text,
        }

        /// <summary>
        /// Get a boolean setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool GetSettingsBool(Setting setting)
        {
            return GetConvar(setting.ToString(), "false") == "true";
        }

        /// <summary>
        /// Get an integer setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static int GetSettingsInt(Setting setting)
        {
            var convarInt = GetConvarInt(setting.ToString(), -1);
            if (convarInt == -1)
            {
                if (int.TryParse(GetConvar(setting.ToString(), "-1"), out var convarIntAlt))
                {
                    return convarIntAlt;
                }
            }
            return convarInt;
        }

        /// <summary>
        /// Get a float setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static float GetSettingsFloat(Setting setting)
        {
            if (float.TryParse(GetConvar(setting.ToString(), "-1.0"), out var result))
            {
                return result;
            }
            return -1f;
        }

        /// <summary>
        /// Get a string setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static string GetSettingsString(Setting setting, string defaultValue = null)
        {
            var value = GetConvar(setting.ToString(), defaultValue ?? "");
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return value;
        }

        /// <summary>
        /// Debugging mode
        /// </summary>
        public static bool DebugMode => IsDuplicityVersion() ? IsServerDebugModeEnabled() : IsClientDebugModeEnabled();

        /// <summary>
        /// Default value for server debugging mode.
        /// </summary>
        /// <returns></returns>
        public static bool IsServerDebugModeEnabled()
        {
            return GetResourceMetadata("vMenu", "server_debug_mode", 0).ToLower() == "true";
        }

        /// <summary>
        /// Default value for client debugging mode.
        /// </summary>
        /// <returns></returns>
        public static bool IsClientDebugModeEnabled()
        {
            return GetResourceMetadata("vMenu", "client_debug_mode", 0).ToLower() == "true";
        }

        #region Get saved locations from the locations.json
        /// <summary>
        /// Gets the locations.json data.
        /// </summary>
        /// <returns></returns>
        public static Locations GetLocations()
        {
            var data = new Locations();

            var jsonFile = LoadResourceFile(GetCurrentResourceName(), "config/locations.json");
            try
            {
                if (string.IsNullOrEmpty(jsonFile))
                {
#if CLIENT
                    vMenuClient.Notify.Error("The locations.json file is empty or does not exist, please tell the server owner to fix this.");
#endif
#if SERVER
                    vMenuServer.DebugLog.Log("The locations.json file is empty or does not exist, please fix this.", vMenuServer.DebugLog.LogLevel.error);
#endif
                }
                else
                {
                    data = JsonConvert.DeserializeObject<Locations>(jsonFile);
                }
            }
            catch (Exception e)
            {
#if CLIENT
                vMenuClient.Notify.Error("An error occurred while processing the locations.json file. Teleport Locations and Location Blips will be unavailable. Please correct any errors in the locations.json file.");
#endif
                Debug.WriteLine($"[vMenu] json exception details: {e.Message}\nStackTrace:\n{e.StackTrace}");
            }

            return data;
        }

        /// <summary>
        /// Gets just the teleport locations data from the locations.json.
        /// </summary>
        /// <returns></returns>
        public static List<TeleportLocation> GetTeleportLocationsData()
        {
            return GetLocations().teleports;
        }

        /// <summary>
        /// Gets just the blips data from the locations.json.
        /// </summary>
        /// <returns></returns>
        public static List<LocationBlip> GetLocationBlipsData()
        {
            return GetLocations().blips;
        }

        /// <summary>
        /// Struct used for deserializing json only.
        /// </summary>
        public struct Locations
        {
            public List<TeleportLocation> teleports;
            public List<LocationBlip> blips;
        }

        /// <summary>
        /// Teleport location struct.
        /// </summary>
        public struct TeleportLocation
        {
            public string name;
            public Vector3 coordinates;
            public float heading;

            public TeleportLocation(string name, Vector3 coordinates, float heading)
            {
                this.name = name;
                this.coordinates = coordinates;
                this.heading = heading;
            }
        }

        /// <summary>
        /// Location blip struct.
        /// </summary>
        public struct LocationBlip
        {
            public string name;
            public Vector3 coordinates;
            public int spriteID;
            public int color;

            public LocationBlip(string name, Vector3 coordinates, int spriteID, int color)
            {
                this.name = name;
                this.coordinates = coordinates;
                this.spriteID = spriteID;
                this.color = color;
            }
        }
        #endregion

        #region Get all the languages from the appropriate json file

        /// <summary>
        /// Gets and stores the languages from the multiple .json's.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string>> GetLanguages()
        {
            Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();

            var metaData = GetResourceMetadata(GetCurrentResourceName(), "languages", GetNumResourceMetadata(GetCurrentResourceName(), "languages") - 1).Replace(" ", "");
            if (!string.IsNullOrEmpty(metaData))
            {
                var languages = metaData.Split(',');
                foreach (var lang in languages)
                {
                    try
                    {
                        string jsonFile = LoadResourceFile(GetCurrentResourceName(), $"config/languages/{lang}.json");
                        if (!string.IsNullOrEmpty(jsonFile))
                        {
                            data.Add(lang, JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonFile));
                        }
                        else
                        {
                            #if CLIENT
                            vMenuClient.Notify.Error($"Unable to load {lang}.json");
                            #endif
                        }
                    }
                    catch
                    {
                        #if CLIENT
                        vMenuClient.Notify.Error($"Unable to load {lang}.json");
                        #endif
                    }
                }
            }

            return data;
        }

        #endregion
    }
}