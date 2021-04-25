using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.ViewManagement;
using ATIS.WinUi.Helper;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public string Version
        {
            get
            {
                var version = Windows.ApplicationModel.Package.Current.Id.Version;
                return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }

        public SettingsPage()
        {
            this.InitializeComponent();
            Loaded += OnSettingsPageLoaded;

            if (ElementSoundPlayer.State == ElementSoundPlayerState.On)
                SoundToggle.IsOn = true;
            if (ElementSoundPlayer.SpatialAudioMode == ElementSpatialAudioMode.On)
                SpatialSoundBox.IsChecked = true;
            //if (NavigationRootPage.Current.NavigationView.PaneDisplayMode == Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Auto)
            //{
            //    navigationLocation.SelectedIndex = 0;
            //}
            //else
            //{
            //    navigationLocation.SelectedIndex = 1;
            //}

            ScreenshotModeToggle.IsOn = UIHelper.IsScreenshotMode;
            ScreenshotFolderLink.Content = UIHelper.ScreenshotStorageFolder.Path;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //     NavigationRootPage.Current.NavigationView.Header = "Settings";
        }

        private void OnSettingsPageLoaded(object sender, RoutedEventArgs e)
        {
            //var currentTheme = ThemeHelper.RootTheme.ToString();
            //(ThemePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == currentTheme)).IsChecked = true;
        }

        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var selectedTheme = ((RadioButton)sender)?.Tag?.ToString();
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            if (selectedTheme != null)
            {
                ThemeHelper.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
            }
        }

        private void OnThemeRadioButtonKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Up)
            {
                //          NavigationRootPage.Current.PageHeader.Focus(FocusState.Programmatic);
            }
        }
        private void spatialSoundBox_Checked(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.IsOn == true)
            {
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.On;
            }
        }

        private void soundToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.IsOn == true)
            {
                SpatialSoundBox.IsEnabled = true;
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
            }
            else
            {
                SpatialSoundBox.IsEnabled = false;
                SpatialSoundBox.IsChecked = false;

                ElementSoundPlayer.State = ElementSoundPlayerState.Off;
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
            }
        }

        private void navigationToggle_Toggled(object sender, RoutedEventArgs e)
        {
            //        NavigationOrientationHelper.IsLeftMode = navigationLocation.SelectedIndex == 0;
        }

        private void screenshotModeToggle_Toggled(object sender, RoutedEventArgs e)
        {
            UIHelper.IsScreenshotMode = ScreenshotModeToggle.IsOn;
        }

        private void spatialSoundBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.IsOn == true)
            {
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
            }
        }

        private void navigationLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //        NavigationOrientationHelper.IsLeftMode = navigationLocation.SelectedIndex == 0;
        }

        private async void FolderButton_Click(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            folderPicker.FileTypeFilter.Add(".png"); // meaningless, but you have to have something
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                UIHelper.ScreenshotStorageFolder = folder;
                ScreenshotFolderLink.Content = UIHelper.ScreenshotStorageFolder.Path;
            }
        }

        private async void screenshotFolderLink_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchFolderAsync(UIHelper.ScreenshotStorageFolder);
        }

        private void OnResetTeachingTipsButtonClick(object sender, RoutedEventArgs e)
        {
            //       ProtocolActivationClipboardHelper.ShowCopyLinkTeachingTip = true;
        }
    }
}
