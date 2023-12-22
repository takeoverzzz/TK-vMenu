﻿using System.Collections.Generic;

using CitizenFX.Core;

using static CitizenFX.Core.Native.API;
using static CitizenFX.Core.UI.Screen;

namespace vMenuClient
{
    #region Error Templates
    /// <summary>
    /// List of error templates.
    /// </summary>
    public enum CommonErrors
    {
        NoVehicle,
        NeedToBeTheDriver,
        UnknownError,
        NotAllowed,
        InvalidModel,
        InvalidInput,
        InvalidSaveName,
        SaveNameAlreadyExists,
        CouldNotLoadSave,
        CouldNotLoad,
        PlayerNotFound,
        PedNotFound,
        WalkingStyleNotForMale,
        WalkingStyleNotForFemale,
        RightAlignedNotSupported,
    };

    /// <summary>
    /// Gets the formatted error message.
    /// </summary>
    public static class ErrorMessage 
    {
        /// <summary>
        /// Returns the formatted error message for the specified error type.
        /// </summary>
        /// <param name="errorType">The error type.</param>
        /// <param name="placeholderValue">An optional string that will be replaced inside the error message (if applicable).</param>
        /// <returns>The error message.</returns>
        public static string Get(CommonErrors errorType, string placeholderValue = null)
        {
            var outputMessage = "";
            var placeholder = placeholderValue != null ? " " + placeholderValue : "";
            outputMessage = errorType switch
            {
                CommonErrors.NeedToBeTheDriver => "You need to be the driver of this vehicle.",
                CommonErrors.NoVehicle => $"You need to be inside a vehicle{placeholder}.",
                CommonErrors.NotAllowed => $"You are not allowed to{placeholder}, sorry.",
                CommonErrors.InvalidModel => $"This model~r~{placeholder} ~s~could not be found, are you sure it's valid?",
                CommonErrors.InvalidInput => $"The input~r~{placeholder} ~s~is invalid or you cancelled the action, please try again.",
                CommonErrors.InvalidSaveName => $"Saving failed because the provided save name~r~{placeholder} ~s~is invalid.",
                CommonErrors.SaveNameAlreadyExists => $"Saving failed because the provided save name~r~{placeholder} ~s~already exists.",
                CommonErrors.CouldNotLoadSave => $"Loading of~r~{placeholder} ~s~failed! Is the saves file corrupt?",
                CommonErrors.CouldNotLoad => $"Could not load~r~{placeholder}~s~, sorry!",
                CommonErrors.PedNotFound => $"The specified ped could not be found.{placeholder}",
                CommonErrors.PlayerNotFound => $"The specified player could not be found.{placeholder}",
                CommonErrors.WalkingStyleNotForMale => $"This walking style is not available for male peds.{placeholder}",
                CommonErrors.WalkingStyleNotForFemale => $"This walking style is not available for female peds.{placeholder}",
                CommonErrors.RightAlignedNotSupported => $"Right aligned menus are not supported for ultra wide aspect ratios.{placeholder}",
                _ => $"An unknown error occurred, sorry!{placeholder}",
            };
            return outputMessage;
        }
    }
    #endregion

    #region Notifications class
    /// <summary>
    /// Notifications class to easilly show notifications using custom made templates,
    /// or completely custom style if none of the templates are fitting for the current task.
    /// </summary>
    public class Notify : BaseScript
    {
        /// <summary>
        /// Show a custom notification above the minimap.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="blink">Should the notification blink 3 times?</param>
        /// <param name="saveToBrief">Should the notification be logged to the brief (PAUSE menu > INFO > Notifications)?</param>
        public static void Custom(string message, bool blink = true, bool saveToBrief = true, string type = "Custom")
        {
            string notiftype = vMenuShared.ConfigManager.GetSettingsString(vMenuShared.ConfigManager.Setting.vmenu_notification_type);
            if (type == "death")
            {
                    SetNotificationTextEntry("CELL_EMAIL_BCON"); // 10x ~a~
                    foreach (var s in CitizenFX.Core.UI.Screen.StringToArray(message))
                    {
                        AddTextComponentSubstringPlayerName(s);
                    }
                    DrawNotification(blink, saveToBrief);
            }
            else
            {
                if (notiftype.ToLower() == "native")
                {
                    SetNotificationTextEntry("CELL_EMAIL_BCON"); // 10x ~a~
                    foreach (var s in CitizenFX.Core.UI.Screen.StringToArray(message))
                    {
                        AddTextComponentSubstringPlayerName(s);
                    }
                    DrawNotification(blink, saveToBrief);
                }
                else if (notiftype.ToLower() == "mosh")
                {
                    if (!((type.ToLower()=="alert")||(type.ToLower()=="error")||(type.ToLower()=="info")||(type.ToLower()=="success")))
                    TriggerEvent("mosh_notify:notify", type, $"<span class=\"text-black\">{message}</span>", "info", "info", 5000);
                }
                else if (notiftype.ToLower() == "none")
                {
    
                }
                else
                {
                    SetNotificationTextEntry("CELL_EMAIL_BCON"); // 10x ~a~
                    foreach (var s in CitizenFX.Core.UI.Screen.StringToArray(message))
                    {
                        AddTextComponentSubstringPlayerName(s);
                    }
                    DrawNotification(blink, saveToBrief);
                }
            }
        }

