using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using SettingsClone.Pages;
using System;
using Windows.System;

namespace SettingsClone
{
    public sealed partial class MainWindow : Window
    {
        private bool isDialogOpen;

        public MainWindow()
        {
            InitializeComponent();
            // The TitleBar manages drag regions; caption buttons remain native.
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(SettingsTitleBar);
            AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonForegroundColor = Colors.Black;
            AppWindow.TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
            ContentFrame.Navigate(typeof(HomePage));
        }

        private async void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton button)
            {
                return;
            }
            string destination = button.Tag?.ToString() ?? string.Empty;
            if (destination == "Home" || destination == "System")
            {
                NavigateTo(destination == "Home" ? typeof(HomePage) : typeof(SystemPage));
            }
            else
            {
                button.IsChecked = false;
                await ShowMockDialogAsync(destination);
            }
        }

        private void NavigateTo(Type pageType)
        {
            if (ContentFrame.CurrentSourcePageType != pageType)
            {
                ContentFrame.Navigate(pageType);
            }
            UpdateNavigationState();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateNavigationState();
        }

        private void UpdateNavigationState()
        {
            HomeButton.IsChecked = ContentFrame.CurrentSourcePageType == typeof(HomePage);
            SystemButton.IsChecked = ContentFrame.CurrentSourcePageType == typeof(SystemPage);
            SettingsTitleBar.IsBackButtonEnabled = ContentFrame.CanGoBack;
        }

        private void SettingsTitleBar_BackRequested(TitleBar sender, object args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new InvalidOperationException($"Cannot open {e.SourcePageType.FullName}.", e.Exception);
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
                await ShowMockDialogAsync("Search", "This demo can open Home or System. Other settings are shown for the screen-cloning exercise only.");
            }
        }

        private async System.Threading.Tasks.Task ShowMockDialogAsync(string title, string? message = null)
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
                    XamlRoot = ShellGrid.XamlRoot,
                    Title = $"{title} (demo)",
                    Content = message ?? "This is a mock action for HW1012. No Windows settings are changed and no online service is opened.",
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

        private void ShellGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            bool isCompact = e.NewSize.Width < 760;
            SidebarColumn.Width = new GridLength(isCompact ? 72 : e.NewSize.Width < 1000 ? 228 : 304);
            ProfileDetails.Visibility = isCompact ? Visibility.Collapsed : Visibility.Visible;
            AvatarBorder.Width = isCompact ? 40 : 60;
            AvatarBorder.Height = isCompact ? 40 : 60;
            foreach (UIElement item in NavigationItems.Children)
            {
                if (item is ToggleButton button && button.Content is Grid itemGrid)
                {
                    itemGrid.ColumnSpacing = isCompact ? 0 : 14;
                    foreach (UIElement child in itemGrid.Children)
                    {
                        if (child is TextBlock label)
                        {
                            label.Visibility = isCompact ? Visibility.Collapsed : Visibility.Visible;
                        }
                    }
                    ToolTipService.SetToolTip(button, button.Tag?.ToString());
                }
            }
            SearchContainer.Width = Math.Clamp(e.NewSize.Width - 440, 120, 520);
            SearchContainer.Margin = e.NewSize.Width >= 1200 ? new Thickness(120, 0, 0, 0) : new Thickness(0);
        }
    }
}
