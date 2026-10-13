using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SettingsClone.Helpers;
using SettingsClone.Models;

namespace SettingsClone.Pages
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public DeviceInfo GetDeviceInfo()
        {
            return new DeviceInfo(DeviceNameText.Text, DeviceModelText.Text, BluetoothToggle.IsOn);
        }

        private void BluetoothToggle_Toggled(object sender, RoutedEventArgs e)
        {
            // Toggled can fire before the named controls are initialized.
            if (FirstConnectButton == null || SecondConnectButton == null)
            {
                return;
            }
            bool isBluetoothOn = BluetoothToggle.IsOn;
            BluetoothPowerText.Text = isBluetoothOn ? "On" : "Off";
            FirstConnectButton.IsEnabled = isBluetoothOn;
            SecondConnectButton.IsEnabled = isBluetoothOn;
            BluetoothStatusText.Text = isBluetoothOn ? "Bluetooth is turned on (demo)" : "Bluetooth is turned off";
            string deviceStatus = isBluetoothOn ? "Paired (demo)" : "Bluetooth is turned off";
            FirstDeviceStatusText.Text = deviceStatus;
            SecondDeviceStatusText.Text = deviceStatus;
        }

        private async void MockAction_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                await DialogHelper.ShowMockAsync(XamlRoot, button.Tag?.ToString() ?? "Settings");
            }
        }

        private async void MockMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem item)
            {
                await DialogHelper.ShowMockAsync(XamlRoot, item.Tag?.ToString() ?? "Device options");
            }
        }

        private async void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            string? name = await DialogHelper.RenameDeviceAsync(XamlRoot, DeviceNameText.Text);
            if (name != null)
            {
                DeviceNameText.Text = name;
            }
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAdaptiveThresholds();
        }

        private void HomePage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateAdaptiveThresholds();
        }

        private void HomeContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateAdaptiveThresholds();
        }

        private void UpdateAdaptiveThresholds()
        {
            if (XamlRoot == null || HomeContent == null || HomeContent.ActualWidth <= 0)
            {
                return;
            }
            // AdaptiveTrigger measures the window. Convert content/card breakpoints
            // into window breakpoints so the sidebar and page margins are accounted for.
            double windowWidth = XamlRoot.Size.Width;
            double contentWidth = HomeContent.ActualWidth;
            double cardWidth = contentWidth < 760 ? contentWidth : (contentWidth - 8) / 2;
            WideHomeTrigger.MinWindowWidth = windowWidth - contentWidth + 760;
            WideHeaderTrigger.MinWindowWidth = windowWidth - contentWidth + 380;
            WideDeviceRowsTrigger.MinWindowWidth = windowWidth - cardWidth + 420;
            WideBackupTrigger.MinWindowWidth = windowWidth - cardWidth + 440;
        }
    }
}
