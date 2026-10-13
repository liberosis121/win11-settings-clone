using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SettingsClone.Helpers;
using SettingsClone.Models;

namespace SettingsClone.Pages
{
    public sealed partial class SystemPage : Page
    {
        public SystemPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DeviceInfo device = e.Parameter as DeviceInfo
                ?? new DeviceInfo("LAPTOP-IUCD4DSS", "IdeaPad Slim 3 15ARP10", false);
            SystemDeviceText.Text = $"{device.Name} · {device.Model} (mock data)";
            SystemBluetoothText.Text = $"Bluetooth: {(device.BluetoothOn ? "On" : "Off")} (demo)";
            SettingsRows.ItemsSource = new[]
            {
                new SettingItem("Display", "Monitors, brightness, and scaling", "\uE7F4"),
                new SettingItem("Sound", "Volume, output, and input devices", "\uE767"),
                new SettingItem("Notifications", "Alerts from apps and the system", "\uEA8F"),
                new SettingItem("Power & battery", "Sleep, power mode, and battery usage", "\uE83E"),
                new SettingItem("Storage", "Storage space and drive information", "\uEDA2"),
                new SettingItem("About", SystemDeviceText.Text, "\uE946")
            };
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            if (DialogHelper.IsOpen)
            {
                return;
            }
            if (Frame.CanGoBack && Frame.BackStack[Frame.BackStack.Count - 1].SourcePageType == typeof(HomePage))
            {
                Frame.GoBack();
            }
            else
            {
                Frame.Navigate(typeof(HomePage));
            }
        }

        private async void SystemSetting_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is SettingItem setting)
            {
                await DialogHelper.ShowMockAsync(XamlRoot, setting.Title,
                    "This System page demonstrates Frame/Page navigation. No real Windows setting is changed.");
            }
        }
    }
}
