using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace SettingsClone.Pages
{
    public sealed partial class SystemPage : Page
    {
        private bool isDialogOpen;

        public SystemPage()
        {
            InitializeComponent();
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
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
            if (isDialogOpen || sender is not Button button)
            {
                return;
            }
            isDialogOpen = true;
            try
            {
                ContentDialog dialog = new ContentDialog
                {
                    XamlRoot = XamlRoot,
                    Title = $"{button.Tag} (demo)",
                    Content = "This System page demonstrates Frame/Page navigation. No real Windows setting is changed.",
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
    }
}
