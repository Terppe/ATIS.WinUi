using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using ATIS.WinUi.Pages;
using ATIS.WinUi.UserControls;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi
{

    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        //  private List<string> _reminderStrList;

        public MainWindow()
        {
            InitializeComponent();

            ContentFrame.Navigate(typeof(HomePage));
        }


        private void NaviView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer.IsSelected)
            {
                // Clicked on an item that is already selected,
                // Avoid navigating to the same page again causing movement.
                return;
            }

            // First determine if the setting is selected
            if (args.IsSettingsInvoked)
            {
                if (ContentFrame.CurrentSourcePageType != typeof(SettingsPage))
                {
                    ContentFrame.Navigate(typeof(SettingsPage));
                }
            }
            else
            {
                // Selected content
                switch (args.InvokedItem)
                {
                    case "Home":
                        ContentFrame.Navigate(typeof(HomePage));
                        break;
                    case "Fishes":
                        ContentFrame.Navigate(typeof(FishesPage));
                        break;
                    case "Plants":
                        ContentFrame.Navigate(typeof(PlantsPage));
                        break;
                    case "Diseases":
                        ContentFrame.Navigate(typeof(DiseasesPage));
                        break;
                    case "Foods":
                        ContentFrame.Navigate(typeof(FoodsPage));
                        break;
                    case "Divs":
                        ContentFrame.Navigate(typeof(DivsPage));
                        break;
                    case "Search":
                        ContentFrame.Navigate(typeof(SearchPage));
                        break;
                    case "About":
                        ContentFrame.Navigate(typeof(AboutPage));
                        break;
                    case "User":
                        ContentFrame.Navigate(typeof(UserPage));
                        break;
                    case "Admin":
                        ContentFrame.Navigate(typeof(AdminPage));
                        break;
                    case "Regnum":
                        ContentFrame.Navigate(typeof(D03RegnumsPage));
                        break;
                    case "Phylum":
                        ContentFrame.Navigate(typeof(D06PhylumsPage));
                        break;
                    case "Subphylum":
                        ContentFrame.Navigate(typeof(D12SubphylumsPage));
                        break;
                    case "Superclass":
                        ContentFrame.Navigate(typeof(D18SuperclassesPage));
                        break;
                    case "Login":
                        ContentFrame.Navigate(typeof(LoginPage));
                        break;
                }
            }


        }

        private void ContentFrame_OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new NotImplementedException("Fehler im Frame");
        }


        private static IconElement GetIcon(string imagePath)
        {
            return imagePath.ToLowerInvariant().EndsWith(".png")
                ? (IconElement)new BitmapIcon()
                { UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute), ShowAsMonochrome = false }
                : (IconElement)new FontIcon()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Glyph = imagePath
                };
        }


        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            //{
            //    sender.ItemsSource = _reminderStrList.Where(i => i.Contains(sender.Text));
            //}
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            sender.Text = args.SelectedItem.ToString();
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string txt = args.QueryText; //Entered text
            if (args.ChosenSuggestion != null)
            {
                // triggered when selecting an item from the prompt box
            }
            else
            {
                // User presses Enter when typing or triggers input when clicking the right button
            }
        }

        public double NaviViewCompactModeThresholdWidth => NaviView.CompactModeThresholdWidth;

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            LogInView log = new LogInView();

            log.InitializeComponent();
            await ContentDialog.ShowAsync();
        }






        private void More_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException("More ...");
        }

    }

}
