using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using SettingsClone.Helpers;
using SettingsClone.Models;
using SettingsClone.Pages;
using System;
using System.Collections.Generic;
using Windows.System;

namespace SettingsClone
{
    public sealed partial class MainWindow : Window
    {
        private readonly List<ToggleButton> navigationButtons = new();

        public MainWindow()
        {
            InitializeComponent();
            NavigationItems.ItemsSource = new[]
            {
                new NavigationItem("Home", "Home", "NavHome", typeof(HomePage)),
                new NavigationItem("System", "System", "NavSystem", typeof(SystemPage)),
                new NavigationItem("Bluetooth & devices", "Bluetooth", "NavBluetooth"),
                new NavigationItem("Network & internet", "Network", "NavNetwork"),
                new NavigationItem("Personalization", "Personalization", "NavPersonalization"),
                new NavigationItem("Apps", "Apps", "NavApps"),
                new NavigationItem("Accounts", "Accounts", "NavAccounts"),
                new NavigationItem("Time & language", "Time", "NavTime"),
                new NavigationItem("Gaming", "Gaming", "NavGaming"),
                new NavigationItem("Accessibility", "Accessibility", "NavAccessibility"),
                new NavigationItem("Privacy & security", "Privacy", "NavPrivacy"),
                new NavigationItem("Windows Update", "Update", "NavUpdate")
            };
            // The TitleBar manages drag regions; caption buttons remain native.
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(SettingsTitleBar);
            AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonForegroundColor = Colors.Black;
            AppWindow.TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
            ContentFrame.Navigate(typeof(HomePage));
        }

        private void NavigationButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button && !navigationButtons.Contains(button))
            {
                navigationButtons.Add(button);
                UpdateNavigationState();
            }
        }

        private void NavigationButton_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button)
            {
                navigationButtons.Remove(button);
            }
        }

        private async void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton button || button.Tag is not NavigationItem item)
            {
                return;
            }
            if (item.PageType != null)
            {
                NavigateTo(item.PageType);
            }
            else
            {
                button.IsChecked = false;
                await DialogHelper.ShowMockAsync(ShellGrid.XamlRoot, item.Label);
            }
        }

        private void NavigateTo(Type pageType)
        {
            if (DialogHelper.IsOpen)
            {
                UpdateNavigationState();
                return;
            }
            if (ContentFrame.CurrentSourcePageType != pageType)
            {
                // System receives the current mock device, including a renamed label.
                object? parameter = pageType == typeof(SystemPage) && ContentFrame.Content is HomePage home
                    ? home.GetDeviceInfo()
                    : null;
                ContentFrame.Navigate(pageType, parameter);
            }
            UpdateNavigationState();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateNavigationState();
        }

        private void UpdateNavigationState()
        {
            foreach (ToggleButton button in navigationButtons)
            {
                button.IsChecked = button.Tag is NavigationItem item
                    && item.PageType != null && item.PageType == ContentFrame.CurrentSourcePageType;
            }
            SettingsTitleBar.IsBackButtonEnabled = ContentFrame.CanGoBack;
        }

        private void SettingsTitleBar_BackRequested(TitleBar sender, object args)
        {
            if (!DialogHelper.IsOpen && ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            // Defer the dialog until Frame has finished handling the failed navigation.
            DispatcherQueue.TryEnqueue(async () =>
            {
                UpdateNavigationState();
                await DialogHelper.ShowMessageAsync(ShellGrid.XamlRoot, "Page unavailable",
                    "The selected page could not be opened. Please choose Home or System and try again.");
            });
        }

        private async void SearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter)
            {
                return;
            }
            e.Handled = true;
            string query = SearchBox.Text.Trim();
            if (query.Equals("Home", StringComparison.OrdinalIgnoreCase))
            {
                NavigateTo(typeof(HomePage));
            }
            else if (query.Equals("System", StringComparison.OrdinalIgnoreCase))
            {
                NavigateTo(typeof(SystemPage));
            }
            else
            {
                await DialogHelper.ShowMockAsync(ShellGrid.XamlRoot, "Search",
                    "This demo can open Home or System. Other settings are shown for the screen-cloning exercise only.");
            }
        }

        private void ShellGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Visual states handle the shell layout; only the search width varies continuously.
            SearchContainer.Width = Math.Clamp(e.NewSize.Width - 440, 120, 514);
        }
    }
}