        /// <summary>
        /// Show a notification with "Alert: " prefixed to the message.
        /// </summary>
        /// <param name="message">The message to be displayed on the notification.</param>
        /// <param name="blink">Should the notification blink 3 times?</param>
        /// <param name="saveToBrief">Should the notification be logged to the brief (PAUSE menu > INFO > Notifications)?</param>
        public static void Alert(string message, bool blink = true, bool saveToBrief = true)
        {
            string notiftype = vMenuShared.ConfigManager.GetSettingsString(vMenuShared.ConfigManager.Setting.vmenu_notification_type);

            if (notiftype.ToLower() == "native")
            {
                Custom("~y~~h~Alert~h~~s~: " + message, blink, saveToBrief, "alert");
            }
            else if (notiftype.ToLower() == "mosh")
            {
                TriggerEvent("mosh_notify:notify", "ALERT", $"<span class=\"text-black\">{message}</span>", "warning", "warning", 5000);

            }
            else if (notiftype.ToLower() == "none")
            {

            }
            else
            {
                Custom("~y~~h~Alert~h~~s~: " + message, blink, saveToBrief, "alert");
            }
        }

        /// <summary>
        /// Show a notification with "Alert: " prefixed to the message.
        /// </summary>
        /// <param name="errorMessage">The error message template.</param>
        /// <param name="blink">Should the notification blink 3 times?</param>
        /// <param name="saveToBrief">Should the notification be logged to the brief (PAUSE menu > INFO > Notifications)?</param>
        /// <param name="placeholderValue">An optional string that will be replaced inside the error message template.</param>
        public static void Alert(CommonErrors errorMessage, bool blink = true, bool saveToBrief = true, string placeholderValue = null)
        {
            var message = ErrorMessage.Get(errorMessage, placeholderValue);
            Alert(message, blink, saveToBrief);
        }

        /// <summary>
        /// Show a notification with "Error: " prefixed to the message.
        /// </summary>
        /// <param name="message">The message to be displayed on the notification.</param>
        /// <param name="blink">Should the notification blink 3 times?</param>
        /// <param name="saveToBrief">Should the notification be logged to the brief (PAUSE menu > INFO > Notifications)?</param>
        public static void Error(string message, bool blink = true, bool saveToBrief = true)
        {
            string notiftype = vMenuShared.ConfigManager.GetSettingsString(vMenuShared.ConfigManager.Setting.vmenu_notification_type);

            if (notiftype.ToLower() == "native")
            {
            Custom("~r~~h~Error~h~~s~: " + message, blink, saveToBrief, "error");
            }
            else if (notiftype.ToLower() == "mosh")
            {
                TriggerEvent("mosh_notify:notify", "ERROR", $"<span class=\"text-black\">{message}</span>", "error", "error", 5000);

            }
            else if (notiftype.ToLower() == "none")
            {

            }
            else
            {
            Custom("~r~~h~Error~h~~s~: " + message, blink, saveToBrief, "error");
            }
            Debug.Write("[vMenu] [ERROR] " + message + "\n");
        }

