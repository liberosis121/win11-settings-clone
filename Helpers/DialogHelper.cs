using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace SettingsClone.Helpers
{
    // One guard for the whole window, including Rename and navigation errors.
    public static class DialogHelper
    {
        public static bool IsOpen { get; private set; }

        public static Task ShowMockAsync(XamlRoot root, string action, string? message = null)
        {
            return ShowMessageAsync(root, $"{action} (demo)", message ??
                "This action is a mock for HW1012. No Windows settings, devices, subscriptions, or cloud files are changed.");
        }

        public static async Task ShowMessageAsync(XamlRoot root, string title, string message)
        {
            if (IsOpen)
            {
                return;
            }
            IsOpen = true;
            try
            {
                ContentDialog dialog = new ContentDialog
                {
                    XamlRoot = root,
                    Title = title,
                    Content = message,
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close
                };
                await dialog.ShowAsync();
            }
            finally
            {
                IsOpen = false;
            }
        }

        public static async Task<string?> RenameDeviceAsync(XamlRoot root, string currentName)
        {
            if (IsOpen)
            {
                return null;
            }
            IsOpen = true;
            try
            {
                TextBox nameBox = new TextBox
                {
                    Header = "Device name (demo only)",
                    Text = currentName,
                    MaxLength = 32
                };
                AutomationProperties.SetAutomationId(nameBox, "RenameNameInput");
                ContentDialog dialog = new ContentDialog
                {
                    XamlRoot = root,
                    Title = "Rename this demo device",
                    Content = nameBox,
                    PrimaryButtonText = "Save",
                    CloseButtonText = "Cancel",
                    IsPrimaryButtonEnabled = !string.IsNullOrWhiteSpace(currentName),
                    DefaultButton = ContentDialogButton.Primary
                };
                nameBox.TextChanged += (_, _) => dialog.IsPrimaryButtonEnabled = !string.IsNullOrWhiteSpace(nameBox.Text);
                return await dialog.ShowAsync() == ContentDialogResult.Primary ? nameBox.Text.Trim() : null;
            }
            finally
            {
                IsOpen = false;
            }
        }
    }
}
