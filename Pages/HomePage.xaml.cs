using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace SettingsClone.Pages
{
    public sealed partial class HomePage : Page
    {
        private bool isDialogOpen;

        public HomePage()
        {
            InitializeComponent();
        }

        private void BluetoothToggle_Toggled(object sender, RoutedEventArgs e)
        {
            // Toggled can fire while XAML is initializing; named controls may not exist yet.
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
                await ShowMockDialogAsync(button.Tag?.ToString() ?? "Settings");
            }
        }

        private async void MockMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem item)
            {
                await ShowMockDialogAsync(item.Tag?.ToString() ?? "Device options");
            }
        }

        private async Task ShowMockDialogAsync(string action)
        {
            if (isDialogOpen)
            {
                return;
            }
            isDialogOpen = true;
            try
            {
                ContentDialog dialog = new ContentDialog
                {
                    XamlRoot = XamlRoot,
                    Title = $"{action} (demo)",
                    Content = "This action is a mock for HW1012. No Windows settings, devices, subscriptions, or cloud files are changed.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close
                };
                await dialog.ShowAsync();
            }
            finally
            {
                isDialogOpen = false;
            }
        }

        private async void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            if (isDialogOpen)
            {
                return;
            }
            isDialogOpen = true;
            try
            {
                TextBox nameBox = new TextBox
                {
                    Header = "Device name (demo only)",
                    Text = DeviceNameText.Text,
                    MaxLength = 32
                };
                Microsoft.UI.Xaml.Automation.AutomationProperties.SetAutomationId(nameBox, "RenameNameInput");
                ContentDialog dialog = new ContentDialog
                {
                    XamlRoot = XamlRoot,
                    Title = "Rename this demo device",
                    Content = nameBox,
                    PrimaryButtonText = "Save",
                    CloseButtonText = "Cancel",
                    DefaultButton = ContentDialogButton.Primary
                };
                nameBox.TextChanged += (_, _) => dialog.IsPrimaryButtonEnabled = !string.IsNullOrWhiteSpace(nameBox.Text);
                if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                {
                    DeviceNameText.Text = nameBox.Text.Trim();
                }
            }
            finally
            {
                isDialogOpen = false;
            }
        }

        private void HomeContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Use the content width, not the window width: the sidebar also occupies space.
            bool useSingleColumn = e.NewSize.Width < 760;
            RightCardColumn.Width = new GridLength(useSingleColumn ? 0 : 1, useSingleColumn ? GridUnitType.Pixel : GridUnitType.Star);
            CardsGrid.ColumnSpacing = useSingleColumn ? 0 : 8;
            Grid.SetColumn(RightCards, useSingleColumn ? 0 : 1);
            Grid.SetRow(RightCards, useSingleColumn ? 1 : 0);
            RightCards.Margin = useSingleColumn ? new Thickness(0, 8, 0, 0) : new Thickness(0);

            Grid.SetRow(DeviceStatuses, useSingleColumn ? 1 : 0);
            Grid.SetColumn(DeviceStatuses, useSingleColumn ? 0 : 1);
            Grid.SetColumnSpan(DeviceSummary, useSingleColumn ? 2 : 1);
            DeviceStatuses.Margin = useSingleColumn ? new Thickness(0, 20, 0, 0) : new Thickness(0);
            DeviceStatuses.HorizontalAlignment = useSingleColumn ? HorizontalAlignment.Left : HorizontalAlignment.Right;
            DeviceStatuses.Orientation = e.NewSize.Width < 380 ? Orientation.Vertical : Orientation.Horizontal;
            DeviceImageBorder.Width = e.NewSize.Width < 380 ? 88 : 120;

            double cardWidth = useSingleColumn ? e.NewSize.Width : (e.NewSize.Width - 8) / 2;
            bool useCompactRows = cardWidth < 420;
            MicrosoftAppsImage.Width = System.Math.Max(80, System.Math.Min(312, cardWidth - 48));
            SetDeviceRowLayout(FirstDeviceActions, useCompactRows);
            SetDeviceRowLayout(SecondDeviceActions, useCompactRows);
            Grid.SetRow(AddDeviceActions, useCompactRows ? 1 : 0);
            Grid.SetColumn(AddDeviceActions, useCompactRows ? 0 : 1);
            AddDeviceActions.Margin = useCompactRows ? new Thickness(0, 12, 0, 0) : new Thickness(0);
            bool useCompactBackup = cardWidth < 440;
            Grid.SetRow(BackupStatusText, useCompactBackup ? 1 : 0);
            Grid.SetColumn(BackupStatusText, useCompactBackup ? 1 : 2);
            BackupStatusText.Margin = useCompactBackup ? new Thickness(0, 4, 0, 0) : new Thickness(0);
        }

        private static void SetDeviceRowLayout(StackPanel actions, bool useCompactRows)
        {
            Grid.SetRow(actions, useCompactRows ? 1 : 0);
            Grid.SetColumn(actions, useCompactRows ? 1 : 2);
            Grid.SetColumnSpan(actions, useCompactRows ? 2 : 1);
            actions.Margin = useCompactRows ? new Thickness(0, 12, 0, 0) : new Thickness(0);
        }
    }
}