        /// <summary>
        /// Show a notification with "Error: " prefixed to the message.
        /// </summary>
        /// <param name="errorMessage">The error message template.</param>
        /// <param name="blink">Should the notification blink 3 times?</param>
        /// <param name="saveToBrief">Should the notification be logged to the brief (PAUSE menu > INFO > Notifications)?</param>
        /// <param name="placeholderValue">An optional string that will be replaced inside the error message template.</param>
        public static void Error(CommonErrors errorMessage, bool blink = true, bool saveToBrief = true, string placeholderValue = null)
        {
            var message = ErrorMessage.Get(errorMessage, placeholderValue);
            Error(message, blink, saveToBrief);
        }

        /// <summary>
        /// Show a notification with "Info: " prefixed to the message.
        /// </summary>
        /// <param name="message">The message to be displayed on the notification.</param>
        /// <param name="blink">Should the notification blink 3 times?</param>
        /// <param name="saveToBrief">Should the notification be logged to the brief (PAUSE menu > INFO > Notifications)?</param>
        public static void Info(string message, bool blink = true, bool saveToBrief = true)
        {
            string notiftype = vMenuShared.ConfigManager.GetSettingsString(vMenuShared.ConfigManager.Setting.vmenu_notification_type);

            if (notiftype.ToLower() == "native")
            {
            Custom("~b~~h~Info~h~~s~: " + message, blink, saveToBrief, "info");       
            }
            else if (notiftype.ToLower() == "mosh")
            {
                TriggerEvent("mosh_notify:notify", "INFO", $"<span class=\"text-black\">{message}</span>", "info", "info", 5000);

            }
            else if (notiftype.ToLower() == "none")
            {

            }
            else
            {
            Custom("~b~~h~Info~h~~s~: " + message, blink, saveToBrief, "info");
            }
        }

        /// <summary>
        /// Show a notification with "Success: " prefixed to the message.
        /// </summary>
        /// <param name="message">The message to be displayed on the notification.</param>
        /// <param name="blink">Should the notification blink 3 times?</param>
        /// <param name="saveToBrief">Should the notification be logged to the brief (PAUSE menu > INFO > Notifications)?</param>
        public static void Success(string message, bool blink = true, bool saveToBrief = true)
        {
            string notiftype = vMenuShared.ConfigManager.GetSettingsString(vMenuShared.ConfigManager.Setting.vmenu_notification_type);

            if (notiftype.ToLower() == "native")
            {
            Custom("~g~~h~Success~h~~s~: " + message, blink, saveToBrief, "success");
            }
            else if (notiftype.ToLower() == "mosh")
            {
                TriggerEvent("mosh_notify:notify", "SUCCESS", $"<span class=\"text-black\">{message}</span>", "success", "success", 5000);

            }
            else if (notiftype.ToLower() == "none")
            {

            }
            else
            {
            Custom("~g~~h~Success~h~~s~: " + message, blink, saveToBrief, "success");
            }
        }

        /// <summary>
        /// Shows a custom notification with an image attached.
        /// </summary>
        /// <param name="textureDict"></param>
        /// <param name="textureName"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="safeToBrief"></param>
        public static void CustomImage(string textureDict, string textureName, string message, string title, string subtitle, bool saveToBrief, int iconType = 0)
        {
            SetNotificationTextEntry("CELL_EMAIL_BCON"); // 10x ~a~
            foreach (var s in CitizenFX.Core.UI.Screen.StringToArray(message))
            {
                AddTextComponentSubstringPlayerName(s);
            }
            SetNotificationMessage(textureName, textureDict, false, iconType, title, subtitle);
            DrawNotification(false, saveToBrief);
        }
    }
    #endregion

    #region Custom Subtitle class
    /// <summary>
    /// Custom Subtitle class used to display subtitles using preformatted templates.
    /// Optionally you can also use a blank/custom style if you don't want to use an existing template.
    /// </summary>
    public static class Subtitle
    {
        /// <summary>
        /// Custom (white/custom text style subtitle)
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="duration">(Optional) duration in ms.</param>
        /// <param name="drawImmediately">(Optional) draw the notification immediately or wait for the previous subtitle text to disappear.</param>
        public static void Custom(string message, int duration = 2500, bool drawImmediately = true)
        {
            BeginTextCommandPrint("CELL_EMAIL_BCON"); // 10x ~a~
            foreach (var s in CitizenFX.Core.UI.Screen.StringToArray(message))
            {
                AddTextComponentSubstringPlayerName(s);
            }
            EndTextCommandPrint(duration, drawImmediately);
        }

        /// <summary>
        /// Alert (yellow text subtitle).
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="duration">(Optional) duration in ms.</param>
        /// <param name="drawImmediately">(Optional) draw the notification immediately or wait for the previous subtitle text to disappear.</param>
        /// <param name="prefix">(Optional) add a prefix to your message, if you use this, only the prefix will be colored. The rest of the message will be left white.</param>
        public static void Alert(string message, int duration = 2500, bool drawImmediately = true, string prefix = null)
        {
            Custom((prefix != null ? "~y~" + prefix + " ~s~" : "~y~") + message, duration, drawImmediately);
        }

        /// <summary>
        /// Error (red text subtitle).
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="duration">(Optional) duration in ms.</param>
        /// <param name="drawImmediately">(Optional) draw the notification immediately or wait for the previous subtitle text to disappear.</param>
        /// <param name="prefix">(Optional) add a prefix to your message, if you use this, only the prefix will be colored. The rest of the message will be left white.</param>
        public static void Error(string message, int duration = 2500, bool drawImmediately = true, string prefix = null)
        {
            Custom((prefix != null ? "~r~" + prefix + " ~s~" : "~r~") + message, duration, drawImmediately);
        }

        /// <summary>
        /// Info (blue text subtitle).
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="duration">(Optional) duration in ms.</param>
        /// <param name="drawImmediately">(Optional) draw the notification immediately or wait for the previous subtitle text to disappear.</param>
        /// <param name="prefix">(Optional) add a prefix to your message, if you use this, only the prefix will be colored. The rest of the message will be left white.</param>
        public static void Info(string message, int duration = 2500, bool drawImmediately = true, string prefix = null)
        {
            Custom((prefix != null ? "~b~" + prefix + " ~s~" : "~b~") + message, duration, drawImmediately);
        }

        /// <summary>
        /// Success (green text subtitle).
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="duration">(Optional) duration in ms.</param>
        /// <param name="drawImmediately">(Optional) draw the notification immediately or wait for the previous subtitle text to disappear.</param>
        /// <param name="prefix">(Optional) add a prefix to your message, if you use this, only the prefix will be colored. The rest of the message will be left white.</param>
        public static void Success(string message, int duration = 2500, bool drawImmediately = true, string prefix = null)
        {
            Custom((prefix != null ? "~g~" + prefix + " ~s~" : "~g~") + message, duration, drawImmediately);
        }
    }
    #endregion

    public static class HelpMessage
    {


        public enum Label
        {
            EXIT_INTERIOR_HELP_MESSAGE
        }

        private static readonly Dictionary<Label, KeyValuePair<string, string>> labels = new()
        {
            [Label.EXIT_INTERIOR_HELP_MESSAGE] = new KeyValuePair<string, string>("EXIT_INTERIOR_HELP_MESSAGE", "Press ~INPUT_CONTEXT~ to exit the building.")
        };



        public static void Custom(string message) => Custom(message, 6000, true);
        public static void Custom(string message, int duration) => Custom(message, duration, true);
        public static void Custom(string message, int duration, bool sound)
        {
            var array = CommonFunctions.StringToArray(message);
            if (IsHelpMessageBeingDisplayed())
            {
                ClearAllHelpMessages();
            }
            BeginTextCommandDisplayHelp("CELL_EMAIL_BCON");
            foreach (var s in array)
            {
                AddTextComponentSubstringPlayerName(s);
            }
            EndTextCommandDisplayHelp(0, false, sound, duration);
        }

        public static void CustomLooped(Label label)
        {
            if (GetLabelText(labels[label].Key) == "NULL")
            {
                AddTextEntry(labels[label].Key, labels[label].Value);
            }
            //string[] array = CommonFunctions.StringToArray(message);
            //if (IsHelpMessageBeingDisplayed())
            //{
            //    ClearAllHelpMessages();
            //}
            //BeginTextCommandDisplayHelp("CELL_EMAIL_BCON");
            //foreach (string s in array)
            //{
            //    AddTextComponentSubstringPlayerName(s);
            //}
            DisplayHelpTextThisFrame(labels[label].Key, true);
            //EndTextCommandDisplayHelp(0, true, false, -1);
        }
    }
}
